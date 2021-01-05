using System;
using System.Collections.Generic;
using System.Linq;
using QLib;

namespace MC
{
    public class CumulativeOperationState : QLib.FSM.StateBase
    {
        public class Context : QLib.FSM.iContext
        {
            public int maxNumberToAdd = 0;
            public int maxNumberToSub = 0;
            public int maxNumberToMul = 0;
            public int questions = 10;
            public int startingNumber = 0;

            public bool AdditionOp { get { return maxNumberToAdd > 0; } }
            public bool SubstractionOp { get { return maxNumberToSub > 0; } }
            public bool MultiplicationOp { get { return maxNumberToMul > 0; } }
        }
        public static QLib.FSM.iContext GetContext(int maxNumberToAdd, int maxNumberToSub, int maxNumberToMul, int startingNumber, int questions)
        {
            Context context = new Context();
            context.maxNumberToAdd = maxNumberToAdd;
            context.maxNumberToSub = maxNumberToSub;
            context.maxNumberToMul = maxNumberToMul;
            context.startingNumber = startingNumber;
            context.questions = questions;
            return context;
        }

        public override void OnContext(QLib.FSM.iContext context)
        {
            if (context == null || !(context is Context))
                throw new Exception("Invalid context");
            this.context = context as Context;

            Console.Clear();
            ConsoleWriter.PrintInColor($"Lets Begin the challenge!", ConsoleColor.DarkGreen);
        }

        // UI update
        public override void Update()
        {
            if (!isPracticeCompleted)
            {
                ConsoleUtils.DoAction(("Lets Go"), "", "n", true, 
                    new ConsoleUtils.ActionParams("y", "y. yes", delegate (ConsoleUtils.ActionParamsContext context) { StartSession(); }),
                    new ConsoleUtils.ActionParams("n", "n. no", delegate (ConsoleUtils.ActionParamsContext context) { Exit(); })
                );
                return;
            }
            
            {
                ConsoleUtils.DoAction(("All Good!, Practice completed. Retry ?"), "", "n", true, 
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
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            DateTime startTime = DateTime.Now;
            DateTime endTime = DateTime.Now;
            int questions = context.questions;
            Stack<int> operations = new Stack<int>();

            while (questions > 0)
            {
                int op = rand.GetRangeInclusive(1, 3);// add, sub, mul
                bool opFound = false;
                switch(op)
                {
                    case 1:
                        if (context.AdditionOp)
                            opFound = true;
                        break;
                    case 2:
                        if (context.SubstractionOp)
                            opFound = true;
                        break;
                    case 3:
                        if (context.MultiplicationOp)
                            opFound = true;
                        break;
                }
                if (opFound)
                {
                    operations.Push(op);
                    questions--;
                }
            }

            int cumulativeNumber = context.startingNumber;
            ConsoleWriter.Print($"{cumulativeNumber}");
            int rightAns = 0, wrongAns = 0;
            while ( operations.Count > 0 )
            {
                int op = operations.Pop();
                int randomNum = 0;
                int answer = 0;
                switch (op)
                {
                    case 1:
                        Utils.Assert(context.AdditionOp);
                        randomNum = rand.GetRangeInclusive(0, context.maxNumberToAdd);
                        cumulativeNumber += randomNum;
                        answer = ConsoleReader.GetUserInputInt("+" + randomNum);
                        break;
                    case 2:
                        Utils.Assert(context.SubstractionOp);
                        randomNum = rand.GetRangeInclusive(0, context.maxNumberToSub);
                        cumulativeNumber -= randomNum;
                        answer = ConsoleReader.GetUserInputInt("-" + randomNum);
                        break;
                    case 3:
                        Utils.Assert(context.MultiplicationOp) ;
                        randomNum = rand.GetRangeInclusive(0, context.maxNumberToMul);
                        cumulativeNumber *= randomNum;
                        answer = ConsoleReader.GetUserInputInt("*" + randomNum);
                        break;
                }

                if (answer != cumulativeNumber)
                {
                    ConsoleWriter.PrintInColor("Naah. That was not right!", ConsoleColor.Yellow);
                    ConsoleWriter.Print($"{cumulativeNumber}");
                    wrongAns++;
                }
                else
                    rightAns++;
            }
            endTime = DateTime.Now;

            ShowResults( rightAns, wrongAns, startTime, endTime);
        }
        private void ShowResults(int rightAns, int wrongAns, DateTime startTime, DateTime endTime )
        {
            ConsoleWriter.PrintNewLine();
            ConsoleWriter.PrintNewLine();
            int successPercentage = (int)( rightAns * 100 / (rightAns + wrongAns));
            ConsoleWriter.Print($"{successPercentage}% Success");

            if (successPercentage < 90/*hack*/)
            {
                ConsoleWriter.Print("Do try again!");
                return;
            }
            int timeTakenInSecs = (int)(endTime - startTime).TotalSeconds;
            ConsoleWriter.Print($"Time Taken:{timeTakenInSecs} secs");
        }


        Context context;
        bool isPracticeCompleted = false;
    }
}
