using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour {


    
    public MenuManager mm;
    private MeshRenderer meshRenderer;

    public enum ButtonID { PVP, PVAI, CONTROLS, SETTINGS, EXIT}
    public ButtonID buttonID;
    public string sceneName;

    //fade effect
    bool isFading = false;
    float fadingValue = 0f;

    // Use this for initialization
    void Start () {
        meshRenderer = GetComponent<MeshRenderer>();
	}
	
	// Update is called once per frame
	void Update () {

        if (isFading)
        {
            FadeOut(ref fadingValue);
        }
	}

    void OnTriggerEnter(Collider col)
    {
        meshRenderer.material = mm.orangeBall;
        //if (sceneName == "")
        //{
        //    Debug.Log("sceneName is empty string");
        //    Application.Quit();
        //}
        //else
        //{
        //    Debug.Log("opening newScene");
        //    SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        //}

        switch (buttonID)
        {
            case ButtonID.PVP:
                SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
                break;
            case ButtonID.PVAI:
                SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
                break;
            case ButtonID.CONTROLS:
                SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
                break;
            case ButtonID.SETTINGS:
                SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
                break;
            case ButtonID.EXIT:
                Application.Quit();
                break;
        }
        
    }

    void OnTriggerExit(Collider col)
    {
        meshRenderer.material = mm.whiteBall;
    }

    public void FadeOut(ref float fader)
    {

    }
}
