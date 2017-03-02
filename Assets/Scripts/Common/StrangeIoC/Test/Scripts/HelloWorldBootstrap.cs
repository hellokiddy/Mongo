using UnityEngine;
using System.Collections;
using strange.extensions.context.impl;

namespace strange.test
{
	public class HelloWorldBootstrap : ContextView
	{

		// Use this for initialization
		void Awake ()
		{
			this.context = new HelloWorldContext (this);
		}
	}
}


