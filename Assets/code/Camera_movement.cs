using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;


public class Camera_movement : MonoBehaviour
{
    private float speed;
    [SerializeField] private float y_border;

    [SerializeField] private Transform target;
    // in constructor we set value of standard variables
    Camera_movement() 
    {
        speed = 0.015f;
    }

    // in Start we set value of specailized (unity) objects
    void Start() {}

    private void move()
    {
        if (target.position.x < 0 || target.position.x > 3) return;

        Vector3 new_position = new Vector3(target.position.x, target.position.y + y_border, -5f);
        transform.position = Vector3.Slerp(transform.position, new_position, speed);
    }

    //there we will call methods which are updating every frame
    void Update()
    {
        move();
    }
}
