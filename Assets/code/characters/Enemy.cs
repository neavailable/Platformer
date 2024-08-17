using UnityEngine;
using System.Timers;

public abstract class Enemy : Character
{
    private bool is_standing, is_running, change_action_when_rest, change_action_when_attack, is_attacking;
    [SerializeField] private float noticed_box_width, noticed_box_height;

    private float start_time_of_standing, end_time_of_standing, start_time_of_attacking, end_time_of_attacking; 

    [SerializeField] private int left_position_border, right_position_border;

    private Transform transform;
    private GameObject player;

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
        is_running = false; is_standing = false; change_action_when_rest = true; change_action_when_attack = true; is_attacking = false;

        end_time_of_standing = 1f; end_time_of_attacking = 1f;
    }

    protected void Start()
    {
        start_time_of_standing = Time.time;

        transform = GetComponent<Transform>();
        player = GameObject.Find("player");
    }

    protected override void set_animation() 
    {
        if (is_attacking)
        {
            GetComponent<Animator>().SetInteger("state", 2);

            return;
        }

        set_basic_animation();
    }

    private void set_can_change_action(ref bool can_change_action)
    {
        if (is_standing && Time.time - start_time_of_standing > end_time_of_standing) can_change_action = true;
    }

    private void stand(int shortest_end_time, int longest_end_time)
    {
        goal = transform.position;

        end_time_of_standing = new System.Random().Next(shortest_end_time, longest_end_time);

        start_time_of_standing = Time.time;

        direction = 0;

        is_standing = true;

        is_running = false;
    }

    private void set_pos_as_goal(float x, float y)
    {
        goal = new Vector2(x, y);

        start_time_of_standing = -1f;

        is_standing = false; is_running = true;
    }

    private void choose_stand_or_walk(ref bool can_change_action, int stand_probability, int shortest_end_time, int longest_end_time, float goal_x)
    {
        set_can_change_action(ref can_change_action);

        if (can_change_action)
        {
            int probability = new System.Random().Next(0, 101);

            if (probability >= 0 && probability <= stand_probability) stand(shortest_end_time, longest_end_time);

            else set_pos_as_goal(goal_x, transform.position.y);

            can_change_action = false;
        }
    }

    private void do_at_resting_state()
    {
        choose_stand_or_walk(ref change_action_when_rest, 30, 1, 3, new System.Random().Next(left_position_border, right_position_border) );
        change_action_when_attack = true;
    }

    private bool is_in_box(Vector2 object_position, float widht, float height)
    {
        return (transform.position.x < object_position.x + widht) &&
               (transform.position.x > object_position.x - widht) &&
               (transform.position.y < object_position.y + height) &&
               (transform.position.y > object_position.y - height) ?
               true : false;
    }

    private void attack()
    {
        start_time_of_attacking = Time.time;

        is_standing = false; is_running = false; change_action_when_rest = true; change_action_when_attack = true; is_attacking = true;
        direction = 0;
    }

    private void do_in_attack_mode()
    {
        if ( is_in_box(player.transform.position, 1.5f, 0.5f) )
        {
            if (is_attacking && Time.time - start_time_of_attacking > end_time_of_attacking)
            {
                Debug.Log(111);
                is_attacking = false;
                is_standing = false;
                change_action_when_attack = true;
            }
            else if (is_attacking && Time.time - start_time_of_attacking <= end_time_of_attacking)
            {
                attack();
                return;
            }

            if (is_standing && Time.time - start_time_of_standing > end_time_of_standing)
            {
                is_attacking = false;
                is_standing = true;
                change_action_when_attack = false;
            }


            if (change_action_when_attack)
            {
                int probability = new System.Random().Next(0, 101);

                if (probability >= 0 && probability <= 50) stand(1, 3);

                else attack();

                change_action_when_attack = false;
            }
        }
        // шоб єбашив 2 рази під ряд

        else
        {
            choose_stand_or_walk(ref change_action_when_attack, 20, 1, 3, player.transform.position.x);
            change_action_when_rest = true;
        }

        goal = player.transform.position;
    }

    private bool has_player_noticed()
    {
        return is_in_box(player.transform.position, noticed_box_width, noticed_box_height) ? true : false;
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


        if (is_in_box(goal, 1f, 1f))
        {
            change_action_when_rest = true; change_action_when_attack = true;
        }

        else move();
    }

    protected void Update()
    {
        is_attacking = false;

        if ( has_player_noticed() )
        {
            do_in_attack_mode();
        }
        else do_at_resting_state();

        if (is_running && !is_attacking) move_to();

        if (is_attacking) Debug.Log(1);
        set_animation();
    }
}
