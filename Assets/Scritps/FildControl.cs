using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshSurface))]
public class FildControl : MonoBehaviour
{
    public static event Action OnFildCreate;
    NavMeshSurface _navMesh;
    public NavMeshSurface navMesh { get { return _navMesh = _navMesh
                ?? GetComponent<NavMeshSurface>(); } }

    void OnEnable()
    {
        LineCreater.OnLineCreateEvent += BuildNavMesh;
    }

    void OnDisable()
    {
        LineCreater.OnLineCreateEvent -= BuildNavMesh;
    }

    void BuildNavMesh()
    {
        navMesh.BuildNavMesh();
        OnFildCreate?.Invoke();
    }
}
