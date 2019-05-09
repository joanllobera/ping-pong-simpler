using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WriteNameManager : MonoBehaviour {


    public Material whiteBall;
    public Material orangeBall;

    public SteamVR_Input_Sources right;
    public SteamVR_Input_Sources left;
    public SteamVR_Action_Boolean triggerPress;

    public Text[] letters; //letters
    int index = 0;
    int letter = 0;
    string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    char[] alph = new char[26];
    public enum OnButton { Nothing, Previous, Next, Ok};
    [HideInInspector] public OnButton onButton;

    // Use this for initialization
    void Start () {
        letters[0].text = letters[1].text = letters[2].text = "";
        onButton = OnButton.Nothing;
        alph = alphabet.ToCharArray();
	}
	
	// Update is called once per frame
	void Update () {

        if (triggerPress.GetStateDown(right))
        {

            if (onButton == OnButton.Ok)
            {
                if (index < 2) {
                    ++index;
                    letter = 0;
                }
                else
                {
                    //guardar nombre
                    string name = letters[0].text + letters[1].text + letters[2].text;
                    Name.nickname = name;
                    //canviar escena a MainMenu
                    SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
                }
            }else if(onButton == OnButton.Next)
            {
                letter++;
                if (letter == 27) letter = 0;
            }else if(onButton == OnButton.Previous)
            {
                letter--;
                if (letter == -1) letter = 26;
            }
        }
        //onButton = OnButton.Nothing;
        letters[index].text = alph[letter].ToString();
    }

    
}
