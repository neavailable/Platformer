using UnityEngine;


public abstract class Character : Moving_item
{
    protected enum states { do_nothing, is_standing, is_running, is_attacking };
    protected states current_state;

    protected int direction;
    protected bool facing_right;

    public Character(float speed_, int direction_, bool facing_right_) 
        : base(speed_)
    {
        current_state = states.do_nothing;

        direction = direction_;
        facing_right = facing_right_;
    }

    private void Start() {}

    protected virtual void set_animation() {}

    protected override void set_basic_animation()
    {
        GetComponent<Animator>().SetInteger("state", (int)current_state);
    }

    protected override void move()
    {
        Vector2 position = transform.position;
        position.x += get_speed() * direction;
        transform.position = position;

        current_state = states.is_running;
    }

    protected void flip()
    {
        Vector2 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    private void Update() {}
}
