using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : PowerupScript
{
    public int Health;
    public override void Use()
    {
        Debug.Log(GetOwner().name + "Used Health pickup");
        GetOwner().GetComponent<PlayerInformationScript>().IncreaseHealth(Health);
        --Uses;
    }
}
