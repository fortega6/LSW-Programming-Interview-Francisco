using System.Collections.Generic;
using UnityEngine;

public class LevelEntrance : MonoBehaviour
{
    public LevelEntranceSO entrance;

    [System.Serializable]
    public class Path
    {
        public Transform[] wayPoints;
    }

    [System.Serializable]
    public class PathList
    {
        public Path [] paths;
    }

    public PathList listOfPathLists;

    public Transform[] GetRandomPath()
    {
        return listOfPathLists.paths[Random.Range(0, listOfPathLists.paths.Length)].wayPoints;
    }
}
