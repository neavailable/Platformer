using UnityEngine.InputSystem;


public class Player : Character
{
    public Player() : base(0.025f, 0, true) {}

    private void Start() {}

    protected override void set_animation() {}
    private void cath_keys()
    {
        if (Keyboard.current.aKey.isPressed)
        {
            if (facing_right)
            {
                flip();
                facing_right = false;
            }

            direction = -1;

            move();
        }

        else if (Keyboard.current.dKey.isPressed)
        {
            if (!facing_right)
            {
                flip();
                facing_right = true;
            }

            direction = 1;

            move();
        }

        else
        {
            current_state = states.is_standing;
            direction = 0;
        }
    }

    private void Update()
    {
        cath_keys();
        set_basic_animation();
    }
};