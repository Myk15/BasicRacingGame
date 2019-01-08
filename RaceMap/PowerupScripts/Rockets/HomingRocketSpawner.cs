using UnityEngine;

public class HomingRocketSpawner : PowerupScript
{
    public GameObject HomingRocket;
    private GameObject Inst; //So we can identify owner
    private Vector3 rotation;
   
    public HomingRocketSpawner()
    {
        Uses = 1;
    }

    public override void Use()
    {
        if (GetOwner().transform.localEulerAngles.y > 90)
        {
            rotation = Vector3.left;
        }

        else
        {
            rotation = Vector3.right;
        }

        Inst = Instantiate(HomingRocket, GetOwner().transform.position + (Vector3.up), Quaternion.identity);
        Inst.GetComponent<HomingRocketScript>().Owner = GetOwner();
        Inst.name = GetOwner().name + " HomingRocket";

        Inst.transform.Rotate(rotation, 90.0f);
        Uses--;

    }
}

