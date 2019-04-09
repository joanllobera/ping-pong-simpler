using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class BulletTimeEffect : MonoBehaviour {
    public static BulletTimeEffect _instance;
    public PostProcessVolume ppVolume;
    ColorGrading colorGradingLayer = null;

    private void Awake() {
        _instance = this;
    }
    private void Start() {
        ppVolume.profile.TryGetSettings(out colorGradingLayer);
        ppVolume.weight = 1;
        colorGradingLayer.active = false;
    }

    public static BulletTimeEffect Instance {
        get { return _instance; }
    }

    public bool Effect {
        
        get {
            return colorGradingLayer.active;
        }
        set {
            colorGradingLayer.active = value;
        }
    }
}
