using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : Singleton<ResourceManager>
{
    [field: SerializeField]
    public GameObject HorizontalWallPrefab { get; private set; }
    [field: SerializeField]
    public GameObject VerticalWallPrefab { get; private set; }

    public event EventHandler OnGemChanged;
    public event EventHandler OnGoldChanged;

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

        OnGemChanged?.Invoke(this, EventArgs.Empty);

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

        OnGoldChanged?.Invoke(this, EventArgs.Empty);

        return true;
    }

    private void ResetGold()
    {
        _gold = 0;
    }
}
