using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlCanvas : UICanvas
{
    [SerializeField]
    private HoldButton _attackHoldButton;
    [SerializeField]
    private Toggle _autoAttackToggle;
    [SerializeField]
    private Button _swapWeaponButton;
    [SerializeField]
    private Button _interactButton;

    private void Awake()
    {
        _attackHoldButton.OnHoldButtonDown.AddListener(() =>
        {
            Player.Instance?.EnableManualAttack();
        });
        _attackHoldButton.OnHoldButtonUp.AddListener(() =>
        {
            Player.Instance?.DisableManualAttack();
        });
        _autoAttackToggle.onValueChanged.AddListener((value) =>
        {
            if (Player.Instance != null && Player.Instance.IsAutoAttacking() != value)
            {
                Player.Instance?.ToggleAutoAttack();
            }
        });
        _swapWeaponButton.onClick.AddListener(() =>
        {
            Player.Instance?.SwapWeapon();
        });
        _interactButton.onClick.AddListener(() =>
        {
            Player.Instance?.Interact();
        });
    }

    public override void Setup()
    {
        base.Setup();

        if (Player.Instance != null)
        {
            _autoAttackToggle.isOn = Player.Instance.IsAutoAttacking();
        }
    }
}
