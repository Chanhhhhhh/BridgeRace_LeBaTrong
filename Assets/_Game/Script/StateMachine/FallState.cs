using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class FallState : IState<Bot>
{
    float time;
    public void OnEnter(Bot bot)
    {        
        bot.SetDestionation(bot.TF.position);
        bot.changAnim(Constants.ANIM_FALLING);
        time = 0;
    }

    public void OnExecute(Bot bot)
    {
        time += Time.deltaTime;
        if(time >= 2f)
        {
            bot.changState(bot.idleState);
        }
    }

    public void OnExit(Bot bot)
    {
        bot.EnableCol();
    }

}
