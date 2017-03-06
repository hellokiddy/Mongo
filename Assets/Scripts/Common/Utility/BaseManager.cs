using UnityEngine;
using System.Collections;

#if UNITY_EDITOR
public abstract class BaseManager : MonoBehaviour
#else
public abstract class BaseManager
#endif
{
	/// <summary>
	/// Init this instance.
	/// </summary>
	public abstract bool Init ();

	/// <summary>
	/// Update this instance.
	/// </summary>
	public virtual  void Update ()
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

