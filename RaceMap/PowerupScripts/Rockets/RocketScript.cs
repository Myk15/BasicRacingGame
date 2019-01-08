using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketScript : MonoBehaviour
{
    public GameObject Owner;
    public float speed;
    public int Damage;
    public float ActiveTime;
    public float TimeTillEnd;

    public void Start()
    {
        TimeTillEnd = 5;
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if ((other.GetComponent<Collider>().name != Owner.name) && (other.GetComponent<Collider>().tag == "Player"))
        {
            Debug.Log("Hit +" + other.name);
            other.GetComponent<PlayerInformationScript>().TakeDamage(Damage);
            Destroy(gameObject);
        }
    }

    public void FixedUpdate()
    {
        gameObject.GetComponent<Rigidbody>().AddForce(Owner.transform.forward * speed, ForceMode.Impulse);
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
