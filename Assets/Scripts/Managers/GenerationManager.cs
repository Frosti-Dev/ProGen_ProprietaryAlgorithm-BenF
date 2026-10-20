using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GenerationManager : MonoBehaviour
{
    public List<Map> maps = new List<Map>();

    //public List<Transform> spawnpoints = new List<Transform>();

    [SerializeField] private Map MainMap;

    private void Start()
    {
        Phase1();
    }

    public void Phase1()
    {
        //int randomID = Random.Range(0, 2);

        //foreach(Transform spawnpoint in spawnpoints)
        //{
        //    Map GO = Instantiate(maps[0], spawnpoint);
        //}

        MainMap.Initialize(maps[0]);

    }
}
