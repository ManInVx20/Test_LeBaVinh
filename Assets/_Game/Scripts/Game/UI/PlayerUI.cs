using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : CustomMonoBehaviour
{
    [SerializeField]
    private Player _player;
    [SerializeField]
    private FlyingPrizeUI _energyFlyingPrizeUIPrefab;
    [SerializeField]
    private FlyingPrizeUI _goldFlyingPrizeUIPrefab;

    private void Start()
    {
        _player.OnCharacterEnergyChanged += Player_OnCharacterEnergyChanged;

        ResourceManager.Instance.OnGoldChanged += ResourceManager_OnGoldChanged;
    }

    private void Update()
    {
        GetTransform().rotation = Quaternion.identity;
    }

    private void Player_OnCharacterEnergyChanged(object sender, Character.OnCharacterEnergyChangedArgs args)
    {
        if (this == null)
        {
            return;
        }

        if (args.Amount > 0.0f)
        {
            Instantiate(_energyFlyingPrizeUIPrefab, GetTransform()).Initialize(args.Amount);
        }
    }

    private void ResourceManager_OnGoldChanged(object sender, ResourceManager.OnGoldChangedArgs args)
    {
        if (this == null)
        {
            return;
        }

        if (args.Amount > 0.0f)
        {
            Instantiate(_goldFlyingPrizeUIPrefab, GetTransform()).Initialize(args.Amount);
        }
    }
}
