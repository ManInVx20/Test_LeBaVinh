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
        public GameObject MinimapGameObject;
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

    [Header("Components")]
    [SerializeField]
    private BattleSystem _battleSystem;

    private void Start()
    {
        _battleSystem.OnBattleStart += BattleSystem_OnBattleStart;
        _battleSystem.OnBattleOver += BattleSystem_OnBattleOver;

        BuildBlocks();
    }

    private void BattleSystem_OnBattleStart(object sender, EventArgs args)
    {
        SpawnDoorBlocks();
    }

    private void BattleSystem_OnBattleOver(object sender, EventArgs args)
    {
        DespawnDoorBlocks();
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

            roomData.MinimapGameObject.SetActive(false);
        }
        else
        {
            for (int i = 0; i < roomData.CorridorBlockTransformArray.Length; i++)
            {
                Instantiate(corridorPrefab, roomData.CorridorBlockTransformArray[i]);
            }
        }
    }

    private void SpawnDoorBlocks()
    {
        GenerateDoorBlocks(_upConnectedRoom, ResourceManager.Instance.HorizontalWallPrefab, _upRoomData);
        GenerateDoorBlocks(_downConnectedRoom, ResourceManager.Instance.HorizontalWallPrefab, _downRoomData);
        GenerateDoorBlocks(_leftConnectedRoom, ResourceManager.Instance.VerticalWallPrefab, _leftRoomData);
        GenerateDoorBlocks(_rightConnectedRoom, ResourceManager.Instance.VerticalWallPrefab, _rightRoomData);
    }

    private void DespawnDoorBlocks()
    {
        DestroyDoorBlocks(_upConnectedRoom, ResourceManager.Instance.HorizontalWallPrefab, _upRoomData);
        DestroyDoorBlocks(_downConnectedRoom, ResourceManager.Instance.HorizontalWallPrefab, _downRoomData);
        DestroyDoorBlocks(_leftConnectedRoom, ResourceManager.Instance.VerticalWallPrefab, _leftRoomData);
        DestroyDoorBlocks(_rightConnectedRoom, ResourceManager.Instance.VerticalWallPrefab, _rightRoomData);
    }

    private void GenerateDoorBlocks(Room connectedRoom, GameObject doorPrefab,  RoomData roomData)
    {
        if (connectedRoom != null)
        {
            for (int i = 0; i < roomData.DoorBlockTransformArray.Length; i++)
            {
                Instantiate(doorPrefab, roomData.DoorBlockTransformArray[i]);
            }
        }
    }

    private void DestroyDoorBlocks(Room connectedRoom, GameObject doorPrefab, RoomData roomData)
    {
        if (connectedRoom != null)
        {
            for (int i = 0; i < roomData.DoorBlockTransformArray.Length; i++)
            {
                roomData.DoorBlockTransformArray[i].ClearChildren();
            }
        }
    }
}
