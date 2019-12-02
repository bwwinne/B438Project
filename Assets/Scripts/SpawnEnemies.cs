using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SpawnEnemies : NetworkBehaviour
{
    public GameObject DragonPrefab;
    public GameObject LogPrefab;
    public Transform DragonSpawn;
    public Transform LogSpawn;

    public override void OnStartServer()
    {
        Invoke("SpawnDragon", 0.5f);
        Invoke("SpawnLog", 0.5f);
        //SpawnDragon();
        //SpawnLog();
    }

    void SpawnDragon()
    {
        GameObject dragon = Instantiate(DragonPrefab, DragonSpawn.position, Quaternion.identity) as GameObject;
        NetworkServer.Spawn(dragon);
    }

    void SpawnLog()
    {
        GameObject log = Instantiate(LogPrefab, LogSpawn.position, Quaternion.identity) as GameObject;
        NetworkServer.Spawn(log);
    }
}
