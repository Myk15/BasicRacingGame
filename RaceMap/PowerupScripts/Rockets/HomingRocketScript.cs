using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingRocketScript : MonoBehaviour
{
    public GameObject Owner;
    public GameObject Target;
    public float speed;
    public int Damage;
    private Vector3 Direction;
    public float ActiveTime;
    public float TimeTillEnd;
    public bool FoundTarget;

    public void Start()
    {
        TimeTillEnd = 10;
        FoundTarget = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.GetComponent<Collider>().name != Owner.name) && (other.GetComponent<Collider>().tag == "Player"))
        {
            Target = other.GetComponent<Collider>().gameObject;
            FoundTarget = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.GetComponent<Collider>().name != Owner.name) && (collision.gameObject.GetComponent<Collider>().tag == "Player"))
        {
            collision.gameObject.GetComponent<PlayerInformationScript>().TakeDamage(Damage);
            Destroy(gameObject);
        }
    }

    public void FixedUpdate()
    {
        if (FoundTarget)
        {
            Direction = Target.transform.position - gameObject.transform.position;
            gameObject.GetComponent<Rigidbody>().AddForce(Direction * speed, ForceMode.Impulse);
        }
        else
        {
            gameObject.transform.position = Owner.transform.position + ((Vector3.up) * 3);
        }
       
    }

    public void Update()
    {
        if(FoundTarget)
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
}
