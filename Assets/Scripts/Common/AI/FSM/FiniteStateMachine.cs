using System;
using System.Collections.Generic;

namespace Mongo.Common.AI
{
    public class FiniteStateMachine
    {
        public IFSMState anyState
        {
            get;
            set;
        }

        public IFSMState entryState
        {
            get;
            set;
        }

        public IFSMState exitState
        {
            get;
            set;
        }

        /// <summary>
        /// The state of the current active.
        /// </summary>
        private IFSMState mActiveState;

        public IFSMState activeState
        {
            get
            {
                return mActiveState;
            }
        }

        /// <summary>
        /// The fsm state list.
        /// </summary>
        private List<IFSMState> mStates;

        public List<IFSMState> states
        {
            get
            {
                return mStates;
            }
        }

        /// <summary>
        /// The sub fsm list.
        /// </summary>
        private List<FiniteStateMachine> mStateMachines;

        public List<FiniteStateMachine> stateMachines
        {
            get
            {
                return mStateMachines;
            }
        }

        /// <summary>
        /// The transtion.
        /// </summary>
        private List<IFSMTransition> mStateMachineTranstion;

        public List<IFSMTransition> stateMachineTransitions
        {
            get
            {
                return mStateMachineTranstion;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Mongo.Common.AI.FiniteStateMachine"/> class.
        /// </summary>
        public FiniteStateMachine()
        {
            mStates = new List<IFSMState>();
            mStateMachines = new List<FiniteStateMachine>();
            mStateMachineTranstion = new List<IFSMTransition>();
        }

        /// <summary>
        /// Start execute the fsm.
        /// </summary>
        public void Start()
        {
            if (entryState == null)
            {
                throw new Exception("the entry state is null!");
            }
            entryState.OnEnter();
            mActiveState = entryState;
        }

        /// <summary>
        /// Update the fsm.
        /// </summary>
        public void Update()
        {
            if (mActiveState == null)
            {
                return;
            }
            for (int i = 0; i < mActiveState.stateTransitions.Count; ++i)
            {
                IFSMTransition transition = mActiveState.stateTransitions[i];
                if (transition == null)
                {
                    continue;
                }
                if (transition.IsValid())
                {
                    //1.exit current active state;
                    mActiveState.OnExit();
                    //2.get next state;
                    mActiveState = transition.targetState;
                    //3.handle tranition logic;
                    transition.OnTransition();
                    //4.execute new active state.
                    if (mActiveState != null)
                    {
                        mActiveState.OnEnter();
                    }
                    break;
                }
            }
            mActiveState.OnUpdate();
        }


        /// <summary>
        /// Adds the state.
        /// </summary>
        /// <param name="state">State.</param>
        public void AddState(IFSMState state)
        {
            if (!mStates.Contains(state))
            {
                mStates.Add(state);
            }
        }

        /// <summary>
        /// Removes the state.
        /// </summary>
        /// <param name="state">State.</param>
        public void RemoveState(IFSMState state)
        {
            if (mStates.Contains(state))
            {
                mStates.Remove(state);
            }
        }

        /// <summary>
        /// Adds the state machine.
        /// </summary>
        /// <param name="stateMachine">State machine.</param>
        public void AddStateMachine(FiniteStateMachine stateMachine)
        {
            if (!mStateMachines.Contains(stateMachine))
            {
                mStateMachines.Add(stateMachine);
            }
        }

        /// <summary>
        /// Removes the state machine.
        /// </summary>
        /// <param name="stateMachine">State machine.</param>
        public void RemoveStateMachine(FiniteStateMachine stateMachine)
        {
            if (mStateMachines.Contains(stateMachine))
            {
                mStateMachines.Remove(stateMachine);
            }
        }

        /// <summary>
        /// Adds the state machine transition.
        /// </summary>
        /// <param name="transition">Transition.</param>
        public void AddStateMachineTransition(IFSMTransition transition)
        {
            if (!mStateMachineTranstion.Contains(transition))
            {
                mStateMachineTranstion.Add(transition);
            }
        }

        /// <summary>
        /// Removes the state machine transition.
        /// </summary>
        /// <param name="transition">Transition.</param>
        public void RemoveStateMachineTransition(IFSMTransition transition)
        {
            if (mStateMachineTranstion.Contains(transition))
            {
                mStateMachineTranstion.Remove(transition);
            }
        }
    }
}

