using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpController : MonoBehaviour
{
    // Start is called before the first frame update
    public float Speed;
    public float AngularSpeed;
    protected Rigidbody r;
    void Start()
    {
        r = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Speed = r.velocity.magnitude;
        AngularSpeed = r.angularVelocity.magnitude;

        //r.AddForce(Vector3.forward);
        if (r.position.y > -2.06)
        {
            r.AddForce(Physics.gravity);
            r.drag = 1;
        }
        else
        {
            r.AddForce(-1 * Physics.gravity);
            r.drag = 1;
        }
    }
}
