using System;
using UnityEngine;

public class PlayerInformationScript : MonoBehaviour {

    public string Status; //racing/DNA/Finished
    public bool Alive;

    public int ShieldHealth;
    public int MaxHealth;
    public int Lap;
    public string[] LapTimes;

    public string[] Positions;
    public string CurrentPosition;

    public bool Checkpoint1; //if player has hit this position
    public bool Checkpoint2;
    public bool Checkpoint3;
    public bool Checkpoint4;

    private int MinuteCount;
    private int SecondCount;
    private float MilliCount;

    public GameObject Pickup;
    public GameObject RaceController;
    public float[] CheckpointTimes;


    public void Awake()
    {
        SetupDefaultLapTimes();
        Status = "Still Racing";
        Alive = true;
        SetupPositions();
        RaceController = GameObject.Find("RaceController");
        MaxHealth = ShieldHealth;
        CheckpointTimes = new float[4];
    }

    public void SetupPositions()
    {
        Positions = new string[5];
        Positions[0] = "1st";
        Positions[1] = "2nd";
        Positions[2] = "3rd";
        Positions[3] = "4th";
        Positions[4] = "5th";
    }
    public void SetupDefaultLapTimes()
    {
        LapTimes = new string[3];
        LapTimes[0] = "--:--:----";
        LapTimes[1] = "--:--:----";
        LapTimes[2] = "--:--:----";
    }

    public int GetPositionIndex()
        {
       int index = Array.IndexOf(Positions, CurrentPosition);
        return index;
    }

    public void Update()
    {
        if(Alive)
        {
            ShouldWeBeDead();
        }
        

        MilliCount += Time.deltaTime * 10;
        if (MilliCount >= 10)
        {
            MilliCount = 0;
            SecondCount += 1;
        }

        if (SecondCount >= 60)
        {
            SecondCount = 0;
            MinuteCount += 1;
        }
    }

    public void SetCheckpointTimes(string Name)
    {
        if (Name == "Checkpoint1")
        {
            Checkpoint1 = true;
            CheckpointTimes[0] = MinuteCount + SecondCount + MilliCount;
        }
        else if (Name == "Checkpoint2")
        {
            Checkpoint2 = true;
            CheckpointTimes[1] = MinuteCount + SecondCount + MilliCount;
        }
        else if (Name == "Checkpoint3")
        {
            Checkpoint3 = true;
            CheckpointTimes[2] = MinuteCount + SecondCount + MilliCount;
        }
        else if (Name == "Checkpoint4")
        {
                Checkpoint4 = true;
                CheckpointTimes[3] = MinuteCount + SecondCount + MilliCount;
        }
    }

    public void CompletedLap() //called when the startfinish line is crossed and Checkpoint 4 has been it
    {
        LapTimes[Lap - 1] = MinuteCount+":" + SecondCount+":" + MilliCount;
            MinuteCount =0;
            SecondCount =0;
            MilliCount = 0;
            Checkpoint1 = false;
            Checkpoint2 = false;
            Checkpoint3 = false;
            Checkpoint4 = false;
            

        if(RaceController.GetComponent<RaceController>().GetMaxLaps() == Lap) //if we have finished
        {
            Status = "Finished";
        }
        IncreaseLapCounter();
    }
    public void IncreaseLapCounter()
    {
        ++Lap;
    }

    public void SetPowerup(GameObject p)
    {
        Pickup = p;
        p.GetComponent<PowerupScript>().SetOwner(gameObject);
    }

    public void TakeDamage(int a)
    {
        ShieldHealth -= a;
    }

    public void IncreaseHealth(int a)
    {
        if (a + ShieldHealth < MaxHealth)
        { ShieldHealth = ShieldHealth + a; }
        else ShieldHealth = MaxHealth;
    }

    public bool ShouldWeBeDead()
    {
        if (ShieldHealth <= 0)
        {
            Alive = false;
            Status = "Died on Lap " + Lap;
            CurrentPosition = "DNA";
            gameObject.SetActive(false);
            RaceController.GetComponent<RaceController>().APlayerDied(gameObject,GetPositionIndex());
           
            return true;
        }
        return false;
    } 

    public void SetPosition(int i)
    {
        if(Alive)
        CurrentPosition = Positions[i];
    }
}
