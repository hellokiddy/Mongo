using System;
using System.Collections;
using System.Collections.Generic;
using Mongo.Common.Utility;

namespace Mongo.Common.Event
{
	public delegate void EventDelegate (IEvent eve);

	public class EventDispatcher : Singleton<EventDispatcher>
	{
		private List<IEvent> mEventCache = null;
		private List<IEvent> mProcessEvents = null;
		private Dictionary<int,List<EventDelegate>> mEventFuncDict = null;

		public override void Init ()
		{
			mEventCache = new List<IEvent> ();
			mProcessEvents = new List<IEvent> ();
			mEventFuncDict = new Dictionary<int, List<EventDelegate>> ();
		}

		public override void Update ()
		{
			base.Update ();
			if (mProcessEvents == null || mProcessEvents.Count < 1) {
				return;
			}
			float ftime = UnityEngine.Time.time;
			for (int i = 0; i < mProcessEvents.Count;) {
				IEvent eve = mProcessEvents [i];
				if (eve == null) {
					mProcessEvents.RemoveAt (i);
					continue;
				}
				if (ftime < eve.timeLock) {
					i++;
					continue;
				}
				ActionEvent (eve);
				mProcessEvents.RemoveAt (i);
			}

		}

		public IEvent PushEvent (int eventId, bool immediately = false)
		{
			IEvent eve = GetEventFromCache (eventId);
			if (immediately) {
				ActionEvent (eve);
			}
			mProcessEvents.Add (eve);
			return eve;
		}

		public void RegisterEvent (int eventId, EventDelegate func)
		{
			if (func == null) {
				return;
			}

			List<EventDelegate> funcList = GetFuncList (eventId);
			if (!funcList.Contains (func)) {
				funcList.Add (func);
			}
		}

		public void UnRegisterEvent (int eventId, EventDelegate func)
		{
			if (func == null) {
				return;
			}

			List<EventDelegate> funcList;
			if (!mEventFuncDict.TryGetValue (eventId, out funcList)) {
				return;
			}

			if (funcList == null) {
				return;
			}
			if (funcList.Contains (func)) {
				funcList.Remove (func);
			}
		}


		private void ActionEvent (IEvent eve)
		{
			if (eve == null) {
				return;
			}
			List<EventDelegate> funcList = GetFuncList (eve.eventId);
			for (int i = 0; i < funcList.Count; i++) {
				EventDelegate func = funcList [i];
				if (func == null) {
					continue;
				}
				try {
					func (eve);
				} catch (Exception ex) {
					UnityEngine.Debug.LogError (string.Format ("{0} ----> {1} ---->{2} ----->{3}", eve.eventId, func.Target,
						func.Method != null ? func.Method.Name : "NULL", ex.ToString ()));
				}
			}
			AddEvent2Cache (eve);
		}

		private void AddEvent2Cache (IEvent eve)
		{
			if (eve == null) {
				return;
			}
			eve.Reset ();
			if (mEventCache.Contains (eve)) {
				mEventCache.Add (eve);
			}
		}

		private IEvent GetEventFromCache (int eventId)
		{
			IEvent eve = null;
			if (mEventCache.Count > 0) {
				eve = mEventCache [0];
				mEventCache.RemoveAt (0);
			} else {
				eve = new IEvent ();
			}
			eve.eventId = eventId;
			return eve;
		}

		private List<EventDelegate> GetFuncList (int eventId)
		{
			List<EventDelegate> funcList = null;
			if (!mEventFuncDict.TryGetValue (eventId, out funcList)) {
				funcList = new List<EventDelegate> ();
				mEventFuncDict.Add (eventId, funcList);
			}
			return funcList;
		}
	}
}

