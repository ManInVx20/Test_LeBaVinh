using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusUI : CustomMonoBehaviour
{
    [SerializeField]
    private Slider _healthSlider;
    [SerializeField]
    private TextMeshProUGUI _heathText;
    [SerializeField]
    private Slider _energySlider;
    [SerializeField]
    private TextMeshProUGUI _energyText;
    [SerializeField]
    private Slider _shieldSlider;
    [SerializeField]
    private TextMeshProUGUI _shieldText;

    private void Start()
    {
        Player.Instance.OnCharacterHealthChanged += Player_OnCharacterHealthChanged;
        Player.Instance.OnCharacterEnergyChanged += Player_OnCharacterEnergyChanged;
        Player.Instance.OnCharacterShieldChanged += Player_OnCharacterShieldChanged;
    }

    private void Player_OnCharacterHealthChanged(object sender, Character.OnCharacterHealthChangedArgs args)
    {
        _healthSlider.value = args.Health / args.MaxHealth;
        _heathText.text = $"{Mathf.CeilToInt(args.Health)}/{Mathf.CeilToInt(args.MaxHealth)}";
    }

    private void Player_OnCharacterEnergyChanged(object sender, Character.OnCharacterEnergyChangedArgs args)
    {
        _energySlider.value = args.Energy / args.MaxEnergy;
        _energyText.text = $"{Mathf.CeilToInt(args.Energy)}/{Mathf.CeilToInt(args.MaxEnergy)}";
    }

    private void Player_OnCharacterShieldChanged(object sender, Character.OnCharacterShieldChangedArgs args)
    {
        _shieldSlider.value = args.Shield / args.MaxShield;
        _shieldText.text = $"{Mathf.CeilToInt(args.Shield)}/{Mathf.CeilToInt(args.MaxShield)}";
    }
}
