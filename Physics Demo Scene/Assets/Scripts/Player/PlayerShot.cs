using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShot : MonoBehaviour
{
    public GameObject bullet;
    public Transform shotPosition;
    public float shotRange = 100;
    public Vector3 mousePosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && Time.timeScale != 0)
        {
            Shot();
        }
    }

    public void Shot()
    {
        //Instantiate(bullet, mousePosition, Quaternion.identity);
        Instantiate(bullet, shotPosition.position, shotPosition.rotation).GetComponent<Rigidbody>().AddForce(this.transform.forward * shotRange, ForceMode.VelocityChange);
    }
}
