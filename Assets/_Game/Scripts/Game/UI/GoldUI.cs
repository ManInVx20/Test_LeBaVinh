using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoldUI : CustomMonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _goldText;

    private void Start()
    {
        ResourceManager.Instance.OnGoldChanged += ResourceManager_OnGoldChanged;
    }

    private void ResourceManager_OnGoldChanged(object sender, System.EventArgs args)
    {
        _goldText.text = ResourceManager.Instance.GetGold().ToString();
    }
}
