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
            if (isGameStart == true){
                if (Input.GetKeyDown(KeyCode.Return)){
                    Debug.Log("UseItem");
                }
            }
        }
    }

    public void GetItemImage(){
        itemImage.sprite = playerInventory.itemBase.itemImage;     
    }

}
