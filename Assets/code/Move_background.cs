using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UIElements;

public class Move_background : MonoBehaviour
{
    private float  start_x,     start_y;
    private float  relocated_x, relocated_y;

    private GameObject     player, backround2;
    private Transform      background1_transform;
    private SpriteRenderer backround2_renderer;

    public Move_background()
    {
        start_x = 0f; start_y = 5f;
        relocated_x = 16.082f; relocated_y = -10.44f;
    }

    void Start()
    {
        backround2 = GameObject.Find("bg_forest_1");
        player = GameObject.Find("player");

        background1_transform = GetComponent<Transform>();
        backround2_renderer = backround2.GetComponent<SpriteRenderer>();
    }

    void relocate()
    {
        Vector3 size = backround2_renderer.bounds.size;
        Vector3 bottom_right = backround2.transform.position + new Vector3(size.x, size.y, 0);

        Vector3 position;

        if (player.transform.position.x > bottom_right.x) position = new Vector3(bottom_right.x, backround2.transform.position.y);
        else position = new Vector3(-4f, 15f);

        background1_transform.position = position;
    }

    void Update()
    {
        relocate();
    }
}