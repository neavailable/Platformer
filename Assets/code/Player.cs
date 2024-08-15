using UnityEngine;
using UnityEngine.InputSystem;


public class Player : Character
{
    Rigidbody2D rb;
    public int jumpPower;

    public Player() : base(0.015f, 0, true) {}

    private void Start() {}


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
        else if(Keyboard.current.spaceKey.isPressed) 
        {
            rb = GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);

        }
        else direction = 0;
    }

    public override void set_animation()
    {
        if (direction == 0) GetComponent<Animator>().SetBool("is_running", false);
        
        else GetComponent<Animator>().SetBool("is_running", true); 
    }

    private void Update()
    {
        cath_keys();
        set_animation();
    }

    

};