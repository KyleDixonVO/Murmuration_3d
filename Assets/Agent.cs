using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    bool shouldTurn = false;
    private AgentParent parent;
    public static float avoidanceDistance = 3.0f;
    public float maxSpeed = 5.0f;
    public float speed = 1.0f;
    float rotationSpeed = 4.0f;

    float neighborDistance;

    // Start is called before the first frame update
    void Start()
    {
        parent = GameObject.Find("Lead").GetComponent<AgentParent>();
        speed = Random.Range(1, maxSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, Vector3.zero) >= parent.spawnMax)
        {
            shouldTurn = true;
        }
        else
        {
            shouldTurn = false;
        }


        if (shouldTurn)
        {
            Vector3 direction = Vector3.zero - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
            speed = Random.Range(1, maxSpeed);
        }
        else
        {
                Flock();
        }


        transform.Translate(0, 0, Time.deltaTime * speed);
    }

    void Flock()
    {
        List<GameObject> agents;
        agents = parent.agents;

        Vector3 agentCenter = Vector3.zero;
        Vector3 avoidance = Vector3.zero;
        float flockSpeed = 1.0f;

        Vector3 target = parent.targetPos;

        float dist;

        int numberOfNeighbors = 0;

        foreach (GameObject agent in agents)
        {
            if (agent != this.gameObject)
            {
                dist = Vector3.Distance(agent.transform.position, this.transform.position);
                if (dist <= neighborDistance)
                {
                    agentCenter += agent.transform.position;
                    numberOfNeighbors++;

                    if (dist < avoidanceDistance)
                    {
                        avoidance += (this.transform.position - agent.transform.position);
                    }

                    flockSpeed += agent.GetComponent<Agent>().speed;
                }
            }
        }

        if (numberOfNeighbors > 0)
        {
            agentCenter = agentCenter / numberOfNeighbors + (target - this.transform.position);
            speed = flockSpeed / numberOfNeighbors;

            Vector3 direction = (agentCenter + avoidance) - transform.position;
            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        this.gameObject.GetComponent<Renderer>().material.color = Color.yellow;
    }

}
