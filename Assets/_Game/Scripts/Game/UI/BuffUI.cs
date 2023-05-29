using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffUI : CustomMonoBehaviour
{
    [SerializeField]
    private Image _iconImage;

    public void Initialize(Sprite iconSprite)
    {
        _iconImage.sprite = iconSprite;
    }

    public void Despawn()
    {
        Destroy(gameObject);
    }
}
