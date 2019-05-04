using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class PingPongAgent : Agent {

    //agent rigid body
    Rigidbody rBody;
    [HideInInspector]
    public Vector3 initialPos;

    //The ball
    public PingPongBall ball;
    private Rigidbody ballRb;

    //past transform
    private Vector3 lastPos;
    private Quaternion lastRot;

    //agent side
    public bool isBottomSide;
    float invMult = -1;

    //velocity
    public float maxAxisForce;

    //rotation
    public float maxRotationPerSecond;
    Quaternion objectiveEulerAngles;

    //tableVariables
    [HideInInspector]
    public PingPongArena arena;
    public Transform table;

    void Awake () {
        rBody = GetComponent<Rigidbody>();

        //set the original Position and rotation
        initialPos = lastPos = transform.position;

        rBody.interpolation = RigidbodyInterpolation.Interpolate;
        invMult = isBottomSide ? 1.0f : -1.0f;

        ballRb = ball.GetComponent<Rigidbody>();
	}

    public override void CollectObservations()
    {

        //relative position (from table)
        Vector3 relativePos = transform.position - table.position;
        AddVectorObs(relativePos.x);
        AddVectorObs(relativePos.y);
        AddVectorObs(relativePos.z);

        //velocity 
        /*
        AddVectorObs(rBody.velocity.x);
        AddVectorObs(rBody.velocity.y);
        AddVectorObs(rBody.velocity.z);*/

        //ball relative position (from table)
        Vector3 ballRelPos = ballRb.transform.position - table.position;
        AddVectorObs(ballRelPos.x);
        AddVectorObs(ballRelPos.y);
        AddVectorObs(ballRelPos.z);

        //ball velocity
        /*
        AddVectorObs(ballRb.velocity.x);
        AddVectorObs(ballRb.velocity.y);
        AddVectorObs(ballRb.velocity.z);*/

        //has the ball bounced on your side of the table? 
        AddVectorObs(ball.bounced);
    }

    public override void AgentAction(float[] vectorAction, string textAction)
    {
        Vector3 move = new Vector3();
        move.x = Mathf.Clamp(vectorAction[0], -1, 1);
        move.y = Mathf.Clamp(vectorAction[1], -1, 1);
        move.z = 0;
        move *= maxAxisForce;
        rBody.AddForce(move);

        Debug.Log(vectorAction[1]);
        //AddReward(Mathf.Lerp(0, -0.1f, transform.position.y - initialPos.y));
       
        objectiveEulerAngles = Quaternion.Euler(Mathf.Lerp(10,60, Mathf.Clamp(vectorAction[2], 0, 1)),0, 0);
        rBody.MoveRotation(Quaternion.RotateTowards(this.transform.rotation, objectiveEulerAngles, maxRotationPerSecond * Time.deltaTime));
    }

    public void Reward()
    {
        //Using sqrt to give small values a relatively bigger negative reward
        float posDifference = -Mathf.Sqrt(Vector3.Distance(this.transform.position, lastPos)) * Time.deltaTime;
        AddReward(posDifference);

        //Angle difference between the 2 quaternions
        float angleDifference = -Mathf.Sqrt(2 * Mathf.Acos(Quaternion.Dot(this.transform.rotation, lastRot))) * Time.deltaTime;
        AddReward(angleDifference);

        //Punish the agent if it is too far away from it's initial position
        AddReward(Vector3.SqrMagnitude(this.transform.position - new Vector3(0, 1.2f, 1.35f * invMult)) > 1f ? -1f * Time.deltaTime : 0f);

        //set positions for next frame
        lastPos = transform.position;
        lastRot = transform.rotation;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag == "Ball")
        {
            ball.HitBall(this);
        }
    }

    public void ResetAgent()
    {
        this.rBody.velocity = Vector3.zero;
        this.rBody.angularVelocity = Vector3.zero;
        rBody.position = initialPos + new Vector3(Random.Range(-0.5f,0.5f),0,0);
    }
}
