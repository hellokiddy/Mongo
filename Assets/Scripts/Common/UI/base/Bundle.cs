using System;
using System.Collections.Generic;

namespace Mongo.Common.UI
{
	public class Bundle
	{
		private Dictionary<string,object> mBundleArgs = null;

		public Bundle ()
		{
			mBundleArgs = new Dictionary<string, object> ();
		}

		public int getArgsCount ()
		{
			if (mBundleArgs == null) {
				return 0;
			}
			return mBundleArgs.Count;
		}

		public bool putInt (string key, int value)
		{
			return AddBundleArgs (key, value);
		}

		public int getInt (string key)
		{
			return GetBundleArgs<int> (key);
		}

		public bool putFloat (string key, float value)
		{
			return AddBundleArgs (key, value);
		}

		public float getFloat (string key)
		{
			return GetBundleArgs<float> (key);
		}

		public bool putDouble (string key, double value)
		{
			return AddBundleArgs (key, value);
		}

		public double getDouble (string key)
		{
			return GetBundleArgs<double> (key);
		}

		public bool putBool (string key, bool value)
		{
			return AddBundleArgs (key, value);
		}

		public bool getBool (string key)
		{
			return GetBundleArgs<bool> (key);
		}

		public bool putString (string key, string value)
		{
			return AddBundleArgs (key, value);
		}

		public string getString (string key)
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
			;
		}
	}
}

