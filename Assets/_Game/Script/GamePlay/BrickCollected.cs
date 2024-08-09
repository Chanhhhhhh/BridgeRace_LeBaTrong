using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickCollected : ColorObject
{
    public override void OnInit()
    {

    }

    public override void OnDespawn()
    {
        SimplePool.Despawn(this);
    }
}
