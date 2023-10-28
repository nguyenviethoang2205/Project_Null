// using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemsManager : MonoBehaviour{    
    public List<ItemBase> items = new List<ItemBase>();
    public ItemBase randomItem1;
    public ItemBase randomItem2;
    public ItemBase randomItem3;
    public PlayerInventory playerInventory;

    public SpriteRenderer itemImage1;
    public Text itemName1;
    public Text itemSkill1;
    public SpriteRenderer itemImage2;
    public Text itemName2;
    public Text itemSkill2;
    public SpriteRenderer itemImage3;
    public Text itemName3;
    public Text itemSkill3;

    private void Start(){
        GetItem1();
        GetItem2();
        GetItem3();
        GetItem4();
        GetItem5();
        GetItem6();
        GetItem7();
        GetItem8();
        GetItem9();
        GetItem10();
        GetItem11();
        GetItem12();

        GetRandom1();
        GetRandom2();
        GetRandom3();
        
        ShowItem1();
        ShowItem2();
        ShowItem3();
    }

    private void GetRandom1(){
        int randomIndex = Random.Range(0, items.Count);
        randomItem1 = items[randomIndex];
        randomItem1.Initialize();
    }

    private void GetRandom2(){
        int randomIndex = Random.Range(0, items.Count);
        randomItem2 = items[randomIndex];
        randomItem2.Initialize();
    }

    private void GetRandom3(){
        int randomIndex = Random.Range(0, items.Count);
        randomItem3 = items[randomIndex];
        randomItem3.Initialize();
    }

    private void ShowItem1(){
        itemName1.text = randomItem1.itemName.ToString();
        itemSkill1.text = randomItem1.itemInfo.ToString();
        itemImage1.sprite = randomItem1.itemImage;
    }

    private void ShowItem2(){
        itemName2.text = randomItem2.itemName.ToString();
        itemSkill2.text = randomItem2.itemInfo.ToString();
        itemImage2.sprite = randomItem2.itemImage;
    }

    private void ShowItem3(){
        itemName3.text = randomItem3.itemName.ToString();
        itemSkill3.text = randomItem3.itemInfo.ToString();
        itemImage3.sprite = randomItem3.itemImage;
    }

    private void GetItem1(){
        GoldPill item = ScriptableObject.CreateInstance<GoldPill>();
        item.Initialize();
        items.Add(item);
    }
    private void GetItem2(){
        BoomBaKuBum item = ScriptableObject.CreateInstance<BoomBaKuBum>();
        item.Initialize();
        items.Add(item);
    }

    private void GetItem3(){
        LovePunch item = ScriptableObject.CreateInstance<LovePunch>();
        item.Initialize();
        items.Add(item);
    }

    private void GetItem4(){
        PowerPickaxe item = ScriptableObject.CreateInstance<PowerPickaxe>();
        item.Initialize();
        items.Add(item);
    }

    private void GetItem5(){
        GachaCapsule item = ScriptableObject.CreateInstance<GachaCapsule>();
        item.Initialize();
        items.Add(item);
    }

    private void GetItem6(){
        MagicOrb item = ScriptableObject.CreateInstance<MagicOrb>();
        item.Initialize();
        items.Add(item);
    }

    private void GetItem7(){
        MysteryBox item = ScriptableObject.CreateInstance<MysteryBox>();
        item.Initialize();
        items.Add(item);
    }

    private void GetItem8(){
        OlekSkillBook item = ScriptableObject.CreateInstance<OlekSkillBook>();
        item.Initialize();
        items.Add(item);
    }

    private void GetItem9(){
        DestinyCoin item = ScriptableObject.CreateInstance<DestinyCoin>();
        item.Initialize();
        items.Add(item);
    }

    private void GetItem10(){
        GalacticMushroom item = ScriptableObject.CreateInstance<GalacticMushroom>();
        item.Initialize();
        items.Add(item);
    }

    private void GetItem11(){
        AntiqueClock item = ScriptableObject.CreateInstance<AntiqueClock>();
        item.Initialize();
        items.Add(item);
    }

    private void GetItem12(){
        DrillingMachine item = ScriptableObject.CreateInstance<DrillingMachine>();
        item.Initialize();
        items.Add(item);
    }
    public void ChooseItem1(){
        playerInventory.AddItem(randomItem1);
    }

    public void ChooseItem2(){
        playerInventory.AddItem(randomItem2);
    }

    public void ChooseItem3(){
        playerInventory.AddItem(randomItem3);
    }
}
