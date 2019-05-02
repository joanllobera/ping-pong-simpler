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
    // Use this for initialization
    void Start()
    {
        needToChange = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (needToChange)
        {
            winner.text = text1;
            winnerPun.text = text2;
            loser.text = text3;
            needToChange = false;
        }

    }

    public void ChangePuntuation(int winner, int winnerP, int loser)
    {
        text1 = winner.ToString();
        text2 = winnerP.ToString();
        text3 = loser.ToString();
        needToChange = true;
    }
}
