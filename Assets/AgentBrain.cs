using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentBrain : MonoBehaviour
{
    public float seekWeight = 0.01f;
    public float separationWeight = 200.0f;
    public float cohesionWeight = 0.2f;
    public float sepSteeringLinear;
    public float sepSteeringAngular;
    public float seekSteeringLinear;
    public float seekSteeringAngular;
    public float cohSteeringLinear;
    public float cohSteeringAngular;

    public GameObject target;
    public Agent agent;
    public Vector3 destination;
    public float maxSpeed = 30.0f;
    public float maxAcceleration = 30.0f;
    public float maxRotation = 5.0f;
    public float maxAngularAcceleration = 5.0f;
    public bool separate;
    public bool seek;
    public bool flock;

    public float separationDistance = 10.0f;
    public List<GameObject> targets;

    public AgentState state;

    public enum AgentState 
    { 
        Seek,
        Separate,
        Flock
    }


    // Start is called before the first frame update
    void Start()
    {
        agent = gameObject.GetComponent<Agent>();
        targets = GameObject.Find("Lead").GetComponent<AgentParent>().agents;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeState(state);
        agent.UpdateSteering(GetSeekSteering(), GetCohSteering(), GetSepSteering(), seekWeight, separationWeight, cohesionWeight);
    }

    public Steering GetSeekSteering()
    {
        //if (separate)
        //{
        //    Steering steering = new Steering();

        //    //steering.linear = transform.position - target.transform.position;
        //    //steering.linear.Normalize();
        //    //steering.linear = steering.linear * agent.maxAccleration;
        //    return steering;
        //}
 
        
            Steering seekSteering = new Steering();
            seekSteering.linear = target.transform.position - transform.position;
            seekSteering.linear.Normalize();
            seekSteering.linear = seekSteering.linear * agent.maxAccleration;   
            return (seekSteering);
    }

    public Steering GetSepSteering()
    {
        Steering sepSteering = new Steering();

        int sepCount = 0;
        float sepDistance;
        foreach (GameObject agents in targets)
        {
            if (agent != null)
            {
                sepDistance = (transform.position - agent.transform.position).magnitude;

                if (sepDistance > 0 && sepDistance < separationDistance)
                {
                    sepSteering.linear += agent.transform.position;
                    sepCount++;
                }
            }

            if (sepCount > 0)
            {
                sepSteering.linear /= sepCount;
                sepSteering.linear -= transform.position;
            }
        }
        return sepSteering;
    }

    public Steering GetCohSteering()
    {
        Steering cohSteering = new Steering();

        int cohCount = 0;
        float cohDistance;
        foreach (GameObject agents in targets)
        {
            if (agent != null)
            {
                cohDistance = (transform.position - agent.transform.position).magnitude;

                if (cohDistance > 0 && cohDistance < separationDistance)
                {
                    Vector3 away = transform.position - agent.transform.position;
                    away.Normalize();
                    away /= cohDistance;
                    cohSteering.linear += away;
                    cohCount++;
                }
            }

            if (cohCount > 0)
            {
                cohSteering.linear /= cohCount;
            }
        }
        return cohSteering;
    }

    public void ChangeState(AgentState state)
    {
        switch (state)
        {
            case AgentState.Seek:
                seek = true;
                separate = false;
                flock = false;
                break;

            case AgentState.Separate:
                seek = false;
                separate = true;
                flock = false;
                break;

            case AgentState.Flock:
                seek = false;
                separate = false;
                flock = true;
                break;
        }
    }
}
