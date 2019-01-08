using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ReadInResults : MonoBehaviour
{
    private string Path;
    private RacerResults Info;

    public Text PlayerName;
    public Text PlayerFinishPos;
    public Text PlayerLap1;
    public Text PlayerLap2;
    public Text PlayerLap3;
    public Text PlayerStatus;

    public Text AIPlayer1Name;
    public Text AIPlayer1FinishPos;
    public Text AIPlayer1Lap1;
    public Text AIPlayer1Lap2;
    public Text AIPlayer1Lap3;
    public Text AIPlayer1Status;

    public Text AIPlayer2Name;
    public Text AIPlayer2FinishPos;
    public Text AIPlayer2Lap1;
    public Text AIPlayer2Lap2;
    public Text AIPlayer2Lap3;
    public Text AIPlayer2Status;

    public Text AIPlayer3Name;
    public Text AIPlayer3FinishPos;
    public Text AIPlayer3Lap1;
    public Text AIPlayer3Lap2;
    public Text AIPlayer3Lap3;
    public Text AIPlayer3Status;

    public Text AIPlayer4Name;
    public Text AIPlayer4FinishPos;
    public Text AIPlayer4Lap1;
    public Text AIPlayer4Lap2;
    public Text AIPlayer4Lap3;
    public Text AIPlayer4Status;

    public void Start()
    {
        Path = Application.streamingAssetsPath+"/Resources/Results.json";
        string Data = File.ReadAllText(Path);
        Info = JsonUtility.FromJson<RacerResults>(Data);
        PlayerName.text = Info.RaceInfo[0].Name;
        PlayerFinishPos.text = Info.RaceInfo[0].Position;
        PlayerLap1.text = Info.RaceInfo[0].LapTimes[0];
        PlayerLap2.text = Info.RaceInfo[0].LapTimes[1];
        PlayerLap3.text = Info.RaceInfo[0].LapTimes[2];
        PlayerStatus.text = Info.RaceInfo[0].Status;

        AIPlayer1Name.text = Info.RaceInfo[1].Name;
        AIPlayer1FinishPos.text = Info.RaceInfo[1].Position;
        AIPlayer1Lap1.text = Info.RaceInfo[1].LapTimes[0];
        AIPlayer1Lap2.text = Info.RaceInfo[1].LapTimes[1];
        AIPlayer1Lap3.text = Info.RaceInfo[1].LapTimes[2];
        AIPlayer1Status.text = Info.RaceInfo[1].Status;

        AIPlayer2Name.text = Info.RaceInfo[2].Name;
        AIPlayer2FinishPos.text = Info.RaceInfo[2].Position;
        AIPlayer2Lap1.text = Info.RaceInfo[2].LapTimes[0];
        AIPlayer2Lap2.text = Info.RaceInfo[2].LapTimes[1];
        AIPlayer2Lap3.text = Info.RaceInfo[2].LapTimes[2];
        AIPlayer2Status.text = Info.RaceInfo[2].Status;

        AIPlayer3Name.text = Info.RaceInfo[3].Name;
        AIPlayer3FinishPos.text = Info.RaceInfo[3].Position;
        AIPlayer3Lap1.text = Info.RaceInfo[3].LapTimes[0];
        AIPlayer3Lap2.text = Info.RaceInfo[3].LapTimes[1];
        AIPlayer3Lap3.text = Info.RaceInfo[3].LapTimes[2];
        AIPlayer3Status.text = Info.RaceInfo[3].Status;

        AIPlayer4Name.text = Info.RaceInfo[4].Name;
        AIPlayer4FinishPos.text = Info.RaceInfo[4].Position;
        AIPlayer4Lap1.text = Info.RaceInfo[4].LapTimes[0];
        AIPlayer4Lap2.text = Info.RaceInfo[4].LapTimes[1];
        AIPlayer4Lap3.text = Info.RaceInfo[4].LapTimes[2];
        AIPlayer4Status.text = Info.RaceInfo[4].Status;

    }
}
