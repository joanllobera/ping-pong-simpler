using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScorePanel : MonoBehaviour {

    //Este script se encarga de actualizar las puntuaciones de los jugadores cada vez que se realiza un punto

    public Text C1J1;
    public Text C1J2;
    public Text C2J1;
    public Text C2J2;

    private string text1, text2, text3, text4;
    private bool needToChange;
    // Use this for initialization
    void Start () {
        needToChange = false;

    }
	
	// Update is called once per frame
	void Update () {
        if (needToChange)
        {
            C1J1.text = text1;
            C1J2.text = text2;
            C2J1.text = text3;
            C2J2.text = text4;
            needToChange = false;
        }

    }

   public void ChangePuntuation(int J1Punt, int J2Punt)
    {
        text1 = J1Punt.ToString();
        text2 = J2Punt.ToString();
        text3 = J1Punt.ToString();
        text4 = J2Punt.ToString();
        needToChange = true;
    }


}
