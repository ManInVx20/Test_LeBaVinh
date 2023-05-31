using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Level : CustomMonoBehaviour
{
    public Vector3 StartPosition => _startPoint.position;

    [SerializeField]
    private Transform _startPoint;
    [SerializeField]
    private Enemy _boss;

    private void Start()
    {
        if (_boss != null)
        {
            _boss.OnCharacterDespawned += Boss_OnCharacterDespawned;
        }
    }

    private void Boss_OnCharacterDespawned(object sender, System.EventArgs args)
    {
        GameManager.Instance.FinishGame(true);

        _boss.OnCharacterDespawned -= Boss_OnCharacterDespawned;
    }

    public void Despawn()
    {
        Destroy(gameObject);
    }
}
