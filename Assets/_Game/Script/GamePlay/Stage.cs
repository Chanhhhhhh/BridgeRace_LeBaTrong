using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Stage : MonoBehaviour
{
    private List<Brick> bricks = new List<Brick>();
    private List<Vector3> emptyPoints = new List<Vector3>();

    public float distance;
    public List<ColorType> colorList = new List<ColorType>();


    // Start is called before the first frame update
    void Start()
    {
        OnInit();
    }
    internal void OnInit()
    {
        
        float x = transform.position.x - 6.5f;
        float y = transform.position.y;
        float z = transform.position.z;
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                Vector3 newVector = new Vector3(x + distance * j, y + 0.1f, z - distance * i);
                emptyPoints.Add(newVector);
            }
        }
    }


    public void OnInitBrick(ColorType color)
    {
        for(int i  = 0; i<8; i++)
        {
            OnBrick(color);
        }
    }

    public void OnBrick(ColorType color)
    {
        if (emptyPoints.Count > 0)
        {
            Vector3 rand = GetEmtyPoint();
            Brick brick = SimplePool.Spawn<Brick>(PoolType.Brick,rand,Quaternion.identity);
            bricks.Add(brick);
            brick.changColor(color);
            brick.OnInit();
        }
    }

    public void RemoveBrick(Brick brick)
    {
        if(brick.colorType != ColorType.Default)
        {
            StartCoroutine(RespawnBrick());
            emptyPoints.Add(brick.TF.position);
        }
        bricks.Remove(brick);
        brick.OnDespawn();
        SimplePool.Despawn(brick);
    }
    

    
    public Vector3 GetEmtyPoint()
    {
        int pointRandom = Random.Range(0, emptyPoints.Count);
        Vector3 EmtyPoint = emptyPoints[pointRandom];
        emptyPoints.RemoveAt(pointRandom);
        return EmtyPoint;
    }

    
    public IEnumerator RespawnBrick()
    {
        yield return new WaitForSeconds(3f);
        if (emptyPoints.Count > 0 && colorList.Count>0)
        {
            OnBrick(colorList[Random.Range(0, colorList.Count)]);
        }
    }
    internal Brick SeekBrickPoint( ColorType color)
    {
        Brick brick = null;
        for (int i = 0; i < bricks.Count; i++)
        {
            if (bricks[i].colorType == color)
            {
                brick = bricks[i];
                break;
            }
        } 
        return brick;
    }
}
