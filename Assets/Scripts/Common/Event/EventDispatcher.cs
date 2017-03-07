using System;
using System.Collections;
using System.Collections.Generic;
using Mongo.Common.Utility;

namespace Mongo.Common.Event
{
	public delegate void EventHandler (IEvent eve);

	public class EventDispatcher : Singleton<EventDispatcher>
	{
		#region Var

		private List<IEvent> mEvents = null;
		private Dictionary<int,List<EventHandler>> mEventHandlers = null;

		#endregion

		#region Override

		public override void Init ()
		{
			mEvents = new List<IEvent> ();
			mEventHandlers = new Dictionary<int, List<EventHandler>> ();
		}

		public override void OnUpdate (float deltaTime)
		{
			base.OnUpdate (deltaTime);
			if (mEvents == null || mEvents.Count < 1) {
				return;
			}

			for (int i = 0; i < mEvents.Count;) {
				IEvent eve = mEvents [i];
				if (eve == null) {
					mEvents.RemoveAt (i);
					continue;
				}
				eve.Delay -= deltaTime;
				if (eve.Delay > 0) {
					i++;
					continue;
				}
				ActionEvent (eve);
				mEvents.RemoveAt (i);
			}

		}

		#endregion

		#region Interface

		public void PushEvent (int eventId)
		{
			PushEvent (eventId, true, 0f, null);
		}

		public void PushEvent (int eventId, bool immediately)
		{
			PushEvent (eventId, immediately, 0f, null);
		}

		public void PushEvent (int eventId, bool immediately, float delay)
		{
			PushEvent (eventId, immediately, delay, null);
		}

		public void PushEvent (int eventId, bool immediately, float delay, Bundle args)
		{
			IEvent eve = GameEvent.Allocate ();
			eve.EventID = eventId;
			eve.EventArgs = args;
			eve.Delay = delay;

			if (immediately) {
				ActionEvent (eve);
			} else {
				mEvents.Add (eve);
			}
		}

		public void AddListener (int eventId, EventHandler func)
		{
			if (func == null) {
				return;
			}

			List<EventHandler> funcList = GetFuncList (eventId);
			if (!funcList.Contains (func)) {
				funcList.Add (func);
			}
		}

		public void RemoveListener (int eventId, EventHandler func)
		{
			if (func == null) {
				return;
			}

			List<EventHandler> funcList;
			if (!mEventHandlers.TryGetValue (eventId, out funcList)) {
				return;
			}

			if (funcList == null) {
				return;
			}
			if (funcList.Contains (func)) {
				funcList.Remove (func);
			}
		}

		#endregion

		#region Internal

		private void ActionEvent (IEvent eve)
		{
			if (eve == null) {
				return;
			}

			List<EventHandler> funcList = GetFuncList (eve.EventID);
			for (int i = 0; i < funcList.Count; i++) {
				EventHandler func = funcList [i];
				if (func == null) {
					continue;
				}
				try {
					func (eve);
				} catch (Exception ex) {
					UnityEngine.Debug.LogError (string.Format ("{0} ----> {1} ---->{2} ----->{3}", eve.EventID, func.Target,
						func.Method != null ? func.Method.Name : "NULL", ex.ToString ()));
				}
			}
			GameEvent.Release (eve);
		}

		private List<EventHandler> GetFuncList (int eventId)
		{
			List<EventHandler> funcList = null;
			if (!mEventHandlers.TryGetValue (eventId, out funcList)) {
				funcList = new List<EventHandler> ();
				mEventHandlers.Add (eventId, funcList);
			}
			return funcList;
		}

		#endregion
	}
}

