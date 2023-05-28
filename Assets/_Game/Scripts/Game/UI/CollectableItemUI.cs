using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CollectableItemUI : CustomMonoBehaviour
{
    [SerializeField]
    private GameObject _collectableGameObject;
    [SerializeField]
    private TextMeshProUGUI _priceText;

    private ICollectable _collectableItem;

    private void Start()
    {
        _collectableItem = _collectableGameObject.GetComponent<ICollectable>();

        if (_collectableItem != null)
        {
            _priceText.text = _collectableItem.Price.ToString();
        }
    }

    private void Update()
    {
        GetTransform().rotation = Quaternion.identity;

        if (_collectableItem.Price == 0 && gameObject.activeInHierarchy)
        {
            gameObject.SetActive(false);
        }
    }
}
