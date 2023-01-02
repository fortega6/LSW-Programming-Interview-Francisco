using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WayPoint
{
    public Transform transform;
    public int wayTime = 1;
}

public class LevelEntrance : MonoBehaviour
{
    public LevelEntranceSO entrance;

    [System.Serializable]
    public class Path
    {
        public WayPoint[] wayPoints;
    }

    [System.Serializable]
    public class PathList
    {
        public Path [] paths;
    }

    public PathList listOfPathLists;

    public WayPoint[] GetRandomPath()
    {
        return listOfPathLists.paths[Random.Range(0, listOfPathLists.paths.Length)].wayPoints;
    }
}
