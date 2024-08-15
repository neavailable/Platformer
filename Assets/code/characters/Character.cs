using UnityEngine;


public abstract class Character : Moving_item
{
    protected int direction;
    protected bool facing_right;

    public Character(float speed_, int direction_, bool facing_right_) 
        : base(speed_)
    {
        direction = direction_;
        facing_right = facing_right_;
    }

    private void Start() {}


    protected virtual void set_animation() {}


    protected override void move()
    {
        Vector2 position = transform.position;
        position.x += get_speed() * direction;
        transform.position = position;
    }

    protected void flip()
    {
        Vector2 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
    protected void set_basic_animation()
    {
        if (direction == 0) GetComponent<Animator>().SetBool("is_running", false);

        else GetComponent<Animator>().SetBool("is_running", true);
    }

    private void Update() {}
}
