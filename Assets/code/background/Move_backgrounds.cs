using UnityEngine;

public class Move_backgrounds : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private GameObject background_left, background_right;

    private float bottom_left_x, bottom_right_x;
    private float left_border, right_border;
    private float size;

    // in constructor we set value of standard variables
    public Move_backgrounds() 
    {
        left_border = 2.5f; right_border = 5f;
    }

    // in Start we set value of specailized (unity) objects
    private void Start()
    {
        player = GameObject.Find("player");

        size = background_right.GetComponent<SpriteRenderer>().bounds.size.x;
    
        bottom_right_x = background_right.transform.position.x + size;
        bottom_left_x = background_right.transform.position.x - size;
    }

    // that is move background script. how it works?
    // left_border and right_border are like box where player are locating
    // when player across left_border or right_border two images relocate by their size
    // borders relocate by size also
    private void move()
    {
        if (player.transform.position.x > right_border)
        {
            right_border += size;
            left_border = right_border - size;

            background_left.transform.position = new Vector2(left_border, background_right.transform.position.y);
            background_right.transform.position = new Vector2(right_border, background_right.transform.position.y);
        }

        else if (player.transform.position.x < left_border)
        {
            left_border -= size;
            right_border = left_border + size;

            background_left.transform.position = new Vector2(left_border, background_right.transform.position.y);
            background_right.transform.position = new Vector2(right_border, background_right.transform.position.y);
        }
    }
    
    //there we will call methods which are updating every frame
    private void Update()
    {
        move();
    }
}