using UnityEngine;
using System.Collections;
using strange.extensions.command.impl;

namespace strange.test
{
	public class HelloWorldStartCommand : Command
	{

		public override void Execute ()
		{
			//perform all game start setup here;
			Debug.Log ("hello,world!!!");
		}
	}
}


