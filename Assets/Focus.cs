using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Focus : MonoBehaviour
{
    public float timer;
    public bool timerFinished;
    // Start is called before the first frame update
    void Start()
    {
        timerFinished = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerFinished)
        {
            transform.position = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10));
            timerFinished = false;
        }
        else if (timer <= 0)
        {
            timer = 10;
        }

        if (timer <= 0)
        {
            timerFinished = true;
        }
        else
        {
            timer -= Time.deltaTime;
        }
        
    }
}
