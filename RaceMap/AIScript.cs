using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class AIScript : MonoBehaviour
{
    [System.Serializable]
    public struct Path
    {
        public int Index;
        public int TotalNodes;
        public List<GameObject> Nodes;
    }
    [System.Serializable]
    public class WC
    {
        public WheelCollider wheelFL;
        public WheelCollider wheelFR;
        public WheelCollider wheelRL;
        public WheelCollider wheelRR;
    }
    [System.Serializable]
    public class WT
    {
        public Transform wheelFL;
        public Transform wheelFR;
        public Transform wheelRL;
        public Transform wheelRR;
    }

    public WT tires;
    public WC wheels;

    public bool RaceStarted;
    public NavMeshAgent agent;
    
    public Path AIPath;
    public float MaxSpeed;
    public float DefaultMaxSpeed;
    public float CrusingSpeed;
    public float MPH;
    public bool IsBreaking;
    public float SlowingDistance;

    public bool Hit = false;

    public GameObject PathObject; //to get all of the child nodes from path

    public PlayerInformationScript Info;

    public bool DebugDraw;

    public float Distance;
    public float TimeBetweenUses = 3.0f;
    public Vector3 CurrentDestination;
    public Rigidbody rb;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        DefaultMaxSpeed = agent.speed;
        agent.speed = 0; //to stop agents moving at the beginning of the race
    }
    // Use this for initialization
    void Start()
    {
        RaceStarted = false;
        AIPath = new Path();
        AIPath.TotalNodes = 0;
        AIPath.Index = 0;
        AIPath.Nodes = new List<GameObject>();
        GetComponent<UsePowerupScript>().Owner = gameObject;

        rb = GetComponent<Rigidbody>();

        Info = GetComponent<PlayerInformationScript>();

        int ChildNodes = PathObject.transform.childCount; // get the total amount of path nodes

        for (int i = 0; i < ChildNodes; i++)
        {
            AIPath.Nodes.Add(PathObject.transform.GetChild(i).gameObject);
            ++AIPath.TotalNodes;
        }

        agent.destination = AIPath.Nodes[AIPath.Index].transform.position;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if(RaceStarted)
        {
            Move();
            Breaking();
            AllignWheels();

            if (DebugDraw)
            {
                DrawDebugLine();
            }
        }
        
    }

    private void Update()
    {
        if (Info.Pickup != null)
        {
            
            if (Info.Pickup.GetComponent<PowerupScript>().Uses == 1)
            {
                GetComponent<UsePowerupScript>().UsePowerup(); //use it straight away;
            }
            else CalculateUse();
        }
    }

    public void CalculateUse() //We check to see if any have been used already, if they have do it
                               //If they have check to see if we are a good distance apart before we use another to ensure they dont stack
    {
        GameObject Pickup = GetComponent<PlayerInformationScript>().Pickup;

        if (Time.time >= TimeBetweenUses + 1)
        {
            GetComponent<UsePowerupScript>().UsePowerup();
            TimeBetweenUses = Time.time;
        }
    }


public void Move()
    {
        MPH = Mathf.Round(GetComponent<NavMeshAgent>().velocity.magnitude * 2.23693629f);
        Distance = CalculateDistance(rb.transform.position, AIPath.Nodes[AIPath.Index].transform.position);
        if (Distance < 1)
        {
            if (AIPath.Index != AIPath.TotalNodes - 1)
            {
                AIPath.Index++;    
            }
            else
             AIPath.Index = 0;
        }
        agent.destination = AIPath.Nodes[AIPath.Index].transform.position;
      

       
    }

    public void Breaking()
    {
        float Distance = Vector3.Distance(gameObject.transform.position, AIPath.Nodes[AIPath.Index].transform.position);
         SlowingDistance = MPH + (MPH / 2f);
        if (Distance < SlowingDistance)
        {
            ReduceSpeed();
        }
        else if(gameObject.GetComponent<NavMeshAgent>().speed < DefaultMaxSpeed) //if we arent slowing down make sure we are going max speed
            Accelerate();
    }

    public float CalculateDistance(Vector3 a, Vector3 b)
    {
        float D = new float();
        return D = Vector3.Distance(a, b);
    }

    public void DrawDebugLine()
    {
        Debug.DrawLine(rb.transform.position, AIPath.Nodes[AIPath.Index].transform.position);
    }

    public void Accelerate()
    {
        
        gameObject.GetComponent<NavMeshAgent>().speed = DefaultMaxSpeed;
    }

    public void ReduceSpeed()
    {
        float speed = gameObject.GetComponent<NavMeshAgent>().speed;
        if(speed > CrusingSpeed)
        {
            speed = speed - 1.0f;
            gameObject.GetComponent<NavMeshAgent>().speed = speed;
        }      
    }

    public void IncreaseSpeed()
    {
        float speed = DefaultMaxSpeed; //we only want double max speed, we dont want the agent to be able to double it again
        speed= speed * 2;
        gameObject.GetComponent<NavMeshAgent>().speed = speed;
    }

    public void HitByPowerup(bool hit)
    {
        Hit = hit;
    }

    public void StartRacing() //as navmeshagent runs independantly
    {
        gameObject.GetComponent<NavMeshAgent>().speed = DefaultMaxSpeed;
        RaceStarted = true;
    }

    void AllignWheels()
    {
        //allign the wheel objs to their colliders

        Quaternion quat;
        Vector3 pos;
        wheels.wheelFL.GetWorldPose(out pos, out quat);
        tires.wheelFL.position = pos;
        tires.wheelFL.rotation = quat;

        wheels.wheelFR.GetWorldPose(out pos, out quat);
        tires.wheelFR.position = pos;
        tires.wheelFR.rotation = quat;

        wheels.wheelRL.GetWorldPose(out pos, out quat);
        tires.wheelRL.position = pos;
        tires.wheelRL.rotation = quat;

        wheels.wheelRR.GetWorldPose(out pos, out quat);
        tires.wheelRR.position = pos;
        tires.wheelRR.rotation = quat;
    }
}
