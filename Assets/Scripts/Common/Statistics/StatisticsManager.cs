using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mongo.Common.Utility;
using Mongo.Common.Event;

namespace Mongo.Common.Statistics
{
	public class StatisticsManager : Singleton<StatisticsManager>
	{
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

	}
}

