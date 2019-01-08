using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InkScript : MonoBehaviour
{
    public bool Active;
    public int TimeTillEnd;
    public float ActiveTime;
    public GameObject hitTarget;
    public int Damage;

    public void Start()
    {
        TimeTillEnd = 10;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().tag == "Player")
        {
            hitTarget = other.gameObject;
            Active = true;
        }
    }

    public void Update()
    {

        if (Active)
        {        
            if (Time.time >= ActiveTime + 1)
            {
                TimeTillEnd--;
                ActiveTime = Time.time;
                hitTarget.GetComponent<PlayerInformationScript>().TakeDamage(Damage);
            }

            if (TimeTillEnd <= 0)
            {
                if (hitTarget.name == "Player")
                { hitTarget.GetComponent<CarControlCS>().maxTorque = hitTarget.GetComponent<CarControlCS>().DefaultMaxTorque; }
                else
                { hitTarget.GetComponent<AIScript>().MaxSpeed = hitTarget.GetComponent<AIScript>().DefaultMaxSpeed; }
                Destroy(gameObject);
            }
        }
    }
}
