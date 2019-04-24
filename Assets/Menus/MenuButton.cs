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
        //if (sceneName == "") Application.Quit();
        if (sceneName == null) Application.Quit();
        else
        {
            SceneManager.LoadScene("sceneName", LoadSceneMode.Single);
        }
    }
}
