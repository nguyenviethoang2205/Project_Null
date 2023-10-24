using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public SpriteRenderer itemImage;
    public PlayerInventory playerInventory;
    
    private void Update(){
        if (playerInventory.isGetItem == true){
            GetItemImage();
        }
    }
    public void GetItemImage(){
        itemImage.sprite = playerInventory.itemBase.itemImage;     
    }
}
