using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace QLib
{
    public static class Utils
    { 
        // Utils

        public static int Clamp(int value, int min, int max)
        {
            return (value < min) ? min : (value > max) ? max : value;
        }
        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
        public static void Assert(bool condition, string message = null)
        {
            if (!condition)
                ConsoleWriter.Print("==== ASSERT here =======");
            Debug.Assert(condition, message);
        }
        public static bool IsThisToday(DateTime date)
        {
            if (date.Day == Utils.Now.Day &&
                date.Month == Utils.Now.Month &&
                date.Year == Utils.Now.Year)
                return true;
            return false;
        }
        public static int ConvertRange(int min, int max, int newMin, int newMax, int value)
        {
            float ratio = (value - min) / (float)(max - min);
            return (int)(newMin + (newMax - newMin) * ratio);
        }
        public static float ConvertRange(float min, float max, float newMin, float newMax, float value)
        {
            float ratio = (value - min) / (float)(max - min);
            return (float)(newMin + (newMax - newMin) * ratio);
        }
        public static int Lerp(int min, int max, int ratio)
        {
            return (int)(min + (max - min) * ratio);
        }
        public static DateTime Now
        {
            get
            {
                return DateTime.Now.ZeroTime();
            }
        }

        public static int[] ConvertCommaAndHyphenSeperateStringToIDs(string uberText)
        {
            string[] IDs = uberText.Split(',');
            List<int> convertedIDs = new List<int>();
            foreach (string idStr in IDs)
            {

                if (idStr.Contains('-'))
                {
                    string[] range = idStr.Split('-');
                    Assert(range != null && range.Length == 2);
                    int num1 = Atoi(range[0], -1);
                    int num2 = Atoi(range[1], -1);
                    if (num1 != -1 && num2 != -1 && num1 <= num2)
                    {
                        while (num1 <= num2)
                        {
                            convertedIDs.Add(num1);
                            num1++;
                        }
                    }
                }
                else
                {
                    int id = Atoi(idStr, -1);
                    if (id != -1)
                        convertedIDs.Add(id);
                }
            }
            return convertedIDs.ToArray();
        }

        public static int Atoi(string txt, int defaul)
        {
            int num = defaul;
            if (int.TryParse(txt, out num))
                return num;
            return defaul;
        }

        public static bool CreateFileIfNotExit(string path, string templateFile)
        {
            if (!File.Exists(path))
            {
                Assert(File.Exists(templateFile), "Template file doesnt exist.");
                File.Copy(templateFile, path);
                return true;
            }
            return false;
        }
        //Execute a command in console. 
        public static void ExecuteCommandInConsole(string command)
        {
            Process proc = new System.Diagnostics.Process();
            proc.StartInfo.FileName = "/bin/bash";
            proc.StartInfo.Arguments = "-c \" " + command + " \"";
            //ConsoleWriter.PrintInRed(proc.StartInfo.Arguments);
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.Start();

            while (!proc.StandardOutput.EndOfStream)
            {
                Console.WriteLine(proc.StandardOutput.ReadLine());
            }
        }
    }

    public class Pair<T, U>
    {
        public Pair()
        {
        }
        public Pair(T first, U second)
        {
            this.First = first;
            this.Second = second;
        }
        public T First { get; set; }
        public U Second { get; set; }
    };

    public class Triple<T, U, V>
    {
        public Triple()
        {
        }
        public Triple(T first, U second, V third)
        {
            this.First = first;
            this.Second = second;
            this.Third = third;
        }
        public T First { get; set; }
        public U Second { get; set; }
        public V Third { get; set; }

        public Triple<T, U, V> Clone()
        {
            Triple<T, U, V> newone = new Triple<T, U, V>();
            newone.First = First;
            newone.Second = Second;
            newone.Third = Third;
            return newone;
        }
    };

    public class Quadrupel<T, U, V, W>
    {
        public Quadrupel()
        {
        }
        public Quadrupel(T first, U second, V third, W forth)
        {
            this.First = first;
            this.Second = second;
            this.Third = third;
            this.Forth = forth;
        }
        public T First { get; set; }
        public U Second { get; set; }
        public V Third { get; set; }
        public W Forth { get; set; }
    };
 }