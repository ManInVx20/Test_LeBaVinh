using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapIcon : CustomMonoBehaviour
{
    private void Update()
    {
        GetTransform().rotation = Quaternion.identity;
    }
}
