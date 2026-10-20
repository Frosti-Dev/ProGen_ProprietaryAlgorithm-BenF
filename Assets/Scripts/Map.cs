using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnpoints = new List<Transform>();

    public GameObject northSpawn;
    public GameObject eastSpawn;
    public GameObject southSpawn;
    public GameObject westSpawn;


    private GenerationManager genManager;

    private void Start()
    {
        northSpawn = GameObject.FindGameObjectWithTag("NorthSpawn");
        spawnpoints.Add(northSpawn.transform);

        eastSpawn = GameObject.FindGameObjectWithTag("EastSpawn");
        spawnpoints.Add(eastSpawn.transform);

        southSpawn = GameObject.FindGameObjectWithTag("SouthSpawn");
        spawnpoints.Add(southSpawn.transform);

        westSpawn = GameObject.FindGameObjectWithTag("WestSpawn");
        spawnpoints.Add(westSpawn.transform);


        genManager = ServiceHub.Instance.generationManager;
    }

    private void OnEnable()
    {
        int randomID = Random.Range(0, 1);
        Initialize(genManager.maps[0]);
    }

    public void Initialize(Map map)
    {
        foreach(Transform spawn in spawnpoints)
        {
            Map GO = Instantiate(map, spawn);
        }
    }
}
