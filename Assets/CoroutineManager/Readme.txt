-----------------------
Coroutine Manager v1.0:
-----------------------

Coroutine Manager is a single Script file that can be attached to an empty game object.
It is used to provide more control over coroutines in Unity3D. 
Coroutines can be started, paused, resumed, reset, looped, and stopped(destroyed).
Coroutines are managed like any regular objects and provide a set of easy to use methods that can be accessed by any script in Unity.
Developer can have unity Coroutine co-exist with coroutines managed by Coroutine Manager.
Additionnally, it is advised to use IEnumerable coroutines instead of the traditional IEnumerator coroutines for a better control(reset and loop on coroutines).







1. Available methods and usage:
----------------------------
	public static CoroutineBase StartCoroutine(IEnumerable coroutineEnumerable)
	
	Example C#:
		IEnumerable myBounce(){
			print("I bounce");
			yield return 0;
		}
		
		/.... Somewhere in your code ..../
		CoroutineManager.StartCoroutine(myBounce());

	Example JavaScript:
		function myBounce() : IEnumerable{
			print("I bounce");
			yield return 0;
		}
		
		/.... Somewhere in your code ..../
		CoroutineManager.StartCoroutine(myBounce());
		
		

	public static CoroutineBase StartCoroutine (IEnumerable coroutineEnumerable, string coroutineName)
	
	Example C#:
		IEnumerable myBounce(){
			print("I bounce");
			yield return 0;
		}
		
		/.... Somewhere in your code ..../
		CoroutineManager.StartCoroutine(myBounce());

	Example JavaScript:
		
		function myBounce() : IEnumerable{
			print("I bounce");
			yield return 0;
		}
		
		/.... Somewhere in your code ..../
		CoroutineManager.StartCoroutine(myBounce(),"my Bounce Coroutine Name");
	
	//Add a coroutine into the coroutinemanager, method returns true, then the coroutine will run
	public static CoroutineBase StartCoroutine (IEnumerable coroutineEnumerable, CoroutineRunCondition method)
	
	
		Example C#:
			
		bool runIfSomeValueIsTrue(){
			return true;
		}
			
		IEnumerable myBounce(){
			while(true){
				print("I bounce");
				yield return 0;
			}
		}
		
		/.... Somewhere in your code ..../
		CoroutineManager.StartCoroutine(myBounce(), runIfSomeValueIsTrue);

		Example JavaScript:
		function runIfSomeValueIsTrue() {
			return true;
		}
		
		function myBounce() : IEnumerable{
			while(true){
				print("I bounce");
				yield return 0;
			}
		}
		
		/.... Somewhere in your code ..../
		CoroutineManager.StartCoroutine(myBounce(), runIfSomeValueIsTrue);
	
	public static CoroutineBase StartCoroutine (IEnumerable coroutineEnumerable, string coroutineName, CoroutineRunCondition method)
	
	// Same as before but put loop as true if the coroutine should be looping.
	public static CoroutineBase StartCoroutine (IEnumerable coroutineEnumerable, string coroutineName, CoroutineRunCondition method, bool loop)
	
	Example C#:
		IEnumerable myBounce(){
			print("I bounce");
			yield return 0;
		}
		/.... Somewhere in your code ..../
		CoroutineManager.StartCoroutine(myBounce(), "myBouncingCoroutine", null, true);
	
	Example JavaScript:
		function myBounce() : IEnumerable{
			print("I bounce");
			yield return 0;
		}
		
		/.... Somewhere in your code ..../
		CoroutineManager.StartCoroutine(myBounce(), "myBouncingCoroutine", null, false);	
	
	
	public static CoroutineBase StartCoroutine(IEnumerator coroutineYield)
	

	public static CoroutineBase StartCoroutine (IEnumerator coroutineYield, string coroutineName)
	
	public static CoroutineBase StartCoroutine (IEnumerator coroutineYield, CoroutineRunCondition method)
	
	public static CoroutineBase StartCoroutine (IEnumerator coroutineYield, string coroutineName, CoroutineRunCondition method)
	
	Example C#:
		bool runIfSomeValueIsTrue(){
			return true;
		}
		IEnumerator myBounce(){
			print("I bounce");
			yield return 0;
		}
		/.... Somewhere in your code ..../
		CoroutineManager.StartCoroutine(myBounce(), "myBouncingCoroutine", runIfSomeValueIsTrue);
	
	Example JavaScript:
		function runIfSomeValueIsTrue() {
			return true;
		}
		function myBounce(){
			print("I bounce");
			yield return 0;
		}
		
		/.... Somewhere in your code ..../
		CoroutineManager.StartCoroutine(myBounce(), "myBouncingCoroutine", runIfSomeValueIsTrue);
	
	
	//Will stop and remove all the coroutines that have this name
	public static void DestroyCoroutine (string name)

	//Will stop this coroutine and remove it from the Coroutine Manager
	//An easier way is to call myCoroutine.Destroy()
	public static void DestroyCoroutine (CoroutineBase coroutine)
	
	//Will pause the coroutine
	//An easier way is to call myCoroutine.Pause()
	public static void PauseCoroutine (CoroutineBase coroutine)
	
	//Will pause all coroutines that have this name
	public static void PauseCoroutine (string name)

	//Will pause all coroutines
	public static void PauseAllCoroutines ()
	
	//Will resume all coroutines that have this name
	//An easier way is to call myCoroutine.Resume()
	public static void ResumeCoroutine (CoroutineBase coroutine)
	
	//Will resume all coroutines that have this name
	public static void ResumeCoroutine (string name)

	//Will resume all coroutines that were paused
	public static void ResumeAllCoroutines ()
	
	//Will reset the coroutine
	//An easier way is to call myCoroutine.Reset()
	//Will only work with IEnumerable coroutine. Not IEnumerator coroutine.
	public static void ResetCoroutine (CoroutineBase coroutine)

	//Will reset all coroutines that have this name
	public static void ResetCoroutine (string name)

	//Will reset all coroutines 
	public static void ResetAllCoroutines ()
	
	//Will return true if the coroutine is paused.
	public static bool isCoroutinePaused(CoroutineBase coroutine){
	
	
	//Will return true if the first found coroutine that has this name is paused.
	public static bool isCoroutinePaused(string name)
	
	//Will return true if the coroutine has finished.
	//You can use the isFinished public member instead
	public static bool isCoroutineFinished(CoroutineBase coroutine)
	
	//Will return true if the first found coroutine that has this name is finished.
	//Be careful with this. It is better to use: 
	//if (myCoroutine.isFinished){ /.../}
	public static bool isCoroutineFinished(string name)
	
	//Will return true if the coroutine is running.
	public static bool isCoroutineRunning(CoroutineBase coroutine){
	
	
	//Will return true if the first found coroutine that has this name is running.
	public static bool isCoroutineRunning(string name)
	
	//Will stop all coroutines and remove them from the Coroutine Manager
	public static void DestroyAllCoroutines ()


	
		
				
2. WaitForSeconds / WaitForEndOfFrame / WaitForFixedUpdate:
--------------------------------------------------------

	In order to make a Coroutine pause for several seconds inside the Coroutine Manager, 
	please replace "UnityEngine.WaitForSeconds(float seconds)" by "CoroutineManager.WaitForSeconds(float seconds)".
	In version 1.0, WaitForFixedUpdate and WaitForEndOfFrame are not supported (yet).
	
	
3. IEnumerable versus IEnumerator:
-------------------------------

	By declaring your coroutine code as IEnumerable instead of Ienumerator, you will have access as two more capabilities:
		-Reset an IEnumerable coroutine.
		-Loop a IEnumerable coroutine until you decide to stop(destroy) it.
	Start, stop, pause and resume are valid both for IEnumerable and IEnumerator
	
	In C#, just replace IEnumerator by IEnumerable. See above examples.
		replace: 
			IEnumerator myBounce(){	//Traditional way
		by 
			IEnumerable myBounce(){  //Recommended way
			
			
	In Javascript, do as the folowwing :
		replace:
			function myBounce(){ //Traditional way
		by 
			function myBounce() : IEnumerable{ //Recommended way
	
	
4. FAQ:
-------	
	How can I yield on a coroutine, so that my code waits for the end of the execution of coroutine
	You can find examples in the sample included in the package in C# and in Javascript.

	What are the methods available from a CoroutineBase coroutine?
	coroutine.Pause()
	coroutine.Resume()
	coroutine.Reset()
	coroutine.Destroy()
	coroutine.isRunning()

	