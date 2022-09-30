using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{

    public float maxSpeed = 5.0f;
    public float speedCap = 5.0f;
    public float maxAccleration = 20.0f;

    public float yRotation;
    public float rotation;

    public float yRotationMin = 0.0f;
    public float yRotationMax = 360.0f;

    public float maxSingleFrameRotation = 30.0f;
    public float maxSingleFrameAngularAcceleration = 30.0f;




    public Vector3 velocity;
    public Steering steering;

    // Start is called before the first frame update
    void Start()
    {
        velocity = Vector3.zero;
        steering = new Steering();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        Vector3 acceleration = velocity * Time.deltaTime;

        if (yRotation < yRotationMin)
        {
            yRotation = yRotationMax;
        }
        else if (yRotation > yRotationMax)
        {
            yRotation = yRotationMin;
        }

        transform.Translate(acceleration, Space.World);
        transform.Rotate(Vector3.up, yRotation);
    }

    public virtual void LateUpdate()
    {
        velocity += steering.linear * Time.deltaTime;
        rotation += steering.angular * Time.deltaTime;

        if (velocity.magnitude > maxSpeed)
        {
            velocity.Normalize();
            velocity = velocity * maxSpeed;
        }

        steering = new Steering();
    }

    public void UpdateSteering(Steering seekSteering, Steering cohSteering, Steering sepSteering, float seekWeight, float sepWeight, float cohWeight)
    {
        this.steering.linear += (((seekWeight * seekSteering.linear) + (sepWeight * sepSteering.linear) + (cohWeight * cohSteering.linear)) / 3);
        this.steering.angular += (((seekWeight * seekSteering.angular) + (sepWeight * sepSteering.angular) + (cohWeight * cohSteering.angular)) / 3);
    }

    public void ResetSpeed()
    {
        maxSpeed = speedCap;
    }
}
