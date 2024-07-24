using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEditor.SceneManagement;
using UnityEngine;

public class SeekState : IState<Bot>
{
    public void OnEnter(Bot t)
    {
        t.changAnim(Constants.ANIM_RUN);
    }

    public void OnExecute(Bot t)
    {
        if (!t.IsDestionation)
        {
            return;
        }
        if (t.BrickCounts >= Constants.TARGETBRICKS)
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
            t.changState(t.seekState);
        }
        else
        {
            t.SetDestionation(brick.TF.position);
        }

    }
}
