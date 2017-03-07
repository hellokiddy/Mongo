using System.Collections;
using System.Collections.Generic;
using Mongo.Common.Utility;

namespace Mongo.Common.Event
{
	public class GameEvent : IEvent
	{
		/// <summary>
		/// the event id.
		/// </summary>
		protected int mEventID = 0;

		public int EventID {
			get {
				return mEventID;
			}
			set {
				mEventID = value;
			}
		}

		/// <summary>
		/// the delay time of event.
		/// </summary>
		protected float mDelay = 0f;

		public float Delay {
			get {
				return mDelay;
			}
			set {
				mDelay = value;
			}
		}

		/// <summary>
		/// the args of event.
		/// </summary>
		protected Bundle mEventArgs = null;

		public Bundle EventArgs {
			get {
				return mEventArgs;
			}
			set {
				mEventArgs = value;
			}
		}


		/// <summary>
		/// Reset this instance.
		/// </summary>
		public virtual void Reset ()
		{
			mEventID = 0;
			mDelay = 0f;
			mEventArgs = null;
		}

		#region Static

		private static ObjectPool<GameEvent> sEventPool = new ObjectPool<GameEvent> (400, 20);

		public static int GetEventPoolCount ()
		{
			return sEventPool.Length;
		}

		public static GameEvent Allocate ()
		{
			//grab the next available object.
			GameEvent instance = sEventPool.Allocate ();
			if (instance == null) {
				instance = new GameEvent ();
			}
			return instance;
		}

		public static void Release (GameEvent instance)
		{
			if (instance == null) {
				return;
			}

			//reset it.
			instance.Reset ();

			//make it available to others.
			sEventPool.Release (instance);
		}

		public static void Release (IEvent instance)
		{
			if (instance == null) {
				return;
			}

			instance.Reset ();
			if (instance is GameEvent) {
				sEventPool.Release (instance as GameEvent);
			}
		}

		#endregion
	}
}

