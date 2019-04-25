using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingPongArena : MonoBehaviour {
    public PingPongAgent agentA;
    public PingPongAgent agentB;
    public PingPongBall ball;

    private void Awake()
    {
        agentA.arena = this;
        agentB.arena = this;

        agentA.table = transform;
        agentB.table = transform;

        ball.arena = this;
    }

    void Start () {
        ResetGame();
	}

    public void ResetGame()
    {

        agentA.ResetAgent();
        agentB.ResetAgent();
        ball.ResetPosition();
        agentA.Done();
        agentB.Done();
    }

}
