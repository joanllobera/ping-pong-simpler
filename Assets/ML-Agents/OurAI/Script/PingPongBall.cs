using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingPongBall : MonoBehaviour {
    /*
     TO DO LIST:
        Done - Check which agent was the last one to touch the ball
        Done - If the ball falls to the ground reward the corresponding agent and punish the other
        Done - if the ball falls on the wrong side of the table reward the corresponding agent and punish the other
        Done - if the ball bounces to times on the same side of the table reward the ... etc
        Todo - If any of those happen reset the game
        Todo - If any of the agents goes outside of his side of the table punish it 
       
    Check HitWall.cs from tenis example for reference
         */

    public PingPongAgent AgentA;
    public PingPongAgent AgentB;
    
    //Last agent that has hit the ball
    public PingPongAgent lastAgentHit;
    private bool bounced;

    private void Update()
    {
        if(this.transform.position.y <= 0)
        {
            if (bounced)
            {
                //lastAgentHit scored
                SetRewards(5f, -2f);
            }
            else
            {
                //lastAgentHit failed miserably
                SetRewards(-2f, 0f);
            }
        }
    }

    public void HitBall(PingPongAgent agent)
    {
        lastAgentHit = agent;
        bounced = false;
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("Table"))
        {
            //Check if the ball has bounced on the side of the lastAgentHit
            if (CheckSide())
            {
                //The ball cannot bounce on the same side as the lastAgentHit
                SetRewards(-2f, 5f);

            } else
            {
                //The ball has already bounced once
                if (bounced)
                {
                    SetRewards(5f, -2f);
                }
                else
                    bounced = true;
            }
        }
    }

    public bool CheckSide()
    {
        //Returns True if the ball is in the same side as the lastAgentHit (last agent that hit the ball)
        //Bottom side = positive z position of the ball
        return (lastAgentHit.isBottomSide ? 1 : -1) * this.transform.position.z > 0;
    }

    public void SetRewards(float lastAgentHitReward, float otherAgentReward)
    {
        PingPongAgent otherAgent = lastAgentHit == AgentA ? AgentB : AgentA;

        lastAgentHit.SetReward(lastAgentHitReward);
        otherAgent.SetReward(otherAgentReward);

        //Reset the simulation
    }


}
