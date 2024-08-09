using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NameTag : GameUnit
{
    [SerializeField] private TextMeshProUGUI text_Name;
    private Transform target;

    private void Update()
    {
        if(target == null)
        {
            return;
        }
        Vector3 pos = Cache.MainCamera.WorldToScreenPoint(target.position + Vector3.up*4);
        this.transform.position = pos;
    }
    public override void OnInit()
    {

    }

    public void OnInit(string name, Transform target)
    {
        text_Name.text = name;
        this.target = target;
    }


    public override void OnDespawn()
    {
        SimplePool.Despawn(this);
    }
}
