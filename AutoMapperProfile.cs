using System.Linq;
using AutoMapper;
using QuizFlow.Dto.Question;
using QuizFlow.Dto.Round;
using QuizFlow.Models;

namespace QuizFlow {
  public class AutoMapperProfile : Profile {
    public AutoMapperProfile() {
      CreateMap<Round, RoundDtoGet>()
        .ForMember(dto => dto.questions, r => r.MapFrom(r => r.roundQuestions.Select(rq => rq.question)));
      CreateMap<RoundDtoAdd, Round>();
      CreateMap<Question, QuestionDtoGet>();
      CreateMap<QuestionDtoAdd, Question>();
    }
  }
}