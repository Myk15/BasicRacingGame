using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePositionScript : MonoBehaviour
{
    private RaceController RaceController;
    public void Start()
    {
        RaceController = GameObject.Find("RaceController").GetComponent<RaceController>();
    }
    /*This script changes the position of the player */
    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().tag == "Player")
        {
            RaceController.ChangePosition(gameObject, other.gameObject);
        }
    }
}
