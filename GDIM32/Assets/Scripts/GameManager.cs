using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


// Author: Jiefu Ling (jieful2)
// This script is used to control game status. 
// Check win or lose status.


public class GameManager : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;

    public GameObject[] enemys;


    // Update is called once per frame
    void Update()
    {
        // No Player Alive
        if (checkPlayerAvailability())
        {
            // Game Lose
            SceneManager.LoadScene("endLose");
        }

        // No Enemy Alive
        if (checkEnemyAvailability())
        {
            // Game Win
        }
        
    }

    private bool checkPlayerAvailability()
    {
        return (player1 == null || player2 == null);
    }

    private void OnTriggerEnter2D (Collider2D collison)
    {
        if (collison.tag == "Player1" || collison.tag == "Player2")
        {
            SceneManager.LoadScene("endWin");
        }
    }

    private bool checkEnemyAvailability()
    {
        bool NoManAlive = true;

        for (int i = 0; i < enemys.Length; i++)
        {

            //If someone one is alive
            if (enemys[i] != null)
            {
                NoManAlive = false;
            }
        }

        return NoManAlive;
    }

}
