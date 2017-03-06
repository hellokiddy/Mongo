using System;
using UnityEngine;

#if UNITY_EDITOR
public abstract class Singleton<T> : BaseManager where T : MonoBehaviour
#else
public abstract class Singleton<T> : BaseManager where T : class, new()
#endif
{
	private static T _instance;

	public static T GetInstance ()
	{
		if (_instance == null) {

			#if UNITY_EDITOR
			_instance = GetInstanceInEditor ();
			#else
			_instance = new T ();
			#endif
		}
		return _instance;
	}

	private static T GetInstanceInEditor ()
	{
		GameObject root = new GameObject (typeof(T).Name);
		Transform tran = root.transform;
		tran.localPosition = Vector3.zero;
		tran.localRotation = Vector3.zero;
		tran.localScale = Vector3.one;
		DontDestroyOnLoad (root);
		return root.AddComponent<T> ();
	}

}
