//#define DEBUG_TROUBLESHOOT

using System;
using System.Collections.Generic;
using QLib.FSM;

namespace QLib
{
    public class MainLoopWithFSM : Singleton<MainLoopWithFSM>
    {
        // Getters

        public StateMachine StateMachine { get { return _stateMachine; } }
        public bool IsExitSignalRaised { get { return exit; } }

        public MainLoopWithFSM()
        {
#if DEBUG
            ConsoleWriter.PrintInRed("Current working directory : " + Environment.CurrentDirectory);
#endif
            _stateMachine = new StateMachine();
        }
        public void PushState(iState state, iContext context = null)
        {
            _stateMachine.PushInNextFrame(state, context);
        }
        public void PopState()
        {
            _stateMachine.Pop();
        }

        public void Update()
        {
            _stateMachine.Update();
            if (!_stateMachine.IsThereAnyStateToUpdate())
            {
#if DEBUG
                ConsoleWriter.PrintInRed("No states were found in the FSM. Exiting");
#endif

                Exit();
                return;
            }
        }

        public void Exit()
        {
            exit = true;
        }

        // private

        private StateMachine _stateMachine = null;
        private bool exit = false;
    }
}
