using UnityEngine;
using System.Timers;

public abstract class Enemy : Character
{
    private bool timer_started, is_running, can_change_action;
    [SerializeField] private float noticed_box_width, noticed_box_height;

    private float start_time, end_time;

    [SerializeField] private int left_position_border, right_position_border;

    private Transform transform;
    private Transform player_transform;

    private Vector2 goal;

    // notice_x and notice_y are sides of box. when player come to the box enemy start move to player

    //  noticed_box_width
    //      <---->
    //    __________
    //   |          |
    //   |          |^
    //   |    E     || noticed_box_height
    //   |          |v
    //   |__________|

    //    E - Enemy

    public Enemy() : base(1f, 1, true)
    {
        is_running = false; timer_started = false; can_change_action = true;

        end_time = 1f;
    }

    protected void Start()
    {
        start_time = Time.time;

        transform = GetComponent<Transform>();
        player_transform = GameObject.Find("player").transform;
    }

    protected override void set_animation() 
    {
        set_basic_animation();
    }

    private bool is_in_box(Vector2 object_position, float widht, float height)
    {
        return (transform.position.x < object_position.x + widht)  &&
               (transform.position.x > object_position.x - widht)  &&
               (transform.position.y < object_position.y + height) &&
               (transform.position.y > object_position.y - height) ? 
               true : false;
    }

    private void set_can_change_action()
    {
        if (timer_started && Time.time - start_time > end_time) can_change_action = true;
    }

    private void do_action()
    {
        if (can_change_action) 
        {
            int percent = new System.Random().Next(0, 101);

            if (percent >= 0 && percent <= 50)
            {
                goal = transform.position;
            
                start_time = Time.time;

                direction = 0;

                timer_started = true;

                is_running = false;
            }

            else
            {
                goal = new Vector2(new System.Random().Next(left_position_border, right_position_border), transform.position.y);

                start_time = -1f;

                timer_started = false;

                is_running = true;
            };

            can_change_action = false;
        }
    }

    private bool has_player_noticed()
    {
        return is_in_box(player_transform.position, noticed_box_width, noticed_box_height) ? true : false;
    }

    private void move_to()
    {
        bool previous_facing_right = facing_right;

        if (goal.x < transform.position.x)
        {
            direction = -1;
            facing_right = false;
        }
        else if (goal.x == transform.position.x)
        {
            direction = 0;
            return;
        }
        else
        { 
            direction = 1;
            facing_right = true;
        }

        if (facing_right != previous_facing_right) flip();


        if ( is_in_box(goal, 1f, 1f) ) can_change_action = true;

        else move();
    }

    protected void Update()
    {
        if ( has_player_noticed() )
        {
            goal = player_transform.position;

            is_running = true;
        }
        else
        {
            set_can_change_action();
            do_action();
        };
        
        if (is_running) move_to();
        set_animation();
    }
}
