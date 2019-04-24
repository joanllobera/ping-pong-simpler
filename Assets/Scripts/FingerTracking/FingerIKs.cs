using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerIKs : MonoBehaviour {

    [SerializeField] private Transform[] _handMalcomPoints;
    [SerializeField] private HandRenderer _leftHand;
    private Vector3[] _handMalcomPointsRegister = new Vector3[21];
    private Quaternion _handMalcomBaseRotationRegister;

	// Use this for initialization
	void Awake () {
        _handMalcomPointsRegister = new Vector3[_handMalcomPoints.Length];
        for (int i = 0; i < _handMalcomPoints.Length; ++i) {
            _handMalcomPointsRegister[i] = _handMalcomPoints[i] != null ? _handMalcomPoints[i].position : Vector3.zero;
        }
        _handMalcomBaseRotationRegister = _handMalcomPoints[0].transform.rotation;
    }

    // Update is called once per frame
    void LateUpdate () {
        if (_leftHand.isLeft) {
            if (_leftHand.Points[0].activeSelf) {
                _handMalcomPoints[0].position = _leftHand.Points[0].transform.position;
                _handMalcomPoints[0].rotation = _leftHand.Points[0].transform.rotation; //(Y:90, Z: -90)
            }/* else {
                _handMalcomPoints[0].position = _handMalcomPointsRegister[0];
                _handMalcomPoints[0].rotation = _handMalcomBaseRotationRegister;
            }*/
            /*for (int i = 1; i < _leftHand.Points.Count; ++i) {
                if (_leftHand.Points[i].activeSelf && _handMalcomPoints[i] != null) {
                    _handMalcomPoints[i].position = _leftHand.Points[i].transform.position;
                } else if(_handMalcomPoints[i] != null) { _handMalcomPoints[i].position = _handMalcomPointsRegister[i]; }
            }*/
        }
	}
}
