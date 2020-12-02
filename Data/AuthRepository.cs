using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using QuizFlow.Dto.User;
using QuizFlow.Models;

namespace QuizFlow.Data {
  public class AuthRepository : IAuthRepository {

    public AuthRepository(IMapper mapper, DataContext context, IConfiguration configuation) {
      _configuation = configuation;
      _context = context;
      _mapper = mapper;
    }

    private readonly IMapper _mapper;
    private readonly DataContext _context;
    private readonly IConfiguration _configuation;

    public async Task<ServiceResponse<int>> register(UserDtoRegister userDto) {

      ServiceResponse<int> res = new ServiceResponse<int>();

      string email = userDto.email;
      string password = userDto.password;
      string displayName = userDto.displayName;

      if (email == null || password == null || displayName == null) {
        res.code = 400;
        res.message = "Missing required fields";
        return res;
      }
      if (await userExists(email)) {
        res.code = 409;
        res.message = "User already exists";
        return res;
      }

      User user = new User(email: email, displayName: displayName);
      createPasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
      user.passwordHash = passwordHash;
      user.passwordSalt = passwordSalt;

      await _context.Users.AddAsync(user);
      await _context.SaveChangesAsync();

      res.message = "success";
      return res;
    }

    public async Task<ServiceResponse<UserDtoGet>> login(UserDtoLogin userDto) {

      ServiceResponse<UserDtoGet> res = new ServiceResponse<UserDtoGet>();

      string email = userDto.email;
      string password = userDto.password;

      if (email == null || password == null) {
        res.code = 400;
        res.message = "Missing required fields";
        return res;
      }

      User user = await _context.Users.FirstOrDefaultAsync(x => x.email.ToLower().Equals(email.ToLower()));

      if (user != null && verifyPasswordHash(password, user.passwordHash, user.passwordSalt)) {
        UserDtoGet foundUser = _mapper.Map<UserDtoGet>(user);
        foundUser.authToken = createToken(user);
        res.data = foundUser;
        res.code = 200;
        res.message = "success";
      } else {
        res.code = 404;
        res.message = "User not found";
      }
      return res;
    }

    public async Task<bool> userExists(string email) {
      if (await _context.Users.AnyAsync(x => x.email.ToLower() == email.ToLower())) {
        return true;
      }
      return false;
    }

    private void createPasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt) {
      using (var hmac = new System.Security.Cryptography.HMACSHA512()) {
        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
      }
    }

    private bool verifyPasswordHash(string password, byte[] passwordHash, byte[] passworddSalt) {
      using (var hmac = new System.Security.Cryptography.HMACSHA512(passworddSalt)) {
        var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        for (int i = 0; i < computeHash.Length; i++) {
          if (computeHash[i] != passwordHash[i]) {
            return false;
          }
        }
        return true;
      }
    }

    private string createToken(User user) {
      List<Claim> claims = new List<Claim> {
        new Claim(ClaimTypes.NameIdentifier, user.id.ToString()),
        new Claim(ClaimTypes.Name, user.email)
      };
      SymmetricSecurityKey key = new SymmetricSecurityKey(
        Encoding.UTF8.GetBytes(_configuation.GetSection("AppSettings:Token").Value)
      );
      SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
      SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor {
        Subject = new ClaimsIdentity(claims),
        Expires = DateTime.Now.AddDays(10),
        SigningCredentials = creds
      };
      JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
      SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
      return tokenHandler.WriteToken(token);
    }
  }
}