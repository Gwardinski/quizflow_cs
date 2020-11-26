using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using QuizFlow.Data;
using QuizFlow.Dto.Question;
using QuizFlow.Models;

namespace QuizFlow.Services.QuestionService {
  public class QuestionService : IQuestionService {

    private readonly IMapper _mapper;
    private readonly DataContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public QuestionService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor) {
      _mapper = mapper;
      _dbContext = context;
      _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ServiceResponse<List<QuestionDtoGet>>> getAllQuestions() {
      ServiceResponse<List<QuestionDtoGet>> serviceResponse = new ServiceResponse<List<QuestionDtoGet>>();
      List<Question> questions = await _dbContext.Questions
        .ToListAsync();
      serviceResponse.data = questions
        .Select(q => _mapper.Map<QuestionDtoGet>(q))
        .ToList();
      return serviceResponse;
    }

    public async Task<ServiceResponse<QuestionDtoGet>> getQuestionById(int id) {
      ServiceResponse<QuestionDtoGet> serviceResponse = new ServiceResponse<QuestionDtoGet>();
      Question question = await _dbContext.Questions
        // .Include(q => q.roundQuestions)
        // .ThenInclude(rq => rq.round)
        .FirstOrDefaultAsync(q => q.id == id);
      serviceResponse.data = _mapper.Map<QuestionDtoGet>(question);
      return serviceResponse;
    }

    public async Task<ServiceResponse<List<QuestionDtoGet>>> getUserQuestions() {
      ServiceResponse<List<QuestionDtoGet>> serviceResponse = new ServiceResponse<List<QuestionDtoGet>>();
      List<Question> questions = await _dbContext.Questions
        .Where(q => q.user.id == getUserId())
        .ToListAsync();
      serviceResponse.data = questions
        .Select(q => _mapper.Map<QuestionDtoGet>(q))
        .ToList();
      return serviceResponse;
    }

    public async Task<ServiceResponse<QuestionDtoGet>> addQuestion(QuestionDtoAdd newQuestion) {
      ServiceResponse<QuestionDtoGet> serviceResponse = new ServiceResponse<QuestionDtoGet>();
      Question question = _mapper.Map<Question>(newQuestion);

      question.user = await _dbContext.Users
        .FirstOrDefaultAsync(u => u.id == getUserId());
      question.createdAt = DateTime.Now;
      question.lastUpdated = DateTime.Now;

      await _dbContext.Questions.AddAsync(question);
      await _dbContext.SaveChangesAsync();

      // getting it back from db confirms it saved correctly
      Question questionDb = await _dbContext.Questions
        .Where(q => q.user.id == getUserId())
        .FirstOrDefaultAsync(q => q.id == question.id);
      serviceResponse.data = _mapper.Map<QuestionDtoGet>(questionDb);
      return serviceResponse;
    }

    public async Task<ServiceResponse<QuestionDtoGet>> editQuestion(QuestionDtoEdit editedQuestion) {
      ServiceResponse<QuestionDtoGet> serviceResponse = new ServiceResponse<QuestionDtoGet>();
      try {
        Question question = await _dbContext.Questions
          .Include(q => q.user)
          .FirstOrDefaultAsync(q => q.id == editedQuestion.id);
        if (question.user.id == getUserId()) {
          question.question = editedQuestion.question;
          question.answer = editedQuestion.answer;
          question.lastUpdated = DateTime.Now;
          _dbContext.Questions.Update(question);
          await _dbContext.SaveChangesAsync();
          serviceResponse.data = _mapper.Map<QuestionDtoGet>(question);
        } else {
          serviceResponse.success = false;
          serviceResponse.message = "User question not found";
        }
      } catch (Exception e) {
        serviceResponse.success = false;
        serviceResponse.message = e.Message;
      }
      return serviceResponse;
    }

    public async Task<ServiceResponse<string>> deleteQuestion(int id) {
      ServiceResponse<string> serviceResponse = new ServiceResponse<string>();
      try {
        Question question = await _dbContext.Questions
          .Where(q => q.user.id == getUserId())
          .FirstOrDefaultAsync(q => q.id == id);
        if (question != null) {
          _dbContext.Questions.Remove(question);
          await _dbContext.SaveChangesAsync();
          serviceResponse.data = "success";
        } else {
          serviceResponse.success = false;
          serviceResponse.message = "User question not found";
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