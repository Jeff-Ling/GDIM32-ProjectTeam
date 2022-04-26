using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerManager : MonoBehaviourPun
{
    private PlayerStats stats;
    private PlayerBehaviour behaviour;
    private InputManager inputManager;
    private bool enable = true;
    public bool Enable
    {
        get { return enable; }
        set { enable = value; }
    }
    void Start()
    {
        OnlineCameraControl cameraControl = this.gameObject.GetComponent<OnlineCameraControl>();


        if (cameraControl != null)
        {
            if (photonView.IsMine)
            {
                cameraControl.OnStartFollowing();
            }
        }
        else
        {
            Debug.LogError("<Color=Red><a>Missing</a></Color> CameraWork Component on playerPrefab.", this);
        }

        stats = GetComponent<PlayerStats>();
        behaviour = GetComponent<PlayerBehaviour>();
        inputManager = InputManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if(!photonView.IsMine && PhotonNetwork.IsConnected) { return; }
        if (stats.IsDead) { return; }
        if (!enable) { return; }
        behaviour.Move(inputManager.MoveInput());
        behaviour.Turn();
    }
}
