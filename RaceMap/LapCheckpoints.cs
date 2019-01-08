using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LapCheckpoints : MonoBehaviour
{
    /*class deals with updating the checkpoints when they have been hit
     * checks to see if a lap has been completed and updates the race information for that player
    */
    private string Name;
    private GameObject RaceController;
    private PlayerInformationScript Player;
    private bool UpdateUI;

    private void Start()
    {
        UpdateUI = false;
        RaceController = GameObject.Find("RaceController").gameObject;
        Name = gameObject.name;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Player = other.GetComponent<PlayerInformationScript>();
            if (other.name == RaceController.GetComponent<RaceController>().GetUIOwner().name)
                UpdateUI = true;
            

            if (Name == "StartFinishLine")
            {
                if (Player.Checkpoint4 == true) //has a lap been completed
                {
                    Player.CompletedLap();
                    if (UpdateUI)
                        RaceController.GetComponent<RaceController>().UpdateLapInfo();
                }
            }
            else
            {
                other.GetComponent<PlayerInformationScript>().SetCheckpointTimes(Name);
                if (UpdateUI)
                    RaceController.GetComponent<RaceController>().SetCheckpointTimes(Name);
            }
        }
    }
}
        
        
    
