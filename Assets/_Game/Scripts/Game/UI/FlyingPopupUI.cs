using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FlyingPopupUI : CustomMonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _valueText;
    [SerializeField]
    private float _despawnTime = 0.5f;

    public void Initialize(float value)
    {
        string text = "";
        if (value > 0.0f)
        {
            text = "+";
        }
        else if (value < 0.0f)
        {
            text = "-";
        }
        text += Mathf.Abs(value);

        _valueText.text = text;

        Invoke(nameof(Despawn), _despawnTime);
    }

    public void Initialize()
    {
        Invoke(nameof(Despawn), _despawnTime);
    }

    public void Despawn()
    {
        Destroy(gameObject);
    }
}
