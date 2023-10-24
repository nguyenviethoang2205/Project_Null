using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Inventory", menuName = "Game/Inventory")]
public class PlayerInventory : ScriptableObject 
{
    public ItemBase itemBase;

    public bool isGetItem;

    public void AddItem(ItemBase item){
        itemBase = item;
        isGetItem = true;
        Debug.Log("Item: " + itemBase.itemName);
    }

    public void RemoveItem(){
        isGetItem = false;
        itemBase = null;
    }

    public void UseItem(Boards boards){
        itemBase.UseItems(boards);
        RemoveItem();
    }
}
