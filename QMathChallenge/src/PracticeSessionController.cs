using System;
using System.Collections.Generic;
using System.Linq;
using QLib;

namespace MC
{
    public class PracticeSessionController
    {
        public PracticeSessionController(QuestionProviderInput challengeInputs)
        {
            this._challengeInputs = challengeInputs;
        }

        public void StartSession()
        {
            _questions = QuestionProvider.GetQuestions(_challengeInputs, true);
            _sessionInProgress = true;
            _wasSessionCompleted = false;
            _currentQuestion = -1;

            _passedQuestions.Clear();
            _failedQuestions.Clear();
            _startTime = DateTime.Now;
        }
        public void StopSessionForcefully()
        {
            StopSession();
        }
        private void StopSession()
        {
            _sessionInProgress = false;
            _wasSessionCompleted = true;

            _endTime = DateTime.Now;
        }
        

        public bool IsNextQuestionAvailable()
        {
            return _sessionInProgress && ((_currentQuestion + 1) < _questions.Length);
        }
        public QuestionProvider.Question GetNextQuestion()
        {
            if (_currentQuestion >= _questions.Length)
                throw new Exception();

            _currentQuestion++;
            return _questions[_currentQuestion];
        }

        public bool IsAnswerForCurrentQuestionRight(int ans)
        {
            return _questions[_currentQuestion].sol == ans;
        }
        public void SetAnswerForCurrentQuestionAndEndSessionIfRequired(int ans)
        {
            QuestionProvider.Question q = _questions[_currentQuestion];
            
            if (ans == q.sol)
                _passedQuestions.Add(q);
            else
                _failedQuestions.Add(q);

            if ( !IsNextQuestionAvailable())
                StopSession();
        }

        QuestionProviderInput _challengeInputs;
        QuestionProvider.Question[] _questions = null;
        bool _sessionInProgress = false;
        int _currentQuestion = 0;

        public List<QuestionProvider.Question> _failedQuestions { get; private set; } = new List<QuestionProvider.Question>();
        public List<QuestionProvider.Question> _passedQuestions { get; private set; } = new List<QuestionProvider.Question>();
        public DateTime _startTime { get; private set; }
        public DateTime _endTime { get; private set; }
        public bool _wasSessionCompleted { get; private set; } = false;
        public bool IsSessionCompleted { get { return _wasSessionCompleted; } }
    }
}