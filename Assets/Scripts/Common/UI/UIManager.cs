using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Honor.Common.UI
{
	/// <summary>
	/// UI管理
	/// 1.UI互斥处理；
	/// 2.UI层级处理；
	/// 3.UI回退处理；
	/// 4.UI自动销毁；
	/// </summary>
	public class UIManager : MonoSingleton<UIManager> 
	{
		private Dictionary<int,object> mPanelStacks;

		#region Override
		protected override void Init()
		{
			mPanelStacks = new Dictionary<int, object> ();
		}

		protected override void OnUpdate(float time)
		{
			
		}

		public override void Reconnect()
		{
			
		}

		public override void Clear()
		{
			
		}
		#endregion

		public void ShowPanel(int id,Bundle args)
		{
			
		}

		private void PreLoadPanel()
		{
			
		}

		private void LoadPanel()
		{
			
		}

	}

	public class PanelConfig
	{
		public int maxPanelCount;
		public List<PanelInfo> panels;
	}

	public class PanelInfo
	{
		public int id;
		public string name;
		public UIPanelType type;
		public int layer;
	}

	public enum UIPanelType
	{
		Normal = 1,
		Fixed = 2,
		PopUp = 3,
		Unique = 4,
	}

	public enum UIShowMode
	{
		DoNothing = 1,
		HideOther = 2,
		NeedBack = 3,
		NoNeedBack = 4,
	}

	public enum UIColliderMode
	{
		None = 1,
		Normal = 2,
		WithBg = 3,
	}
}

