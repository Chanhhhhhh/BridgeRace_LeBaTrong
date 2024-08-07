using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Character : ColorObject
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
    [SerializeField] protected LayerMask GroundLayer;
    [SerializeField] protected LayerMask StairLayer;
    [SerializeField] protected Animator animator;
    [SerializeField] protected GameObject PlayerSkin;
    [SerializeField] protected Collider col;
    [SerializeField] private Transform BoxBrick;

    protected bool IsFall;
    protected List<BrickCollected> ListBrick = new List<BrickCollected>();

    private bool isCanMove;
    private string currentAnim;

    public float lenghtRaycast = 4f;
    public Stage stage;
    public int BrickCounts => ListBrick.Count;


    public virtual void OnInit()
    {
        changAnim(Constants.ANIM_IDLE);
        ClearBrick();
    }

    public Vector3 checkGround(Vector3 nextPoint)
    {
        RaycastHit hit;
        if (Physics.Raycast(nextPoint + Vector3.up, Vector3.down, out hit, lenghtRaycast, GroundLayer))
        {
            return hit.point;
        }

        return TF.position;
    }

    public bool CanMove(Vector3 nextPoint)
    {
        isCanMove = true;
        RaycastHit hit;
        if (Physics.Raycast(nextPoint + Vector3.up, Vector3.down, out hit, lenghtRaycast, StairLayer))
        {
            Stair stair = Cache.GetStair(hit.collider);
            if (stair.colorType != colorType && ListBrick.Count > 0)
            {
                isCanMove = true;
                stair.changColor(colorType);
                RemoveBrick();
            }
            if (stair.colorType != colorType && ListBrick.Count == 0 && PlayerSkin.transform.forward.z > 0f)
            {
                isCanMove = false;
            }
        }
        return isCanMove;
    }
    public void AddBrick(Transform tf)
    {
        int index = ListBrick.Count;
        BrickCollected Brick = SimplePool.Spawn<BrickCollected>(PoolType.BrickCollected, tf.position, tf.rotation);
        Brick.changColor(colorType);
        Brick.TF.SetParent(BoxBrick);
        Brick.TF.localRotation = Quaternion.Euler(Vector3.zero);
        Brick.TF.DOLocalMove(Vector3.back * 0.5f + index * 0.25f * Vector3.up + Vector3.up * 1.5f, 1f);
        ListBrick.Add(Brick);
    }

    public void RemoveBrick()
    {
        int index = ListBrick.Count - 1;
        if (index >= 0)
        {
            BrickCollected Brick = ListBrick[index];
            ListBrick.RemoveAt(index);
            Brick.OnDespawn();
        }
    }

    public void ClearBrick()
    {
        for (int i = 0; i < ListBrick.Count; i++)
        {
            ListBrick[i].OnDespawn();
        }
        ListBrick.Clear();
    }


    public void changAnim(string animName)
    {
        if (currentAnim != animName)
        {
            animator.ResetTrigger(currentAnim);
            currentAnim = animName;
            animator.SetTrigger(animName);

        }
    }
    public void Falling()
    {
        col.enabled = false;
        IsFall = true;
        DropBricks();
    }

    public void DropBricks()
    {
        for (int i = 0; i < ListBrick.Count; i++)
        {
            Vector3 randomPos = new Vector3(TF.position.x + Random.Range(-3, 3), this.stage.transform.position.y, TF.position.z + Random.Range(-3, 3));
            Quaternion randomRot = Quaternion.Euler(0, Random.Range(0, 360f), 0);
            Brick newBrick = SimplePool.Spawn<Brick>(PoolType.Brick, this.TF.position + Vector3.up*2, randomRot);
            newBrick.changColor(ColorType.Default);
            newBrick.TF.DOMove(randomPos, 1.5f).OnComplete(() =>
            {
                newBrick.OnInit();
            });
        }
        ClearBrick();
    }
    
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.TAG_BRICK))
        {
            Brick brick = Cache.GetBrick(other);
            if (brick.colorType == colorType || brick.colorType == ColorType.Default)
            {
                stage.RemoveBrick(brick);
                AddBrick(brick.TF);
            }
        }

    }


}

