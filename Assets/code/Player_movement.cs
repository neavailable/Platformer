using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Player_movement : MonoBehaviour
{
    private short direction;
    private float speed;
    private bool facing_right;
    private Animator animator;

    // in constructor we set value of standard variables
    Player_movement()
    {
        direction = 0;
        speed = 0.02f;
        facing_right = true;
    }

    // in Awake we set value of specailized (unity) objects
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

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

            facing_right = false;
        }

        else if (Keyboard.current.dKey.isPressed)
        {
            if (!facing_right) flip();

            direction = 1;

            facing_right = true;
        }

        else
        {
            direction = 0;

            return;
        }
    }
    private void set_animation()
    {
        if (direction == 0) animator.SetBool("is_running", false);
        else animator.SetBool("is_running", true);
    }

    private void Update()
    {
        cath_keys();
        move();
        set_animation();
    }
};