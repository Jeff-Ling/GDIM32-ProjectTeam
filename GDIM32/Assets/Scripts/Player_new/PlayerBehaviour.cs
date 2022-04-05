using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    private PlayerStats stats;
    public void Move(Vector3 input)
    {
        this.transform.position += input * stats.MoveSpeed * Time.deltaTime;
    }
    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponent<PlayerStats>();
    }
}
