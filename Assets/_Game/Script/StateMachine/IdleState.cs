using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;
public class IdleState : IState<Bot>
{
    float time;
    public void OnEnter(Bot t)
    {
        t.changAnim(Constants.ANIM_IDLE);
        t.SetDestionation(t.TF.position);
        time = 0;
    }

    public void OnExecute(Bot t)
    {
        time += Time.deltaTime;
        if(time >= 0.3f)
        {
            t.changState(t.seekState);
        }
    }

    public void OnExit(Bot t)
    {

    }

}
