using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPosition : MonoBehaviour {

    private BallController ballController;

    public ScorePanel panel;                        //Instanciar el panel

    private float ballPos;
    private float preBallPos;

    private bool colBox1;
    private bool colBox2;

    private bool firstBot;

    private bool lastPlayerTouchingBall;           //True = Is from player1    False = Is from player2

    private float puntuationP1;
    private float puntuationP2;
    public float sumPuntuationXPoint = 1.0f;

    private bool IsColliding;
    // Use this for initialization
    void Start () {
        ballController = GameObject.Find(Constants.Ball).GetComponent<BallController>();

        ballPos = transform.position.z;
        lastPlayerTouchingBall = true;

        puntuationP1 = 0.0f;
        puntuationP2 = 0.0f;

        firstBot = false;

        colBox1 = false;
        colBox2 = false;


        panel.ChangePuntuation(puntuationP1, puntuationP2); 
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
            if (!colBox1)
            {
                colBox1 = true;
                if (!IsColliding)
                {
                    //Debug.Log("Collision with field1");
                    if (lastPlayerTouchingBall)             //Si toca el campo 1 y el ultimo en tocar la pelota ha sido J1
                    {
                        puntuationP2 += sumPuntuationXPoint;               //Tocar el propio campo = Punto para el adversario 
                        panel.ChangePuntuation(puntuationP1, puntuationP2);      //Actualizar puntuaciones en el HUD
                        ResetBall();
                    }
                    else                                    //Si toca el campo 2 y el ultimo en tocar la pelota es J2 
                    {

                        //Segundo bote 
                        if (firstBot)
                        {
                            puntuationP2 += sumPuntuationXPoint;           //Tocar 2 veces en campo rival = Punto para el adversario
                            panel.ChangePuntuation(puntuationP1, puntuationP2);  //Act punt
                            ResetBall();

                        }
                        else
                        {
                            firstBot = true;               //Si la pelota vuelve a tocar el mismo campo, será segundo bote 
                        }


                    }
                    IsColliding = true;
                }
            }

        }
        else
        {
            colBox1 = false;
        }

        if (col.gameObject.name == "Box2")          //Campo Player 1
        {
            if (!colBox2)
            {
               colBox2 = true;

                if (!IsColliding)
                {
                    //Debug.Log("Collision with field2");
                    if (lastPlayerTouchingBall)
                    {
                        puntuationP1 += sumPuntuationXPoint;               //Tocar el propio campo = Punto para el adversario
                        panel.ChangePuntuation(puntuationP1, puntuationP2);          //Act punt
                        ResetBall();
                    }
                    else
                    {
                        if (firstBot)
                        {
                            puntuationP2 += sumPuntuationXPoint;           //Tocar 2 veces en campo rival = Punto para el adversario 
                            panel.ChangePuntuation(puntuationP1, puntuationP2);    //Act punt
                            ResetBall();
                        }
                        else
                        {
                            firstBot = true;               //
                        }
                    }
                    IsColliding = true;
                }
            }
        }
        else
        {
            colBox2 = false;
        }

        if(col.gameObject.name == "LimitFloor")
        {
            if (!IsColliding)
            {
                Debug.Log("Floor Collision!!");
                if (lastPlayerTouchingBall)             //El ultimo jugador en tocar la bola fue P1
                {

                    if (firstBot)                       //Si la bola ya ha botado
                    {
                        puntuationP1 += sumPuntuationXPoint;
                        panel.ChangePuntuation(puntuationP1, puntuationP2);  //Act punt
                        ResetBall();
                    }
                    else                                //Si la bola aún no ha botado 
                    {
                        puntuationP2 += sumPuntuationXPoint;
                        panel.ChangePuntuation(puntuationP1, puntuationP2);  //Act punt
                        ResetBall();
                    }
                }
                else                                   //El ultimo jugador en tocar la bola fue P2 
                {
                    if (firstBot)                       //Si la bola ya ha botado
                    {
                        puntuationP2 += sumPuntuationXPoint;
                        panel.ChangePuntuation(puntuationP1, puntuationP2);  //Act punt
                        ResetBall();
                    }
                    else                                //Si la bola aún no ha botado 
                    {
                        puntuationP1 += sumPuntuationXPoint;
                        panel.ChangePuntuation(puntuationP1, puntuationP2);  //Act punt
                        ResetBall();
                    }

                }
                IsColliding = true;
            }
        }
    }

    private void OnCollisionExit(Collision col)
    {
            IsColliding = false;  
    }

        private void ResetBall()
    {
        DirectionBallChanged();
        ballController.serve = true;
        IsColliding = false;
    }


    private void DirectionBallChanged()
    {
        firstBot = false;
        
    }
}
