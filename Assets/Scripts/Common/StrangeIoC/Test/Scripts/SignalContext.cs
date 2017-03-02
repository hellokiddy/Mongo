using UnityEngine;
using System.Collections;
using strange.extensions.signal.impl;
using strange.extensions.context.impl;
using strange.extensions.command.api;
using strange.extensions.command.impl;

namespace strange.test
{
	public class SignalContext : MVCSContext
	{

		public SignalContext (MonoBehaviour contextView) : base (contextView)
		{
			
		}

		protected override void addCoreComponents ()
		{
			base.addCoreComponents ();

			//bind signal command binder
			injectionBinder.Unbind<ICommandBinder> ();
			injectionBinder.Bind<ICommandBinder> ().To<SignalCommandBinder> ().ToSingleton ();
		}

		public override void Launch ()
		{
			base.Launch ();
			Signal startSignal = injectionBinder.GetInstance<StartSingal> ();
			startSignal.Dispatch ();
		}
	
	}
}


