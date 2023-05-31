using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : CustomMonoBehaviour
{
    public Enemy Enemy { get; private set; }

    [SerializeField]
    private Enemy.Type _enemyType;

    public void Spawn()
    {
        EnemyPool enemyPool = null;
        switch (_enemyType)
        {
            case Enemy.Type.Zombie:
                enemyPool = ResourceManager.Instance.ZombieEnemyPool;

                break;
            case Enemy.Type.Witch:
                enemyPool = ResourceManager.Instance.WitchEnemyPool;

                break;
        }

        Enemy = enemyPool.GetPrefabInstance();
        Enemy.GetTransform().SetParent(GetTransform(), false);
    }
}
