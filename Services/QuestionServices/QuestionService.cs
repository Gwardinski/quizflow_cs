using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using QuizFlow.Dto.Question;
using QuizFlow.Models;

namespace QuizFlow.Services.QuestionServices {
  public class QuestionService : IQuestionService {

    private static List<Question> questions = new List<Question>{
            new Question { id = 0, question = "q1", answer = "a1" },
            new Question { id = 1, question = "q2", answer = "a2" },
            new Question { id = 2, question = "q3", answer = "a3" }
        };

    private readonly IMapper _mapper;

    public QuestionService(IMapper mapper) {
      _mapper = mapper;
    }

    public async Task<ServiceResponse<List<QuestionDtoGet>>> getAllQuestions() {
      ServiceResponse<List<QuestionDtoGet>> serviceResponse = new ServiceResponse<List<QuestionDtoGet>>();
      serviceResponse.data = questions.Select(q => _mapper.Map<QuestionDtoGet>(q)).ToList();
      return serviceResponse;
    }

    public async Task<ServiceResponse<QuestionDtoGet>> getQuestionById(int id) {
      ServiceResponse<QuestionDtoGet> serviceResponse = new ServiceResponse<QuestionDtoGet>();
      serviceResponse.data = _mapper.Map<QuestionDtoGet>(questions.FirstOrDefault(q => q.id == id));
      return serviceResponse;
    }

    public async Task<ServiceResponse<QuestionDtoGet>> addQuestion(QuestionDtoAdd newQuestion) {
      ServiceResponse<QuestionDtoGet> serviceResponse = new ServiceResponse<QuestionDtoGet>();
      Question question = _mapper.Map<Question>(newQuestion);
      question.id = questions.Max(q => q.id) + 1;
      questions.Add(question);
      serviceResponse.data = _mapper.Map<QuestionDtoGet>(question);
      return serviceResponse;
    }

    public async Task<ServiceResponse<QuestionDtoGet>> editQuestion(QuestionDtoEdit editedQuestion) {
      ServiceResponse<QuestionDtoGet> serviceResponse = new ServiceResponse<QuestionDtoGet>();
      try {
        Question question = questions.FirstOrDefault(q => q.id == editedQuestion.id);
        question.question = editedQuestion.question;
        question.answer = editedQuestion.answer;
        serviceResponse.data = _mapper.Map<QuestionDtoGet>(question);
      } catch (Exception e) {
        serviceResponse.success = false;
        serviceResponse.message = e.Message;
      }
      return serviceResponse;
    }
  }
}