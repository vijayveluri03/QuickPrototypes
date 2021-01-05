using System;
using System.Collections.Generic;
using System.Text;

namespace QLib
{

    public static class ConsoleUtils
    {
        public class ActionParamsContext
        {
            //@todo - Clean up
            private static bool showAllCommands = false;
            public static bool DisplayAllCommands { get { return showAllCommands; } set { showAllCommands = value; } }
        }

        public class ActionParams
        {
            public ActionParams(string userAction, string heading, System.Action<ActionParamsContext> actionToPerform, ActionParamsContext context = null)
            {
                this.userActionLowerCase = userAction.ToLower();
                this.heading = heading;
                this.actionToPerform = actionToPerform;
                this.context = context;
            }
            public ActionParams SetVisible(bool visible) { this.visible = visible; return this; }
            public ActionParams SetContext(ActionParamsContext context) { this.context = context; return this; }

            public string userActionLowerCase;
            public string heading;
            public System.Action<ActionParamsContext> actionToPerform;
            public ActionParamsContext context;
            public bool visible = true;
        }
        public static void SetMaxColumnsForOptions(int max)
        {
            maxColumnsInALine = max;
        }
        public static void DoAction(string heading, string question, string defaultAction, bool optionsInNewLine, params ActionParams[] prms)
        {
            bool isDefaultActionAvailable = !string.IsNullOrEmpty(defaultAction);
            ActionParams selectedAction = null;
            int maxColumnsInEachLine = optionsInNewLine ? 1 : maxColumnsInALine;
            string userInput = "";
            while (true)
            {
                int maxCharLength = 0;
                foreach (ActionParams prm in prms)
                {
                    if (prm.heading.Length > maxCharLength)
                        maxCharLength = prm.heading.Length;
                }
                maxCharLength += 2;

                System.Text.StringBuilder consoleMsg = new System.Text.StringBuilder();
                consoleMsg.Append("" + heading + "\n");
                int shown = 0;
                for (int i = 0; i < prms.Length; i++)
                {
                    // If we have context and in the context we are alking not to show this params, then hide it
                    if (!prms[i].visible)
                    {
                        if (ActionParamsContext.DisplayAllCommands)
                        {
                        }
                        else
                            continue;
                    }
                    consoleMsg.Append(string.Format("{0,-" + maxCharLength + "}", prms[i].heading));
                    if ((shown + 1) % maxColumnsInEachLine == 0 && (shown + 1) < prms.Length)
                        consoleMsg.Append("\n");
                    shown++;
                }

                consoleMsg.Append("\n" + question);
                // if ( isDefaultActionAvailable )
                //     consoleMesg.Append( "(default is " + defaultAction + ")");

                userInput = ConsoleReader.GetUserInputString(consoleMsg.ToString(), ConsoleColor.Blue, isDefaultActionAvailable ? defaultAction : "");
                userInput = userInput.ToLower();
                //ConsoleWriter.ResetConsoleColor();

                foreach (ActionParams prm in prms)
                {
                    if (prm.userActionLowerCase == userInput)
                    {
                        selectedAction = prm;
                        break;
                    }
                }
                // trying to see if its a big command with context
                if (selectedAction == null && (userInput.Contains("|")))
                {
                    string command = userInput.Split("|")[0];
                    foreach (ActionParams prm in prms)
                    {
                        if (prm.userActionLowerCase.Contains(command))
                        {
                            selectedAction = prm;
                            break;
                        }
                    }
                }
                if (selectedAction != null)
                    break;
                else
                    ConsoleWriter.Print("Action was invalid. Please try again...");
            }

            selectedAction.actionToPerform(selectedAction.context);
        }
        public static string SelectFrom(string heading, string question, string defaultValue, params string[] prms)
        {
            while (true)
            {
                int maxCharLength = 0;
                foreach (string prm in prms)
                {
                    if (prm.Length > maxCharLength)
                        maxCharLength = prm.Length;
                }
                maxCharLength += 4;
                int defaultValueInt = -1;

                System.Text.StringBuilder consoleMesg = new System.Text.StringBuilder();
                consoleMesg.Append("" + heading + "\n");
                for (int i = 0; i < prms.Length; i++)
                {
                    consoleMesg.Append(string.Format("{0,-" + maxCharLength + "}", (i + 1) + ". " + prms[i]));
                    if ((i + 1) % maxColumnsInALine == 0 && (i + 1) < prms.Length)
                        consoleMesg.Append("\n");
                    if (defaultValue == prms[i])
                        defaultValueInt = i;
                }

                consoleMesg.Append("\n" + question);
                int userInput = ConsoleReader.GetUserInputInt(consoleMesg.ToString(), defaultValueInt + 1);

                userInput--;
                if (userInput < prms.Length && userInput >= 0)
                    return prms[userInput];

                ConsoleWriter.Print("Action was invalid. Please try again...");
            }
        }
        private static int maxColumnsInALine = 5;
    }
}
