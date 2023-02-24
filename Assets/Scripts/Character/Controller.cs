using UnityEngine;

public class Controller : MonoBehaviour
{
    #region Variables
    [field: SerializeField] public Character Character { get; private set; }
    #endregion Variables
    
    #region Unity Functions
    protected virtual void Awake()
    {
        if (!Character)
        {
            Character = GetComponent<Character>();

            if (!Character)
            {
                Debug.LogError("Controller is missing Character.");

                enabled = false;
            }
        }
    }
    #endregion Unity Functions
}
