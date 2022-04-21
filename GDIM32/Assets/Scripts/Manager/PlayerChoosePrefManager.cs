using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class PlayerChoosePrefManager : MonoBehaviourPunCallbacks, IPunObservable
{
    public string playerType = "Gun";
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(transform.Find("Ready").gameObject.GetComponent<Text>().enabled);
            stream.SendNext(transform.Find("Sheild").gameObject.GetComponent<Image>().enabled);
            stream.SendNext(transform.Find("Gun").gameObject.GetComponent<Image>().enabled);
            stream.SendNext(playerType);
        }
        else
        {
            transform.Find("Ready").gameObject.GetComponent<Text>().enabled = (bool)stream.ReceiveNext();
            transform.Find("Sheild").gameObject.GetComponent<Image>().enabled = (bool)stream.ReceiveNext();
            transform.Find("Gun").gameObject.GetComponent<Image>().enabled = (bool)stream.ReceiveNext();
            playerType = (string)stream.ReceiveNext();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
