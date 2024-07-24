using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;



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
    private bool isCanMove;
    private string currentAnim;
    public float lenghtRaycast = 4f;
    private List<Brick> ListBrick = new List<Brick>();




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
            Stair stair = hit.collider.gameObject.GetComponent<Stair>();
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
    public void AddBrick()
    {
        int index = ListBrick.Count;
        Brick Brick = SimplePool.Spawn<Brick>(PoolType.BrickCollected);
        Brick.changColor(colorType);
        Brick.TF.SetParent(BoxBrick);
        Brick.TF.localRotation = Quaternion.Euler(Vector3.zero);
        Brick.TF.localPosition = Vector3.back * 0.5f + index * 0.25f * Vector3.up + Vector3.up * 1.5f;
        ListBrick.Add(Brick);
    }

    public void RemoveBrick()
    {
        int index = ListBrick.Count - 1;
        if (index >= 0)
        {
            Brick Brick = ListBrick[index];
            ListBrick.RemoveAt(index);
            SimplePool.Despawn(Brick);
        }
    }

    public void ClearBrick()
    {
        for (int i = 0; i < ListBrick.Count; i++)
        {
            Destroy(ListBrick[i].gameObject);
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
            Vector3 randomPos = new Vector3(TF.position.x + Random.Range(1, 3), this.stage.transform.position.y, TF.position.z + Random.Range(1, 3));
            Brick newBrick = SimplePool.Spawn<Brick>(PoolType.Brick, randomPos, Quaternion.identity);
            newBrick.changColor(ColorType.Default);
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
                AddBrick();
            }
        }

    }


}

