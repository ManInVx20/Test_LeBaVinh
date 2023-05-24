using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : CustomMonoBehaviour
{
    [Serializable]
    private struct RoomData
    {
        public Transform[] DoorBlockTransformArray;
        public Transform[] CorridorBlockTransformArray;
    }

    [Header("Room Data")]
    [SerializeField]
    private RoomData _upRoomData;
    [SerializeField]
    private RoomData _downRoomData;
    [SerializeField]
    private RoomData _leftRoomData;
    [SerializeField]
    private RoomData _rightRoomData;

    [Header("Connected Rooms")]
    [SerializeField]
    private Room _upConnectedRoom;
    [SerializeField]
    private Room _downConnectedRoom;
    [SerializeField]
    private Room _leftConnectedRoom;
    [SerializeField]
    private Room _rightConnectedRoom;

    [Header("Props")]
    [SerializeField]
    private BattleSystem _battleSystem;

    private void Start()
    {
        BuildBlocks();
    }

    private void BuildBlocks()
    {
        GenerateBlocks(_upConnectedRoom, ResourceManager.Instance.HorizontalWallPrefab, ResourceManager.Instance.VerticalWallPrefab, _upRoomData);
        GenerateBlocks(_downConnectedRoom, ResourceManager.Instance.HorizontalWallPrefab, ResourceManager.Instance.VerticalWallPrefab, _downRoomData);
        GenerateBlocks(_leftConnectedRoom, ResourceManager.Instance.VerticalWallPrefab, ResourceManager.Instance.HorizontalWallPrefab, _leftRoomData);
        GenerateBlocks(_rightConnectedRoom, ResourceManager.Instance.VerticalWallPrefab, ResourceManager.Instance.HorizontalWallPrefab, _rightRoomData);
    }

    private void GenerateBlocks(Room connectedRoom, GameObject doorPrefab, GameObject corridorPrefab, RoomData roomData)
    {
        if (connectedRoom == null)
        {
            for (int i = 0; i < roomData.DoorBlockTransformArray.Length; i++)
            {
                Instantiate(doorPrefab, roomData.DoorBlockTransformArray[i]);
            }
        }
        else
        {
            for (int i = 0; i < roomData.CorridorBlockTransformArray.Length; i++)
            {
                Instantiate(corridorPrefab, roomData.CorridorBlockTransformArray[i]);
            }
        }
    }
}
