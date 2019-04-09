using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class PingPongAgent : Agent {

    /*
     TO DO LIST:
        - Add the necessary torque to the agent so that it gets to the target rotation
        - Add "energy" punishment to avoid innecesary movements? still untested
        - Reset method
         */

    Rigidbody rBody;

    public Transform table;
    public Rigidbody ballRb;

    //invert Z axis
    bool invertZ;
    float invMult;

    public float maxAxisForce;

    public float maxTorque;
    Vector3 objectiveEulerAngles;

    //observation of ball bounce 
    public bool hasBallBounced;

	void Start () {
        rBody = GetComponent<Rigidbody>();
        invMult = invertZ ? -1.0f : 1.0f;
	}

    public override void CollectObservations()
    {

        //relative position (from table)
        Vector3 relativePos = transform.position - table.position;
        AddVectorObs(relativePos.x * invMult);
        AddVectorObs(relativePos.y);
        AddVectorObs(relativePos.z * invMult);

        //relative velocity 
        AddVectorObs(rBody.velocity.x * invMult);
        AddVectorObs(rBody.velocity.y);
        AddVectorObs(rBody.velocity.z * invMult);

        //rotation 
        Vector3 rotation = transform.rotation.eulerAngles;
        AddVectorObs(rotation.x);
        AddVectorObs(rotation.y);
        AddVectorObs(rotation.z);

        //ball relative position (from body)
        Vector3 ballRelPos = ballRb.transform.position - transform.position;
        AddVectorObs(ballRelPos.x);
        AddVectorObs(ballRelPos.y);
        AddVectorObs(ballRelPos.z);

        //ball relative Velocity (from body)
        AddVectorObs((ballRelPos.x - rBody.velocity.x)*invMult);
        AddVectorObs(ballRelPos.y - rBody.velocity.y);
        AddVectorObs((ballRelPos.z - rBody.velocity.z)*invMult);

        //has the ball bounced on your side of the table? 
        AddVectorObs(hasBallBounced);
    }

    public override void AgentAction(float[] vectorAction, string textAction)
    {
        Vector3 move = new Vector3(maxAxisForce*vectorAction[0]*invMult, maxAxisForce * vectorAction[1], maxAxisForce * vectorAction[2] * invMult);
        rBody.AddForce(move);

        objectiveEulerAngles = new Vector3(vectorAction[3], vectorAction[4], vectorAction[5]);
    }
}
