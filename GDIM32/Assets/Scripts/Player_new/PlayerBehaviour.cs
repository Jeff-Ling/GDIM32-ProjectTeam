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
    public void Turn()
    {
        //float turn = - (m_TurnInputValue * m_TurnSpeed * Time.deltaTime);
        //transform.Rotate(Vector3.forward * turn);
        Vector3 mousePoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));
        Vector3 dir = (mousePoint - transform.position);
        dir = new Vector3(dir.x, dir.y, 0f).normalized;
        float theta = Mathf.Atan(dir.y / dir.x) * Mathf.Rad2Deg;
        theta = dir.x < 0f ? 180f + theta : theta;
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, theta);
    }
    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponent<PlayerStats>();
    }
}
