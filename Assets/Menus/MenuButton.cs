using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour {


    public string sceneName;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider col)
    {
        if (sceneName == "")
        {
            Debug.Log("sceneName is empty string");
            Application.Quit();
        }
        else
        {
            Debug.Log("opening newScene");
            SceneManager.LoadScene("sceneName", LoadSceneMode.Single);
        }
    }
}
