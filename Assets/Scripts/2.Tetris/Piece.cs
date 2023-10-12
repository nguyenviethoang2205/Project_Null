﻿using UnityEngine;
using UnityEngine.Tilemaps;

public class Piece : MonoBehaviour{

    public Tile[] tiles;
    public GameOverScreen overScreen; 
    public PauseScreen pauseScreen;
    public VictoryScreen victoryScreen;
    public Tile selectTile;
    public Boards board {get; private set;}
    public TetrominoData data {get; private set;}
    public Vector3Int[] cells  {get; private set;}
    public Vector3Int position  {get; private set;}
    public int rotationIndex {get; private set;}

    public float stepDelay = 1f;
    public float lockDelay = 0.5f;

    private float stepTime;
    private float lockTime;

    public void Initialize(Boards board, Vector3Int position, TetrominoData data){
        this.board = board;
        this.position = position;
        this.data = data; 

        this.rotationIndex = 0; 
        this.stepTime = Time.time + this.stepDelay;
        this.lockTime = 0f;

        if (this.cells == null){
            this.cells = new Vector3Int[data.cells.Length];
        }

        for (int i = 0; i < data.cells.Length; i++){
            this.cells[i] = (Vector3Int)data.cells[i];
        }
    }

    // Chọn gạch ngẫu nghiên
    public Tile RandomTile(){
        int PieceColor = Random.Range(0, tiles.Length);
        selectTile = tiles[PieceColor];
        return selectTile;
    }

    // Chọn gạch theo màu nó có
    public Tile GetColorTile(int Index){
        selectTile = tiles[Index];
        return selectTile;
    }

    // Điều khiển
    private void Update(){
        // Vô hiêu hóa điều khiển nếu dừng, thua hoặc thắng trò chơi
        if(pauseScreen.isPause == false && overScreen.isOver == false && victoryScreen.isVictory == false && board.isAnimationRun == false){
            this.board.Clear(this);

            this.lockTime += Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || (Input.GetKeyDown(KeyCode.X))){
                Rotate(1);
            }

            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)){
                Move(Vector2Int.left);
            } else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)){
                Move(Vector2Int.right);
            }

            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)){
                Move(Vector2Int.down);
            }

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.KeypadEnter)){
                HardDrop();
            }

            if (Time.time >= this.stepTime) {
                Step();
            }

            this.board.Set(this);
        }
    }

    // hàm tự động chuyển khối xuống phía dưới
    private void Step(){
        this.stepTime = Time.time + this.stepDelay;

        Move(Vector2Int.down);

        // Làm cho khối đứng im trong khoảng thời gian ngắn 
        if(this.lockTime >= this.lockDelay){
            Lock();
        }
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
            this.lockTime = 0f;
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