using System;
using System.Collections.Generic;
using System.Linq;
using QLib;

namespace MC
{
    public class ChooseChallengeModeState : QLib.FSM.StateBase
    {
        public class Context : QLib.FSM.iContext
        {
            public System.Action OnExitCallback;
        }
        public static QLib.FSM.iContext GetContext ( System.Action onExitCallback )
        {
            Context context = new Context();
            context.OnExitCallback = onExitCallback;
            return context;
        }
        public override void OnContext(QLib.FSM.iContext context)
        {
            this.context = context as Context;
        }

        // UI update
        public override void Update()
        {
            ConsoleWriter.PrintNewLine();
            ConsoleWriter.PrintNewLine();

            ConsoleUtils.DoAction("Choose mode", "", "1", true, 

                new ConsoleUtils.ActionParams("1", "1. Practice mode ", delegate (ConsoleUtils.ActionParamsContext context)
                {
                    _sm.PushInNextFrame(new ChooseDifficultyLevelState().Initialize(_sm));
                }),
                new ConsoleUtils.ActionParams("2", "2. Cumulative operations ", delegate (ConsoleUtils.ActionParamsContext context) {

                    int maxAdditionNumber = ConsoleReader.GetUserInputInt("Max Addition digit. (Leave it as 0 to ignore this operation)", 0);
                    int maxSubstractionNumber = ConsoleReader.GetUserInputInt("Max Substraction digit. (Leave it as 0 to ignore this operation)", 0);
                    int maxMultiplicationNumber = ConsoleReader.GetUserInputInt("Max Multiplication digit. (Leave it as 0 to ignore this operation)", 0);

                    int startingNumber = ConsoleReader.GetUserInputInt("Starting Number", 0);
                    int questions = ConsoleReader.GetUserInputInt("Question count", 25);

                    QLib.FSM.iContext cummulativeContext = CumulativeOperationState.GetContext( maxAdditionNumber, maxSubstractionNumber, maxMultiplicationNumber, startingNumber, questions);
                    _sm.PushInNextFrame(new CumulativeOperationState().Initialize(_sm), cummulativeContext);
                }),

                //new ConsoleUtils.ActionParams("2", "2. Learning mode", delegate (ConsoleUtils.aActionParamsContext context)
                //{
                //    ConsoleWriter.PrintInRed("Not yet implemented");
                //}),
                new ConsoleUtils.ActionParams("b", "b. back", delegate (ConsoleUtils.ActionParamsContext context)
                {
                    Exit();
                })
            );
        }

        public override void Exit()
        {
            context.OnExitCallback();
            base.Exit();
        }

        // Private
        //QuestionProviderInput challengeInputs;
        Context context;

    }
}
