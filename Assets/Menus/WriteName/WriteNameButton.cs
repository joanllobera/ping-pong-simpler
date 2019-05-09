using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WriteNameButton : MonoBehaviour {

    WriteNameManager manager;
    public  WriteNameManager.OnButton buttonType;
    private MeshRenderer renderer;

	// Use this for initialization
	void Start () {
        manager = GameObject.Find("NameManager").GetComponent<WriteNameManager>();
        renderer = GetComponent<MeshRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider col)
    {
        manager.onButton = buttonType;
        renderer.material = manager.orangeBall;
    }

    void OnTriggerExit(Collider col)
    {
        manager.onButton = WriteNameManager.OnButton.Nothing;
        renderer.material = manager.whiteBall;
    }
}
