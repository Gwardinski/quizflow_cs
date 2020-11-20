using System.Linq;
using AutoMapper;
using QuizFlow.Dto.Question;
using QuizFlow.Dto.Quiz;
using QuizFlow.Dto.Round;
using QuizFlow.Models;

namespace QuizFlow {
  public class AutoMapperProfile : Profile {
    public AutoMapperProfile() {
      CreateMap<Quiz, QuizDtoGet>();
      CreateMap<QuizDtoAdd, Quiz>();

      CreateMap<Round, RoundDtoGet>();
      CreateMap<RoundDtoAdd, Round>();

      CreateMap<Question, QuestionDtoGet>();
      CreateMap<QuestionDtoAdd, Question>();
    }
  }
}