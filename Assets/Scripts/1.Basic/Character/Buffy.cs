using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buffy: Character
{
    public int skillEnergy;
    public bool skillReady;
    public bool skillActive;
    public int changePieceUses;
    public float timeFlag;
    private int skillEnergyMax = 20;
    public override void Awake()
    {
        GetName();
        SetAtk(5);
        SetSkillName("");
        SetSkillDetail("For 5s, Drop Speed will decrease 5 time. When the skill is active, reactive will change the Currnet Piece into the Next Piece, max 3 time. Energy skill: 20");
        //characterSkillBar.setMaxSkillValue(skillEnergyMax);
        //characterSkillBar.setSkillValue(skillEnergy);
        this.skillEnergy = 15;
        this.skillReady = true;
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
        }
        catch (Exception e){
            Debug.Log(e);
        }
        //--------------------------//

        if (skillReady == true && Input.GetKeyDown(KeyCode.E))
        {
            if (skillActive == false)
                timeFlag = Time.time;
            CharacterSkill();
        }
        if (timeFlag != -1 && timeFlag + 5f <= Time.time)
        {
            skillActive = false;
            skillReady = false;
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
            if (this.changePieceUses < 3)
            {
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
                    this.changePieceUses++;
                }
                else
                    this.boards.Set(this.boards.activePiece);
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
                checkEnegry(totalLineClear);
                break;
            case 2:
                checkEnegry(totalLineClear);
                break;
            case 3:
                checkEnegry(totalLineClear);
                break;
            case 4:
                checkEnegry(totalLineClear);
                break;
            default:
                Debug.LogWarning("some thing wrong");
                break;
        }
    }
    private void checkEnegry(int totalLineClear)
    {
        if (this.skillReady == false)
        {
            this.skillEnergy = this.skillEnergy + 1 + 2 * (totalLineClear - 1);
            if (this.skillEnergy >= this.skillEnergyMax)
            {
                this.skillReady = true;
                this.skillEnergy = 0;
            }
        }
    }
    public override string GetName()
    {
        SetName("Buffy");
        return name;

    }
}
