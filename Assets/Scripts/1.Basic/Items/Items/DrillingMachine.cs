using UnityEngine;

public class DrillingMachine : ItemBase
{
    public override void Initialize()
    {
        itemName = "Handheld Drilling Machine";
        itemInfo = "randomly destroy 3 adjacent columns and have a rate of dealing 5 damage on each destroyed column.";
        itemImage = Resources.Load<Sprite>("Items/Item12");
    }

    public override void UseItems(Boards boards){
        boards.PlayerUseItemAnimation();
        boards.ItemDestroyColumn();
        int chooseRandom = Random.Range(1,3);
        if (chooseRandom == 1){
            boards.ItemsDealDamage(5);
            chooseRandom = Random.Range(1,3);
            if (chooseRandom == 1){
                boards.ItemsDealDamage(5);
                chooseRandom = Random.Range(1,3);
                if (chooseRandom == 1){
                    boards.ItemsDealDamage(5);
                }
            }
        }

    }
}