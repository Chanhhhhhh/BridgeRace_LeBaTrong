using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Level : MonoBehaviour
{
    private NavMeshSurface meshSurface;
    public int BotAmount;
    public Transform startPoint;
    public Transform finishPoint;

    public void OnInit()
    {
        meshSurface = GetComponent<NavMeshSurface>();
        NavMesh.RemoveAllNavMeshData();
        NavMesh.AddNavMeshData(meshSurface.navMeshData);
    }
}
