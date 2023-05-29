using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthUI : CustomMonoBehaviour
{
    [SerializeField]
    private Enemy _enemy;
    [SerializeField]
    private Slider _healthSlider;
    [SerializeField]
    private TextMeshProUGUI _valueText;

    private void OnEnable()
    {
        _enemy.OnCharacterHealthChanged += Enemy_OnCharacterHealthChanged;
    }

    private void OnDisable()
    {
        _enemy.OnCharacterHealthChanged -= Enemy_OnCharacterHealthChanged;
    }

    private void Update()
    {
        GetTransform().rotation = Quaternion.identity;
    }

    private void Enemy_OnCharacterHealthChanged(object sender, Character.OnCharacterHealthChangedArgs args)
    {
        _healthSlider.value = args.Health / args.MaxHealth;
        _valueText.text = $"{Mathf.CeilToInt(args.Health)}/{Mathf.CeilToInt(args.MaxHealth)}";
    }
}
