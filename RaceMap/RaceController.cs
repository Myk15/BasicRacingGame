using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RaceController : MonoBehaviour
{ //class updates race information for the player on the ui as well as if any player dies,
    //changes the positioning of the remaining player

    private bool Android; //temp fix so game runs
    
    private int LapCount;
    public int MaxLaps;
    public Text LapText;
    public Text MaxLapText;
    private int MinuteCount;
    private int SecondCount;
    private float MilliCount;

    private float currentSpeed;
    public Text MPH;

    public Text Checkpoint1Text;
    public Text Checkpoint2Text;
    public Text Checkpoint3Text;
    public Text Checkpoint4Text;

    public GameObject StartTimerPanel; //so we can disable it once game has started
    public Text CountdownText;

    private bool Started = false;
    private int TimeTillStart = 5;

    private GameObject player; //for reference to update ui
    public Text HealthText;
    public GameObject HealthBar;
    private int Health;
    private float EachBitOfHealth;
    private float Width;
    public Text Position;

    private List<GameObject> RaceDrivers; //so positing can be changed
    public GameObject CheckpointObject; //so we can get all checkpoints;
    private List<GameObject> Checkpoints;

    
    private void Awake() //who ever the camera is following update ui from them
    {
        player = GameObject.Find("Main Camera").GetComponent<CameraScript>().GetTarget().gameObject;
    }
    private void Start()
    {
#if UNITY_ANDROID
        Android = true;
#endif
        
        
        SetupCheckpointsAndDrivers();
        SetupUIDisplay();
    }

    private void SetupCheckpointsAndDrivers()
    {
        RaceDrivers = new List<GameObject>();
        Checkpoints = new List<GameObject>();
        foreach (GameObject Player in GameObject.FindGameObjectsWithTag("Player")) // find all players
        {
            RaceDrivers.Add(Player);

        } 
        for (int i = 0; i < CheckpointObject.transform.childCount; i++)
        {
            Checkpoints.Add(CheckpointObject.transform.GetChild(i).gameObject);
        }
    }

    private void SetupUIDisplay()
    {
        currentSpeed = Mathf.Round(player.GetComponent<Rigidbody>().velocity.magnitude * 2.23693629f);
        MPH.text = currentSpeed.ToString();
        CountdownText.text = TimeTillStart.ToString();
        ResetCheckpoints();

        LapCount = player.GetComponent<PlayerInformationScript>().Lap;
        LapText.text = LapCount.ToString();
        MaxLapText.text = MaxLaps.ToString();

        Width = HealthBar.transform.GetChild(1).GetComponent<RectTransform>().rect.width;
        Health = player.GetComponent<PlayerInformationScript>().ShieldHealth;
        EachBitOfHealth = Width / Health;
        HealthText.text = Health.ToString();
    }


    // Update is called once per frame
    void Update()
    {
        if(!player.GetComponent<PlayerInformationScript>().ShouldWeBeDead()) //if player isnt dead
        {
            UpdateUIInformation();
            if (Started)
            {
                currentSpeed = Mathf.Round(player.GetComponent<Rigidbody>().velocity.magnitude * 2.23693629f);
                MPH.text = currentSpeed.ToString();
            }
        }
        else
        {
            LoadResultsScene("ResultsScreen");
        }

        MilliCount += Time.deltaTime * 10;
        if (MilliCount >= 10)
        {
            MilliCount = 0;
            SecondCount += 1;

            if(!Started)
            {
                RaceStartCountDown();
            }
            
        }

        if (SecondCount >= 60)
        {
            SecondCount = 0;
            MinuteCount += 1;
        }
    }

    private void UpdateUIInformation()
    {
        Position.text = player.GetComponent<PlayerInformationScript>().CurrentPosition;
        Health = player.GetComponent<PlayerInformationScript>().ShieldHealth;
        float W = EachBitOfHealth * Health;
        float Y = HealthBar.transform.GetChild(1).GetComponent<RectTransform>().sizeDelta.y;
        HealthBar.transform.GetChild(1).GetComponent<RectTransform>().sizeDelta = new Vector2(W, Y);
        HealthText.text = Health.ToString();
    }

    public void UpdateLapInfo()
    {
        LapText.text = player.GetComponent<PlayerInformationScript>().Lap.ToString();
        ResetCheckpoints();
    }

    public void SetCheckpointTimes(string Name)
    {
        if (Name == "Checkpoint1")
        {
            Checkpoint1Text.text = MinuteCount.ToString("0") + ":" + SecondCount.ToString("00") + ":" + MilliCount.ToString("0.00");

        }
        else if (Name == "Checkpoint2")
        {
            Checkpoint2Text.text = MinuteCount.ToString("0") + ":" + SecondCount.ToString("00") + ":" + MilliCount.ToString("0.00");
        }
        else if (Name == "Checkpoint3")
        {
            Checkpoint3Text.text = MinuteCount.ToString("0") + ":" + SecondCount.ToString("00") + ":" + MilliCount.ToString("0.00");
        }
       else if (Name == "Checkpoint4")
        {
            Checkpoint4Text.text = MinuteCount.ToString("0") + ":" + SecondCount.ToString("00") + ":" + MilliCount.ToString("0.00");
                
        }

    }

    public void ResetCheckpoints()
    {
        MilliCount = 0;
        SecondCount = 0;
        MinuteCount = 0;
        Checkpoint1Text.text = "-:-:----".ToString();
        Checkpoint2Text.text = "-:-:----".ToString();
        Checkpoint3Text.text = "-:-:----".ToString();
        Checkpoint4Text.text = "-:-:----".ToString();

        if (PlayerFinishedRace()) //check if finished race
        {
            if (!Android)
            {
                WriteRaceInformationToFile File = new WriteRaceInformationToFile(RaceDrivers);
                LoadResultsScene("ResultsScreen");
            }

            else
            {
                LoadResultsScene("Thankyou");
            } 
        }
    }

    public bool PlayerFinishedRace()
    {
        if (player.GetComponent<PlayerInformationScript>().Lap == MaxLaps + 1) //need to finish final lap
        {
            return true;
        }

        else return false;
    }
    public void LoadResultsScene(string Name)
    {
            SceneManager.LoadScene(Name, LoadSceneMode.Single);  
    }

    public void RaceStartCountDown()
    {
        --TimeTillStart;
        CountdownText.text = TimeTillStart.ToString();

        if (TimeTillStart == 0)
        {
            StartRace();
        }
    }

    public void StartRace()
    {
        Started = true;
        for (int i = 0; i < RaceDrivers.Count; i++)
        {
            if(RaceDrivers[i].name == "Player")
            {
                RaceDrivers[i].GetComponent<CarControlCS>().RaceStarted = true;
            }
            else
            {
                RaceDrivers[i].GetComponent<AIScript>().StartRacing();
            }
        }
        Destroy(StartTimerPanel.gameObject); //we don't need it anymore as race has started
    }

    public GameObject GetUIOwner()
    {
        return player;
    }

    public int GetMaxLaps()
    {
        return MaxLaps;
    }

    public void APlayerDied(GameObject Player, int Pos) //someone died if anyone behind player need to update position
    {
        for(int i = 0; i < RaceDrivers.Count; i++)
        {
            if(RaceDrivers[i].name != Player.name) //if it the player
            {
                if(RaceDrivers[i].GetComponent<PlayerInformationScript>().Alive) //if they are alive
                {
                    int index = RaceDrivers[i].GetComponent<PlayerInformationScript>().GetPositionIndex();
                    //if they are behind change position
                    if (index > Player.GetComponent<PlayerInformationScript>().GetPositionIndex())
                    {
                        RaceDrivers[i].GetComponent<PlayerInformationScript>().SetPosition(--index);
                    }
                }
                
            }
        }
    }

    public void ChangePosition(GameObject OvertakingPlayer, GameObject CurrentPlayer)
    {
        int OvertakingLap = OvertakingPlayer.GetComponent<PlayerInformationScript>().Lap;
        int CurrentLap = CurrentPlayer.GetComponent<PlayerInformationScript>().Lap;

        //if both players are on the same map
        if(OvertakingLap == CurrentLap)
        {
            int OvertakingPlayerPos = OvertakingPlayer.GetComponent<PlayerInformationScript>().GetPositionIndex();
            int CurrentPlayerPos = CurrentPlayer.GetComponent<PlayerInformationScript>().GetPositionIndex();

            if (OvertakingPlayerPos > CurrentPlayerPos) //if they are behind the player
            {
                OvertakingPlayer.GetComponent<PlayerInformationScript>().SetPosition(--OvertakingPlayerPos);
                CurrentPlayer.GetComponent<PlayerInformationScript>().SetPosition(++CurrentPlayerPos);
            }
        }
       
        


    }
}
