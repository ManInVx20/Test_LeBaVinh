using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Level : CustomMonoBehaviour
{
    [SerializeField]
    private Transform _startPoint;
    [SerializeField]
    private Enemy _boss;

    private CameraFollow[] _cameraFollowArray;

    private void Awake()
    {
        _cameraFollowArray = FindObjectsOfType<CameraFollow>();
    }

    private void Start()
    {
        Player.Instance.GetTransform().position = _startPoint.position;

        for (int i = 0; i < _cameraFollowArray.Length; i++)
        {
            _cameraFollowArray[i].SetTarget(Player.Instance.GetTransform());
        }

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
        for (int i = 0; i < _cameraFollowArray.Length; i++)
        {
            _cameraFollowArray[i].SetTarget(null);
        }

        Destroy(gameObject);
    }
}
