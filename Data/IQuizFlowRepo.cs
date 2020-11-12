using System.Collections.Generic;
using QuizFlow.Models;

namespace QuizFlow.Data {
    public interface IQuizFlowRepo {
        IEnumerable<Question> getAllQuestions();
        Question getQuestionById(int id);
    }
}