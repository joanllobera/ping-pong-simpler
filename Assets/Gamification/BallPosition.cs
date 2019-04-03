using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPosition : MonoBehaviour {


    private float ballPos;
    private float preBallPos;

    public GameObject field1;
    public GameObject field2;

    private bool firstBot;
    private bool secondBot; 

    private bool lastPlayerTouchingBall;           //True = Is from player1    False = Is from player2

    private float puntuationP1;
    private float puntuationP2;
    public float sumPuntuationXPoint = 1.0f;
    // Use this for initialization
    void Start () {
        ballPos = transform.position.z;
        lastPlayerTouchingBall = true;

        puntuationP1 = 0.0f;
        puntuationP2 = 0.0f;

    }
	
	// Update is called once per frame
	void Update () {
        ballPos = transform.position.z;
        if (ballPos >= preBallPos)
        {
            lastPlayerTouchingBall = true;
        }
        else
        {
            lastPlayerTouchingBall = false;
        }

        preBallPos = ballPos;

    }

    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.name == "Box1")
        {
            Debug.Log("Collision with field1");
            if (lastPlayerTouchingBall)
            {
                puntuationP2 += 1.0f;
            }
            else
            {

            }
            
        }

        if (col.gameObject.name == "Box2")
        {
            Debug.Log("Collision with field2");
            if (lastPlayerTouchingBall)
            {
                puntuationP1 += 1.0f;
            }
            else
            {

            }
        }
    }
}
