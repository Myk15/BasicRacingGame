using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public int Damage;
    public float ActiveTime;
    public float TimeTillEnd;

    public void Start()
    {
        TimeTillEnd = 3;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if((collision.collider.name == "Grass" )|| (collision.collider.name =="Track")) //if this bullet hits the ground
            {
            Destroy(gameObject);
        }

        if (collision.collider.tag == "Player")
        {
                collision.collider.GetComponent<PlayerInformationScript>().TakeDamage(Damage);     
        }
    }

    public void Update()
    {
        if (Time.time >= ActiveTime + 1)
        {
            TimeTillEnd--;
            ActiveTime = Time.time;
        }
        if (TimeTillEnd <= 0)
        { Destroy(gameObject); }
    }
}
