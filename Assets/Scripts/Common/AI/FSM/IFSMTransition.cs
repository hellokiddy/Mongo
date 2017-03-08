using System;

namespace Mongo.Common.AI
{
    public interface IFSMTransition
    {
        IFSMState targetState{ get; set; }

        FiniteStateMachine targetStateMachine{ get; set; }

        /// <summary>
        /// Determines whether this instance is valid.
        /// </summary>
        /// <returns><c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        bool IsValid();

        /// <summary>
        /// when this transitions fired,can handle any necessary behavioral logic.
        /// </summary>
        void OnTransition();
    }
}

