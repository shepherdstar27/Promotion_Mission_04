using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private static Inventory _instance;
    public static Inventory Instance
    {
        get { return _instance; }
        set { _instance = value; }
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }



    public void AddItemToInventory(string itemId, int count)
    {
        GameManager.Inst.PlayerModel.AddItem(itemId, count);
        RefreshInventoryUI();
    }

    public void RefreshInventoryUI()
    {

    }
}