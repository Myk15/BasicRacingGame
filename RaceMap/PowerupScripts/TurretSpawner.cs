using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSpawner : PowerupScript
{
    public GameObject Turret;
    private GameObject Tut; //So we can identify owner
    public TurretSpawner()
    {
        Uses = 1;
    }
    public override void Use()
    { 
            Tut = Instantiate(Turret, GetOwner().transform.position + (Vector3.forward) * 3, Quaternion.identity);
            Tut.GetComponent<TurretScript>().Owner = GetOwner();
            Tut.name = GetOwner().name + " Turret";
            Uses--;
        
    }
}
