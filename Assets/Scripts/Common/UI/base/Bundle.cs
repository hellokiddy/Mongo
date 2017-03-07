using System;
using System.Collections.Generic;

namespace Mongo.Common.Utility
{
	public class Bundle
	{
		private Dictionary<string,object> mBundleArgs = null;

		public Bundle ()
		{
			mBundleArgs = new Dictionary<string, object> ();
		}

		public void Reset ()
		{
			if (mBundleArgs != null) {
				mBundleArgs.Clear ();
			}
		}

		public int GetArgsCount ()
		{
			if (mBundleArgs == null) {
				return 0;
			}
			return mBundleArgs.Count;
		}

		public bool PutInt (string key, int value)
		{
			return AddBundleArgs (key, value);
		}

		public int GetInt (string key)
		{
			return GetBundleArgs<int> (key);
		}

		public bool PutFloat (string key, float value)
		{
			return AddBundleArgs (key, value);
		}

		public float GetFloat (string key)
		{
			return GetBundleArgs<float> (key);
		}

		public bool PutDouble (string key, double value)
		{
			return AddBundleArgs (key, value);
		}

		public double GetDouble (string key)
		{
			return GetBundleArgs<double> (key);
		}

		public bool PutBool (string key, bool value)
		{
			return AddBundleArgs (key, value);
		}

		public bool GetBool (string key)
		{
			return GetBundleArgs<bool> (key);
		}

		public bool PutString (string key, string value)
		{
			return AddBundleArgs (key, value);
		}

		public string GetString (string key)
		{
			return GetBundleArgs<string> (key);
		}

		public bool AddBundleArgs (string key, object value)
		{
			if (mBundleArgs.ContainsKey (key)) {
				return false;
			}
			mBundleArgs.Add (key, value);
			return true;
		}

		public T GetBundleArgs<T> (string key)
		{
			object value;
			if (mBundleArgs.TryGetValue (key, out value)) {
				mBundleArgs.Remove (key);
				return (T)value;
			}
			return default(T);
		}

		#region Bundle Pool

		private static ObjectPool<Bundle> sBundlePool = new ObjectPool<Bundle> (20, 10);

		public static int GetBundlePoolCount ()
		{
			return sBundlePool.Length;
		}

		public static Bundle Allocate ()
		{
			//grab the next available object.
			Bundle instance = sBundlePool.Allocate ();
			if (instance == null) {
				instance = new Bundle ();
			}
			return instance;
		}

		public static void Release (Bundle instance)
		{
			if (instance == null) {
				return;
			}

			//reset it.
			instance.Reset ();

			//make it available to others.
			sBundlePool.Release (instance);
		}

		#endregion
	}
}

