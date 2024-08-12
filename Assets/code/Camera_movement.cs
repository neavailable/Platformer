using UnityEngine;


public class Camera_movement : Moving_item
{
    [SerializeField] private float y_border;
    [SerializeField] private float x_left_border;
    [SerializeField] private float x_right_border;

    [SerializeField] private Transform target;

    Camera_movement() : base(0.015f) { }

    void Start() {}

    // in first line we check whether camera has crossed the border. after creating the map
    // we will change the border
    // next idk i copied it from tutorial. i gonna try to get it later and leave comment
    private void move()
    {
        if (target.position.x < x_left_border || target.position.x > x_right_border) return;

        Vector3 new_position = new Vector3(target.position.x, target.position.y + y_border, -10f);
        transform.position = Vector3.Slerp(transform.position, new_position, speed);
    }

    void Update()
    {
        move();
    }
}
