using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class Ranking {
    private static string path;

    public static void InitRanking()
    {
        DefinePathOnce();
        //CreateRankingLog();
    }
    private static void DefinePathOnce()
    {
        path = Application.dataPath + "/Ranking.txt"; //path of the file, in the Assets folder
    }

    //static void CreateRankingLog()
    //{
    //    //create file if it doesn't exist
    //    if (!File.Exists(path))
    //    {
    //        File.WriteAllText(path, "Ping-Pong Ranking\n");
    //    }
    //}
    
    public static void AddPlayerScore(string name, int score)
    {
        string content = name + ":" + score.ToString("D3") + "\n";
        //create file if it doesn't exist
        if (!File.Exists(path))
        {
            File.WriteAllText(path, content);
        }
        else
        {
            //Add the text to the file
            File.AppendAllText(path, content);
        }
    }

    private static void ClampLogs(int max = 10)
    {
        string[] readText = File.ReadAllLines(path);
        if (readText.Length > max)
        {
            string[] contents = new string[max];
            for (int i = 0; i < max; i++)
            {
                contents[i] = readText[i];
            }
            File.WriteAllLines(path, contents);
        }
    }

    public static string[] GetBestPlayersList()
    {
        ClampLogs();
        SortRanking();
        string[] readText = File.ReadAllLines(path);
        return readText;
    }

    public static void SortRanking()
    {
        string[] readText = File.ReadAllLines(path);
        bool sorted = false;
        while (!sorted)
        {
            sorted = true;
            for (int i = 0; i < readText.Length - 1; i++)
            {
                int score = int.Parse(readText[i].Substring(readText[i].IndexOf(":") + 1));
                int scoreNext = int.Parse(readText[i + 1].Substring(readText[i + 1].IndexOf(":") + 1));

                if (score < scoreNext)
                {
                    string aux = readText[i + 1];
                    readText[i + 1] = readText[i];
                    readText[i] = aux;
                    sorted = false;
                }
            }
        }
        File.WriteAllLines(path, readText);
    }
	
}
