using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // No Player Alive
        if (checkPlayerAvailability())
        {
            // Game Lose
        }

        // No Enemy Alive
        /*
        if (checkEnemyAvailability())
        {
            // Game Win
        }
        */
    }

    private bool checkPlayerAvailability()
    {
        return (player1 == null && player2 == null);
    }

    /*
    private bool checkEnemyAvailability()
    {

    }
    */
}
