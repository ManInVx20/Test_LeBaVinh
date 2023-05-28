using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FlyingPrizeUI : CustomMonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _valueText;

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

        Invoke(nameof(Despawn), 0.5f);
    }

    public void Despawn()
    {
        Destroy(gameObject);
    }
}
