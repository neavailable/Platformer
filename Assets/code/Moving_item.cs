using UnityEngine;

public abstract class Moving_item : MonoBehaviour
{
    [SerializeField] protected float speed;

    // in constructor we set value of standard variables
    public Moving_item(float speed_)
    {
        speed = speed_;
    }

    // in Start we set value of specailized (unity) objects
    void Start() {}

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