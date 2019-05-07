﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RulesSubMenuButton : MonoBehaviour {

    public MenuManager mm;
    private MeshRenderer meshRenderer;

    public enum ButtonID { ACTIVATE, RETURN }
    public ButtonID buttonID;
    public string sceneName;

    //fade effect
    bool isFading = false;
    float fadingValue = 0f;

    private bool isRulesActive;
    // Use this for initialization
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        isRulesActive = true;
    }

    // Update is called once per frame
    void Update()
    {

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
            case ButtonID.ACTIVATE:
                if (isRulesActive)
                {
                    isRulesActive = false;
                }else if (!isRulesActive)
                {
                    isRulesActive = true;
                }
                else
                {
                    Debug.Log("Unexpected Error");
                }
                break;

            case ButtonID.RETURN:
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
