using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTime : MonoBehaviour {

	public delegate void VoidDelegate(float mod);
    public event VoidDelegate OnBulletTimeStarted;

    public static BulletTime _instance;

    [SerializeField] private float _percentage = 0.7f;
    private bool _bulletTimeActivated = false;

    private void Awake() {
        _instance = this;
    }

    public static BulletTime Instance {
        get { return _instance; }
    }

    public bool SwitchBulletTime {
        get { return _bulletTimeActivated; }
        set { _bulletTimeActivated = value; }
    }

    public void TriggerBulletTime() {
        if(!_bulletTimeActivated) OnBulletTimeStarted?.Invoke(_percentage);
    }

}
