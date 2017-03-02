using UnityEngine;
using System.Collections;

namespace strange.test
{
	public class HelloWorldContext : SignalContext
	{

		public HelloWorldContext (MonoBehaviour contextView) : base (contextView)
		{
			
		}

		protected override void mapBindings ()
		{
			base.mapBindings ();

			//bind a command to `StartSignal` since it is invoked by SignalContext on Launch();
			commandBinder.Bind<StartSingal> ().To<HelloWorldStartCommand> ().Once ();
		}
	}
}



