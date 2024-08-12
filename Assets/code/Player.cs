using UnityEngine;
using UnityEngine.InputSystem;


public class Player : Character
{
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