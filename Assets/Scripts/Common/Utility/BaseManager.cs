using UnityEngine;
using System.Collections;

namespace Mongo.Common.Utility
{
	#if UNITY_EDITOR
	public abstract class BaseManager : MonoBehaviour
	#else
	public abstract class BaseManager
	#endif
	{
		/// <summary>
		/// Init this instance.
		/// </summary>
		public abstract void Init ();

		/// <summary>
		/// Update this instance.
		/// </summary>
		public virtual  void OnUpdate (float deltaTime)
		{
			
		}

		/// <summary>
		/// Reconnect this instance.
		/// </summary>
		public virtual  void Reconnect ()
		{
		}

		/// <summary>
		/// Clear this instance.
		/// </summary>
		public virtual  void Clear ()
		{
		}
	}
}


