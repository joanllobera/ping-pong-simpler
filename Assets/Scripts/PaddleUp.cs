using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleUp : MonoBehaviour
{

    public delegate void VoidDelegate(float mod);
    public event VoidDelegate OnPaddleUpStarted;

    public static PaddleUp _instance;

    [SerializeField] private float _size = 3.0f;
    private bool _paddleUpActivated = false;

    private void Awake()
    {
        _instance = this;
    }

    public static PaddleUp Instance
    {
        get { return _instance; }
    }

    public bool SwitchPaddleUp
    {
        get { return _paddleUpActivated; }
        set { _paddleUpActivated = value; }
    }

    public void TriggerPaddleUp()
    {
        if (!_paddleUpActivated) OnPaddleUpStarted?.Invoke(_size);
    }

}
