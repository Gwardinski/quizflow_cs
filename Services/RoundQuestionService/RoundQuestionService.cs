using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using QuizFlow.Data;
using QuizFlow.Dto.Round;
using QuizFlow.Dto.RoundQuestion;
using QuizFlow.Models;

namespace QuizFlow.Services.RoundQuestionService {
  public class RoundQuestionService : IRoundQuestionService {

    private readonly IMapper _mapper;
    private readonly DataContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public RoundQuestionService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor) {
      _mapper = mapper;
      _dbContext = context;
      _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ServiceResponse<RoundDtoGet>> addRoundQuestion(RoundQuestionDtoAdd newRoundQuestion) {
      ServiceResponse<RoundDtoGet> response = new ServiceResponse<RoundDtoGet>();
      try {
        Round round = await _dbContext.Rounds
            .Include(r => r.roundQuestions)
            .ThenInclude(qr => qr.question)
            .FirstOrDefaultAsync(
                r => r.id == newRoundQuestion.roundId &&
                r.user.id == int.Parse(_httpContextAccessor.HttpContext.User
                  .FindFirstValue(ClaimTypes.NameIdentifier)
                )
            );
        if (round == null) {
          response.success = false;
          response.message = "Round not found";
          return response;
        }
        Question question = await _dbContext.Questions
            .FirstOrDefaultAsync(r => r.id == newRoundQuestion.questionId);
        if (question == null) {
          response.success = false;
          response.message = "Question not found";
          return response;
        }
        RoundQuestion roundQuestion = new RoundQuestion {
          round = round,
          question = question,
        };

        await _dbContext.RoundQuestions.AddAsync(roundQuestion);
        await _dbContext.SaveChangesAsync();
        response.data = _mapper.Map<RoundDtoGet>(round);
        return response;
      } catch (Exception e) {
        response.success = false;
        response.message = e.Message;
        return response;
      }

    }

    public Task<ServiceResponse<RoundDtoGet>> deleteRoundQuestion(RoundQuestionDtoAdd deleteRoundQuestion) {
      throw new System.NotImplementedException();
    }
  }
}