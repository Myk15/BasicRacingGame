using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedScript : PowerupScript
{
    public float ActiveTime;
    public int TimeTillEnd;
    public bool Active;


    public void Start()
    {
        TimeTillEnd = 10;
    }
    public override void Use()
    {
        Debug.Log(GetOwner().name+ "Used Speed pickup");

        if (GetOwner().name == "Player")
        {
           GetOwner().GetComponent<CarControlCS>().SetMaxTorque(GetOwner().GetComponent<CarControlCS>().maxTorque*2);
        }
        else
        {
            GetOwner().GetComponent<AIScript>().IncreaseSpeed();
        }

        Active = true;
        Uses--;
    }

    public void Update()
    {

        if (Active)
        {
            if (Time.time >= ActiveTime + 1)
            {
                TimeTillEnd--;
                ActiveTime = Time.time;
            }

            if (TimeTillEnd <= 0)
            {

                if (GetOwner().name == "Player")
                {
                    GetOwner().GetComponent<CarControlCS>().SetMaxTorque(GetOwner().GetComponent<CarControlCS>().DefaultMaxTorque);
                }
                else
                {
                    GetOwner().GetComponent<AIScript>().MaxSpeed = GetOwner().GetComponent<AIScript>().DefaultMaxSpeed;
                }
                Destroy(gameObject);

            }
        }
    }
}
