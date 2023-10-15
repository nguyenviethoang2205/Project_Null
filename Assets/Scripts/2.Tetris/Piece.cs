﻿using UnityEngine;
using UnityEngine.Tilemaps;
using System.Threading;

public class Piece : MonoBehaviour{

    public bool control = true;
    public Tile[] tiles;
    public GameOverScreen overScreen; 
    public PauseScreen pauseScreen;
    public VictoryScreen victoryScreen;
    public Tile selectTile;
    public Tile blackTile;
    public Boards board {get; private set;}
    public TetrominoData data {get; private set;}
    public Vector3Int[] cells  {get; private set;}
    public Vector3Int position  {get; private set;}
    public int rotationIndex {get; private set;}

    public float dropSpeed;

    private float stepTime;
    private int delayControl = 130;
    public void Initialize(Boards board, Vector3Int position, TetrominoData data, float dropSpeed){
        this.board = board;
        this.position = position;
        this.data = data;
        this.dropSpeed = dropSpeed;

        this.rotationIndex = 0; 
        this.stepTime = Time.time + this.dropSpeed;

        if (this.cells == null){
            this.cells = new Vector3Int[data.cells.Length];
        }

        for (int i = 0; i < data.cells.Length; i++){
            this.cells[i] = (Vector3Int)data.cells[i];
        }
    }

    // Chọn mau gạch ngẫu nghiên
    public Tile RandomTile(){
        int PieceColor = Random.Range(0, 4);
        selectTile = tiles[PieceColor];
        return selectTile;
    }

    // Chọn gạch theo màu nó có
    public Tile GetColorTile(int Index){
        selectTile = tiles[Index];
        return selectTile;
    }

    public Tile GetBlackTile(){
        blackTile = tiles[4];
        return blackTile;
    }

    // Điều khiển
    private void Update(){
        // Vô hiêu hóa điều khiển nếu dừng, thua hoặc thắng trò chơi
        if(pauseScreen.isPause == false && overScreen.isOver == false && victoryScreen.isVictory == false && board.isAnimationRun == false){
            this.board.Clear(this);


            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.X))
            {
                Rotate(1);
            }

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                if (control == true)
                {
                    board.MoveSound();
                    Move(Vector2Int.left);
                    Thread.Sleep(this.delayControl);
                    control = false;
                }
                else
                {
                    board.MoveSound();
                    Thread.Sleep(50);
                    Move(Vector2Int.left);
                }
            }
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                if (control == true)
                {
                    board.MoveSound();
                    Move(Vector2Int.right);
                    Thread.Sleep(this.delayControl);
                    control = false;
                }
                else
                {
                    board.MoveSound();
                    Thread.Sleep(50);
                    Move(Vector2Int.right);
                }
            }

            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                if (control == true)
                {
                    board.MoveSound();
                    Move(Vector2Int.down);
                    Thread.Sleep(this.delayControl);
                    control = false;
                }
                else
                {
                    board.MoveSound();
                    Thread.Sleep(50);
                    Move(Vector2Int.down);
                }
                this.stepTime = Time.time + this.dropSpeed;
            }
            if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.DownArrow))
            {
                control = true;
            }


            if (Input.GetKeyDown(KeyCode.Space))
            {
                board.MoveSound();
                HardDrop();
            }

            if (Time.time >= this.stepTime)
            {
                Step();
            }

            this.board.Set(this);
        }
    }

    // hàm tự động chuyển khối xuống phía dưới
    private void Step()
    {
        this.stepTime = Time.time + this.dropSpeed;

        MoveDown(Vector2Int.down);

    }

    // Hàm không cho khối tetris rơi
    private void Lock(){
        this.board.Set(this);
        this.board.ClearLines();
        this.board.SpawmPiece();
    }

    private void HardDrop(){
        while (Move(Vector2Int.down)){
            continue;
        }
        Lock();
    }

    // Di chuyển khối
    private bool Move(Vector2Int translation){
        Vector3Int newPosition = this.position;
        newPosition.x += translation.x;
        newPosition.y += translation.y;

        bool valid = this.board.IsValidPosition(this, newPosition);

        if (valid) {
            this.position = newPosition;
        }

        return valid;
    }

    private bool MoveDown(Vector2Int translation)
    {
        Vector3Int newPosition = this.position;
        newPosition.x += translation.x;
        newPosition.y += translation.y;

        bool valid = this.board.IsValidPosition(this, newPosition);

        if (valid)
        {
            this.position = newPosition;
        }
        else
        {
            Lock();
        }
        return valid;
    }


    // Xoay khối
    private void Rotate(int direction){
        int originalRotation = this.rotationIndex;
        this.rotationIndex = Wrap(this.rotationIndex + direction, 0, 4);
    
        ApplyRotationMatrix(direction);            

        if (!TestWallKicks(this.rotationIndex, direction)){
            this.rotationIndex = originalRotation;
            ApplyRotationMatrix(-direction);
        }
    }

    private void ApplyRotationMatrix(int direction){     
        for (int i = 0; i < this.cells.Length; i++){
            int x, y;

            Vector3 cell = this.cells[i]; 

            switch(this.data.tetromino){
                case Tetromino.I:
                case Tetromino.O:
                    cell.x -= 0.5f;
                    cell.y -= 0.5f;
                    x = Mathf.CeilToInt((cell.x * Data.RotationMatrix[0] * direction) + (cell.y * Data.RotationMatrix[1] * direction));
                    y = Mathf.CeilToInt((cell.x * Data.RotationMatrix[2] * direction) + (cell.y * Data.RotationMatrix[3] * direction));
                    break;
                default:
                    x = Mathf.RoundToInt((cell.x * Data.RotationMatrix[0] * direction) + (cell.y * Data.RotationMatrix[1] * direction));
                    y = Mathf.RoundToInt((cell.x * Data.RotationMatrix[2] * direction) + (cell.y * Data.RotationMatrix[3] * direction));
                    break;
            }

            this.cells[i] = new Vector3Int(x, y, 0);
        }
    }

    private bool TestWallKicks(int rotationIndex, int rotationDirection){
        int wallKickIndex = GetWallKickIndex(rotationIndex, rotationDirection);

        for (int i = 0; i < this.data.wallKicks.GetLength(1); i++ ){
            Vector2Int translation = this.data.wallKicks[wallKickIndex, i];

            if (Move(translation)){
                return true;
            }
        }

        return false;
    }

    private int GetWallKickIndex(int rotationIndex, int rotationDirection){
        int wallKickIndex = rotationIndex * 2;

        if (rotationDirection < 0){
            wallKickIndex--;
        }

        return Wrap(wallKickIndex, 0, this.data.wallKicks.GetLength(0));
    }

    private int Wrap(int input, int min, int max){
        if (input < min){
            return max - (min - input) % (max - min);
        } else {
            return min + (input - min) % (max - min);
        }
    }
}