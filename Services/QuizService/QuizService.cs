using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using QuizFlow.Data;
using QuizFlow.Dto.Quiz;
using QuizFlow.Models;

namespace QuizFlow.Services.QuizService {
  public class QuizService : IQuizService {

    private readonly IMapper _mapper;
    private readonly DataContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public QuizService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor) {
      _mapper = mapper;
      _dbContext = context;
      _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ServiceResponse<List<QuizDtoGet>>> getAllQuizzes() {
      ServiceResponse<List<QuizDtoGet>> serviceResponse = new ServiceResponse<List<QuizDtoGet>>();
      List<Quiz> quizzes = await _dbContext.Quizzes
        .ToListAsync();
      serviceResponse.data = quizzes
        .Select(q => _mapper.Map<QuizDtoGet>(q))
        .ToList();
      return serviceResponse;
    }

    public async Task<ServiceResponse<QuizDtoGet>> getQuizById(int id) {
      ServiceResponse<QuizDtoGet> serviceResponse = new ServiceResponse<QuizDtoGet>();
      Quiz quiz = await _dbContext.Quizzes
        .Include(q => q.quizRounds)
        .ThenInclude(qr => qr.round)
        .FirstOrDefaultAsync(q => q.id == id);
      serviceResponse.data = _mapper.Map<QuizDtoGet>(quiz);
      return serviceResponse;
    }

    public async Task<ServiceResponse<List<QuizDtoGet>>> getUserQuizzes() {
      ServiceResponse<List<QuizDtoGet>> serviceResponse = new ServiceResponse<List<QuizDtoGet>>();
      List<Quiz> quizzes = await _dbContext.Quizzes
        .Where(q => q.user.id == getUserId())
        .ToListAsync();
      serviceResponse.data = quizzes
        .Select(q => _mapper.Map<QuizDtoGet>(q))
        .ToList();
      return serviceResponse;
    }

    public async Task<ServiceResponse<QuizDtoGet>> addQuiz(QuizDtoAdd newQuiz) {
      ServiceResponse<QuizDtoGet> serviceResponse = new ServiceResponse<QuizDtoGet>();
      Quiz quiz = _mapper.Map<Quiz>(newQuiz);

      quiz.user = await _dbContext.Users
        .FirstOrDefaultAsync(u => u.id == getUserId());
      quiz.createdAt = DateTime.Now;
      quiz.lastUpdated = DateTime.Now;

      await _dbContext.Quizzes.AddAsync(quiz);
      await _dbContext.SaveChangesAsync();

      // getting it back from db confirms it saved correctly
      Quiz quizDb = await _dbContext.Quizzes
        .Where(q => q.user.id == getUserId())
        .FirstOrDefaultAsync(q => q.id == quiz.id);
      serviceResponse.data = _mapper.Map<QuizDtoGet>(quizDb);
      return serviceResponse;
    }

    public async Task<ServiceResponse<QuizDtoGet>> editQuiz(QuizDtoEdit editedQuiz) {
      ServiceResponse<QuizDtoGet> serviceResponse = new ServiceResponse<QuizDtoGet>();
      try {
        Quiz quiz = await _dbContext.Quizzes
          .Where(q => q.user.id == getUserId())
          .Include(q => q.quizRounds)
          .ThenInclude(qr => qr.round)
          .FirstOrDefaultAsync(q => q.id == editedQuiz.id);
        if (quiz != null) {
          quiz.title = editedQuiz.title;
          quiz.description = editedQuiz.description;
          quiz.lastUpdated = DateTime.Now;
          _dbContext.Quizzes.Update(quiz);
          await _dbContext.SaveChangesAsync();
          serviceResponse.data = _mapper.Map<QuizDtoGet>(quiz);
        } else {
          serviceResponse.success = false;
          serviceResponse.message = "User quiz not found";
        }
      } catch (Exception e) {
        serviceResponse.success = false;
        serviceResponse.message = e.Message;
      }
      return serviceResponse;
    }

    public async Task<ServiceResponse<string>> deleteQuiz(int id) {
      ServiceResponse<string> serviceResponse = new ServiceResponse<string>();
      try {
        Quiz quiz = await _dbContext.Quizzes
          .Where(q => q.user.id == getUserId())
          .FirstOrDefaultAsync(q => q.id == id);
        if (quiz != null) {
          _dbContext.Quizzes.Remove(quiz);
          await _dbContext.SaveChangesAsync();
          serviceResponse.data = "success";
        } else {
          serviceResponse.success = false;
          serviceResponse.message = "User quiz not found";
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
