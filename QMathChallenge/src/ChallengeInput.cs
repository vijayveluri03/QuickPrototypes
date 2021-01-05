using System;
using System.Collections.Generic;
using QLib;

namespace MC
{
    public enum ModuleType
    {
        Addition,
        substraction,
        multiplication,
        division,
        reminder
    }
    public enum Stars
    {
        One,
        Two,
        Three,

        COUNT = 3
    }
    public class Module
    {
        public Module(ModuleType type, int questions, int minA, int minB, int maxA, int maxB) { Type = type; NumberOfQuestions = questions; MinA = minA; MinB = minB; MaxA = maxA; MaxB = maxB; }
        public ModuleType Type { get; private set; }
        public int NumberOfQuestions { get; private set; }
        public int MinA { get; private set; }
        public int MinB { get; private set; }
        public int MaxA { get; private set; }
        public int MaxB { get; private set; }
    }
    public class ChallengeInput
    {
        public List<Module> Modules { get; private set; } = new List<Module>();
        public int[] timeInSecs;
        public int passPercent;

        public ChallengeInput ( int passPercent, int timeForThreeStars, int timeForTwoStars, int timeForOneStar )
        {
            if ( timeForOneStar < timeForTwoStars || timeForTwoStars < timeForThreeStars)
                ConsoleWriter.PrintInRed("Invalid time for stars");

            timeInSecs = new int[(int)Stars.COUNT];
            timeInSecs[(int)Stars.One] = timeForOneStar;
            timeInSecs[(int)Stars.Two] = timeForTwoStars;
            timeInSecs[(int)Stars.Three] = timeForThreeStars;

            this.passPercent = passPercent;
        }

        public int GetStarsForTime ( int timeInSecs )
        {
            if (timeInSecs < this.timeInSecs[(int)Stars.Three]) return 3;
            if (timeInSecs < this.timeInSecs[(int)Stars.Two]) return 2;
            if (timeInSecs < this.timeInSecs[(int)Stars.One]) return 1;
            return 0;
        }

        public void AddModule(Module module)
        {
            Modules.Add(module);
        }
        public void AddAdditionModule(int questions, int minA, int minB, int maxA, int maxB) { AddModule(new Module(ModuleType.Addition, questions, minA, maxA, minB, maxB)); }
        public void AddSubstractionModule(int questions, int minA, int minB, int maxA, int maxB) { AddModule(new Module(ModuleType.substraction, questions, minA, maxA, minB, maxB)); }
        public void AddMultiplicationModule(int questions, int minA, int minB, int maxA, int maxB) { AddModule(new Module(ModuleType.multiplication, questions, minA, maxA, minB, maxB)); }
        public void AddDivitionModule(int questions, int minA, int minB, int maxA, int maxB) { AddModule(new Module(ModuleType.division, questions, minA, maxA, minB, maxB)); }
        public void AddReminderModule(int questions, int minA, int minB, int maxA, int maxB) { AddModule(new Module(ModuleType.reminder, questions, minA, maxA, minB, maxB)); }
    }
}
