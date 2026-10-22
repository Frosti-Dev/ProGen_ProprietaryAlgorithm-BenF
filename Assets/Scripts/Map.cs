using System.Collections.Generic;
using System.Data;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnpoints = new List<Transform>();

    public GameObject northSpawn;
    public GameObject eastSpawn;
    public GameObject southSpawn;
    public GameObject westSpawn;

    [SerializeField] private GameObject lastRoom = null;
    [SerializeField] private string lastRoomDir = null;

    #region BlockPaths
    [SerializeField] private bool BlockNorth = false;
    [SerializeField] private bool BlockSouth = false;
    [SerializeField] private bool BlockWest = false;
    [SerializeField] private bool BlockEast = false;

    [SerializeField] private GameObject NorthWall;
    [SerializeField] private GameObject SouthWall;
    [SerializeField] private GameObject WestWall;
    [SerializeField] private GameObject EastWall;

    #endregion

    [SerializeField] private Map newMapScript;
    [SerializeField] private GameObject mapParent;
    [SerializeField] private GenerationManager genManager;

    private void Start()
    {
        spawnpoints.Clear();

        mapParent = GameObject.FindGameObjectWithTag("MapParent");

        //spawns
        northSpawn = GameObject.FindGameObjectWithTag("NorthSpawn");
        spawnpoints.Add(northSpawn.transform);
        southSpawn = GameObject.FindGameObjectWithTag("SouthSpawn");
        spawnpoints.Add(southSpawn.transform);
        westSpawn = GameObject.FindGameObjectWithTag("WestSpawn");
        spawnpoints.Add(westSpawn.transform);
        eastSpawn = GameObject.FindGameObjectWithTag("EastSpawn");
        spawnpoints.Add(eastSpawn.transform);

        //walls
        NorthWall = GameObject.FindGameObjectWithTag("NorthWall");
        NorthWall.SetActive(false);
        SouthWall = GameObject.FindGameObjectWithTag("SouthWall");
        SouthWall.SetActive(false);
        WestWall = GameObject.FindGameObjectWithTag("WestWall");
        WestWall.SetActive(false);
        EastWall = GameObject.FindGameObjectWithTag("EastWall");
        EastWall.SetActive(false);

        genManager = ServiceHub.Instance.generationManager;
    }

    private void OnEnable()
    { 
        Debug.Log("Cur Room POS: " + this.gameObject.transform.position);
        if(lastRoom != null)
        {
            Debug.Log("Last Room POS: " + lastRoom.gameObject.transform.position);
        }

        if (lastRoom != null && lastRoom.gameObject.transform.position.y != this.gameObject.transform.position.y)
        {
            if (lastRoom.gameObject.transform.position.y > this.gameObject.transform.position.y)
            {
                lastRoomDir = "North";
                spawnpoints.Remove(northSpawn.transform);
            }
            else
            {
                lastRoomDir = "South";
                spawnpoints.Remove(southSpawn.transform);
            }

        }
        else if (lastRoom != null && lastRoom.gameObject.transform.position.x != this.gameObject.transform.position.x)
        {
            if (lastRoom.gameObject.transform.position.x > this.gameObject.transform.position.x)
            {
                lastRoomDir = "East";
                spawnpoints.Remove(southSpawn.transform);

            }
            else
            {
                lastRoomDir = "West";
                spawnpoints.Remove(westSpawn.transform);
            }
        }
        
        BlockPath(lastRoomDir);

        int randomID = Random.Range(0, 1);
        Initialize(genManager.maps[0], genManager.count);
    }

    private void Update()
    {
        if (BlockNorth) NorthWall.SetActive(true);
        if (BlockSouth) SouthWall.SetActive(true);
        if (BlockWest) WestWall.SetActive(true);
        if (BlockEast) EastWall.SetActive(true);
    }

    public int Initialize(Map map, int count)
    {
        count++;

        if (genManager.spawnEnabled)
        {
            foreach (Transform spawn in spawnpoints)
            {
                Map GO = Instantiate(map, spawn.position, spawn.rotation, mapParent.transform);
                newMapScript = GO.GetComponent<Map>();
                newMapScript.lastRoom = this.gameObject;
            }
        }

        return count;
    }

    public void BlockPath(string blockDirection)
    {
        if (blockDirection == "North")
        {
            BlockNorth = true;
        }
        if (blockDirection == "South")
        {
            BlockSouth = true;
        }
        if (blockDirection == "West")
        {
            BlockWest = true;
        }
        if (blockDirection == "East")
        {
            BlockEast = true;
        }
    }
}
