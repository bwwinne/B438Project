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

    public delegate void DieDelegate();
    public delegate void RespawnDelegate();

    [SyncEvent(channel = 1)]
    public event DieDelegate EventDie;
    [SyncEvent]
    public event RespawnDelegate EventRespawn;

    public override void OnStartServer()
    {
        SpawnDragon();
        SpawnLog();
    }

    

    public void SpawnDragon()
    {
        StartCoroutine(SpawnDragonCoroutine());
    }

    public void SpawnLog()
    {
        StartCoroutine(SpawnLogCoroutine());
    }

    private IEnumerator SpawnDragonCoroutine()
    {
        yield return new WaitForSeconds(.5f);
        GameObject dragon = Instantiate(DragonPrefab, DragonSpawn.position, Quaternion.identity) as GameObject;
        NetworkServer.Spawn(dragon);
    }

    private IEnumerator SpawnLogCoroutine()
    {
        yield return new WaitForSeconds(.5f);
        GameObject log = Instantiate(LogPrefab, LogSpawn.position, Quaternion.identity) as GameObject;
        NetworkServer.Spawn(log);
    }
    /*
    void SpawnDragon()
    {
        GameObject dragon = Instantiate(DragonPrefab, DragonSpawn.position, Quaternion.identity) as GameObject;
        NetworkServer.Spawn(dragon);
    }

    void SpawnLog()
    {
        GameObject log = Instantiate(LogPrefab, LogSpawn.position, Quaternion.identity) as GameObject;
        NetworkServer.Spawn(log);
    } */
}
