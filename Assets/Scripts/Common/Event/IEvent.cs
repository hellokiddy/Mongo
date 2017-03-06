using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mongo.Common.UI;

namespace Mongo.Common.Event
{
	public class IEvent
	{
		private int mEventId = -1;

		public int eventId {
			get {
				return mEventId;
			}
			set {
				if (mEventId != value) {
					mEventId = value;
				}
			}
		}

		private float mTimeLock = 0;

		public float timeLock {
			get {
				return mTimeLock;
			}
			set {
				mTimeLock = value;
			}
		}

		private Bundle mEventArgs = null;

		public IEvent ()
		{
			mEventId = -1;
			mEventArgs = new Bundle ();
			mTimeLock = 0;
		}

		public void Reset ()
		{
			mEventId = -1;
			mTimeLock = 0;
			if (mEventArgs != null) {
				mEventArgs.Reset ();
			}
		}

		public bool AddParameter<T> (string key, T value)
		{
			bool result = false;
			if (mEventArgs != null) {
				result = mEventArgs.AddBundleArgs (key, value);
			}
			return result;
		}

		public T GetParameter<T> (string key)
		{
			T value = default(T);
			if (mEventArgs != null) {
				value = mEventArgs.GetBundleArgs<T> (key);
			}
			return value;
		}

	}
}


