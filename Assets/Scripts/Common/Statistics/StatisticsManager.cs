using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mongo.Common.Utility;
using Mongo.Common.Event;
using System.Text;

namespace Mongo.Common.Statistics
{
	public class StatisticsManager : Singleton<StatisticsManager>
	{
		private static StringBuilder sBuilder = new StringBuilder ();

		/// <summary>
		/// the fps value.
		/// </summary>
		private const float fpsMeasurePeriod = 0.5f;
		private int mFpsAccumulator = 0;
		private float mFpsNextPeriod = 0;
		private int mCurrentFps;

		/// <summary>
		/// The size of the object pool.
		/// </summary>
		private const float poolTimeInterval = 5.0f;
		private int mMaxBundleSize;
		private int mMaxEventSize;

		/// <summary>
		/// The size of the memory.
		/// </summary>
		private const float memTimeInterval = 5.0f;
		private int mHeapMemorySize;
		private int mNativeMemorySize;

		public override void Init ()
		{
			mFpsNextPeriod = Time.realtimeSinceStartup + fpsMeasurePeriod;
		}

		public override void OnUpdate (float deltaTime)
		{
			UpdateFPS ();
			UpdatePoolSize ();
		}

		private void UpdateFPS ()
		{
			// measure average frames per second
			mFpsAccumulator++;
			if (Time.realtimeSinceStartup > mFpsNextPeriod) {
				mCurrentFps = (int)(mFpsAccumulator / fpsMeasurePeriod);
				mFpsAccumulator = 0;
				mFpsNextPeriod += fpsMeasurePeriod;
			}
		}

		private void UpdatePoolSize ()
		{
			mMaxBundleSize = Bundle.GetBundlePoolCount ();
			mMaxEventSize = GameEvent.GetEventPoolCount ();
		}

		private void UpdateMemorySize ()
		{
			
		}

		public void Statistice (string tag)
		{
			sBuilder.Length = 0;
			sBuilder.AppendFormat ("========== Statistics Report  Time:{0} Tag:{1} ==========\n", "2017-03-07 02:26:40", tag);
			sBuilder.AppendLine ();

			sBuilder.Append ("Pool Size:\n");
			sBuilder.AppendFormat ("Bundle Pool Size:{0}\n", mMaxBundleSize);
			sBuilder.AppendFormat ("Event Pool Size:{0}\n", mMaxEventSize);
			sBuilder.AppendLine ();

			sBuilder.Append ("Memory Size:\n");
			sBuilder.AppendFormat ("Heap Memory Size:{0}\n", mHeapMemorySize);
			sBuilder.AppendFormat ("Native Memory Size:{0}\n", mNativeMemorySize);
			sBuilder.AppendLine ();

			sBuilder.AppendFormat ("========== Statistics end ==========");
			Debug.Log (sBuilder.ToString ());
		}

	}
}

