using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WinnerCanvas : MonoBehaviour {

    //Este script se encarga de actualizar las puntuaciones de los jugadores cada vez que se realiza un punto

    public Text winner;
    public Text winnerPun;
    public Text loser;

    private string text1, text2, text3;
    private bool needToChange;

    private bool matchEnd;


    public MyClientManager myClientManagerScript;
    // Use this for initialization
    void Start()
    {
        needToChange = false;
        matchEnd = false;

    }

    // Update is called once per frame
    void Update()
    {
        

        if (matchEnd)
        {
            gameObject.SetActive(gameObject.activeSelf);

            if (needToChange)
            {
                winner.text = text1;
                winnerPun.text = text2;
                loser.text = text3;
                myClientManagerScript.goToMainMenu = true;
                needToChange = false;
            }
        }
        else
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }
    }

    public void ChangePuntuation(string winner, int winnerP, int loser)
    {
        matchEnd = true;
        text1 = winner;
        text2 = winnerP.ToString();
        text3 = loser.ToString();
        needToChange = true;
    }
}
