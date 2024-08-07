using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Camerafollow : MonoBehaviour
{
    private Transform tf;
    public Transform TF
    {
        get
        {
            tf = tf ?? gameObject.transform;
            return tf;
        }
    }
    public Transform playerTF;

    [SerializeField] Vector3 offset;
    [SerializeField] Vector3 NewOffset;
    private void LateUpdate()
    { 
        if (!GameManager.IsState(GameState.GamePlay))
        {
            TF.position = Vector3.Lerp(TF.position, LevelManager.Instance.GetFinishPos() + NewOffset, Time.deltaTime * 3f);
            return;
        }
        TF.position = Vector3.Lerp(TF.position, playerTF.position + offset, Time.deltaTime * 5f);
    }

}
