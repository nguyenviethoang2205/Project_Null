// using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemsManager : MonoBehaviour{    
    public List<ItemBase> items = new List<ItemBase>();
    public ItemBase randomItem2;

    public PlayerInventory playerInventory;

    public SpriteRenderer itemImage2;
    public Text itemName2;
    public Text itemSkill2;

    private void Start()
    {
        BoomBaKuBum item1 = ScriptableObject.CreateInstance<BoomBaKuBum>();
        item1.Initialize();
        items.Add(item1);
        
        int randomIndex = Random.Range(0, items.Count);
        randomItem2 = items[randomIndex];
        randomItem2.Initialize();
        ShowItem2();
    }

    private void ShowItem2(){
        
        itemName2.text = randomItem2.itemName.ToString();
        itemSkill2.text = randomItem2.itemInfo.ToString();
        itemImage2.sprite = randomItem2.itemImage;
    }

    public void ChooseItem2(){
        playerInventory.AddItem(randomItem2);
    }
}
