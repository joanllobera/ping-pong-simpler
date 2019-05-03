using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingPongBall : MonoBehaviour {

    public PingPongAgent AgentBottom;
    //public PingPongAgent AgentTop;
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
        //if the agent hits the ball 2 times set negative reward and reset the game
        if(lastAgentHit == agent)
        {
            agent.SetReward(-1f);
            lastAgentHit = agent;
            arena.ResetGame();
            return;
            //serveBottom = !serveBottom;
        }
        //if the ball has bounced on the table we add a small reward to the agent
        else if(bounced)
        {
            agent.SetReward(0.05f);
        }
        bounced = false;
        //set last agent hit to this agent
        lastAgentHit = agent;
    }

    public void OnCollisionEnter(Collision collision)
    {
        //colisiones con la mesa
       if (collision.collider.gameObject.CompareTag("Table"))
        {
            //Check if the ball has bounced on the side of the lastAgentHit
            if (CheckSide())
            {
                //The ball cannot bounce on the same side as the lastAgentHit
                SetRewards(-1f, 0f);
                arena.ResetGame();
                //serveBottom = !serveBottom;
            } else
            {
                //The ball has already bounced once
                if (bounced)
                {
                    SetRewards(0f, -1f);
                    arena.ResetGame();
                    //serveBottom = !serveBottom;
                }
                else
                    bounced = true;
            }
        }
        else if (collision.collider.gameObject.CompareTag("Ground"))
        {
            if (bounced)
            {
                //lastAgentHit scored
                SetRewards(1f, -1f);
                arena.ResetGame();
            }
            else
            {
                //lastAgentHit failed miserably
                SetRewards(-0f, 1f);
                arena.ResetGame();
                serveBottom = !serveBottom;
            }
        }
        else if(collision.collider.gameObject.CompareTag("HalfTable"))
        {
            lastAgentHit.AddReward(0.2f);
            lastAgentHit = null;
            bounced = false;

        }
    }

    //Returns True if the ball is in the same side as the lastAgentHit (last agent that hit the ball)
    //Bottom side = positive z position of the ball
    public bool CheckSide()
    {
        if(lastAgentHit!=null)
            return (lastAgentHit.isBottomSide ? 1 : -1) * this.transform.position.z > 0;
        return false;
    }

    public void SetRewards(float lastAgentHitReward, float otherAgentReward)
    {
        //PingPongAgent otherAgent = lastAgentHit == AgentBottom ? AgentTop : AgentBottom;
        if(lastAgentHit!=null)
            lastAgentHit.SetReward(lastAgentHitReward);
        else
        {
            AgentBottom.SetReward(otherAgentReward);
        }
        //otherAgent.SetReward(otherAgentReward);
    }

    public void ResetPosition()
    {

        this.rb.velocity = Vector3.up*0.5f;
        this.rb.angularVelocity = Vector3.zero;
        /*if (serveBottom)
        {
            transform.position = AgentBottom.initialPos + new Vector3(0, 0.25f, 0);
            lastAgentHit = AgentTop;
        }
        else
        {
            transform.position = AgentTop.initialPos + new Vector3(0, 0.25f, 0);
            lastAgentHit = AgentBottom;
        }*/
        transform.position = AgentBottom.initialPos + new Vector3(Random.Range(-0.75f,0.75f), 0.75f, 0.0f);
        lastAgentHit = null;    
        bounced = true;
    }


}
