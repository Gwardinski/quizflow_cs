using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuizFlow.Data;
using QuizFlow.Dto.Question;
using QuizFlow.Models;

namespace QuizFlow.Services.QuestionService {
  public class QuestionService : IQuestionService {

    private readonly IMapper _mapper;
    private readonly DataContext _dbContext;

    public QuestionService(IMapper mapper, DataContext context) {
      _dbContext = context;
      _mapper = mapper;
    }

    public async Task<ServiceResponse<List<QuestionDtoGet>>> getAllQuestions() {
      // create res
      ServiceResponse<List<QuestionDtoGet>> serviceResponse = new ServiceResponse<List<QuestionDtoGet>>();
      // get data from DB
      List<Question> dbQuestions = await _dbContext.Questions.ToListAsync();
      // add data to res
      serviceResponse.data = dbQuestions.Select(q => _mapper.Map<QuestionDtoGet>(q)).ToList();
      // return res
      return serviceResponse;
    }

    public async Task<ServiceResponse<QuestionDtoGet>> getQuestionById(int id) {
      // create res
      ServiceResponse<QuestionDtoGet> serviceResponse = new ServiceResponse<QuestionDtoGet>();
      // get data from DB
      Question question = await _dbContext.Questions.FirstOrDefaultAsync(q => q.id == id);
      // add data to res
      serviceResponse.data = _mapper.Map<QuestionDtoGet>(question);
      // return res
      return serviceResponse;
    }

    public async Task<ServiceResponse<List<QuestionDtoGet>>> getUserQuestions(int id) {
      // create res
      ServiceResponse<List<QuestionDtoGet>> serviceResponse = new ServiceResponse<List<QuestionDtoGet>>();
      // get users data from DB
      List<Question> dbQuestions = await _dbContext.Questions.Where(q => q.user.id == id).ToListAsync();
      // add data to res
      serviceResponse.data = dbQuestions.Select(q => _mapper.Map<QuestionDtoGet>(q)).ToList();
      // return res
      return serviceResponse;
    }

    public async Task<ServiceResponse<QuestionDtoGet>> addQuestion(QuestionDtoAdd newQuestion) {
      // create res
      ServiceResponse<QuestionDtoGet> serviceResponse = new ServiceResponse<QuestionDtoGet>();
      // add data to DB
      Question question = _mapper.Map<Question>(newQuestion);
      await _dbContext.Questions.AddAsync(question);
      await _dbContext.SaveChangesAsync();
      // add data to res
      serviceResponse.data = _mapper.Map<QuestionDtoGet>(question);
      // return res
      return serviceResponse;
    }

    public async Task<ServiceResponse<QuestionDtoGet>> editQuestion(QuestionDtoEdit editedQuestion) {
      ServiceResponse<QuestionDtoGet> serviceResponse = new ServiceResponse<QuestionDtoGet>();
      try {
        Question question = await _dbContext.Questions.FirstOrDefaultAsync(q => q.id == editedQuestion.id);
        question.question = editedQuestion.question;
        question.answer = editedQuestion.answer;
        // must set all values manually or will change to default values
        _dbContext.Questions.Update(question);
        await _dbContext.SaveChangesAsync();
        serviceResponse.data = _mapper.Map<QuestionDtoGet>(question);
      } catch (Exception e) {
        serviceResponse.success = false;
        serviceResponse.message = e.Message;
      }
      return serviceResponse;
    }

    public async Task<ServiceResponse<string>> deleteQuestion(int id) {
      ServiceResponse<string> serviceResponse = new ServiceResponse<string>();
      try {
        Question question = await _dbContext.Questions.FirstAsync(q => q.id == id);
        _dbContext.Questions.Remove(question);
        await _dbContext.SaveChangesAsync();
        serviceResponse.data = "success";
      } catch (Exception e) {
        serviceResponse.success = false;
        serviceResponse.message = e.Message;
      }
      return serviceResponse;
    }

  }
}