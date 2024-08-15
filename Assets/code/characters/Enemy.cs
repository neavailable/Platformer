using UnityEngine;
using System.Timers;

public abstract class Enemy : Character
{
    private bool has_noticed, timer_started, is_running, can_change_action;
    [SerializeField] private float notice_x, notice_y;

    private float start_time, end_time;

    [SerializeField] private int left_position_border, right_position_border;

    private Transform transform;
    private Transform player_transform;

    private Vector2 goal;

    // notice_x and notice_y are sides of box. when player come to the box enemy start move to player

    //     notice_x
    //      <---->
    //    __________
    //   |          |
    //   |          |^
    //   |    E     || notice_y
    //   |          |v
    //   |__________|

    //    E - Enemy

    public Enemy() : base(1f, 1, true)
    {
        has_noticed = false; is_running = false; timer_started = false; can_change_action = true;

        end_time = 1f;
    }

    protected void Start()
    {
        start_time = Time.time;

        transform = GetComponent<Transform>();
        player_transform = GameObject.Find("player").transform;
    }

    protected override void set_animation() {}

    private void do_action()
    {
        if (can_change_action) 
        {
            int percent = new System.Random().Next(0, 101);

            if (percent >= 0 && percent <= 30)
            {
                goal = transform.position;
            
                start_time = Time.time;

                direction = 0;

                timer_started = true;

                is_running = false;
            }

            else
            {
                goal = new Vector2(new System.Random().Next(-10, 20), transform.position.y);

                start_time = -1f;

                timer_started = false;

                is_running = true;
            };

            can_change_action = false;
        }
    }

    private void set_can_change_action()
    {
        if (timer_started && Time.time - start_time > end_time) can_change_action = true;
    }

    private void has_player_noticed()
    {
        has_noticed =
            (transform.position.x < player_transform.position.x + notice_x) &&
            (transform.position.x > player_transform.position.x - notice_x) &&
            (transform.position.y < player_transform.position.y + notice_y) &&
            (transform.position.y > player_transform.position.y - notice_y) ?
            true : false;
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

        if
        (!
           ((transform.position.x < goal.x + 1f) &&
            (transform.position.x > goal.x - 1f) &&
            (transform.position.y < goal.y + 1f) &&
            (transform.position.y > goal.y - 1f))
        ) move();
       
        else can_change_action = true;
    }

    protected void Update()
    {
        has_player_noticed();

        if (has_noticed) goal = player_transform.position;

        else
        {
            set_can_change_action();
            do_action();
        };
        
        if (is_running) move_to();
        set_basic_animation();
    }
}
