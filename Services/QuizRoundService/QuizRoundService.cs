using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using QuizFlow.Data;
using QuizFlow.Dto.Quiz;
using QuizFlow.Dto.QuizRound;
using QuizFlow.Models;

namespace QuizFlow.Services.QuizRoundService {

  public class QuizRoundService : IQuizRoundService {

    private readonly IMapper _mapper;
    private readonly DataContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public QuizRoundService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor) {
      _mapper = mapper;
      _dbContext = context;
      _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ServiceResponse<QuizDtoGet>> addQuizRound(QuizRoundDtoAdd newQuizRound) {
      ServiceResponse<QuizDtoGet> response = new ServiceResponse<QuizDtoGet>();
      try {
        Quiz quiz = await _dbContext.Quizzes
            .Include(q => q.quizRounds)
            .ThenInclude(qr => qr.round)
            .FirstOrDefaultAsync(
                q => q.id == newQuizRound.quizId &&
                q.user.id == int.Parse(_httpContextAccessor.HttpContext.User
                  .FindFirstValue(ClaimTypes.NameIdentifier)
                )
            );
        if (quiz == null) {
          response.success = false;
          response.message = "Quiz not found";
          return response;
        }
        Round round = await _dbContext.Rounds
            .FirstOrDefaultAsync(r => r.id == newQuizRound.roundId);
        if (round == null) {
          response.success = false;
          response.message = "Round not found";
          return response;
        }
        QuizRound quizRound = new QuizRound {
          quiz = quiz,
          round = round,
        };

        await _dbContext.QuizRounds.AddAsync(quizRound);
        await _dbContext.SaveChangesAsync();
        response.data = _mapper.Map<QuizDtoGet>(quiz);
        return response;
      } catch (Exception e) {
        response.success = false;
        response.message = e.Message;
        return response;
      }
    }

    public Task<ServiceResponse<QuizDtoGet>> deleteQuizRound(QuizRoundDtoAdd deleteQuizRound) {
      throw new NotImplementedException();
    }
  }
}