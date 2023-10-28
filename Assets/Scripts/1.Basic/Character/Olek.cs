using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Olek : Character
{
    private int skillEnergy;
    private int skillEnergyMax = 20;
    private int readyStack;//số skill tích trữ
    private int activeStack;// số stack tăng damage
    public override void Awake()
    {
        GetName();
        SetAtk(5);
        SetSkillName("");
        SetSkillDetail("When active put a stack of \"Loving Punch\", max 3. The next time you deal damage, for each stack, damage will increase by 100%. Energy skill: 20, stack up to 3.");
        //characterSkillBar.setMaxSkillValue(skillEnergyMax);
        //characterSkillBar.setSkillValue(skillEnergy);
        this.skillEnergy = 0;
        this.readyStack = 1;
        this.activeStack = 0;
    }
    private void Update()
    {
        //--------------------------//
        try{
            board = GameObject.FindGameObjectWithTag("Board");
            boards = board.GetComponent<Boards>();
        }
        catch (Exception e){
            Debug.Log(e);
        }
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
