using UnityEngine;
 

public abstract class Enemy : Character
{
    private bool change_action_when_rest, change_action_when_attack;
    [SerializeField] private float noticed_box_width, noticed_box_height;

    private float start_time_of_standing, end_time_of_standing, start_time_of_attacking, end_time_of_attacking; 

    [SerializeField] private int left_position_border, right_position_border;

    private int attacking_chance, standing_chance;

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
        change_action_when_rest = true; change_action_when_attack = true;

        end_time_of_standing = 1f; end_time_of_attacking = 2f;

        attacking_chance = 80; standing_chance = 20;
    }

    protected void Start()
    {
        start_time_of_standing = Time.time;

        transform = GetComponent<Transform>();
        player = GameObject.Find("player");
    }

    protected override void set_animation()
    {
        if (current_state == states.is_attacking)
        {
            GetComponent<Animator>().SetInteger("state", 2);

            return;
        }

        set_basic_animation();
    }

    private void set_can_change_action(states state_, ref bool can_change_action, float start_time, float end_time)
    {
        if (current_state == state_ && Time.time - start_time > end_time)
        {
            current_state = states.do_nothing;

            can_change_action = true;
        }
    }

    private void stand(int shortest_end_time, int longest_end_time)
    {
        end_time_of_standing = new System.Random().Next(shortest_end_time, longest_end_time);

        start_time_of_standing = Time.time;

        direction = 0;

        current_state = states.is_standing;
    }

    private void set_pos_as_goal(float x, float y)
    {
        goal = new Vector2(x, y);

        start_time_of_standing = -1f;

        current_state = states.is_running;
    }

    private void generate_action(ref bool can_change_action, int stand_probability, int run_probaility, int shortest_end_time, int longest_end_time, float goal_x)
    {
        if (can_change_action)
        {
            int probability = new System.Random().Next(0, 101);

            if (probability >= 0 && probability <= stand_probability) { Debug.Log(1); stand(shortest_end_time, longest_end_time); }

            else if (probability > stand_probability && probability <= stand_probability + run_probaility)
            {
                Debug.Log(2); set_pos_as_goal(goal_x, transform.position.y);
            }

            else
            {
                Debug.Log(3);
                attack();
            }
            can_change_action = false;
        }
    }

    private void do_at_resting_state()
    {
        set_can_change_action(states.is_standing, ref change_action_when_rest, start_time_of_standing, end_time_of_standing);

        generate_action(ref change_action_when_rest, 40, 60, 1, 3, new System.Random().Next(left_position_border, right_position_border) );
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

        current_state = states.is_attacking; change_action_when_rest = true; change_action_when_attack = true; 
        direction = 0;
    }

    private void do_in_attack_mode()
    {
        if ( is_in_box(player.transform.position, 1.5f, 0.7f) )
        {
            set_can_change_action(states.is_standing,  ref change_action_when_attack, start_time_of_standing,  end_time_of_standing);
            set_can_change_action(states.is_attacking, ref change_action_when_attack, start_time_of_attacking, end_time_of_attacking);

            generate_action(ref change_action_when_attack, 20, 0, 1, 2, player.transform.position.x);
        }
        // шоб єбашив 2 рази під ряд

        else
        {
            set_can_change_action(states.is_standing, ref change_action_when_attack, start_time_of_standing, end_time_of_standing);
            generate_action(ref change_action_when_attack, 20, 80, 1, 3, player.transform.position.x);
        }

        change_action_when_rest = true;

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
        if ( has_player_noticed() ) do_in_attack_mode();
        
        else do_at_resting_state();

        if (current_state == states.is_running) move_to();

        set_animation();
    }
}

