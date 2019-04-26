using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CupManager cManager;
    
    public void Win()
    {
        print("Win");

        EndGame();
    }

    public void Lose()
    {
        print("Lose");

        foreach(CupMovement cup in cManager.cups)
        {
            if(cup.HasBall())
            {
                StartCoroutine(cup.Show());
            }
        }

        EndGame();
    }


    void EndGame()
    {
        cManager.EndGame();
    }

}
