using System;

public abstract class Singleton<T> where T : class,new()
{
	private static T _instance;

	public static T GetInstance()
	{
		if(_instance == null)
		{
			_instance = new T ();
		}
		return _instance;
	}

	/// <summary>
	/// 初始化；
	/// </summary>
	public abstract void Init ();

	/// <summary>
	/// 逻辑更新；
	/// </summary>
	/// <param name="time">Time.</param>
	public virtual void Update(float time)
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
