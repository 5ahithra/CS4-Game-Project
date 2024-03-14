using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball4 : MonoBehaviour
{
    public float angle;
    public Transform transform;
    public Transform parentTransform;
    private float radius;
    // Start is called before the first frame update
    void Start()
    {
        transform = GetComponent<Transform>();
        Vector3 parentDistance = transform.position - parentTransform.position;
        radius = (float)Math.Sqrt(Math.Pow(parentDistance.x, 2f) + Math.Pow(parentDistance.y, 2f));
        angle = (float)(Math.Atan(parentDistance.y/parentDistance.x));
        if (angle < 0) {
            if (parentDistance.x < 0) {
                angle += (float)Math.PI;
            }
            if (parentDistance.y < 0) {
                angle += (float)(2 * Math.PI);
            }
            //else angle += (float)(2 * Math.PI);
        } else {
            if (parentDistance.x < 0) {
                angle += (float)Math.PI;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        angle -= (float)(0.6 * Math.PI * Time.deltaTime);
        if (angle < 0) {
            angle = (float)(2 * Math.PI);
        }
        transform.position = new Vector3(radius * (float)Math.Cos(angle) + 0.1753073f, radius * (float)Math.Sin(angle) - 10.82f);
    }
}
