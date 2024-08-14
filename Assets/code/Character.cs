using UnityEngine;


public abstract class Character : Moving_item
{
    protected short direction;
    protected bool  facing_right;

    public Character(float speed_, short direction_, bool facing_right_) 
        : base(speed_)
    {
        direction = direction_;
        facing_right = facing_right_;
    }

    void Start() {}

    protected override void move()
    {
        Vector2 position = transform.position;
        position.x += speed * direction;
        transform.position = position;
    }

    protected void flip()
    {
        Vector2 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    protected virtual void set_animation() {}

    void Update() {}
}
