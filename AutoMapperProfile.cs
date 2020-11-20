using System.Linq;
using AutoMapper;
using QuizFlow.Dto.Question;
using QuizFlow.Dto.Quiz;
using QuizFlow.Dto.Round;
using QuizFlow.Dto.User;
using QuizFlow.Models;

namespace QuizFlow {
  public class AutoMapperProfile : Profile {
    public AutoMapperProfile() {
      CreateMap<User, UserDtoGet>();

      CreateMap<Quiz, QuizDtoGet>();
      CreateMap<QuizDtoAdd, Quiz>();

      CreateMap<Round, RoundDtoGet>();
      CreateMap<RoundDtoAdd, Round>();

      CreateMap<Question, QuestionDtoGet>();
      CreateMap<QuestionDtoAdd, Question>();
    }
  }
}