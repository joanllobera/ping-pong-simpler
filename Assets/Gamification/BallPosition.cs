using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPosition : MonoBehaviour {

    private BallController ballController;

    private float ballPos;
    private float preBallPos;

    public GameObject field1;
    public GameObject field2;
    public GameObject floor;

    private bool firstBot;

    private bool lastPlayerTouchingBall;           //True = Is from player1    False = Is from player2

    private float puntuationP1;
    private float puntuationP2;
    public float sumPuntuationXPoint = 1.0f;
    // Use this for initialization
    void Start () {
        ballController = GameObject.Find(Constants.Ball).GetComponent<BallController>();

        ballPos = transform.position.z;
        lastPlayerTouchingBall = true;

        puntuationP1 = 0.0f;
        puntuationP2 = 0.0f;

        firstBot = false;
    }
	
	// Update is called once per frame
	void Update () {
        ballPos = transform.position.z;
        if (ballPos >= preBallPos)              //Si la pelota va dirección a J1 --> J2
        {
            if (!lastPlayerTouchingBall)        //Si hay un cambio de dirección, hay que reiniciar los datos       
            {
                DirectionBallChanged();
                lastPlayerTouchingBall = true;
            }
        }
        else                                    //Si la pelota va dirección a J2 --> J1
        {
            if (lastPlayerTouchingBall)         //Si cambia de dirección 
            {
                DirectionBallChanged();         //Se reinician los botes 
                lastPlayerTouchingBall = false; 
            }
        }

        preBallPos = ballPos;                   //Actualización de la pelota para el proximo frame

    }

    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.name == "Box1")           //Campo Player 1
        {
            Debug.Log("Collision with field1");
            if (lastPlayerTouchingBall)             //Si toca el campo 1 y el ultimo en tocar la pelota ha sido J1
            {
                puntuationP2 += 1.0f;               //Tocar el propio campo = Punto para el adversario 
                ResetBall();
            }
            else                                    //Si toca el campo 2 y el ultimo en tocar la pelota es J2 
            {

                //Segundo bote 
                if (firstBot)
                {
                    puntuationP2 += 1.0f;           //Tocar 2 veces en campo rival = Punto para el adversario 
                    ResetBall();

                }
                else
                {
                    firstBot = true;               //Si la pelota vuelve a tocar el mismo campo, será segundo bote 
                }


            }
            

        }

        if (col.gameObject.name == "Box2")          //Campo Player 1
        {
            Debug.Log("Collision with field2");
            if (lastPlayerTouchingBall)
            {
                puntuationP1 += 1.0f;               //Tocar el propio campo = Punto para el adversario
                ResetBall();
            }
            else
            {
                if (firstBot)
                {
                    puntuationP2 += 1.0f;           //Tocar 2 veces en campo rival = Punto para el adversario 
                    ResetBall();
                }
                else
                {
                    firstBot = true;               //
                }
            }
        }

        if(col.gameObject.name == "LimitFloor")
        {
            Debug.Log("Floor Collision!!");
            if (lastPlayerTouchingBall)             //El ultimo jugador en tocar la bola fue P1
            {
                if (firstBot)                       //Si la bola ya ha botado
                {
                    puntuationP1 += 1.0f;
                    ResetBall();
                }
                else                                //Si la bola aún no ha botado 
                {
                    puntuationP2 += 1.0f;
                    ResetBall();
                }
            }else                                   //El ultimo jugador en tocar la bola fue P2 
            {
                if (firstBot)                       //Si la bola ya ha botado
                {
                    puntuationP2 += 1.0f;
                    ResetBall();
                }
                else                                //Si la bola aún no ha botado 
                {
                    puntuationP1 += 1.0f;
                    ResetBall();
                }

            }
        }
    }

    private void ResetBall()
    {
        DirectionBallChanged();
        ballController.serve = true;
    }


    private void DirectionBallChanged()
    {
        firstBot = false;
        
    }
}
