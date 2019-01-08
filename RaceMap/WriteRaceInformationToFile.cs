using System.Collections.Generic;
using System.IO;

using UnityEngine;

[SerializeField]
public class WriteRaceInformationToFile
{
    private static string path = Application.streamingAssetsPath + "/Resources/Results.json";

    [SerializeField]

    public WriteRaceInformationToFile(List<GameObject> Players)
    {
        Write(Players);
    }
    
    public static void Write(List<GameObject> Players)
    {
        StreamWriter writer = new StreamWriter(path, false);
        RacerResults RacerInfo = new RacerResults();

        for (int i = 0; i < Players.Count; i++)
        {
            string Name = Players[i].GetComponent<PlayerInformationScript>().name;
            string Status = Players[i].GetComponent<PlayerInformationScript>().Status;
            string Pos = Players[i].GetComponent<PlayerInformationScript>().CurrentPosition;
            string[] LapTimes = Players[i].GetComponent<PlayerInformationScript>().LapTimes;

            RaceInformation Info = new RaceInformation(Name, LapTimes,Pos,Status);
            RacerInfo.RaceInfo[i] = Info;

        }
        string json = JsonUtility.ToJson(RacerInfo);
        writer.Write(json);
        writer.Close();
    }
}

[System.Serializable]
public class RacerResults //wrap all the race information
{
    public RaceInformation[] RaceInfo = new RaceInformation[5];

}

[System.Serializable]
public class RaceInformation
{

    public string Name;
    [SerializeField]
    public string[] LapTimes;
    public string Position;
    public string Status; //if finished/still racing or destroyed


    public RaceInformation(string n, string[]l, string Pos, string Stat)
    {
        LapTimes = new string[3];
        Name = n;
        Position = Pos;
        Status = Stat;
        LapTimes = l;
    }
}
