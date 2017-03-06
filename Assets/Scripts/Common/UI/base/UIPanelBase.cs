using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// User interface panel base.
///
/// UIPanelBase LiftCycle:
/// 							  loop
/// 					-----------<<-------------
/// 					|                        |
/// 					V                        |
/// OnCreate() ------> OnShow(Bundle) ------> OnClose() ------> OnDestroy()
///
/// </summary>

namespace Mongo.Common.UI
{
	public abstract class UIPanelBase : MonoBehaviour
	{

		// Use this for initialization
		void Start ()
		{

		}

		// Update is called once per frame
		void Update ()
		{

		}

		#region Abstract

		public abstract void OnCreate ();

		public abstract void OnShow (Bundle saveInstance);

		public abstract void OnClose ();

		public abstract void OnDestroy ();

		#endregion
	}
}

