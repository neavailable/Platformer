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
    protected override void set_basic_animation()
    {
        //if (true)
        //{
        //    GetComponent<Animator>().SetInteger("state", 2);
        //    return;
        //}

        if (direction == 0) GetComponent<Animator>().SetInteger("state", 0);

        else GetComponent<Animator>().SetInteger("state", 1);
    }

    private void Update() {}
}
