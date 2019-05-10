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
    {/*
        string content = name + ":" + score.ToString("D2") + "\n";
        //create file if it doesn't exist
        if (!File.Exists(path))
        {
            File.WriteAllText(path, content);
        }
        else
        {
            //Add the text to the file
            File.AppendAllText(path, content);
        }*/
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

    /// <summary>Converts an array of strings with all the ranks to a single string of variable length</summary>
    public static string ArrayToSingleString(string[] arr) //variable length
    {
        string str = "";
        for(int i = 0; i < arr.Length; i++)
        {
            str += arr[i] + ",";
        }
        
        return str;
    }

    /// <summary> Converts the single string with all the ranks to an array of strings of variable length</summary>
    public static string[] StringToArrayOfStrings(string str)
    {
        List<string> listAux = new List<string>();
        int index = str.IndexOf(",");
        while(index != -1)
        {
            listAux.Add(str.Substring(0, index));
            str = str.Remove(0, index + 1);
            index = str.IndexOf(",");
        }
        string[] arr = listAux.ToArray();
        return arr;
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
