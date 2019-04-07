using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class BulletTimeEffect : MonoBehaviour {
    public static BulletTimeEffect _instance;
    public PostProcessVolume ppVolume;

    private void Awake() {
        _instance = this;
    }

    public static BulletTimeEffect Instance {
        get { return _instance; }
    }

    public bool Effect {
        
        get {
            if (ppVolume.weight > 0.1) return true;
            else return false;
        }
        set {
            if (value) ppVolume.weight = 1;
            else ppVolume.weight = 0;
        }
    }

}
