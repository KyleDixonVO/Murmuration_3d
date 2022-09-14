using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    public GameObject focus;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (focus.transform.position.x > this.gameObject.transform.position.x)
        {
            this.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(10.0f, 0, 0));
        }
        else if (focus.transform.position.x < this.gameObject.transform.position.x)
        {
            this.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(-10.0f, 0, 0));
        }

        if (focus.transform.position.y > this.gameObject.transform.position.y)
        {
            this.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 10.0f, 0));
        }
        else if (focus.transform.position.y < this.gameObject.transform.position.y)
        {
            this.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, -10.0f, 0));
        }

        if (focus.transform.position.z > this.gameObject.transform.position.z)
        {
            this.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, 10.0f));
        }
        else if (focus.transform.position.z < this.gameObject.transform.position.z)
        {
            this.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, -10.0f));
        }
    }
}
