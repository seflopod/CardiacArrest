using UnityEngine;
using System;

/// <summary>
/// Singleton class.
/// </summary>
/// <description>
/// <para>The Singleton abstract class should be a base class for classes that you wish to have
/// exactly one instance of in the scene.  One common use for singletons is for game management
/// classes that would cause erratic behaviour if multiple instances existed.  This class is made to
/// work specifically with Unity, and inherits from MonoBehaviour so that whatever is declared a
/// Singleton can also use MonoBehaviour methods (Awake, Start, Update, etc).
/// </para>
/// </description>
abstract public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
	/// <summary>
	/// The instance of T.  This is just the internal representation of it.
	/// </summary>
	protected static T _instance;
	
	/// <summary>
	/// Implementation of MonoBehaviour method Awake
	/// </summary>
	/// <description>
	/// <para>The Awake method for this generic is a little special.  Since the pattern I'm trying to
	/// enforce is a singleton, there has to be means of making sure only one instance of a class
	/// exists.  Unity makes this a little more difficult because any MonoBehaviour attached to an 
	/// object is automatically instantiated and initialized.  The result is that both the Awake and
	/// Start methods are called (if they exist).  In order to enforce a singleton we cannot prevent
	/// the creation of a new instance, we have to destroy all but one instances of whatever is
	/// supposed to be single.  I guess I could have tried disabling the object, but this way was
	/// quicker for me.
	/// </para>
	/// <para>As always, be sure to run base.Awake() in any class that inherits from this.
	/// </para>
	/// </description>
	protected virtual void Awake()
	{
		//T dummy = MySingleton<T>.Instance;
		T[] allT = (T[]) MonoBehaviour.FindObjectsOfType(typeof(T));
		if(allT.Length > 1)
		{
			for(int i=1;i<allT.Length;++i)
				GameObject.Destroy(allT[i].gameObject);
			Debug.LogError("Only one instance of " + typeof(T) + " is allowed.  Random offending objects have been destroyed.");
		}
	}
	
	/// <summary>
	/// Returns the instance of the singleton.
	/// </summary>
	/// <description>
	/// <para>Before simply returning <see cref="_instance" />_instance this will check to see if it
	/// is null.  If it is this means that no instance of T exists, and since we cannot just create
	/// one (well, I guess we could, but that would involve adding extra GameObjects and such) this
	/// will throw an error and return the null value.
	/// </para>
	/// </description>
	public static T Instance
	{
		get
		{
			if(_instance == null)
			{
				_instance = (T) MonoBehaviour.FindObjectOfType(typeof(T));
				if(_instance == null)
				{
					Debug.LogError("An instance of " + typeof(T) + " is required.");
				}
			}
			return _instance;
		}
	}
}