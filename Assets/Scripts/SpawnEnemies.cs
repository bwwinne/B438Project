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
}
