using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupManager : MonoBehaviour
{
    //0 = Left, 1 = Middle, 2 = Right
    public CupMovement[] cups;    
    public Vector3[] positions;
    public GameObject ball;
    public GameObject cupObject;

    GameObject thisRoundsBall;
    

    float speed;
    int moves;

    public IEnumerator StartGame(int moves, float speed)
    {
        this.moves = moves;
        this.speed = speed;

        ResetGame();

        //Make Random starting Cup
        CupMovement startingCup = cups[GetRandomCup()];
        thisRoundsBall = startingCup.GiveBall(ball);

        //Show which cup has the ball
        StartCoroutine(startingCup.Show());

        //Wait a few seconds
        yield return new WaitForSeconds(3f);

        //Start moving cups
        MakeRandomMove();
    }

    IEnumerator MakeMove(int cup1, int cup2)
    {
        moves--;

        //Move cup1 to cup2 position
        StartCoroutine(cups[cup1].Move(positions[cup2], speed));
        //Move cup2 to cup1 position
        StartCoroutine(cups[cup2].Move(positions[cup1], speed));

        //Temporary variabe for swapping 2 variables' data
        var tempCup = cups[cup1];

        cups[cup1] = cups[cup2];
        cups[cup2] = tempCup;

        //Buffer time between moves
        yield return new WaitForSeconds(0.8f / speed);

        if(moves > 0)
        {
            MakeRandomMove();
        }
        else
        {
            MakeCupsClickable();
        }

    }

    public void EasyGame()
    {
        StopAllCoroutines();
        StopAllCoroutines();
        ResetGame();
        StartCoroutine(StartGame(5, 1));
    }

    public void MediumGame()
    {
        StopAllCoroutines();
        StartCoroutine(StartGame(8, 3));
    }

    public void HardGame()
    {
        StopAllCoroutines();
        StartCoroutine(StartGame(12, 5));
    }

    public void GodLike()
    {
        StopAllCoroutines();
        StartCoroutine(StartGame(99, 6));
    }

    void MakeRandomMove()
    {
        int cup1, cup2;

        //Get cup1
        cup1 = GetRandomCup();

        //Get Cup2 and change it if its the same cup as 1
        do
        {
            cup2 = GetRandomCup();
        }
        while (cup2 == cup1);

        StartCoroutine(MakeMove(cup1, cup2));
    }

    void ResetGame()
    {
        for (int i = 0; i < cups.Length; i++)
        {
            Destroy(cups[i].gameObject);
            cups[i] = Instantiate(cupObject, positions[i], Quaternion.identity).GetComponent<CupMovement>();
        }

        Destroy(thisRoundsBall);

    }

    public void EndGame()
    {
        MakeCupsUnClickable();
    }
    
    public void MakeCupsClickable()
    {
        foreach (CupMovement cup in cups)
        {
            cup.SetClickable(true);
        }
    }

    public void MakeCupsUnClickable()
    {
        foreach (CupMovement cup in cups)
        {
            cup.SetClickable(false);
        }
    }

    int GetRandomCup()
    {
        float chance = Random.Range(0, 100);
        if (chance > 66) // Left Cup
        {
            return 0;
        }
        else if (chance > 33) // Middle Cup
        {
            return 1;
        }
        else //Right Cup
        {
            return 2;
        }
    }

}
