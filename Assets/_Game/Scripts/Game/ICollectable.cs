using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollectable
{
    public int Price { get; set; }

    void Collect();
}
