using System;
using System.Collections.Generic;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;

class Program
{
    static void Main(string[] args)
    {
        QLib.ConsoleWriter.PushColor(ConsoleColor.White);
        QLib.MainLoopWithFSM app = new QLib.MainLoopWithFSM();
        app.PushState(new MC.ChooseChallengeModeState().Initialize(app.StateMachine), MC.ChooseChallengeModeState.GetContext( ()=> { app.Exit(); }));
        
        //app.PushState(new MC.ChooseDifficultyState().Initialize(app.StateMachine));

        while (true)
        {
            if (app.IsExitSignalRaised)
                break;
            app.Update();
        }
    }
}
