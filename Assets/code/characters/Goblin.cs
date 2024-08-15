using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Goblin : Enemy
{
    public Goblin() : base()
    {
    
    }

    protected void Start()
    {
        base.Start();
    }

    protected virtual void set_weapon() { }

    protected void Update()
    {
        base.Update();
    }
}
