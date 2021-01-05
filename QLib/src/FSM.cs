using System;
using System.Collections;
using System.Collections.Generic;

namespace QLib
{
    namespace FSM
    {
        public interface iContext { }
        public abstract class iState
        {
            public abstract void OnContext(iContext context);
            public abstract void OnEnter();
            public abstract void OnExit();
            public abstract void Update();
        }

        public class StateBase : iState
        {
            public virtual StateBase Initialize (StateMachine sm) { _sm = sm; return this; }
            public virtual void Exit() { if (_sm != null) _sm.Pop(); else ConsoleWriter.PrintInRed("Exit called with out setting the sm"); }

            public override void OnContext(iContext context) { }
            public override void Update() { }
            public override void OnEnter() { }
            public override void OnExit() { }

            protected StateMachine _sm;
        }

        public class StateMachine
        {
            public int StateCount { get { return states.Count; } }

            public bool IsThereAnyStateToUpdate()
            {
                return states.Count > 0;
            }

            public void PushInNextFrame(iState state, iContext context = null)
            {
                nextState = state;
                nextStateContext = context;
            }
            public void Pop()
            {
                popRequest = true;
            }

            public void Update()
            {
                if (nextState != null)
                {
                    states.Push(nextState);
                    nextState.OnContext(nextStateContext);
                    nextState.OnEnter();

                    nextState = null;
                    nextStateContext = null;
                }

                if (popRequest)
                {
                    states.Peek().OnExit();
                    states.Pop();
                    popRequest = false;
                }

                if (IsThereAnyStateToUpdate())
                    states.Peek().Update();
            }

            // Private

            private Stack<iState> states = new Stack<iState>();
            private iState nextState = null;
            private iContext nextStateContext = null;
            private bool popRequest = false;
        }
    }
}

