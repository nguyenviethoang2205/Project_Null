using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buffy: Character
{
    public bool skillReady;
    public bool skillActive;
    public int changePieceUses;
    public float timeFlag;
    public override void Awake()
    {
        skillEnergy = 15;
        skillEnergyMax = 20;
        skillImage = Resources.Load<Sprite>("PlayerSkill/Buffy_Skill");
        GetStyle();
        GetDifficulty();
        GetName();
        SetAtk(4);
        SetSkillName("FREEZE TIME");
        SetSkillDetail("When activated, the block falling speed will significantly decrease for a short time. During that time, you can change the current block into the next block a maximum of 3 times. (Countdown: 20 Energy, restore energy by clearing lines).");

        this.skillReady = false;

        this.skillActive = false;
        this.changePieceUses = 0;
        this.timeFlag = -1;
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
        //--------------------------//
        if (changePieceUses > 0){ 
            try{
                boards.levelAnimationUIManager.SkillCanUse();
                boards.levelAnimationUIManager.EnergyFull();
            } catch (Exception e){
                Debug.Log(e);
            }
        } else if (skillEnergy < skillEnergyMax){
            try{
                boards.levelAnimationUIManager.SkillCannotUse();
                boards.levelAnimationUIManager.EnergyNotFull();
            } catch (Exception e){
                Debug.Log(e);
            }
        } 

        if (skillReady == true && (Input.GetKeyDown(KeyCode.E)||Input.GetKeyDown(KeyCode.C)))
        {
            try{
                boards.levelAudioPlayer.PlayPlayerAttackSound();
                boards.animationCharacter.PlayerDoAttackAction();
                boards.characterAnimation.EnemyDoDefenseAction();
            }
            catch (Exception e){
                Debug.Log(e);
            }
            if (skillActive == false)
                timeFlag = Time.time;
            CharacterSkill();
        }
        if (timeFlag != -1 && timeFlag + 5f <= Time.time)
        {
            skillActive = false;
            skillReady = false;
            skillEnergy = 0;
            skillEnergyMax = 20;
            boards.levelAnimationUIManager.SkillCannotUse();
            boards.levelAnimationUIManager.EnergyNotFull();
            timeFlag = -1;
            changePieceUses = 0;
            this.boards.activePiece.getSpeed(0.2f);
            this.boards.dropSpeed *= 0.2f;
        }
    }
    private void CharacterSkill()
    {
        if (skillActive == false)
        {
            skillActive = true;
            this.boards.activePiece.getSpeed(5);
            this.boards.dropSpeed *= 5;
        }
        else
        {
            if (this.changePieceUses > 0)
            {
                skillEnergy--;
                RectInt bounds = this.boards.Bounds;
                Vector3Int currentPosition = this.boards.activePiece.position;
                Vector3Int[] checkCells = new Vector3Int[4];
                for (int i = 0; i < checkCells.Length; i++)
                {
                    checkCells[i] = this.boards.nextBox.nextPiece.cells[i];
                }
                this.boards.Clear(this.boards.activePiece);
                if (IsValidPosition(checkCells, currentPosition))
                {
                    this.boards.SpawmPiece(currentPosition);
                }
                else
                    this.boards.Set(this.boards.activePiece);
                changePieceUses--;
            }
        }
    }
    private bool IsValidPosition(Vector3Int[] cells, Vector3Int position)
    {
        RectInt bounds = this.boards.Bounds;

        for (int i = 0; i < cells.Length; i++)
        {
            Vector3Int tilePosition = cells[i] + position;

            // Nằm ngoài biên
            if (!bounds.Contains((Vector2Int)tilePosition))
            {
                return false;
            }

            // Vị trí đã có block
            if (this.boards.tilemap.HasTile(tilePosition))
            {
                return false;
            }
        }
        return true;
    }
    public override void CheckBeforeClearLine(int totalLineClear)
    {
        switch (totalLineClear)
        {
            case 0:
                break;
            case 1:
                checkEnergy(totalLineClear);
                break;
            case 2:
                checkEnergy(totalLineClear);
                break;
            case 3:
                checkEnergy(totalLineClear);
                break;
            case 4:
                checkEnergy(totalLineClear);
                break;
            default:
                Debug.LogWarning("some thing wrong");
                break;
        }
    }
    private void checkEnergy(int totalLineClear)
    {
        if (this.skillReady == false)
        {
            skillEnergy = skillEnergy + 1 + 2 * (totalLineClear - 1);
            if (skillEnergy >= skillEnergyMax)
            {
                skillReady = true;
                skillEnergyMax = 3;
                skillEnergy = skillEnergyMax;
                changePieceUses = 3;
                boards.levelAnimationUIManager.SkillCanUse();
                boards.levelAnimationUIManager.EnergyFull();
            }
        }
    }
    public override string GetName()
    {
        SetName("Buffy");
        return name;
    }

    public override string GetStyle()
    {
        SetCharStyle("Control");
        return charStyle;

    }

    public override string GetDifficulty()
    {
        SetCharDifficulty("Hard");
        return charDifficulty;
    }
}
