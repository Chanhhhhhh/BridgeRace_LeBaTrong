using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;

public class Brick : GameUnit
{
    [SerializeField] private Collider col;
    public override void OnInit()
    {
        col.enabled = true;
    }
    public override void OnDespawn()
    {
        col.enabled = false;
    }


}
