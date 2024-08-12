using UnityEngine;

public abstract class Moving_item : MonoBehaviour
{
    protected float speed;

    // in Start we set value of specailized (unity) objects
    void Start() {}

    // in constructor we set value of standard variables
    public Moving_item(float speed_)
    {
        speed = speed_;
    }

    protected virtual void move() { }

    //there we will call methods which are updating every frame
    void Update() {}
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