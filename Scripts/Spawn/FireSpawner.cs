using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class VariantsSpawnPos
{
    public Vector3 Fire1;
    public Vector3 Fire2;
    public Vector3 Fire3;
}

public class FireSpawner : MonoBehaviour
{
    public GameObject FirePrefab;
    public Vector2 fieldSize = new Vector2(5f, 5f);
    [SerializeField]public VariantsSpawnPos[] variantsSpawn;

    void Start()
    {
        VariantsSpawnPos variantsSpawnPos = variantsSpawn[UnityEngine.Random.Range(0, variantsSpawn.Length)];
        Instantiate(FirePrefab, variantsSpawnPos.Fire1, Quaternion.identity);
        Instantiate(FirePrefab, variantsSpawnPos.Fire2, Quaternion.identity);
        Instantiate(FirePrefab, variantsSpawnPos.Fire3, Quaternion.identity);
    }
}