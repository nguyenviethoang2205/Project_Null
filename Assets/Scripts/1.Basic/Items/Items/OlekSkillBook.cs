using UnityEngine;

[CreateAssetMenu(fileName = "OlekSkillBook", menuName = "Game/OlekSkillBook")]
public class OlekSkillBook : ItemBase
{
    public override void Initialize()
    {
        itemName = "Olek's martial arts secret";
        itemInfo = "+10 to the current combo you have.";
        itemImage = Resources.Load<Sprite>("Items/Item8");;
    }

    public override void UseItems(Boards boards){
        boards.ItemsInsertCombo(10);
    }
}