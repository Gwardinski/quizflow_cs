using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using QuizFlow.Data;
using QuizFlow.Dto.Round;
using QuizFlow.Models;

namespace QuizFlow.Services.RoundService {
  public class RoundService : IRoundService {

    private readonly IMapper _mapper;
    private readonly DataContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public RoundService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor) {
      _mapper = mapper;
      _dbContext = context;
      _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ServiceResponse<List<RoundDtoGet>>> getAllRounds() {
      ServiceResponse<List<RoundDtoGet>> serviceResponse = new ServiceResponse<List<RoundDtoGet>>();
      List<Round> rounds = await _dbContext.Rounds
        .ToListAsync();
      serviceResponse.data = rounds
        .Select(q => _mapper.Map<RoundDtoGet>(q))
        .ToList();
      return serviceResponse;
    }

    public async Task<ServiceResponse<RoundDtoGet>> getRoundById(int id) {
      ServiceResponse<RoundDtoGet> serviceResponse = new ServiceResponse<RoundDtoGet>();
      Round round = await _dbContext.Rounds
        .Include(q => q.roundQuestions)
        .ThenInclude(rq => rq.question)
        .FirstOrDefaultAsync(q => q.id == id);
      serviceResponse.data = _mapper.Map<RoundDtoGet>(round);
      return serviceResponse;
    }

    public async Task<ServiceResponse<List<RoundDtoGet>>> getUserRounds() {
      ServiceResponse<List<RoundDtoGet>> serviceResponse = new ServiceResponse<List<RoundDtoGet>>();
      List<Round> rounds = await _dbContext.Rounds
        .Where(r => r.user.id == getUserId())
        .ToListAsync();
      serviceResponse.data = rounds
        .Select(r => _mapper.Map<RoundDtoGet>(r))
        .ToList();
      return serviceResponse;
    }

    public async Task<ServiceResponse<RoundDtoGet>> addRound(RoundDtoAdd newRound) {
      ServiceResponse<RoundDtoGet> serviceResponse = new ServiceResponse<RoundDtoGet>();
      Round round = _mapper.Map<Round>(newRound);

      round.user = await _dbContext.Users
        .FirstOrDefaultAsync(u => u.id == getUserId());
      round.createdAt = DateTime.Now;
      round.lastUpdated = DateTime.Now;

      await _dbContext.Rounds.AddAsync(round);
      await _dbContext.SaveChangesAsync();

      // getting it back from db confirms it saved correctly
      Round roundDb = await _dbContext.Rounds
        .Where(r => r.user.id == getUserId())
        .FirstOrDefaultAsync(r => r.id == round.id);
      serviceResponse.data = _mapper.Map<RoundDtoGet>(roundDb);
      return serviceResponse;
    }

    public async Task<ServiceResponse<RoundDtoGet>> editRound(RoundDtoEdit editedRound) {
      ServiceResponse<RoundDtoGet> serviceResponse = new ServiceResponse<RoundDtoGet>();
      try {
        Round round = await _dbContext.Rounds
          .Where(q => q.user.id == getUserId())
          .Include(q => q.roundQuestions)
          .ThenInclude(qr => qr.question)
          .FirstOrDefaultAsync(q => q.id == editedRound.id);
        if (round != null) {
          round.title = editedRound.title;
          round.description = editedRound.description;
          round.lastUpdated = DateTime.Now;
          _dbContext.Rounds.Update(round);
          await _dbContext.SaveChangesAsync();
          serviceResponse.data = _mapper.Map<RoundDtoGet>(round);
        } else {
          serviceResponse.success = false;
          serviceResponse.message = "User round not found";
        }
      } catch (Exception e) {
        serviceResponse.success = false;
        serviceResponse.message = e.Message;
      }
      return serviceResponse;
    }

    public async Task<ServiceResponse<string>> deleteRound(int id) {
      ServiceResponse<string> serviceResponse = new ServiceResponse<string>();
      try {
        Round round = await _dbContext.Rounds
          .Where(r => r.user.id == getUserId())
          .FirstOrDefaultAsync(r => r.id == id);
        if (round != null) {
          _dbContext.Rounds.Remove(round);
          await _dbContext.SaveChangesAsync();
          serviceResponse.data = "success";
        } else {
          serviceResponse.success = false;
          serviceResponse.message = "User round not found";
        }
      } catch (Exception e) {
        serviceResponse.success = false;
        serviceResponse.message = e.Message;
      }
      return serviceResponse;
    }

    private int getUserId() {
      return int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
    }

  }
}
