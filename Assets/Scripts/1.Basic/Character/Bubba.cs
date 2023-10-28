using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubba : Character
{
    public override void Awake()
    {
        skillEnergyMax = 3000;
        skillEnergy = 0;
        skillImage = Resources.Load<Sprite>("PlayerSkill/Bubba_Skill");
        GetStyle();
        GetDifficulty();
        GetName();
        SetAtk(5);
        SetSkillName("I-BLOCK INCOMING");
        SetSkillDetail("When activated, your next block is an I-Block (Countdown: 15s).");
        this.skillReady = true;
        this.skillTiming = Time.time;
    }
    private void Start()
    {


    }
    private bool skillReady;
    public float skillTiming;
    private float coolDown = 15f;

    public void CharacterSkill()
    {
        this.boards.nextBox.ClearPiece();
        this.boards.nextBox.SpawmPiece(0);
        this.boards.ChangeNextPiece(0);
    }

    private void Update()
    {
        //--------------------------//
        try{
            board = GameObject.FindGameObjectWithTag("Board");
            boards = board.GetComponent<Boards>();
            boards.levelAnimationUIManager.SetMaxEnergy(skillEnergyMax);
            boards.levelAnimationUIManager.SetEnergy(skillEnergy);
            if (skillEnergy == skillEnergyMax){
                boards.levelAnimationUIManager.EnergyFull();
                boards.levelAnimationUIManager.SkillCanUse();
            } else {
                boards.levelAnimationUIManager.EnergyNotFull();
                boards.levelAnimationUIManager.SkillCannotUse();
            }
        }
        catch (Exception e){
            Debug.Log(e);
        }
        //--------------------------//

        if (skillEnergy == skillEnergyMax)
        {
            this.skillReady = true;
            skillEnergy = 3000;
        } else {
            if (boards.activePiece.pauseScreen.isPause == false && boards.activePiece.overScreen.isOver == false && boards.activePiece.victoryScreen.isVictory == false && boards.isAnimationRun == false && boards.checkEnemyScreen.isPause == false)
                skillEnergy++;
        }
        if (this.skillReady == true && (Input.GetKeyDown(KeyCode.E)||Input.GetKeyDown(KeyCode.C)) && skillEnergy == skillEnergyMax){
            CharacterSkill();
            skillReady = false;
            skillTiming = Time.time;
            skillEnergy = 0;
            try{
                boards.levelAudioPlayer.PlayPlayerAttackSound();
                boards.animationCharacter.PlayerDoAttackAction();
                boards.characterAnimation.EnemyDoDefenseAction();
            }
            catch (Exception e){
                Debug.Log(e);
            }
        }
    }

    public override string GetName()
    {
        SetName("Bubba");
        return name;
    }

    public override string GetStyle()
    {
        SetCharStyle("Balance");
        return charStyle;

    }

    public override string GetDifficulty()
    {
        SetCharDifficulty("Easy");
        return charDifficulty;
    }
}
