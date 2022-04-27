using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerManager : MonoBehaviourPun, IPunObservable
{
    private PlayerStats stats;
    private PlayerBehaviour behaviour;
    private InputManager inputManager;
    private bool enable = true;
    private bool canInteract = true;
    private Vector3 moveInput;
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
        Debug.Log(canInteract);
        Debug.Log("enable" + enable);       
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
        behaviour.Fire(inputManager.ShootInput());
    }

    private void FixedUpdate()
    {
        if (!canInteract) { return; }
        behaviour.Move(moveInput);
        behaviour.Turn();
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // We own this player: send the others our data
            stream.SendNext(stats.CurrentHP);
        }
        else
        {
            // Network player, receive data
            this.stats.CurrentHP = (float)stream.ReceiveNext();
        }
    }
}
