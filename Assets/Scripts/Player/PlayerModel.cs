using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InventorySlotData
{
    private string _itemId = "";
    private int _count = 0;

    public string ItemId
    {
        get { return _itemId; }
        set { _itemId = value; }
    }

    public int Count
    {
        get { return _count; }
        set { _count = value; }
    }
}


[Serializable]
public class PlayerModel
{
    private string _playerName;
    private int _playerTotalExp;
    private string _lastMapDataId;
    private Vector3 _lastMapPosition;


    // 프로퍼티
    public string PlayerName { get { return _playerName; } set { _playerName = value; } }
    public int PlayerTotalExp { get { return _playerTotalExp; } set { _playerTotalExp = value; } }
    public string LastMapDataId { get { return _lastMapDataId; } set { _lastMapDataId = value; } }
    public Vector3 LastMapPosition { get { return _lastMapPosition; } set { _lastMapPosition = value; } }

    public void AddItem(string itemId, int count)
    {
    }

    public void RemoveItem(string itemId, int count)
    {
    }

    public void ResetToDefault()
    {
    }



}