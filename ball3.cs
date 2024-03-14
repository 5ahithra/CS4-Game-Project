using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball3 : MonoBehaviour
{
    public Vector2 initialVelocity;
    public Rigidbody2D rigidBody;
    private Transform transform;
    private Vector3 initialPosition;

    // Start is called before the first frame update
    void Start()
    {
        initialVelocity *= 2.5f;
        rigidBody.velocity = new Vector3(initialVelocity.x, initialVelocity.y, 0);
        transform = this.gameObject.GetComponent<Transform>();
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Bounce")
        {
            MoveClockwise();
        }
        if (target.name == "ball reflect up") {
            print(transform.position.x - 1.283073f);
            print(0.7810476 + transform.position.y);
            print(transform.position);
        }
    }

    void MoveClockwise()
    {
        float initial = Math.Abs(initialVelocity.x + initialVelocity.y);
        Vector3 velocity = new Vector3(0, 0, 0);
        if (rigidBody.velocity.x > 0) {
            velocity.x = 0;
            velocity.y = -1f * initial;
        }
        if (rigidBody.velocity.x < 0) {
            velocity.x = 0;
            velocity.y = initial;
        }
        if (rigidBody.velocity.y > 0) {
            velocity.y = 0;
            velocity.x = initial;
        }
        if (rigidBody.velocity.y < 0) {
            velocity.y = 0;
            velocity.x = -1f * initial;
        }
        rigidBody.velocity = velocity;
    }
}
