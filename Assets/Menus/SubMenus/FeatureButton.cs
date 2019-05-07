using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FeatureButton : MonoBehaviour {


    
    public MenuManager mm;
    private MeshRenderer meshRenderer;

    public enum ButtonID { RULES, SYNES, FINGER, MACHINE, MAGUS, SUPER, MAIN}
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
            case ButtonID.RULES:
                SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
                break;
            case ButtonID.SYNES:
                SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
                break;
            case ButtonID.FINGER:
                SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
                break;
            case ButtonID.MACHINE:
                SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
                break;
            case ButtonID.SUPER:
                SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
                break;
            case ButtonID.MAIN:
                SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
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
