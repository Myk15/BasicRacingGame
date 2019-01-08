using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketSpawnerScript : PowerupScript
{
    public GameObject Rocket;
    private GameObject Inst; //So we can identify owner
    private Vector3 rotation;

    public RocketSpawnerScript()
    {
        Uses = 1;
    }

    public override void Use()
    {
        if (GetOwner().transform.localEulerAngles.y > 90)
        {
            rotation = Vector3.left;
        }

        else {
            rotation = Vector3.right;
        }

        Inst = Instantiate(Rocket, GetOwner().transform.forward, GetOwner().transform.rotation);
        Inst.GetComponent<RocketScript>().Owner = GetOwner();
        Inst.name = GetOwner().name + " Rocket";


        Inst.transform.Rotate(rotation, 90.0f);
        Uses--;

    }
}
