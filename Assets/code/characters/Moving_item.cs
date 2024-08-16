using UnityEngine;

public abstract class Moving_item : MonoBehaviour
{
    [SerializeField] private float speed;

    // in constructor we set value of standard variables
    public Moving_item(float speed_)
    {
        speed = speed_;
    }

    // in Start we set value of specailized (unity) objects
    private void Start() {}

    protected float get_speed()
    {
        return speed;
    }

    protected virtual void move() { }

    protected virtual void set_basic_animation() { }

    //there we will call methods which are updating every frame
    private void Update() {}
}


//    _________
//   | _ _ _ _ |
//   |  +   +  |
//   |    o    |
//    =========
//       -|-
//       / \
//      
//    це мішаня