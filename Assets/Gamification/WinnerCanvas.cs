using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class WinnerCanvas : MonoBehaviour {

    //Este script se encarga de actualizar las puntuaciones de los jugadores cada vez que se realiza un punto

    public Text winner;
    public Text winnerPun;
    public Text loser;

    private string text1, text2, text3;
    private bool needToChange;

    private bool matchEnd;

    float waitTime = 5f;

    public ClientManager ClientManagerScript;
    // Use this for initialization
    void Start()
    {
        needToChange = true;
        matchEnd = false;
        transform.GetChild(0).gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (matchEnd)
        {
            Debug.Log("matchEnd = true");
            transform.GetChild(0).gameObject.SetActive(true);
            Debug.Log("Set Active");

            winner.text = text1;
            winnerPun.text = text2;
            loser.text = text3;
            //myClientManagerScript.goToMainMenu = true;
            
            waitTime -= Time.deltaTime;
            if(waitTime <= 0 && needToChange)
            {
                Debug.Log("Changing scene");
                SceneManager.LoadScene(ClientManagerScript.MainMenuSceneName, LoadSceneMode.Single);
                needToChange = false;
            }
            
        }
    }

    public void ChangePuntuation(string _winner, int _winnerP, int _loser)
    {
        Debug.Log("Entro a cambiar");
        
        text1 = _winner;
        text2 = _winnerP.ToString("D2");
        text3 = _loser.ToString("D2");
        Debug.Log("String leidos");
        ClientManagerScript.ReturnToMainMenu();
        matchEnd = true;
        


    }
}
