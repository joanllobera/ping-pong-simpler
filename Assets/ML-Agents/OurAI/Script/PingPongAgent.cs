using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class PingPongAgent : Agent {
    
    /*
     TO DO LIST:
        Done - Add the necessary torque to the agent so that it gets to the target rotation
        Done - Add "energy" punishment to avoid innecesary movements? still untested
        Done - Reset method
         */

    Rigidbody rBody;
    public PingPongBall ball;

    public Transform table;
    public Rigidbody ballRb;

    private Transform lastTransform;
    public bool isBottomSide;
    //invert Z axis
    float invMult = -1;

    public float maxAxisForce;

    public float maxRotationPerSecond;
    Quaternion objectiveEulerAngles;

	void Awake () {
        rBody = GetComponent<Rigidbody>();
        lastTransform = this.transform;
        rBody.interpolation = RigidbodyInterpolation.Interpolate;
        invMult = isBottomSide ? 1.0f : -1.0f;
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
        AddVectorObs(rotation.x * invMult);
        AddVectorObs(rotation.y);
        AddVectorObs(rotation.z * invMult);

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
        AddVectorObs(ball.bounced);
    }

    public override void AgentAction(float[] vectorAction, string textAction)
    {
        Vector3 move = new Vector3(maxAxisForce*vectorAction[0]*invMult, maxAxisForce * vectorAction[1], maxAxisForce * vectorAction[2] * invMult);
        rBody.AddForce(move);

        objectiveEulerAngles = Quaternion.Euler(vectorAction[3] * invMult, vectorAction[4], vectorAction[5] * invMult);
        rBody.MoveRotation(Quaternion.RotateTowards(this.transform.rotation, objectiveEulerAngles, maxRotationPerSecond * Time.deltaTime));
    }

    public void Reward()
    {
        //Using sqrt to give small values a relatively bigger negative reward
        float posDifference = -Mathf.Sqrt(Vector3.Distance(this.transform.position, lastTransform.position)) * Time.deltaTime;
        AddReward(posDifference);

        //Angle difference between the 2 quaternions
        float angleDifference = -Mathf.Sqrt(2 * Mathf.Acos(Quaternion.Dot(this.transform.rotation, lastTransform.rotation))) * Time.deltaTime;
        AddReward(angleDifference);
        //Punish the agent if it is too far away from it's initial position
        AddReward(Vector3.SqrMagnitude(this.transform.position - new Vector3(0, 1.2f, 1.35f * invMult)) > 1f ? -1f * Time.deltaTime : 0f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        PingPongBall ball = collision.collider.gameObject.GetComponent<PingPongBall>();
        if (ball != null)
        {
            if(ball.lastAgentHit == this)
            {
                ball.ResetEnvironment();
                AddReward(-1f);
            }
            else if (ball.bounced)
            {
                ball.HitBall(this);
                AddReward(2f);
            } else
            {
                ball.ResetEnvironment();
                AddReward(-1f);
            }
        }
        else
            AddReward(-1f);
    }

    public void ResetAgent()
    {
        this.rBody.velocity = Vector3.zero;
        this.rBody.angularVelocity = Vector3.zero;
        this.rBody.MovePosition(new Vector3(0, 1.2f, 1.35f * (isBottomSide ? 1 : -1)));
        this.rBody.MoveRotation(Quaternion.identity);
    }
}
