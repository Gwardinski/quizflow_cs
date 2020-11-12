using AutoMapper;
using QuizFlow.Dto.Question;
using QuizFlow.Models;

namespace QuizFlow {
  public class AutoMapperProfile : Profile {
    public AutoMapperProfile() {
      CreateMap<Question, QuestionDtoGet>();
      CreateMap<QuestionDtoAdd, Question>();
    }
  }
}