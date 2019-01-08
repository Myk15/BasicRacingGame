using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GivePowerupScript : MonoBehaviour {

    public GameObject Button;
    public GameObject Pickups;
    public GameObject Inst;

    public Image Image;
    public GameObject PowerupIconCanvas;

    public int RespawnTimer; //How long Till Respawn
    public int TimeTillRespawn;
    public float TimeSpawn;
    public bool Disabled = false;

    private void Start()
    {
        Random.InitState(System.Environment.TickCount);
        TimeTillRespawn = 5;
        RespawnTimer = TimeTillRespawn;
        TimeSpawn = Time.time;
    }

    //gives the player a random pickup and enables the use button

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().tag == "Player")
        {
                int PickupNum = Pickups.transform.childCount;

                int Ran = Random.Range(0, PickupNum);

                if (other.GetComponent<Collider>().GetComponent<PlayerInformationScript>().Pickup == null)
                {
                    //Instantiate a copy of whatever was chosen, set the powerup and enable to use button
                    Inst = Instantiate(Pickups.transform.GetChild(Ran).gameObject);
                    string name = Pickups.transform.GetChild(Ran).gameObject.name;
                    Inst.name = other.GetComponent<Collider>().name + " " + Pickups.transform.GetChild(Ran).gameObject.name;
                    other.GetComponent<Collider>().GetComponent<PlayerInformationScript>().SetPowerup(Inst); // assign a pickup to player

                    if (other.GetComponent<Collider>().GetComponent<Collider>().name == "Player") //if it's the player then update UI
                    {
                        Image = PowerupIconCanvas.transform.GetChild(0).GetComponent<Image>();
                        PowerupIconCanvas.transform.GetChild(1).GetComponent<Text>().text = name;
                        Image.sprite = other.GetComponent<Collider>().GetComponent<PlayerInformationScript>().Pickup.GetComponent<PowerupScript>().Image;
                        PowerupIconCanvas.SetActive(true);
                        Button.SetActive(true);
                    }
                    Debug.Log(Pickups.transform.GetChild(Ran).gameObject.name + " " + other.gameObject.name);
                    Disabled = true;
                    EnableOrDisablePickup(false);
                }
        }
    }

        void Update () //we rotate the pickups if they arent disabled; if they are disabled countdown to they are re-enabled
    {
        if (!Disabled)
        {
            transform.Rotate(new Vector3(0,1,0),1f);
        }
        if (Disabled)
        {
            if  (Time.time >= TimeSpawn + 1)
            {
                RespawnTimer--;
                TimeSpawn = Time.time;
            }

            if (RespawnTimer <= 0)
            {
                EnableOrDisablePickup(true);
                Disabled = false;
                RespawnTimer = TimeTillRespawn;
            }
        }
	}

    public void EnableOrDisablePickup(bool O)
    {
        gameObject.GetComponent<Renderer>().enabled = O;
        gameObject.GetComponent<Collider>().enabled = O;
    }
}
