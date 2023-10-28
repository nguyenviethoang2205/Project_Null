// using System.Diagnostics;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Olek : Character{
    private int readyStack;//số skill tích trữ
    private int activeStack;// số stack tăng damage
    public override void Awake()
    {
        skillImage = Resources.Load<Sprite>("PlayerSkill/Olek_Skill");
        GetName();
        SetAtk(6);
        SetSkillName("");
        SetSkillDetail("When active put a stack of \"Loving Punch\", max 3. The next time you deal damage, for each stack, damage will increase by 100%. Energy skill: 20, stack up to 3.");
        skillEnergy = 0;
        skillEnergyMax = 60;
        this.readyStack = 1;
        this.activeStack = 0;
    }
    private void Update()
    {
        //--------------------------//
        try{
            board = GameObject.FindGameObjectWithTag("Board");
            boards = board.GetComponent<Boards>();
            boards.levelAnimationUIManager.SetMaxEnergy(skillEnergyMax);
            boards.levelAnimationUIManager.SetEnergy(skillEnergy);
        }
        catch (Exception e){
            Debug.Log(e);
        }

        if (skillEnergy < 20 * (activeStack + 1) ){
            try{
                boards.levelAnimationUIManager.SkillCannotUse();
                boards.levelAnimationUIManager.EnergyNotFull();
            } catch (Exception e){
                Debug.Log(e);
            }
        }
        //--------------------------//
        if (activeStack < 3 && skillEnergy >= 20 * (activeStack + 1)  && Input.GetKeyDown(KeyCode.E)){
            try{
                boards.levelAudioPlayer.PlayPlayerAttackSound();
                boards.animationCharacter.PlayerDoAttackAction();
                boards.characterAnimation.EnemyDoDefenseAction();
            }
            catch (Exception e){
                Debug.Log(e);
            }
            CharacterSkill();
            activeStack++;
        }

    }
    private void CharacterSkill()
    {
        this.boards.additionDamage += 100;
    }
    public override void CheckBeforeClearLine(int totalLineClear){
        switch (totalLineClear){
            case 1:
                skillEnergy += 20;
                checkEnergy();
                boards.levelAnimationUIManager.SetEnergy(skillEnergy);
                break;
            case 2:
                skillEnergy += 20;
                checkEnergy();
                boards.levelAnimationUIManager.SetEnergy(skillEnergy);
                break;
            case 3:
                skillEnergy += 40;
                checkEnergy();
                boards.levelAnimationUIManager.SetEnergy(skillEnergy);
                break;
            case 4:
                skillEnergy += 60;
                checkEnergy();
                boards.levelAnimationUIManager.SetEnergy(skillEnergy);
                break;
        }
    }
    public override void CheckAfterClearLine(int totalLineClear)
    {
        boards.additionDamage = boards.additionDamage - 100 * activeStack;
        activeStack = 0;
    }
    private void checkEnergy(){
        if (skillEnergy >= skillEnergyMax){
            skillEnergy = skillEnergyMax;
        }
        // Check Skill
        if (skillEnergy >= (activeStack + 1) * 20){
            boards.levelAnimationUIManager.SkillCanUse();
            boards.levelAnimationUIManager.EnergyFull();
        }
    }
    public override string GetName()
    {
        SetName("Olek");
        return name;
    }
}
