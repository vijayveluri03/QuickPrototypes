using System;
using System.Collections.Generic;
using QLib;

namespace MC
{
    public enum eOperation
    {
        Addition,
        substraction,
        multiplication,
        division,
        reminder
    }
    public enum eStars
    {
        One,
        Two,
        Three,

        COUNT = 3
    }
    public class QuestionProviderInput
    {
        public class OperationInput
        {
            public OperationInput(eOperation type, int questions, int minA, int minB, int maxA, int maxB) { Type = type; NumberOfQuestions = questions; MinA = minA; MinB = minB; MaxA = maxA; MaxB = maxB; }
            public OperationInput(eOperation type, int questions, int maxTable ) { Type = type; NumberOfQuestions = questions; MaxTable = maxTable; }
            public eOperation Type { get; private set; }
            public int NumberOfQuestions { get; private set; }
            public int MinA { get; private set; }
            public int MinB { get; private set; }
            public int MaxA { get; private set; }
            public int MaxB { get; private set; }

            public int MaxTable { get; private set; }
        }

        public List<OperationInput> Modules { get; private set; } = new List<OperationInput>();
        public int[] timeInSecs;
        public int passPercent;

        public QuestionProviderInput ( int passPercent, int timeForThreeStars, int timeForTwoStars, int timeForOneStar )
        {
            if ( timeForOneStar < timeForTwoStars || timeForTwoStars < timeForThreeStars)
                ConsoleWriter.PrintInRed("Invalid time for stars");

            timeInSecs = new int[(int)eStars.COUNT];
            timeInSecs[(int)eStars.One] = timeForOneStar;
            timeInSecs[(int)eStars.Two] = timeForTwoStars;
            timeInSecs[(int)eStars.Three] = timeForThreeStars;

            this.passPercent = passPercent;
        }

        public int GetStarsForTime ( int timeInSecs )
        {
            if (timeInSecs < this.timeInSecs[(int)eStars.Three]) return 3;
            if (timeInSecs < this.timeInSecs[(int)eStars.Two]) return 2;
            if (timeInSecs < this.timeInSecs[(int)eStars.One]) return 1;
            return 0;
        }

        public void AddModule(OperationInput module)
        {
            Modules.Add(module);
        }
        public void AddAdditionModule(int questions, int minA, int minB, int maxA, int maxB) { AddModule(new OperationInput(eOperation.Addition, questions, minA, maxA, minB, maxB)); }
        public void AddSubstractionModule(int questions, int minA, int minB, int maxA, int maxB) { AddModule(new OperationInput(eOperation.substraction, questions, minA, maxA, minB, maxB)); }
        public void AddMultiplicationModule(int questions, int minA, int minB, int maxA, int maxB) { AddModule(new OperationInput(eOperation.multiplication, questions, minA, maxA, minB, maxB)); }
        public void AddDivitionModule(int questions, int maxTable) { AddModule(new OperationInput(eOperation.division, questions, maxTable)); }
        public void AddReminderModule(int questions, int maxTable) { AddModule(new OperationInput(eOperation.reminder, questions, maxTable)); }
    }
}
