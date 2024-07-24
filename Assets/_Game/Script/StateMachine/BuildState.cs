using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildState : IState<Bot>
{
    public void OnEnter(Bot t)
    {        
        t.SetDestionation(LevelManager.Instance.finishPoint.position);
    }

    public void OnExecute(Bot t)
    {
        if (t.CanMove(t.TF.position + Vector3.forward))
        {
            return;
        }
        t.SetDestionation(t.TF.position);
        t.changState(t.seekState);

    }

    public void OnExit(Bot t)
    {

    }

        
    
}
