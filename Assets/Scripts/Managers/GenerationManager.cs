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
    [Header("Generation Settings")]
    public List<Map> maps = new List<Map>();
    public bool spawnEnabled = true;
    public int toggleSpawnCount = 5;
    public int noRoomChance = 3;

    [Header("Room Limits")]
    public int count = 0;
    public int maxRooms = 15;

    [Header("Tracking Info")]
    public List<Map> AllCurRooms = new List<Map>();

    [SerializeField] private Map MainRoom;

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
        count = 0;
        AllCurRooms.Clear();
        MainRoom.Initialize(MainRoom);
    }
}
