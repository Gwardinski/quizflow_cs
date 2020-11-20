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
    private readonly DataContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMapper _mapper;

    public RoundQuestionService(DataContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper) {
      _context = context;
      _httpContextAccessor = httpContextAccessor;
      _mapper = mapper;
    }

    public async Task<ServiceResponse<RoundDtoGet>> addRoundQuestion(RoundQuestionDtoAdd newRoundQuestion) {
      ServiceResponse<RoundDtoGet> res = new ServiceResponse<RoundDtoGet>();
      try {
        Round round = await _context.Rounds
            .Include(r => r.roundQuestions)
            .ThenInclude(rq => rq.question)
            .FirstOrDefaultAsync(r =>
                r.id == newRoundQuestion.roundId
                && r.user.id == int.Parse(
                    _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)
                    )
                );
        if (round == null) {
          res.success = false;
          res.message = "No round found";
          return res;
        }
        Question question = await _context.Questions
            .FirstOrDefaultAsync(q => q.id == newRoundQuestion.questionId);
        if (question == null) {
          res.success = false;
          res.message = "No question found";
          return res;
        }
        RoundQuestion roundQuestion = new RoundQuestion {
          round = round,
          question = question,
        };
        await _context.RoundQuestions.AddAsync(roundQuestion);
        await _context.SaveChangesAsync();
        res.data = _mapper.Map<RoundDtoGet>(round);
      } catch (Exception e) {
        res.success = false;
        res.message = e.Message;
      }
      return res;
    }
  }
}