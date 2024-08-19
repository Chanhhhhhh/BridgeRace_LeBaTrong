using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionScene : UICanvas
{
    [SerializeField] private Animator Anim;
    string currentAnim;
    public void ChangAnim(string AnimName)
    {
        if(currentAnim != null)
        {
            Anim.ResetTrigger(currentAnim);
            currentAnim = AnimName;
            Anim.SetTrigger(currentAnim);
        }

    }
    public void RunAnim()
    {
        ChangAnim(Constants.GetRandomAnimScene());
    }
}
