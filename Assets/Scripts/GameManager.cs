using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    [field: SerializeField] public Vector3 SpawnPoint { get; protected set; }
    public PlayerController PlayerController { get; protected set; }

    #region Unity Functions
    protected override void Awake()
    {
        base.Awake();

        DontDestroyOnLoad(this);
    }
    #endregion Unity Functions
    
    #region GameManager
    public void SetPlayerController(PlayerController playerController)
    {
        if (!PlayerController && playerController)
        {
            PlayerController = playerController;

            if (SpawnPoint == Vector3.zero)
                SpawnPoint = PlayerController.transform.position;
        }
    }
    #endregion GameManager
}
