using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlineCameraControl : MonoBehaviour
{
    #region Private Fields


    private Vector3 offset = new Vector3(-20, 20, -12);
    private float moveSpeed = 3;
    [Tooltip("Set this as false if a component of a prefab being instanciated by Photon Network, and manually call OnStartFollowing() when and if needed.")]
    [SerializeField] private bool followOnStart = false;


    // cached transform of the target
    Transform cameraTransform;


    // maintain a flag internally to reconnect if target is lost or camera is switched
    bool isFollowing;



    #endregion


    #region MonoBehaviour Callbacks


    /// <summary>
    /// MonoBehaviour method called on GameObject by Unity during initialization phase
    /// </summary>
    void Start()
    {
        // Start following the target if wanted.
        if (followOnStart)
        {
            OnStartFollowing();
        }
    }


    void LateUpdate()
    {
        // The transform target may not destroy on level load,
        // so we need to cover corner cases where the Main Camera is different everytime we load a new scene, and reconnect when that happens
        if (cameraTransform == null && isFollowing)
        {
            OnStartFollowing();
        }


        // only follow is explicitly declared
        if (isFollowing)
        {
            Follow();
        }
    }


    #endregion


    #region Public Methods


    /// <summary>
    /// Raises the start following event.
    /// Use this when you don't know at the time of editing what to follow, typically instances managed by the photon network.
    /// </summary>
    public void OnStartFollowing()
    {
        cameraTransform = Camera.main.transform;
        isFollowing = true;
        // we don't smooth anything, we go straight to the right camera shot
        Cut();
    }


    #endregion


    #region Private Methods


    /// <summary>
    /// Follow the target smoothly
    /// </summary>
    void Follow()
    {
        if (Vector3.Distance(cameraTransform.position, this.transform.position + offset) > 0.5)
            cameraTransform.position += (this.transform.position + offset - cameraTransform.position) * moveSpeed * Time.deltaTime;
    }


    void Cut()
    {
        cameraTransform.position = this.transform.position + offset;
    }
    #endregion
}
