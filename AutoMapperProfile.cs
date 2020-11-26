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

      CreateMap<Quiz, QuizDtoGet>()
        .ForMember(dto => dto.rounds, q => q.MapFrom(q => q.quizRounds.Select(qr => qr.round)));
      CreateMap<QuizDtoAdd, Quiz>();

      CreateMap<Round, RoundDtoGet>()
        .ForMember(dto => dto.questions, r => r.MapFrom(r => r.roundQuestions.Select(rq => rq.question)));
      CreateMap<RoundDtoAdd, Round>();

      CreateMap<Question, QuestionDtoGet>();
      CreateMap<QuestionDtoAdd, Question>();
    }
  }
}