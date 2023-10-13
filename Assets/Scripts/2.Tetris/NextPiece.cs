using UnityEngine;
using UnityEngine.Tilemaps;
public class NextPiece : MonoBehaviour
{
    public NextBox board {get; private set;}
    public TetrominoData data {get; private set;}
    public Vector3Int position {get; private set;}
    public Vector3Int[] cells {get; private set;}
    public Tile[] tiles;
    public Tile selectTile;

    public int nextPieceColor = -1;

    public void Initialize(NextBox board, Vector3Int position, TetrominoData data){
        this.board = board;
        this.position = position;
        this.data = data; 

        if (this.cells == null){
            this.cells = new Vector3Int[data.cells.Length];
        }

        for (int i = 0; i < data.cells.Length; i++){
            this.cells[i] = (Vector3Int)data.cells[i];
        }
    }

    public Tile RandomTile(){
        int random = Random.Range(0, 3);
        selectTile = tiles[random];
        nextPieceColor = random;
        return selectTile;
    }

}

