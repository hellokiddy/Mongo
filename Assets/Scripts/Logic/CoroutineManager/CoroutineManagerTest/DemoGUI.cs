using UnityEngine;
using System.Collections;

public class DemoGUI : MonoBehaviour
{
	
	public Texture2D imagePause;
	public Texture2D imageStop;
	public Texture2D imageReset;
	public Texture2D imagePlay;
	
	public GUISkin guiSkin;

	void OnGUI ()
	{
		GUI.skin = guiSkin;
		
		//Top coroutine UI buttons code
		//**************************
		// Coroutine that can be reset (and looped): It is an IEnumerable instead of IEnumerator
		//**************************
		if (resettableCoroutine != null) {
			if (resettableCoroutine.isRunning ()) {
				if (!resettableCoroutine.isPaused) {
					if (GUI.Button (new Rect (Screen.width / 12, Screen.height / 12, 64, 64), imagePause)) {
						CoroutineManager.PauseCoroutine (resettableCoroutine);
						//or you could write:
						//resettableCoroutine.Pause();
					}
				} else {
					if (GUI.Button (new Rect (Screen.width / 12, Screen.height / 12, 64, 64), imagePlay)) {
						CoroutineManager.ResumeCoroutine (resettableCoroutine);
						//or you could write:
						//resettableCoroutine.Resume();
					}
					
				}
			}
			if (resettableCoroutine.coroutineIEnumerable != null) { //This coroutine is an IEnumerable instead of a IEnumerator: It can be reset
				if (GUI.Button (new Rect (Screen.width / 12, Screen.height * 3 / 12, 64, 64), imageReset)) {
					resettableCoroutine.Reset ();
					//or you could write:
					//CoroutineManager.ResetCoroutine(resettableCoroutine);
				}
				
			}
			
		}
		//**************************
		//Normal coroutine (IEnumerator): It cannot be reset or looped by the Coroutine Manager.
		//**************************
		if (normalCoroutine != null) {
			if (normalCoroutine.isRunning ()) {
				if (!normalCoroutine.isPaused) {
					if (GUI.Button (new Rect (Screen.width / 12, Screen.height * 8 / 12, 64, 64), imagePause)) {
						CoroutineManager.PauseCoroutine (normalCoroutine);
						//or you could write:
						//normalCoroutine.Pause();
					}
				} else {
					if (GUI.Button (new Rect (Screen.width / 12, Screen.height * 8 / 12, 64, 64), imagePlay)) {
						CoroutineManager.ResumeCoroutine (normalCoroutine);
						//or you could write:
						//normalCoroutine.Resume();
					}
					
				}
			}
			
		}
		
		
		
		

	}

	IEnumerator bounce (GameObject go)
	{
		int i = 0;
		while (true) {
			Vector3 pos = go.transform.position;
			pos.x = Mathf.PingPong ((++i) / 50f, 5.0f);
			go.transform.position = pos;
			yield return 0;
		}
	}

	
	IEnumerable resettableBounce (GameObject go)
	{
		int i = 0;
		while (true) {
			Vector3 pos = go.transform.position;
			pos.x = Mathf.PingPong (++i / 50f, 5.0f);
			go.transform.position = pos;
			yield return 0;//new CoroutineManager.WaitForSeconds(0.01f);
		}
	}

	
	public bool someValue = true;

	bool runIfSomeValueIsTrue ()
	{ //Use this if you need to prevent a coroutine from running based on external logic
		return someValue;
	}

	public bool someOtherValue = true;

	bool runIfSomeOtherValueIsTrue ()
	{ //Use this if you need to prevent a coroutine from running based on external logic
		return someOtherValue;
	}

	CoroutineBase resettableCoroutine;
	CoroutineBase normalCoroutine;
	
			
	// Use this for initialization
	void Start ()
	{
		GameObject goSphere = GameObject.Find ("Sphere");
		resettableCoroutine = CoroutineManager.StartCoroutine (resettableBounce (goSphere), runIfSomeValueIsTrue, false);

	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
