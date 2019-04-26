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
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
	}

   public void ChangePuntuation(int J1Punt, int J2Punt)
    {
        C1J1.text = J1Punt.ToString();
        C1J2.text = J2Punt.ToString();
        C2J1.text = J1Punt.ToString();
        C2J2.text = J2Punt.ToString();
    }


}
