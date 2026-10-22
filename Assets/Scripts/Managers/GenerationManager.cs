using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Net;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GenerationManager : MonoBehaviour
{
    public List<Map> maps = new List<Map>();
    public int toggleSpawnCount = 5;
    public int count = 0;
    public bool spawnEnabled = true;

    [SerializeField] private Map MainMap;

    private void Start()
    {
        StartGeneration();
    }

    private void Update()
    {
        if(count >= toggleSpawnCount)
        {
            spawnEnabled = true;
        }
        else
        {
            spawnEnabled = false;
        }
    }

    public void StartGeneration()
    {
        count = MainMap.Initialize(MainMap, count);
    }
}
