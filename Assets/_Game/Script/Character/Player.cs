using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : Character
{
    [SerializeField] private float moveSpeed = 5f;

    void Update()
    {
        if (!GameManager.IsState(GameState.GamePlay))
        {            
            return;
        }
        if (IsFall)
        {
            return;
        }
        Vector3 nextPoint = JoytickController.direct * moveSpeed * Time.deltaTime + TF.position;
        if (Input.GetMouseButton(0))
        {
            changAnim(Constants.ANIM_RUN);
            if (CanMove(nextPoint))
            {
                TF.position = checkGround(nextPoint);
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            changAnim(Constants.ANIM_IDLE);
        }
        if (JoytickController.direct != Vector3.zero)
        {
            PlayerSkin.transform.forward = JoytickController.direct;
        }

    }
    public void StandUp()
    {
        IsFall = false;
        changAnim(Constants.ANIM_IDLE);
        col.enabled = true;
    }
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.CompareTag(Constants.TAG_PLAYER) || other.CompareTag(Constants.TAG_BOT))
        {
            Character character = Cache.GetCharacter(other);
            if (BrickCounts < character.BrickCounts)
            {
                changAnim(Constants.ANIM_FALLING);
                Falling();
                Invoke(nameof(StandUp), 2f);
            }
        }

        if (other.CompareTag(Constants.TAG_FINISHBOX))
        {
            changAnim(Constants.ANIM_WIN);
            GameManager.ChangeState(GameState.Win);
        }
    }


}
