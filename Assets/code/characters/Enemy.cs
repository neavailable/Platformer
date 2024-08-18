using UnityEngine;
 

public abstract class Enemy : Character
{
    private bool change_action_when_rest, change_action_when_follow, change_action_when_attack;
    [SerializeField] private float notice_box_width, notice_box_height;

    private float start_time_of_standing, start_time_of_attacking, end_time_of_standing, end_time_of_attacking; 

    [SerializeField] private int left_position_border, right_position_border;

    private int standing_probility;

    private Transform transform, player_transform;

    private Vector2 goal;

    // notice_x and notice_y are sides of box. when player come to the box enemy start move to player

    //  notice_box_width
    //      <---->
    //    __________
    //   |          |
    //   |          |^
    //   |    E     || notice_box_height
    //   |          |v
    //   |__________|

    //    E - Enemy

    public Enemy() : base(1f, 1, true)
    {
        change_action_when_rest = true; change_action_when_follow = true; change_action_when_attack = true;

        end_time_of_standing = 1f; end_time_of_attacking = 1.8f;

        standing_probility = 30;
    }

    protected void Start()
    {
        start_time_of_standing = Time.time;

        transform = GetComponent<Transform>();
        player_transform = GameObject.Find("player").transform;
    }

    private bool is_in_box(Vector2 object_position, float widht, float height)
    {
        return (transform.position.x < object_position.x + widht) &&
               (transform.position.x > object_position.x - widht) &&
               (transform.position.y < object_position.y + height) &&
               (transform.position.y > object_position.y - height) ?
               true : false;
    }

    private bool has_player_noticed()
    {
        return is_in_box(player_transform.position, notice_box_width, notice_box_height) ? true : false;
    }

    private void should_flip()
    {
        bool previous_facing_right = facing_right;

        if (goal.x < transform.position.x)
        {
            direction = -1;
            facing_right = false;
        }
        else
        {
            direction = 1;
            facing_right = true;
        }

        if (facing_right != previous_facing_right) flip();
    }

    private void move_to()
    {
        if (is_in_box(goal, 1f, 1f))
        {
            change_action_when_rest = true; change_action_when_follow = true; change_action_when_attack = true;
        }

        else move();
    }

    private void set_can_change_action(states state, ref bool can_change_action, float start_time, float end_time)
    {
        if (current_state == state && Time.time - start_time > end_time)
        {
            current_state = states.do_nothing;

            can_change_action = true;
        }
    }

    private void stand(float end_time_)
    {
        start_time_of_standing = Time.time;
        
        end_time_of_standing = end_time_;

        direction = 0;

        standing_probility = current_state == states.is_standing ? 0 : 30;
            
        current_state = states.is_standing;

        standing_probility = 0;
    }

    private void set_pos_as_goal(float x, float y)
    {
        goal = new Vector2(x, y);

        start_time_of_standing = -1f;

        current_state = states.is_running;

        change_action_when_attack = true;
    }

    private void attack()
    {
        start_time_of_attacking = Time.time;

        current_state = states.is_attacking; change_action_when_rest = true; change_action_when_follow = true;
        direction = 0;

        standing_probility += 30;
    }

    private void generate_action(ref bool can_change_action, int stand_probability, int run_probaility, float end_time, float goal_x)
    {
        set_can_change_action(states.is_standing, ref can_change_action, start_time_of_standing, end_time_of_standing);

        if (can_change_action)
        {
            int probability = new System.Random().Next(0, 101);

            if (probability >= 0 && probability <= stand_probability) stand(end_time);

            else if (probability > stand_probability && probability <= stand_probability + run_probaility) set_pos_as_goal(goal_x, transform.position.y);
            
            else attack();
            
            can_change_action = false;
        }
    }

    private void do_at_resting_state()
    {
        generate_action( ref change_action_when_rest, 40, 60, (float) (new System.Random().NextDouble() * (2.5f - 0.5f) + 0.5f), new System.Random().Next(left_position_border, right_position_border) );

        change_action_when_follow = true; change_action_when_attack = true;

        standing_probility = 30;
    }

    private void do_at_follow_mode()
    {
        generate_action(ref change_action_when_follow, 10, 90, (float) (new System.Random().NextDouble() * (2.5f - 0.5f) + 0.5f), player_transform.position.x);

        change_action_when_attack = true;

        standing_probility = 30;
    }

    private void do_at_attack_mode()
    {
        set_can_change_action(states.is_attacking, ref change_action_when_attack, start_time_of_attacking, end_time_of_attacking);

        generate_action(ref change_action_when_attack, standing_probility, 0, (float) (new System.Random().NextDouble() * (1f - 0.5f) + 0.5f), player_transform.position.x);
        
        change_action_when_follow = true;
    }

    private void do_if_player_noticed()
    {
        if ( is_in_box(player_transform.position, 1.5f, 0.7f) ) do_at_attack_mode();
        
        else do_at_follow_mode();
        
        change_action_when_rest = true;

        goal = player_transform.position;
    }

    protected void Update()
    {
        if ( has_player_noticed() ) do_if_player_noticed();
        
        else do_at_resting_state();

        should_flip();
        if (current_state == states.is_running) move_to();
        
        set_basic_animation();
    }
}