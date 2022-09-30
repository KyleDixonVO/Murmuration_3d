using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentParent : MonoBehaviour
{
    public GameObject target;
    public GameObject agentPrefab;
    public List<GameObject> agents;
    public int numberOfAgents = 10;
    // Start is called before the first frame update
    void Start()
    {
        agents = new List<GameObject>();
        
        for (int i = 0; i < numberOfAgents; i++)
        {
            Vector3 spawnPoint = new Vector3(Random.Range(-20, 20), 0, Random.Range(-20, 20));
            GameObject instance = Instantiate(agentPrefab, transform.position + spawnPoint, transform.rotation);
            instance.GetComponent<AgentBrain>().target = target;
            agents.Add(instance);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += (target.transform.position - transform.position).normalized * Time.deltaTime * 5.0f;
    }
}
