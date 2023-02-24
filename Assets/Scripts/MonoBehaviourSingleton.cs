using UnityEngine;

public abstract class MonoBehaviourSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
	#region Runtime variables
	public static T Instance { get; protected set; }
	#endregion

	#region MonoBehaviour
	protected virtual void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = GetComponent<T>();
    }
	#endregion
}