using UnityEngine;
using UnityEngine.Tilemaps;

public class NextPiece : MonoBehaviour
{
    public Boards boards;
    public Piece nextPeice;
    public Tilemap tilemap{get; private set;}


    private void Awake(){
        this.tilemap = GetComponentInChildren<Tilemap>();
    }

    public void Spawm()
    {
            int random = Random.Range(0, boards.tetrominoes.Length);
            TetrominoData data = boards.tetrominoes[random];

            // this.nextPiece.Initialize(this, this.spawnPosition, data);
            
            // this.nextPiece.RandomTile();
            
            // if(IsValidPosition(this.activePiece, this.spawnPosition)){
            //     Set(this.activePiece);
            // }
    }

    // public void Set(Piece piece){
    //     for ( int i = 0; i < piece.cells.Length; i++ ){
    //         Vector3Int tilePosition = piece.cells[i] + piece.position;
    //         this.tilemap.SetTile(tilePosition, piece.selectTile);
    //     }
    // }
}
