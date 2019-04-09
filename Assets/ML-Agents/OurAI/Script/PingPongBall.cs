using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingPongBall : MonoBehaviour {
    /*
     TO DO LIST:
        - Check which agent was the last one to touch the ball
        - If the ball falls to the ground reward the corresponding agent and punish the other
        - if the ball falls on the wrong side of the table reward the corresponding agent and punish the other
        - if the ball bounces to times on the same side of the table reward the ... etc
        - If any of those happen reset the game
        - If any of the agents goes outside of his side of the table punish it 
       
    Check HitWall.cs from tenis example for reference
         */

    PingPongAgent agentA;
    PingPongAgent agentB;

    

}
