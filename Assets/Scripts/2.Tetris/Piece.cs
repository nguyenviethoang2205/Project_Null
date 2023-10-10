using UnityEngine;
using UnityEngine.Tilemaps;
using System.Threading;

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

    private float stepTime;

    private static bool control = true;

    public void Initialize(Boards board, Vector3Int position, TetrominoData data){
        this.board = board;
        this.position = position;
        this.data = data; 

        this.rotationIndex = 0; 
        this.stepTime = Time.time + this.stepDelay;

        if (this.cells == null){
            this.cells = new Vector3Int[data.cells.Length];
        }

        for (int i = 0; i < data.cells.Length; i++){
            this.cells[i] = (Vector3Int)data.cells[i];
        }
    }

    // Chọn gạch ngẫu nghiên
    public Tile RandomTile(){
        int randomIndex = Random.Range(0, tiles.Length);
        selectTile = tiles[randomIndex];
        return selectTile;
    }

    // Điều khiển
    private void Update(){
        // Vô hiêu hóa điều khiển nếu dừng, thua hoặc thắng trò chơi
        if(pauseScreen.isPause == false && overScreen.isOver == false && victoryScreen.isVictory == false){
            this.board.Clear(this);

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Rotate(1);
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                if (control == true)
                {
                    Move(Vector2Int.left);
                    Thread.Sleep(110);
                    control = false;
                }
                else
                {
                    Thread.Sleep(50);
                    Move(Vector2Int.left);
                }
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                if (control == true)
                {
                    Move(Vector2Int.right);
                    Thread.Sleep(110);
                    control = false;
                }
                else
                {
                    Thread.Sleep(50);
                    Move(Vector2Int.right);
                }
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                if (control == true)
                {
                    Move(Vector2Int.down);
                    Thread.Sleep(110);
                    control = false;
                }
                else
                {
                    Thread.Sleep(50);
                    Move(Vector2Int.down);
                }
                this.stepTime = Time.time + this.stepDelay;
            }
            if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.DownArrow))
            {
                control = true;
            }


            if (Input.GetKeyDown(KeyCode.Space))
            {
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
    private void Step(){
        this.stepTime = Time.time + this.stepDelay;

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
