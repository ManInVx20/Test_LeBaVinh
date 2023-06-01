using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : CustomMonoBehaviour
{
    public Enemy Enemy { get; private set; }

    [SerializeField]
    private Enemy.Type _enemyType;

    public Enemy Spawn()
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
            case Enemy.Type.Boss_Zombie:
                enemyPool = ResourceManager.Instance.BossZombieEnemyPool;

                break;
        }

        Enemy = enemyPool.GetPrefabInstance();
        Enemy.GetTransform().SetParent(GetTransform(), false);

        return Enemy;
    }
}
