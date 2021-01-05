using System;
using System.Collections.Generic;
using System.Linq;
using QLib;

namespace MC
{
    
    public class Question
    {
        public int a;
        public int b;

        public string op;
        public int sol;
    }
    public class Questioner
    {
        public static Question[] GetQuestions ( ChallengeInput inputs , bool randomize )
        {
            if (inputs == null)
                throw new Exception("invalid param");
            if (inputs.Modules == null || inputs.Modules.Count == 0)
                throw new Exception("invalid param");

            List<Question> questions = new List<Question>();
            foreach( Module module in inputs.Modules )
            {
                int currQuest = 0;
                for (currQuest = 0; currQuest < module.NumberOfQuestions; currQuest++)
                {
                    Question q = new Question();
                    q.a = GetRandomNumber(module.MinA, module.MaxA);
                    q.b = GetRandomNumber(module.MinB, module.MaxB);

                    switch( module.Type)
                    {
                        case ModuleType.Addition:
                            q.op = "+";
                            q.sol = q.a + q.b;
                            break;
                        case ModuleType.substraction:
                            q.op = "-";
                            q.sol = q.a - q.b;
                            break;
                        case ModuleType.multiplication:
                            q.op = "*";
                            q.sol = q.a * q.b;
                            break;
                        case ModuleType.division:
                            q.op = "/";
                            q.sol = (int)Math.Floor (q.a / (float)q.b);
                            break;
                        case ModuleType.reminder:
                            //q.op = "+";
                            //q.sol =  q.a + q.b;
                            throw new Exception("NYI");
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
