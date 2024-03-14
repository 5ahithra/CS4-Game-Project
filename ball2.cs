using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball2 : MonoBehaviour
{

    public double initialVelocity;
    public Rigidbody2D rigidBody;
    public Transform transform;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody.velocity = new Vector3(0, (float)initialVelocity, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D target) {
        if (target.tag == "Bounce") {
            rigidBody.velocity *= -1;
        }
    }
}
