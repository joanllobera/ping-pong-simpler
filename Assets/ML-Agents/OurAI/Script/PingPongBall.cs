using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingPongBall : MonoBehaviour {

    public PingPongAgent AgentBottom;
    public PingPongAgent AgentTop;
    private Rigidbody rb;
    
    //Last agent that has hit the ball
    [HideInInspector]
    public PingPongAgent lastAgentHit;
    [HideInInspector]
    public bool bounced;
    private bool serveBottom = true;
    private int serves = 0;

    [HideInInspector]
    public PingPongArena arena;
    private void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
    }

   /*private void Update()
    {
        if(this.transform.position.y <= 0)
        {
            
        }
    }*/

    public void HitBall(PingPongAgent agent)
    {
        lastAgentHit = agent;
        bounced = false;
        if(lastAgentHit == agent)
        {
            agent.SetReward(-1f);
            arena.ResetGame();
            serveBottom = !serveBottom;
        }
        else
        {
            if (bounced)
                agent.SetReward(0.1f);
            else
                agent.SetReward(-1f);
        }

    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("Table"))
        {
            //Check if the ball has bounced on the side of the lastAgentHit
            if (CheckSide())
            {
                //The ball cannot bounce on the same side as the lastAgentHit
                SetRewards(-2f, 0f);
                arena.ResetGame();
                serveBottom = !serveBottom;
            } else
            {
                //The ball has already bounced once
                if (bounced)
                {
                    SetRewards(0f, -2f);
                    arena.ResetGame();
                    serveBottom = !serveBottom;
                }
                else
                    bounced = true;
            }
        }
        if (collision.collider.gameObject.CompareTag("Ground"))
        {
            if (!CheckSide())
            {
                if (bounced)
                {
                    //lastAgentHit scored
                    SetRewards(5f, -2f);
                    arena.ResetGame();
                }
                else
                {
                    //lastAgentHit failed miserably
                    SetRewards(-2f, 0f);
                    arena.ResetGame();
                    serveBottom = !serveBottom;
                }
            }
            else
            {
                //lastAgentHit failed miserably
                SetRewards(-2f, 0f);
                arena.ResetGame();
                serveBottom = !serveBottom;
            }
        }
        else
        {
            SetRewards(-2f, 0f);
        }
    }

    //Returns True if the ball is in the same side as the lastAgentHit (last agent that hit the ball)
    //Bottom side = positive z position of the ball
    public bool CheckSide()
    {
        return (lastAgentHit.isBottomSide ? 1 : -1) * this.transform.position.z > 0;
    }

    public void SetRewards(float lastAgentHitReward, float otherAgentReward)
    {
        PingPongAgent otherAgent = lastAgentHit == AgentBottom ? AgentTop : AgentBottom;

        lastAgentHit.SetReward(lastAgentHitReward);
        otherAgent.SetReward(otherAgentReward);
    }

    public void ResetPosition()
    {

        this.rb.velocity = Vector3.zero;
        this.rb.angularVelocity = Vector3.zero;
        if (serveBottom)
        {
            transform.position = AgentBottom.initialPos + new Vector3(0, 0.25f, 0);
            lastAgentHit = AgentTop;
        }
        else
        {
            transform.position = AgentTop.initialPos + new Vector3(0, 0.25f, 0);
            lastAgentHit = AgentBottom;
        }
        bounced = true;
    }


}
