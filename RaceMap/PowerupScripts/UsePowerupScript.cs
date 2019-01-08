using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UsePowerupScript : MonoBehaviour {

    public GameObject Owner;
    public GameObject PowerupIcon;
    public Image Image;
    

    public void UsePowerup()
    {     
        Owner.GetComponent<PlayerInformationScript>().Pickup.GetComponent<PowerupScript>().Use();

        if (Owner.GetComponent<PlayerInformationScript>().Pickup.GetComponent<PowerupScript>().Uses == 0)
        {
            Destroy(Owner.GetComponent<PlayerInformationScript>().Pickup.gameObject);
            if(Owner.name == "Player")
            {
                PowerupIcon.SetActive(false);
                gameObject.SetActive(false); //disable use button
            }
           
        }
       
    }
}
