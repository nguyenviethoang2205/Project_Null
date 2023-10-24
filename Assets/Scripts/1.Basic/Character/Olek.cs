using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Olek : CharacterCore
{
    private int skillEnergy;
    private int skillEnergyMax = 20;
    private int skillStack;//số skill tích trữ
    private int activeStack;// số stack tăng damage
    public override void Awake()
    {
        GetName();
        SetLevel(1);
        SetExp(0);
        SetMaxExp(100);
        SetAtk(5);
        this.skillEnergy = 0;
        this.skillStack = 1;
        this.activeStack = 0;
    }
    private void Update()
    {
        if (activeStack < 3 && skillStack > 0 && Input.GetKeyDown(KeyCode.E))
        {
            CharacterSkill();
            activeStack++;
            skillStack--;
        }
    }
    public void CharacterSkill()
    {
        this.boards.additionDamage += 100;
    }
    public override void CheckBeforeClearLine(int totalLineClear)
    {
        switch (totalLineClear)
        {
            case 1:
                skillEnergy += 1;
                stackEnegry();
                break;
            case 2:
                skillEnergy += 3;
                stackEnegry();
                break;
            case 3:
                skillEnergy += 5;
                stackEnegry();
                break;
            case 4:
                skillEnergy += 7;
                stackEnegry();
                break;
            default:
                Debug.LogWarning("some thing wrong");
                break;
        }
    }
    public override void CheckAfterClearLine(int totalLineClear)
    {
        boards.additionDamage = boards.additionDamage - 100 * activeStack;
        activeStack = 0;
    }
    private void stackEnegry()
    {
        if (skillEnergy >= skillEnergyMax && skillStack < 2)
        {
            skillStack++;
            skillEnergy -= skillEnergyMax;
        }
        else if (skillEnergy >= skillEnergyMax && skillStack == 2)
        {
            skillStack++;
            skillEnergy = 0;
        }
        else
            skillEnergy = 0;
    }
    public override string GetName()
    {
        SetName("Olek");
        return characterName;
    }
}
