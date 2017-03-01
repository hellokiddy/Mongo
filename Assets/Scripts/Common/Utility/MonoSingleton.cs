using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
	private static T _instance;

	public static Transform node {
		get;
		set;
	}

	public static T instance{
		get{
			if(_instance == null)
			{
				_instance = GameObject.FindObjectOfType<T> ();
				node = _instance.transform;
				DontDestroyOnLoad (_instance.gameObject);
			}
			if(_instance == null)
			{
				GameObject singleton = new GameObject (typeof(T).ToString ());
				_instance = singleton.AddComponent<T>();
				node = singleton.transform;
				DontDestroyOnLoad (singleton);
			}
			return _instance;
		}
	}

	void Awake()
	{
		Init ();
	}

	void Update()
	{
		OnUpdate (Time.deltaTime);
	}

	/// <summary>
	/// 初始化；
	/// </summary>
	protected abstract void Init ();

	/// <summary>
	/// 逻辑更新；
	/// </summary>
	/// <param name="time">Time.</param>
	protected virtual void OnUpdate(float time)
	{

	}

	/// <summary>
	/// 处理游戏断线重连；
	/// </summary>
	public abstract void Reconnect ();

	/// <summary>
	/// 清理单例中的数据；
	/// </summary>
	public abstract void Clear ();
}
