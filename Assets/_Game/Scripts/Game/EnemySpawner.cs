using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : CustomMonoBehaviour
{
    [SerializeField]
    private EnemyPool[] _enemyPoolArray;
    [SerializeField]
    private Transform[] _spawnPointArray;

    public void Spawn()
    {
        Enemy enemy = _enemyPoolArray[0].GetPrefabInstance();
        enemy.GetTransform().position = _spawnPointArray[0].position;
    }
}
