using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatY : MonoBehaviour {

    public float alt;
    public float spd;

    void Update()
    {
        this.transform.position += new Vector3(0, Mathf.Sin(Time.fixedTime * Mathf.PI * spd) * alt, 0);
    }
}
