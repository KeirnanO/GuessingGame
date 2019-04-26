using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupMovement : MonoBehaviour
{

    public SpriteRenderer outline;   
    public Transform cupSprite;

    GameManager gm;

    bool hasBall;
    bool clickable;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    public IEnumerator Move(Vector3 position, float speed)
    {
        float t = 0;

        while (position != transform.position)
        {
            t += Time.deltaTime * speed;
            transform.position = Vector3.Lerp(transform.position, position, t);

            yield return null;
        }
    }

    //I wanted to use the move method to Move the sprite but then when the game plays, the sprites will move but the ball will stay in the same spot
    //And if i make the cups move normally the ball would move with the cup when trying to show it at the beginning of the game
    IEnumerator MoveCupSprite(Vector3 position, float speed)
    {
        float t = 0;

        while (position != cupSprite.position)
        {
            t += Time.deltaTime * speed;
            cupSprite.position = Vector3.Lerp(cupSprite.position, position, t);

            yield return null;
        }
    }

    public bool HasBall()
    {
        return hasBall;
    }

    public void SetClickable(bool isClickable)
    {
        clickable = isClickable;
    }

    public GameObject GiveBall(GameObject ball)
    {
        GameObject newBall = Instantiate(ball, transform);
        newBall.transform.localPosition += new Vector3(0, 0f, 0.3f);
        hasBall = true;

        return newBall;
    }

    public IEnumerator Show()
    {
        //If is clickable - can no longer click 
        clickable = false;

        //Get original position
        Vector3 originalPos = cupSprite.position;

        //Move Cup up
        StartCoroutine(MoveCupSprite(cupSprite.position + Vector3.up * 2, 1));

        //Wait 2 seconds
        yield return new WaitForSeconds(2f);

        //Move Cup back down
        StartCoroutine(MoveCupSprite(originalPos, 1));
    }

    void OnMouseEnter()
    {
        if (clickable)
        {
            outline.enabled = true;
        }
    }
    void OnMouseExit()
    {
        outline.enabled = false;
    }
    private void OnMouseDown()
    {
        if (clickable)
        {
            StartCoroutine(Show());

            if (hasBall)
            {
                gm.Win();
            }
            else
            {
                gm.Lose();
            }
        }
    }
}
