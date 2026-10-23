using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] private GenerationManager genManager;

    private List<Transform> potentialSpawns = new List<Transform>();
    [SerializeField] private GameObject lastRoom = null;
    [SerializeField] private string lastRoomDir = null;

    [Header("Spawn Points")]
    public GameObject northSpawn;
    public GameObject eastSpawn;
    public GameObject southSpawn;
    public GameObject westSpawn;

    #region BlockPaths
    [SerializeField] private bool BlockNorth = false;
    [SerializeField] private bool BlockSouth = false;
    [SerializeField] private bool BlockWest = false;
    [SerializeField] private bool BlockEast = false;

    [Header("Walls")]
    [SerializeField] private GameObject NorthWall;
    [SerializeField] private GameObject SouthWall;
    [SerializeField] private GameObject WestWall;
    [SerializeField] private GameObject EastWall;

    #endregion

    private Map newMapScript;
    private GameObject mapParent;

    #region detector
    [Header("Detection Settings")]
    [SerializeField] private LayerMask roomLayer; // Set this to your "Room" or "Map" physics layer!
    [SerializeField] private float checkDistance = 10f;

    #endregion

    private void Awake()
    {
        //add references for prefabs
        mapParent = GameObject.FindGameObjectWithTag("MapParent");

        potentialSpawns.Clear();

        potentialSpawns.Add(northSpawn.transform);
        potentialSpawns.Add(southSpawn.transform);
        potentialSpawns.Add(westSpawn.transform);
        potentialSpawns.Add(eastSpawn.transform);

        //walls
        NorthWall.SetActive(false);
        SouthWall.SetActive(false);
        WestWall.SetActive(false);
        EastWall.SetActive(false);

        if(ServiceHub.Instance != null)
        {
            genManager = ServiceHub.Instance.generationManager;
        }
    }

    private void OnEnable()
    {
        //after instantiate
        if(genManager != null)
        {
            genManager.AllCurRooms.Add(this);

            StartCoroutine(GenerationRoutine());
        }
    }

    private IEnumerator GenerationRoutine()
    {
        //generate
        yield return new WaitForFixedUpdate();

        CheckSurroundingRooms();

        if (genManager != null && genManager.spawnEnabled)
        {
            if(genManager.count >= genManager.maxRooms)
            {
                yield break;
            }

            if (genManager.count >= genManager.toggleSpawnCount)
            {
                //GENERATION RANDOMNESS
                int randomMap = Random.Range(0, genManager.maps.Count);
                Initialize(genManager.maps[randomMap]);
            }
        }
    }

    private void Update()
    {
        //check blocks
        if (BlockNorth) NorthWall.SetActive(true);
        if (BlockSouth) SouthWall.SetActive(true);
        if (BlockWest) WestWall.SetActive(true);
        if (BlockEast) EastWall.SetActive(true);

        //always be checking surrounding rooms
        CheckSurroundingRooms();
    }

    public void Initialize(Map map)
    {
        List<Transform> activeSpawns = new List<Transform>(potentialSpawns);

        foreach (Transform spawn in activeSpawns)
        {
            if (genManager.count >= genManager.maxRooms) break;

            //check if no room is made
            int NoRoomChance = Random.Range(0, genManager.noRoomChance);
            if (NoRoomChance == 0)
            {
                break;
            }

            else
            {
                //increase room count
                genManager.count++;

                //make random room
                Map GO = Instantiate(map, spawn.position, spawn.rotation, mapParent.transform);
                newMapScript = GO.GetComponent<Map>();
                newMapScript.lastRoom = this.gameObject;

                //last room addition
                if (GO.gameObject.transform.position.y != this.gameObject.transform.position.y)
                {
                    if (GO.gameObject.transform.position.y > this.gameObject.transform.position.y)
                    {
                        GO.lastRoomDir = "South";
                    }
                    else
                    {
                        GO.lastRoomDir = "North";
                    }

                }
                else if (GO.gameObject.transform.position.x != this.gameObject.transform.position.x)
                {
                    if (GO.gameObject.transform.position.x > this.gameObject.transform.position.x)
                    {
                        GO.lastRoomDir = "West";
                    }
                    else
                    {
                        GO.lastRoomDir = "East";
                    }

                }
            }
        }
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


    public bool IsObjectInDirection(Vector2 direction)
    {
        Vector2 checkPosition = (Vector2)transform.position + (direction * checkDistance);

        Collider2D hit = Physics2D.OverlapCircle(checkPosition, 0.5f, roomLayer);

        return hit != null;

    }

    public void CheckSurroundingRooms()
    {
        // Check North 
        if (IsObjectInDirection(Vector2.up))
        {
            potentialSpawns.Remove(northSpawn.transform);
            NorthWall.SetActive(false);
        }
        else
        {
            BlockPath("North");
        }

        // Check East 
        if (IsObjectInDirection(Vector2.right))
        {
            potentialSpawns.Remove(eastSpawn.transform);
            EastWall.SetActive(false);
        }
        else
        {
            BlockPath("East");
        }

        // Check South 
        if (IsObjectInDirection(Vector2.down))
        {
            potentialSpawns.Remove(southSpawn.transform);
            SouthWall.SetActive(false);
        }
        else
        {
            BlockPath("South");
        }

        // Check West
        if (IsObjectInDirection(Vector2.left))
        {
            potentialSpawns.Remove(westSpawn.transform);
            WestWall.SetActive(false);
        }
        else
        {
            BlockPath("West");
        }
    }
}
