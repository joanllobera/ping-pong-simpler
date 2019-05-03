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
    //[HideInInspector]
    public bool bounced;
    private bool serveBottom = true;
    private int serves = 0;

    [HideInInspector]
    public PingPongArena arena;

    private void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    public void HitBall(PingPongAgent agent)
    {
        agent.AddReward(0.05f);
    }

    public void OnCollisionEnter(Collision collision)
    {
        //colisiones con la mesa
       if (collision.collider.gameObject.CompareTag("Table"))
        {
            if (!bounced)
                bounced = true;
            else
            {
                AgentBottom.SetReward(-0.5f);
                arena.ResetGame();
            }
        }
        else if (collision.collider.gameObject.CompareTag("Ground"))
        {
            if(bounced)
                AgentBottom.SetReward(-1f);
            else
                AgentBottom.SetReward(1f);
            
            arena.ResetGame();
        }
        else if(collision.collider.gameObject.CompareTag("HalfTable"))
        {
            AgentBottom.AddReward(0.5f);
            bounced = false;
            //arena.ResetGame();
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
        this.rb.velocity = Vector3.up*2f;
        this.rb.angularVelocity = Vector3.zero;
        transform.position = AgentBottom.initialPos + new Vector3(Random.Range(-0.5f,0.5f), 1.75f, 0.0f);
        lastAgentHit = null;    
        bounced = true;
    }


}
