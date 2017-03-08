using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mongo.Common.AI
{
    public interface IFSMState
    {
        List<IFSMTransition> stateTransitions
        {
            get;
        }

        void OnEnter();

        void OnUpdate();

        void OnExit();

        void AddStateTransition(IFSMTransition transition);

        void RemoveStateTransition(IFSMTransition transition);
    }
}

