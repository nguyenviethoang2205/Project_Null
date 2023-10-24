using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public SpriteRenderer itemImage;
    public PlayerInventory playerInventory;

    public bool isGameStart = false;
    
    private void Update(){
        if (playerInventory.isGetItem == true){
            GetItemImage();
        }
    }

    public void UseItems(Boards boards){
        DeleteItemImage();
        playerInventory.UseItem(boards);
    }

    public void GetItemImage(){
        itemImage.sprite = playerInventory.itemBase.itemImage;     
    }

    private void DeleteItemImage(){
        itemImage.sprite = null;     
    }

}
