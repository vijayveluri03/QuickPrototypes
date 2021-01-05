using System;
using System.Collections.Generic;
using System.Text;

namespace QLib
{
    public static class ConsoleReader
    {
        public static string GetUserInputString(string message, string defaultInput = "")
        {
            if (!string.IsNullOrEmpty(defaultInput))
                message += "( defaults to " + defaultInput + ")";
            message += ":";
            ConsoleWriter.PrintWithOutLineBreak(message);
            string input = Console.ReadLine();
            if (string.IsNullOrEmpty(input) && !string.IsNullOrEmpty(defaultInput))
                return defaultInput;
            return input;
        }
        public static string GetUserInputString(string message, ConsoleColor color, string defaultInput = "")
        {
            if (!string.IsNullOrEmpty(defaultInput))
                message += "( defaults to '" + defaultInput + "')";
            message += ":";
            ConsoleWriter.PushColor(color);

            ConsoleWriter.PrintWithOutLineBreak(message, false);
            string input = Console.ReadLine();

            ConsoleWriter.PopColor();

            if (string.IsNullOrEmpty(input) && !string.IsNullOrEmpty(defaultInput))
                return defaultInput;
            return input;
        }
        public static string[] GetUserInputStringArray(string message)
        {
            ConsoleWriter.PrintWithOutLineBreak(message + "(Double Enter to exit):");
            List<string> input = new List<string>();
            while (true)
            {
                string line = Console.ReadLine();

                if (string.IsNullOrEmpty(line))
                    break;
                else
                    input.Add(line);
            }

            return input.ToArray();
        }
        public static int GetUserInputInt(string message, int defaultInput = -999)
        {
            if (defaultInput != -999)
                message += "( defaults to " + defaultInput + ")";
            message += ":";
            ConsoleWriter.PrintWithOutLineBreak(message);
            string input = Console.ReadLine();

            if (string.IsNullOrEmpty(input) && defaultInput != -999)
                return defaultInput;

            int result = 0;
            return int.TryParse(input, out result) ? result : 0;
        }
        public static int[] GetUserInputIntArray(string message)
        {
            ConsoleWriter.PrintWithOutLineBreak(message + "(use comma to seperate");
            string input = Console.ReadLine();

            if (string.IsNullOrEmpty(input))
                return null;

            if (input.Contains(","))
            {
                string[] splitStr = input.Split(',');
                int[] result = new int[splitStr.Length];

                for (int i = 0; i < result.Length; i++)
                {
                    int temp = 0;
                    temp = int.TryParse(splitStr[i], out temp) ? temp : 0;
                    result[i] = temp;
                }
                return result;
            }
            else
            {
                int result = 0;
                result = int.TryParse(input, out result) ? result : 0;
                return new int[] { result };
            }
        }
        public static bool GetConfirmationFromUser(string message, bool takeNoByDefault = false)
        {
            if (!takeNoByDefault)
                ConsoleWriter.PrintWithOutLineBreak(message + " (enter yes or y to confirm, default) :");
            else
                ConsoleWriter.PrintWithOutLineBreak(message + " (enter no or n to confirm, default) :");

            string input = Console.ReadLine();
            bool isYes = input.ToLower() == "yes" || input.ToLower() == "y";

            if (string.IsNullOrEmpty(input))
            {
                if (takeNoByDefault)
                    return false;
                else
                    return true;
            }

            return isYes;
        }
        public static DateTime GetDateFromUser(string message)
        {
            return GetDateFromUser(message, DateTime.MinValue);
        }
        public static DateTime GetDateFromUser(string message, DateTime defaultDate)
        {
            message += "( default or x to " + defaultDate.ShortForm() + ")";

            DateTime date = DateTime.MinValue;
            ConsoleUtils.DoAction(message, ":", "", false,
                new ConsoleUtils.ActionParams("", ". default " + defaultDate.ShortForm(), delegate (ConsoleUtils.ActionParamsContext context) {
                    date = defaultDate;
                }),
                new ConsoleUtils.ActionParams("x", "x. default " + defaultDate.ShortForm(), delegate (ConsoleUtils.ActionParamsContext context) {
                    date = defaultDate;
                }),
                new ConsoleUtils.ActionParams("r", "r. reset " + DateTime.MinValue.ShortForm(), delegate (ConsoleUtils.ActionParamsContext context) {
                    date = DateTime.MinValue;
                }),
                new ConsoleUtils.ActionParams("0", "0. today " + Utils.Now.ShortForm(), delegate (ConsoleUtils.ActionParamsContext context) {
                    date = Utils.Now;
                }),
                new ConsoleUtils.ActionParams("-1", "-1. yest " + Utils.Now.AddDays(-1).ShortForm(), delegate (ConsoleUtils.ActionParamsContext context) {
                    date = Utils.Now.AddDays(-1);
                }),
                new ConsoleUtils.ActionParams("-2", "-2. " + Utils.Now.AddDays(-2).ShortForm(), delegate (ConsoleUtils.ActionParamsContext context) {
                    date = Utils.Now.AddDays(-2);
                }),
                new ConsoleUtils.ActionParams("-3", "-3. " + Utils.Now.AddDays(-3).ShortForm(), delegate (ConsoleUtils.ActionParamsContext context) {
                    date = Utils.Now.AddDays(-3);
                }),
                new ConsoleUtils.ActionParams("1", "1. tomo " + Utils.Now.AddDays(1).ShortForm(), delegate (ConsoleUtils.ActionParamsContext context) {
                    date = Utils.Now.AddDays(1);
                }),
                new ConsoleUtils.ActionParams("2", "2. " + Utils.Now.AddDays(2).ShortForm(), delegate (ConsoleUtils.ActionParamsContext context) {
                    date = Utils.Now.AddDays(2);
                }),
                new ConsoleUtils.ActionParams("3", "3. " + Utils.Now.AddDays(3).ShortForm(), delegate (ConsoleUtils.ActionParamsContext context) {
                    date = Utils.Now.AddDays(3);
                }),
                new ConsoleUtils.ActionParams("4", "4. " + Utils.Now.AddDays(4).ShortForm(), delegate (ConsoleUtils.ActionParamsContext context) {
                    date = Utils.Now.AddDays(4);
                }),
                new ConsoleUtils.ActionParams("7", "7. " + Utils.Now.AddDays(7).ShortForm(), delegate (ConsoleUtils.ActionParamsContext context) {
                    date = Utils.Now.AddDays(7);
                }),
                new ConsoleUtils.ActionParams("14", "14. " + Utils.Now.AddDays(14).ShortForm(), delegate (ConsoleUtils.ActionParamsContext context) {
                    date = Utils.Now.AddDays(14);
                }),
                new ConsoleUtils.ActionParams("30", "30. " + Utils.Now.AddDays(30).ShortForm(), delegate (ConsoleUtils.ActionParamsContext context) {
                    date = Utils.Now.AddDays(30);
                }),
                new ConsoleUtils.ActionParams("ci", "c. custom addition ", delegate (ConsoleUtils.ActionParamsContext context) {
                    date = Utils.Now.AddDays(GetUserInputInt("days from today:", 0));
                }),
                new ConsoleUtils.ActionParams("c", "c. custom ", delegate (ConsoleUtils.ActionParamsContext context) {
                    date = GetCustomDateFromUser("Enter Date (mm/dd) or (yyyy/mm/dd):");
                })
            );
            return date;
        }
        public static DateTime GetCustomDateFromUser(string message)
        {
            ConsoleWriter.PrintWithOutLineBreak(message);

            string input = Console.ReadLine();
            DateTime result;
            if (DateTime.TryParse(input, null, System.Globalization.DateTimeStyles.RoundtripKind, out result))
                return result;
            else
                return DateTime.MinValue;
        }
        public static DateTime GetCustomDateFromUser(string message, DateTime defaultValue, bool showXToReset = true)
        {
            if (defaultValue != DateTime.MinValue)
                message += "( defaults to " + defaultValue + ")";
            if (showXToReset)
                message += "(x to reset to min)";
            ConsoleWriter.PrintWithOutLineBreak(message);

            string input = Console.ReadLine();
            DateTime result;
            if (input == "x")
                return DateTime.MinValue;
            if (DateTime.TryParse(input, null, System.Globalization.DateTimeStyles.RoundtripKind, out result))
                return result;
            else
                return defaultValue;
        }
    }
}
