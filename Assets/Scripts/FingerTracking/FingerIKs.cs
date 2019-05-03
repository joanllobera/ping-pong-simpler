using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerIKs : MonoBehaviour {
    public enum GesturesTypes { None, OK, Point, Fist, Five, Like }

    [Header("Malcom's left hand fingers")]
    [SerializeField] private Transform[] _handMalcomPoints;

    [Header("Malcom's left hand gesture rotations")]
    [SerializeField] private Quaternion[] _handMalcomPointsDefault;
    [SerializeField] private Quaternion[] _handMalcomPointsFist;
    [SerializeField] private Quaternion[] _handMalcomPointsFive;
    [SerializeField] private Quaternion[] _handMalcomPointsOK;
    [SerializeField] private Quaternion[] _handMalcomPointsPoint;
    [SerializeField] private Quaternion[] _handMalcomPointsLike;

    [Header("Needed References")]
    [Header("This GO HandRenderer's reference")]
    [SerializeField] private HandRenderer _leftHand;
    private Vector3[] _handMalcomPointsRegister = new Vector3[21];
    private Quaternion _handMalcomBaseRotationRegister;

    private Transform _wristTrans = null;

    [Header("Avatar Manager in AvatarVR")]
    public AvatarSystem.AvatarManager _avatarManager;
    private AvatarSystem.AvatarSensorsController _sensors;

    [Header("ClientManager's reference")]
    public ClientManager _clientManager;
    private GesturesTypes _gesture = GesturesTypes.None;

    public Transform GetWrist { get { return _leftHand.Points[0].transform; } }

	// Use this for initialization
	void Awake () {
        _handMalcomPointsRegister = new Vector3[_handMalcomPoints.Length];
        for (int i = 0; i < _handMalcomPoints.Length; ++i) {
            _handMalcomPointsRegister[i] = _handMalcomPoints[i] != null ? _handMalcomPoints[i].position : Vector3.zero;
        }
        _handMalcomBaseRotationRegister = _handMalcomPoints[0].transform.rotation;

        this.gameObject.GetComponent<HandRenderer>().OnSkeletonHandInitialized += SetWrist;
        _clientManager.OnGestureChanged += SetGestureToMalcom;
    }

    // Update is called once per frame
    void LateUpdate () {
        if (_leftHand.isLeft && _leftHand.Points.Count > 0) {
            if (_leftHand.Points[0].activeSelf)
            {
                _sensors.ToggleFingerTrackingUsage(true);
                _handMalcomPoints[0].position = _leftHand.Points[0].transform.position;
                _handMalcomPoints[0].rotation = _leftHand.Points[0].transform.rotation; //(Y:90, Z: -90)
            }
            else {
                _sensors.ToggleFingerTrackingUsage(false);
            }/* else {
                _handMalcomPoints[0].position = _handMalcomPointsRegister[0];
                _handMalcomPoints[0].rotation = _handMalcomBaseRotationRegister;
            }*/
            /*for (int i = 1; i < _leftHand.Points.Count; ++i) {
                if (_leftHand.Points[i].activeSelf && _handMalcomPoints[i] != null) {
                    _handMalcomPoints[i].position = _leftHand.Points[i].transform.position;
                } else if(_handMalcomPoints[i] != null) { _handMalcomPoints[i].position = _handMalcomPoints[i].position; }
            }*/
            SetRotations();
        }
	}

    private void SetWrist(Transform wrist) {
        if (_wristTrans != null) return;
        Debug.Log("WRIST INIT");
        wrist.rotation = Quaternion.Euler(new Vector3(0, 90, -90));
        _wristTrans = wrist;
        _sensors = (AvatarSystem.AvatarSensorsController)_avatarManager.GetController();
        _sensors.SetWristFingerTracking(wrist);
        _sensors.ToggleFingerTrackingUsage(true);
    }

    [ContextMenu("RegisterRotation")]
    public void RegisterRotation() {
        for (int i = 1; i < _handMalcomPoints.Length; ++i) {
            _handMalcomPointsLike[i] = _handMalcomPoints[i].localRotation;
        }
    }

    [ContextMenu("RestoreRotation")]
    public void RestoreRotation()
    {
        for (int i = 1; i < _handMalcomPoints.Length; ++i)
        {
             _handMalcomPoints[i].rotation = _handMalcomPointsLike[i];
        }
    }

    public void SetRotations() {
        Debug.Log("CHANGE GESTURE " + _gesture);
        switch (_gesture)
        {
            case GesturesTypes.None:
                for (int i = 1; i < _handMalcomPoints.Length; ++i)
                {
                    _handMalcomPoints[i].localRotation = _handMalcomPointsDefault[i];
                }
                break;
            case GesturesTypes.OK:
                for (int i = 1; i < _handMalcomPoints.Length; ++i)
                {
                    _handMalcomPoints[i].localRotation = _handMalcomPointsOK[i];
                }
                break;
            case GesturesTypes.Fist:
                for (int i = 1; i < _handMalcomPoints.Length; ++i)
                {
                    _handMalcomPoints[i].localRotation = _handMalcomPointsFist[i];
                }
                break;
            case GesturesTypes.Point:
                for (int i = 1; i < _handMalcomPoints.Length; ++i)
                {
                    _handMalcomPoints[i].localRotation = _handMalcomPointsPoint[i];
                }
                break;
            case GesturesTypes.Like:
                for (int i = 1; i < _handMalcomPoints.Length; ++i)
                {
                    _handMalcomPoints[i].localRotation = _handMalcomPointsLike[i];
                }
                break;
            case GesturesTypes.Five:
                for (int i = 1; i < _handMalcomPoints.Length; ++i)
                {
                    _handMalcomPoints[i].localRotation = _handMalcomPointsFive[i];
                }
                break;
        }
    }

    public void SetGestureToMalcom(GesturesTypes gesture) {
        _gesture = gesture;
    }
}
