using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class SeekState : IState<Bot>
{
    int TargetBrick;
    public void OnEnter(Bot t)
    {
        //Debug.Log("SeekState");
        t.changAnim(Constants.ANIM_RUN);
        t.SetDestionation(t.TF.position);
        TargetBrick = Random.Range(4, 9);
    }

    public void OnExecute(Bot t)
    {
        if (!t.IsDestionation)
        {
            return;
        }
        if (t.BrickCounts >= TargetBrick)
        {
            t.changState(t.buildState);
        }
        else
        {
            SeekBrick(t);
        }
    }

    public void OnExit(Bot t)
    {

    }
    public void SeekBrick(Bot t)
    {
        if (t.stage == null)
        {
            t.changState(t.idleState);
            return;
        }

        Brick brick = t.stage.SeekBrickPoint(t.colorType);
        if (brick == null)
        {
            t.changState(t.idleState);
        }
        else
        {
            t.SetDestionation(brick.TF.position);
        }

    }
}
