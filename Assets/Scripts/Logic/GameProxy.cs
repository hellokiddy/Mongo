using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mongo.Common.Utility;

public class GameProxy : MonoBehaviour
{
	private List<BaseManager> mAllManager = new List<BaseManager> ();

	// Use this for initialization
	void Start ()
	{
		mAllManager.Add (TestManager.GetInstance ());
		foreach (BaseManager mgr in mAllManager) {
			mgr.Init ();
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
