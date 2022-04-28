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
    private bool canInteract = true;
    private Vector3 moveInput;

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
        if (!photonView.IsMine && PhotonNetwork.IsConnected) 
        {
            canInteract = false;
            return; 
        }
        if (stats.IsDead) 
        {
            canInteract = false;
            return; 
        }
        if (!enable) 
        {
            canInteract = false;
            return; 
        }
        moveInput = inputManager.MoveInput();
        canInteract = true;
        //this.GetComponent<PhotonView>().RPC("Fire", RpcTarget.All,inputManager.ShootInput());
        behaviour.Fire(inputManager.ShootInput());
    }

    private void FixedUpdate()
    {
        if (!canInteract) { return; }
        behaviour.Move(moveInput);
        behaviour.Turn();
    }

    [PunRPC] 
    public void SetActive(bool active)
    {
        Debug.Log(1);
        enable = active;
    }
}
