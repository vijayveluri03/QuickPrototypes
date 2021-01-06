using System;
using System.Collections.Generic;
using System.Linq;
using QLib;

namespace MC
{
    public class ChooseDifficultyLevelState : QLib.FSM.StateBase
    {

        public override void OnContext(QLib.FSM.iContext context)
        {
        }

        // UI update
        public override void Update()
        {
            ConsoleWriter.PrintNewLine();
            ConsoleWriter.PrintNewLine();

            ConsoleUtils.DoAction("Choose Level of difficulty", "", "1", true, 

                new ConsoleUtils.ActionParams("1", "1. Level 1 (25 questions)", delegate (ConsoleUtils.ActionParamsContext context)
                {
                    int levelNumber = 1;
                    int questions = 25;
                    challengeInputs = new QuestionProviderInput(90/* success percent*/, (int)(questions * 2.5)/* three star time*/, questions * 4 /*two star*/, questions * 6/*Single star*/);
                    challengeInputs.AddAdditionModule(questions/*Questions*/, 0, 5, 0, 5);
                    

                    _sm.PushInNextFrame(new ChallengeSessionState().Initialize(_sm), ChallengeSessionState.GetContext(levelNumber, challengeInputs));
                }),

                new ConsoleUtils.ActionParams("2", "2. Level 2 (25 questions)", delegate (ConsoleUtils.ActionParamsContext context)
                {
                    int levelNumber = 2;
                    int questions = 25;
                    challengeInputs = new QuestionProviderInput(90/* success percent*/, (int)(questions * 2.5)/* three star time*/, questions * 4 /*two star*/, questions * 6/*Single star*/);
                    challengeInputs.AddAdditionModule(questions/*Questions*/, 0, 10, 0, 10);
                    

                    _sm.PushInNextFrame(new ChallengeSessionState().Initialize(_sm), ChallengeSessionState.GetContext(levelNumber, challengeInputs));
                }),

                new ConsoleUtils.ActionParams("3", "3. Level 3 (25 questions)", delegate (ConsoleUtils.ActionParamsContext context)
                {
                    int levelNumber = 3;
                    int questions = 25;
                    challengeInputs = new QuestionProviderInput(90/* success percent*/, (int)(questions * 2.5)/* three star time*/, questions * 4 /*two star*/, questions * 6/*Single star*/);
                    challengeInputs.AddMultiplicationModule(questions/3/*Questions*/, 2, 6, 2, 6);
                    challengeInputs.AddAdditionModule(questions/3, 0, 12, 0, 12);
                    challengeInputs.AddSubstractionModule(questions / 3, 10, 20, 0, 10);

                    _sm.PushInNextFrame(new ChallengeSessionState().Initialize(_sm), ChallengeSessionState.GetContext(levelNumber, challengeInputs));
                }),
                new ConsoleUtils.ActionParams("4", "4. Level 4 (25 questions)", delegate (ConsoleUtils.ActionParamsContext context)
                {
                    int levelNumber = 4;
                    int questions = 25;
                    challengeInputs = new QuestionProviderInput(90/* success percent*/, (int)(questions * 2.5)/* three star time*/, questions * 4 /*two star*/, questions * 6/*Single star*/);

                    challengeInputs.AddMultiplicationModule(questions / 3/*Questions*/, 2, 7, 2, 7);
                    challengeInputs.AddAdditionModule(questions / 3, 0, 15, 0, 15);
                    challengeInputs.AddSubstractionModule(questions / 3, 15, 30, 0, 10);


                    _sm.PushInNextFrame(new ChallengeSessionState().Initialize(_sm), ChallengeSessionState.GetContext(levelNumber, challengeInputs));
                }),
                new ConsoleUtils.ActionParams("5", "5. Level 5 (50 questions)", delegate (ConsoleUtils.ActionParamsContext context)
                {
                    int levelNumber = 5;
                    int questions = 25*2;
                    challengeInputs = new QuestionProviderInput(90/* success percent*/, (int)(questions * 2.5)/* three star time*/, questions * 4 /*two star*/, questions * 6/*Single star*/);

                    challengeInputs.AddMultiplicationModule(questions / 4, 2, 8, 2, 8);
                    challengeInputs.AddAdditionModule(questions / 4, 0, 20, 0, 20);
                    challengeInputs.AddSubstractionModule(questions / 4, 15, 40, 0, 15);
                    challengeInputs.AddDivitionModule(questions / 4, 5);


                    _sm.PushInNextFrame(new ChallengeSessionState().Initialize(_sm), ChallengeSessionState.GetContext(levelNumber, challengeInputs));
                }),
                new ConsoleUtils.ActionParams("6", "6. Level 6 (50 questions)", delegate (ConsoleUtils.ActionParamsContext context)
                {
                    int levelNumber = 6;
                    int questions = 25*2;
                    challengeInputs = new QuestionProviderInput(90/* success percent*/, (int)(questions * 2.5)/* three star time*/, questions * 4 /*two star*/, questions * 6/*Single star*/);

                    challengeInputs.AddMultiplicationModule(questions / 4, 2, 9, 2, 9);
                    challengeInputs.AddAdditionModule(questions / 4, 0, 30, 0, 30);
                    challengeInputs.AddSubstractionModule(questions / 4, 20, 40, 0, 20);
                    challengeInputs.AddDivitionModule(questions / 4, 6);


                    _sm.PushInNextFrame(new ChallengeSessionState().Initialize(_sm), ChallengeSessionState.GetContext(levelNumber, challengeInputs));
                }),
                new ConsoleUtils.ActionParams("7", "7. Level 7 (75 questions)", delegate (ConsoleUtils.ActionParamsContext context)
                {
                    int levelNumber = 7;
                    int questions = 25 * 3;
                    challengeInputs = new QuestionProviderInput(90/* success percent*/, (int)(questions * 2.5)/* three star time*/, questions * 4 /*two star*/, questions * 6/*Single star*/);

                    challengeInputs.AddMultiplicationModule(questions / 5, 2, 10, 2, 10);
                    challengeInputs.AddAdditionModule(questions / 5, 0, 40, 0, 40);
                    challengeInputs.AddSubstractionModule(questions / 5, 40, 80, 0, 40);
                    challengeInputs.AddDivitionModule(questions / 5, 8);
                    challengeInputs.AddReminderModule(questions / 5, 5);

                    _sm.PushInNextFrame(new ChallengeSessionState().Initialize(_sm), ChallengeSessionState.GetContext(levelNumber, challengeInputs));
                }),
                new ConsoleUtils.ActionParams("8", "8. Level 8 (75 questions)", delegate (ConsoleUtils.ActionParamsContext context) {
                    int levelNumber = 8;
                    int questions = 25 * 3;
                    challengeInputs = new QuestionProviderInput(92/* success percent*/, (int)(questions * 2.5)/* three star time*/, questions * 4 /*two star*/, questions * 6/*Single star*/);

                    challengeInputs.AddMultiplicationModule(questions / 5, 2, 10, 2, 10);
                    challengeInputs.AddAdditionModule(questions / 5, 0, 40, 0, 40);
                    challengeInputs.AddSubstractionModule(questions / 5, 30, 60, 0, 30);
                    challengeInputs.AddDivitionModule(questions / 5, 9);
                    challengeInputs.AddReminderModule(questions / 5, 6);

                    _sm.PushInNextFrame(new ChallengeSessionState().Initialize(_sm), ChallengeSessionState.GetContext(levelNumber, challengeInputs));
                }),
                new ConsoleUtils.ActionParams("9", "9. Level 9 (75 questions)", delegate (ConsoleUtils.ActionParamsContext context) {
                    int levelNumber = 9;
                    int questions = 25 * 3;
                    challengeInputs = new QuestionProviderInput(94/* success percent*/, (int)(questions * 2.5)/* three star time*/, questions * 4 /*two star*/, questions * 6/*Single star*/);

                    challengeInputs.AddMultiplicationModule(questions / 5, 2, 10, 2, 10);
                    challengeInputs.AddAdditionModule(questions / 5, 0, 40, 0, 40);
                    challengeInputs.AddSubstractionModule(questions / 5, 30, 60, 0, 30);
                    challengeInputs.AddDivitionModule(questions / 5, 10);
                    challengeInputs.AddReminderModule(questions / 5, 7);

                    _sm.PushInNextFrame(new ChallengeSessionState().Initialize(_sm), ChallengeSessionState.GetContext(levelNumber, challengeInputs));
                }),
                new ConsoleUtils.ActionParams("10", "10. Level 10 (75 questions)", delegate (ConsoleUtils.ActionParamsContext context) {
                    int levelNumber = 10;
                    int questions = 25 * 3;
                    challengeInputs = new QuestionProviderInput(95/* success percent*/, (int)(questions * 2.5)/* three star time*/, questions * 4 /*two star*/, questions * 6/*Single star*/);

                    challengeInputs.AddMultiplicationModule(questions / 5, 2, 10, 2, 10);
                    challengeInputs.AddAdditionModule(questions / 5, 0, 40, 0, 40);
                    challengeInputs.AddSubstractionModule(questions / 5, 30, 60, 0, 30);
                    challengeInputs.AddDivitionModule(questions / 5, 10);
                    challengeInputs.AddReminderModule(questions / 5, 10);

                    _sm.PushInNextFrame(new ChallengeSessionState().Initialize(_sm), ChallengeSessionState.GetContext(levelNumber, challengeInputs));
                }),

                new ConsoleUtils.ActionParams("c", "c. Custom", delegate (ConsoleUtils.ActionParamsContext context) {
                    int levelNumber = 0;
                    int questionsForAddition = ConsoleReader.GetUserInputInt("Addition question count:", 25);
                    int questionsForSubstractions = ConsoleReader.GetUserInputInt("Substractions question count:", 25);
                    int questionsForMultiplications = ConsoleReader.GetUserInputInt("Multiplication question count:", 25);
                    int questionsForDivision = ConsoleReader.GetUserInputInt("Division question count:", 25);
                    int questionsForReminder = ConsoleReader.GetUserInputInt("Reminder question count:", 25);

                    int questions = questionsForAddition + questionsForMultiplications + questionsForSubstractions + questionsForReminder + questionsForDivision ;// ConsoleReader.GetUserInputInt("Number of questions", 100);
                    challengeInputs = new QuestionProviderInput(90/* success percent*/, (int)(questions * 2.5)/* three star time*/, questions * 4 /*two star*/, questions * 6/*Single star*/);

                    if (questionsForAddition > 0)
                    {
                        ConsoleWriter.PrintInRed("Provide inputs for Additions:");
                        int from = ConsoleReader.GetUserInputInt("From");
                        int to = ConsoleReader.GetUserInputInt("To");
                        challengeInputs.AddMultiplicationModule(questionsForAddition, from , to, from, to);
                    }
                    if (questionsForSubstractions > 0)
                    {
                        ConsoleWriter.PrintInRed("Provide inputs for Substractions:");
                        int from = ConsoleReader.GetUserInputInt("From");
                        int to = ConsoleReader.GetUserInputInt("To");
                        int from2 = ConsoleReader.GetUserInputInt("From");
                        int to2 = ConsoleReader.GetUserInputInt("To");

                        challengeInputs.AddSubstractionModule(questionsForSubstractions, from, to, from2, to2);
                    }
                    if (questionsForMultiplications > 0)
                    {
                        ConsoleWriter.PrintInRed("Provide inputs for Multiplications:");
                        int from = ConsoleReader.GetUserInputInt("From");
                        int to = ConsoleReader.GetUserInputInt("To");
                        int from2 = ConsoleReader.GetUserInputInt("From");
                        int to2 = ConsoleReader.GetUserInputInt("To");

                        challengeInputs.AddMultiplicationModule(questionsForMultiplications, from, to, from2, to2);
                    }
                    if (questionsForDivision > 0)
                    {
                        ConsoleWriter.PrintInRed("Provide inputs for Division:");
                        int maxTable = ConsoleReader.GetUserInputInt("Max Table");

                        challengeInputs.AddDivitionModule(questionsForDivision, maxTable);
                    }
                    if (questionsForReminder > 0)
                    {
                        ConsoleWriter.PrintInRed("Provide inputs for Reminders:");
                        int maxTable = ConsoleReader.GetUserInputInt("Max Table");

                        challengeInputs.AddReminderModule(questionsForReminder, maxTable);
                    }


                    _sm.PushInNextFrame(new ChallengeSessionState().Initialize(_sm), ChallengeSessionState.GetContext(levelNumber, challengeInputs));
                }),


                new ConsoleUtils.ActionParams("b", "b. Go back", delegate (ConsoleUtils.ActionParamsContext context)
                {
                    Exit();
                })
            );
        }

        public override void Exit()
        {
            base.Exit();
        }

        // Private
        QuestionProviderInput challengeInputs;

    }
}
