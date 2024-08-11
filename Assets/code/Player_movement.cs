using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;


public class Player_movement : MonoBehaviour
{
    private short direction;
    private float speed;
    private bool facing_right;    

    // in constructor we set value of standard variables
    Player_movement()
    {
        direction = 0;
        speed = 0.015f;
        facing_right = true;
    }

    // in Start we set value of specailized (unity) objects
    private void Start() {}

    private void move()
    {
        Vector2 position = transform.position;
        position.x += speed * direction;
        transform.position = position;
    }

    private void flip()
    {
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    private void cath_keys()
    {
        if (Keyboard.current.aKey.isPressed)
        {
            if (facing_right) flip();

            direction = -1;

            move();

            facing_right = false;
        }

        else if (Keyboard.current.dKey.isPressed)
        {
            if (!facing_right) flip();

            direction = 1;

            move();

            facing_right = true;
        }

        else direction = 0;
    }
    
    private void set_animation()
    {
        if (direction == 0) GetComponent<Animator>().SetBool("is_running", false);
        
        else GetComponent<Animator>().SetBool("is_running", true); 
    }

    //there we will call methods which are updating every frame
    private void Update()
    {
        cath_keys();
        set_animation();
    }
};