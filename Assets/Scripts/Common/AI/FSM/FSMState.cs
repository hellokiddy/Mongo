using System;
using System.Collections.Generic;

namespace Mongo.Common.AI
{
    public class FSMState : IFSMState
    {
        private List<IFSMTransition> mStateTransitions;

        public List<IFSMTransition> stateTransitions
        {
            get
            {
                return mStateTransitions;
            }
        }

        public FSMState()
        {
            mStateTransitions = new List<IFSMTransition>();
        }

        public virtual void OnEnter()
        {
            
        }

        public virtual void OnUpdate()
        {
            
        }

        public virtual void OnExit()
        {
            
        }

        public virtual void AddStateTransition(IFSMTransition transition)
        {
            if (mStateTransitions.Contains(transition))
            {
                return;
            }
            mStateTransitions.Add(transition);
        }

        public virtual void RemoveStateTransition(IFSMTransition transition)
        {
            if (!mStateTransitions.Contains(transition))
            {
                return;
            }
            mStateTransitions.Remove(transition);
        }
    }
}

