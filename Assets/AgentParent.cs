using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentParent : MonoBehaviour
{
    public GameObject agentPrefab;
    public List<GameObject> agents;
    public int numberOfAgents = 50;
    public float spawnMin = -20;
    public float spawnMax = 20;
    public Vector3 targetPos;
    // Start is called before the first frame update
    void Start()
    {
        agents = new List<GameObject>();
        
        for (int i = 0; i < numberOfAgents; i++)
        {
            Vector3 spawnPoint = new Vector3(Random.Range(spawnMin, spawnMax), Random.Range(spawnMin, spawnMax), Random.Range(spawnMin, spawnMax));
            GameObject instance = Instantiate(agentPrefab, spawnPoint, Quaternion.identity);
            agents.Add(instance);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Random.Range(0, 2000) < 50)
        {
            targetPos = new Vector3(Random.Range(spawnMin, spawnMax), Random.Range(spawnMin, spawnMax), Random.Range(spawnMin, spawnMax));
        }
    }
}
