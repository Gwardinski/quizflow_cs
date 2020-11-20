using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using QuizFlow.Models;

namespace QuizFlow.Data {
  public class AuthRepository : IAuthRepository {

    public AuthRepository(DataContext context, IConfiguration configuation) {
      _configuation = configuation;
      _context = context;
    }

    private readonly DataContext _context;
    private readonly IConfiguration _configuation;

    public async Task<ServiceResponse<int>> register(string username, string password) {

      ServiceResponse<int> res = new ServiceResponse<int>();
      if (await userExists(username)) {
        res.success = false;
        res.message = "User already exists";
        return res;
      }
      User user = new User(username: username);
      createPasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
      user.passwordHash = passwordHash;
      user.passwordSalt = passwordSalt;

      await _context.Users.AddAsync(user);
      await _context.SaveChangesAsync();
      res.data = user.id;
      return res;
    }

    public async Task<ServiceResponse<string>> login(string username, string password) {
      ServiceResponse<string> res = new ServiceResponse<string>();
      User user = await _context.Users.FirstOrDefaultAsync(x => x.username.ToLower().Equals(username.ToLower()));
      res.success = false;
      res.message = "Incorrect credentials";
      if (user != null) {
        if (verifyPasswordHash(password, user.passwordHash, user.passwordSalt)) {
          res.success = true;
          res.message = "success";
          res.data = createToken(user);
        }
      }
      return res;
    }

    public async Task<bool> userExists(string username) {
      if (await _context.Users.AnyAsync(x => x.username.ToLower() == username.ToLower())) {
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
        new Claim(ClaimTypes.Name, user.username)
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