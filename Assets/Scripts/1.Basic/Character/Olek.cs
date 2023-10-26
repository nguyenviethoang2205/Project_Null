using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Olek : CharacterCore
{
    private int skillEnergy;
    private int skillEnergyMax = 20;
    private int readyStack;//số skill tích trữ
    private int activeStack;// số stack tăng damage
    public override void Awake()
    {
        GetName();
        SetLevel(1);
        SetExp(0);
        SetMaxExp(100);
        SetAtk(5);
        //characterSkillBar.setMaxSkillValue(skillEnergyMax);
        //characterSkillBar.setSkillValue(skillEnergy);
        this.skillEnergy = 0;
        this.readyStack = 1;
        this.activeStack = 0;
    }
    private void Update()
    {
        //--------------------------//
        board = GameObject.FindGameObjectWithTag("Board");
        boards = board.GetComponent<Boards>();
        //--------------------------//
        if (activeStack < 3 && readyStack > 0 && Input.GetKeyDown(KeyCode.E))
        {
            CharacterSkill();
            activeStack++;
            readyStack--;
        }
    }
    private void CharacterSkill()
    {
        this.boards.additionDamage += 100;
    }
    public override void CheckBeforeClearLine(int totalLineClear)
    {
        switch (totalLineClear)
        {
            case 1:
                skillEnergy += 1;
                checkEnegry();
                break;
            case 2:
                skillEnergy += 3;
                checkEnegry();
                break;
            case 3:
                skillEnergy += 5;
                checkEnegry();
                break;
            case 4:
                skillEnergy += 7;
                checkEnegry();
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
    private void checkEnegry()
    {
        if (skillEnergy >= skillEnergyMax && readyStack < 2)
        {
            readyStack++;
            skillEnergy -= skillEnergyMax;
        }
        else if (skillEnergy >= skillEnergyMax && readyStack == 2)
        {
            readyStack++;
            skillEnergy = 0;
        }
        else
            skillEnergy = 0;
    }
    public override string GetName()
    {
        SetName("Olek");
        return name;
    }
}
