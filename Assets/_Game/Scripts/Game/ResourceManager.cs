using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : Singleton<ResourceManager>
{
    [field: Header("Important Prefabs")]
    [field: SerializeField]
    public GameObject HorizontalWallPrefab { get; private set; }
    [field: SerializeField]
    public GameObject VerticalWallPrefab { get; private set; }
    [field: SerializeField]
    public Player PlayerPrefab { get; private set; }
    [field: SerializeField]
    public Level[] LevelPrefabArray { get; private set; }

    [field: Header("Pools")]
    [field: SerializeField]
    public EnemyPool ZombieEnemyPool { get; private set; }
    [field: SerializeField]
    public EnemyPool WitchEnemyPool { get; private set; }
    [field: SerializeField]
    public HitVFXPool BulletVFXPool { get; private set; }
    [field: SerializeField]
    public HitVFXPool OrbtVFXPool { get; private set; }
    [field: SerializeField]
    public HitVFXPool SlashVFXPool { get; private set; }

    [field: Header("UI Prefabs")]
    [field: SerializeField]
    public FlyingPopupUI ClearUIPrefab { get; private set; }

    public class OnGemChangedArgs : EventArgs
    {
        public float Amount;
    }
    public event EventHandler<OnGemChangedArgs> OnGemChanged;
    public class OnGoldChangedArgs : EventArgs
    {
        public float Amount;
    }
    public event EventHandler<OnGoldChangedArgs> OnGoldChanged;

    private int _gem;
    private int _gold;

    public int GetGem()
    {
        return _gem;
    }

    public bool TryChangeGem(int value)
    {
        if (_gem + value < 0)
        {
            return false;
        }

        _gem += value;

        OnGemChanged?.Invoke(this, new OnGemChangedArgs
        {
            Amount = value,
        });

        return true;
    }

    public int GetGold()
    {
        return _gold;
    }

    public bool TryChangeGold(int value)
    {
        if (_gold + value < 0)
        {
            return false;
        }

        _gold += value;

        OnGoldChanged?.Invoke(this, new OnGoldChangedArgs
        {
            Amount = value,
        });

        return true;
    }

    private void ResetGold()
    {
        _gold = 0;
    }
}
