using System;
using System.Collections.Generic;
using System.Linq;
using QLib;

namespace MC
{
    public class QuestionProvider
    {
        
        public class Question
        {
            public int a;
            public int b;

            public eOperation operation;
            public string operatorStr;
            public int sol;
        }
        public static Question[] GetQuestions ( QuestionProviderInput inputs , bool randomize )
        {
            if (inputs == null)
                throw new Exception("invalid param");
            if (inputs.Modules == null || inputs.Modules.Count == 0)
                throw new Exception("invalid param");

            List<Question> questions = new List<Question>();
            foreach(QuestionProviderInput.OperationInput module in inputs.Modules )
            {
                int currQuest = 0;
                for (currQuest = 0; currQuest < module.NumberOfQuestions; currQuest++)
                {
                    Question q = new Question();
                    q.operation = module.Type;

                    switch ( module.Type)
                    {
                        case eOperation.Addition:
                            q.a = GetRandomNumber(module.MinA, module.MaxA);
                            q.b = GetRandomNumber(module.MinB, module.MaxB);
                            q.operatorStr = "+";
                            q.sol = q.a + q.b;
                            break;

                        case eOperation.substraction:
                            q.a = GetRandomNumber(module.MinA, module.MaxA);
                            q.b = GetRandomNumber(module.MinB, module.MaxB);
                            q.operatorStr = "-";
                            q.sol = q.a - q.b;
                            break;

                        case eOperation.multiplication:
                            q.a = GetRandomNumber(module.MinA, module.MaxA);
                            q.b = GetRandomNumber(module.MinB, module.MaxB);
                            q.operatorStr = "*";
                            q.sol = q.a * q.b;
                            break;

                        case eOperation.division:
                            // For Division we use MaxTable, instead of A and B
                            q.b = GetRandomNumber(1, module.MaxTable);
                            q.a = q.b * GetRandomNumber(1, 10);
                            q.operatorStr = "/";
                            q.sol = (int)Math.Floor (q.a / (float)q.b);
                            break;

                        case eOperation.reminder:
                            // For Division we use MaxTable, instead of A and B
                            q.b = GetRandomNumber(1, module.MaxTable);
                            q.a = GetRandomNumber(1, q.b * 10);
                            q.operatorStr = "%";
                            q.sol = q.a % q.b;
                            break;
                        default:
                            throw new Exception("Invalid execution state");

                    }
                    questions.Add(q);
                }
            }

            if (randomize)
                questions.Shuffle();

            return questions.ToArray();
        }

        public static int GetRandomNumber(int minInclusive, int maxInclusive )
        {
            return randomInstance.GetRangeInclusive(minInclusive, maxInclusive);
        }
        private static Random randomInstance = new Random(); 
    }
}
