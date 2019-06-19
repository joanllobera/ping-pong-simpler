using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingPongArena : MonoBehaviour {
    public PingPongAgent agentA;
    //public PingPongAgent agentB;
    public PingPongBall ball;

    private void Awake()
    {
        agentA.arena = this;
        //agentB.arena = this;

        agentA.table = transform;
        //agentB.table = transform;

        ball.arena = this;
    }

    void Start () {
        ResetGame();
	}
    private void Update()
    {
        if (ball.transform.position.y < -10)
            ResetGame();
    }

    public void ResetGame()
    {
        agentA.ResetAgent();
        agentA.Done();
    }

}
