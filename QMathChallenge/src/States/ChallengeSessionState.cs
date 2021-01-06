using System;
using System.Collections.Generic;
using System.Linq;
using QLib;

namespace MC
{
    public class ChallengeSessionState : QLib.FSM.StateBase
    {
        public class Context : QLib.FSM.iContext
        {
            public int levelNumber;
            public QuestionProviderInput challengeInputs;
        }
        public static QLib.FSM.iContext GetContext(int levelNumber, QuestionProviderInput challengeInput)
        {
            Context context = new Context();
            context.levelNumber = levelNumber;
            context.challengeInputs = challengeInput;
            return context;
        }

        public override void OnContext(QLib.FSM.iContext context)
        {
            if (context == null || !(context is Context))
                throw new Exception("Invalid context");
            level = ((Context)context).levelNumber;
            challengeInputs = ((Context)context).challengeInputs;
            controller = new PracticeSessionController(challengeInputs);

            Console.Clear();
            ConsoleWriter.PrintInColor($"Lets Begin the challenge for Level {level}", ConsoleColor.DarkGreen);
        }

        // UI update
        public override void Update()
        {
            if (!controller.IsSessionCompleted)
            {
                ConsoleUtils.DoAction(("Lets Go"), "", "n", true, 
                    new ConsoleUtils.ActionParams("y", "y. yes", delegate (ConsoleUtils.ActionParamsContext context) { StartSession(); }),
                    new ConsoleUtils.ActionParams("n", "n. no", delegate (ConsoleUtils.ActionParamsContext context) { Exit(); })
                );
                return;
            }
            
            {
                ConsoleUtils.DoAction(("All Good!, Practice completed. Retry ?"), ":", "n", true, 
                    new ConsoleUtils.ActionParams("y", "y. yes", delegate (ConsoleUtils.ActionParamsContext context) { StartSession(); }),
                    new ConsoleUtils.ActionParams("n", "n. no", delegate (ConsoleUtils.ActionParamsContext context) { Exit(); })
                );
                return;
            }
        }

        public override void Exit()
        {
            base.Exit();
        }

        private void StartSession()
        {
            controller.StartSession();   

            while ( controller.IsNextQuestionAvailable())
            {
                QuestionProvider.Question q = controller.GetNextQuestion();
                ConsoleWriter.Print($"{q.a} {q.operatorStr} {q.b}");
                int ans = ConsoleReader.GetUserInputInt("");
                if (!controller.IsAnswerForCurrentQuestionRight(ans))
                    ConsoleWriter.PrintInColor("Naah. That was not right!", ConsoleColor.Yellow);
                controller.SetAnswerForCurrentQuestionAndEndSessionIfRequired(ans);
            }

            ShowResults();
        }
        private void ShowResults()
        {
            ConsoleWriter.PrintNewLine();
            ConsoleWriter.PrintNewLine();
            int successPercentage = (int)( controller._passedQuestions.Count * 100 / (controller._passedQuestions.Count + controller._failedQuestions.Count));
            ConsoleWriter.Print($"{successPercentage}% Success");

            if (successPercentage < challengeInputs.passPercent)
            {
                int timeTakenInSecs = (int)(controller._endTime - controller._startTime).TotalSeconds;
                ConsoleWriter.Print($"Time Taken:{timeTakenInSecs} secs");
                ConsoleWriter.Print("Do try again!");
                return;
            }
            else
            {
                int timeTakenInSecs = (int)(controller._endTime - controller._startTime).TotalSeconds;
                int stars = challengeInputs.GetStarsForTime(timeTakenInSecs);

                ConsoleWriter.Print($"Level {level} - Test Passed with {stars} stars! Time Taken:{timeTakenInSecs} secs");
            }
        }


        // Private
        int level;
        QuestionProviderInput challengeInputs;

        PracticeSessionController controller;
    }
}
