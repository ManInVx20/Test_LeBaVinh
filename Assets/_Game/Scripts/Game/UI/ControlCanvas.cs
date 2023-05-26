using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlCanvas : UICanvas
{
    [SerializeField]
    private HoldButton _attackHoldButton;
    [SerializeField]
    private Button _autoAttackButton;
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
        _autoAttackButton.onClick.AddListener(() =>
        {
            Player.Instance?.ToggleAutoAttack();
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
}
