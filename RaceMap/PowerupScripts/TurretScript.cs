using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
{
    public GameObject Owner;
    public bool Active;
    public int TimeTillEnd;
    public float ActiveTime;
    public List<GameObject> Targets; //list of targets
    private bool AlreadyATarget;
    public GameObject Target;
    public float Temp = 1000f; //Temp Distance check to player
    public float Dist;
    public GameObject Bullet;
    public GameObject BulletInst;
    private float Noise; //add noise to bullets
    public Vector3 TargetPos;
    public float Magnitude; // bullet speed;

    public bool DrawLOS;

    private int TurretNum;

    public void Start()
    {
        Random.InitState(System.Environment.TickCount);
        Targets = new List<GameObject>();
        TurretNum = transform.childCount;
        TimeTillEnd = 15;
        Dist = Temp;
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.GetComponent<Collider>().tag == "Player") && (Owner.name != other.name)) //not who owns turret;
        {
            for (int i = 0; i < Targets.Count; i++)
            {
                if (other.name == Targets[i].name)
                {
                    AlreadyATarget = true;
                }
            }
            if (AlreadyATarget != true)
            {
                Targets.Add(other.gameObject);
            }
            Active = true;
        }
    }

    private void OnTriggerExit(Collider other) // remove once out of range;
    {
        if ((other.GetComponent<Collider>().tag == "Player") && (Owner.name != other.name)) //not who owns turret;
        {
            for (int i = 0; i < Targets.Count; i++)
            {
                if (other.name == Targets[i].name)
                {
                    Targets.Remove(Targets[i]);
                }
            }
            if (Targets.Count == 0)
            {
                Target = null;
                Active = false;
            }
        }
    }

    public void Update()
    {
        gameObject.transform.position = Owner.transform.position + Vector3.up;
        if (Active)
        {
            for (int i = 0; i < Targets.Count; i++)
            {
                Temp = Vector3.Distance(Targets[i].transform.position, gameObject.transform.position); // loop and check who is closest to turret
                if (Temp <=Dist)
                {
                    Target = Targets[i].gameObject;
                    Dist = Temp;
                }

                //add noise to x and y
                
                float RanX = Random.Range(0, 0.5f);
                float RanY = Random.Range(0, 0.5f);
                Vector3 Noise = new Vector3(RanX, RanY);
                TargetPos = (Target.transform.position + Noise) - gameObject.transform.position;
                TargetPos.Normalize();
                if (DrawLOS)
                {
                    Debug.DrawLine(gameObject.transform.position, TargetPos, Color.red);
                }
                gameObject.transform.LookAt(Target.transform);

                if (Time.time >= ActiveTime + 1)
                {
                    TimeTillEnd--;

                    for (int j = 0; j < TurretNum; j++)
                    {
                        if (Alive())
                        {
                            BulletInst = Instantiate(Bullet, gameObject.transform.GetChild(j).position, Quaternion.identity) as GameObject;
                            BulletInst.GetComponent<Rigidbody>().AddForce(TargetPos * Magnitude);
                            BulletInst.name = gameObject.name + " Bullet";
                        }
                        
                    }

                    ActiveTime = Time.time;
                }

                if (TimeTillEnd <= 0)
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    public bool Alive()
    {
        if(Target!=null)
        { return true; }
        else
        {
            Targets.Remove(Target);
                 return false;
        }
    }

}
