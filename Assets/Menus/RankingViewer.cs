using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class RankingViewer : MonoBehaviour {

    public Text rank1, rank2, rank3, ranks;


	// Use this for initialization
	void Start () {
        if(Name.ranking != "")
        {
            SetRankingViewerFields(Ranking.StringToArrayOfStrings(Name.ranking));
        }else ResetTexts();
        //request ranking to the server? if connected
        
    }
	
	// Update is called once per frame
	void Update () {
		
        
	}

    //this function should be called when receiving the ranking data from server in a packet
    public void SetRankingViewerFields(string[] list)
    {
        int listSize = list.Length;
        if(listSize > 0)
        {
            rank1.text = "1. " + list[0].Substring(0, 3) + "     " + list[0].Substring(4);
            if(listSize > 1)
            {
                rank2.text = "2. " + list[1].Substring(0, 3) + "     " + list[1].Substring(4);
                if(listSize > 2)
                {
                    rank3.text = "3. " + list[2].Substring(0, 3) + "     " + list[2].Substring(4);
                    if(listSize > 3)
                    {
                        ranks.text = "";
                        for (int i = 3; i < 10; i++)
                        {
                            //string line = (i+1).ToString() + ". " + list[i].Substring(0, 3) + "     " + list[i].Substring(4);
                            string line = (i + 1).ToString() + ". ";
                            if (i < listSize) line += list[i].Substring(0, 3) + "     " + list[i].Substring(4);
                            ranks.text += line + "\n";
                        }
                    }
                }
            }
        }

    }

    private void ResetTexts()
    {
        rank1.text = "1.";
        rank2.text = "2.";
        rank3.text = "3.";
        ranks.text = "4.\n5.\n6.\n7.\n8.\n9.\n10.";
    }
}
