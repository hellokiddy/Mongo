using System;

namespace Mongo.Common.AI
{
    public class FSMTransition : IFSMTransition
    {
        private IFSMState mTargetState = null;

        public IFSMState targetState
        {
            get
            {
                return mTargetState;
            }
            set
            {
                mTargetState = value;
            }
        }

        private FiniteStateMachine mTargetStateMachine = null;

        public FiniteStateMachine targetStateMachine
        {
            get
            {
                return mTargetStateMachine;
            }
            set
            {
                mTargetStateMachine = value;
            }
        }

        public FSMTransition()
        {
        }

        public virtual bool IsValid()
        {
            return false;
        }


        public virtual void OnTransition()
        {
            
        }
    }
}

