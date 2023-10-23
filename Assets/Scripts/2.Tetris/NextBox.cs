using UnityEngine;
using UnityEngine.Tilemaps;
public class NextBox : MonoBehaviour
{
    public Tilemap nextTilemap {get; private set;}
    public NextPiece nextPiece {get; private set;}
    public TetrominoData[] tetrominoes;
    public Vector3Int nextPosition;
    
    public int nextPieceIndex = -1;
    public int nextPieceColor = -1;

    private void Awake(){
        this.nextTilemap = GetComponentInChildren<Tilemap>();
        this.nextPiece  = GetComponentInChildren<NextPiece>();
        for ( int i = 0; i < this.tetrominoes.Length; i++ ){
            this.tetrominoes[i].Initialize();
        }
    }

    private void Start(){
        SpawmPiece();
    }

    public void SpawmPiece(){
        nextPieceIndex = Random.Range(0, this.tetrominoes.Length);
        TetrominoData data = this.tetrominoes[nextPieceIndex];
    
        this.nextPiece.Initialize(this, this.nextPosition, data);
            
        this.nextPiece.RandomTile();
        
        Set(this.nextPiece);
        GetColor();
    }
    public void SpawmPiece(int pieceIndex)
    {
        TetrominoData data = this.tetrominoes[pieceIndex];

        this.nextPiece.Initialize(this, this.nextPosition, data);

        this.nextPiece.RandomTile();

        Set(this.nextPiece);
        GetColor();
    }

    public void ClearPiece(){
        Clear(this.nextPiece);
    }

    public void GetColor(){
        nextPieceColor = nextPiece.nextPieceColor;
    }

    private void Set(NextPiece nextPiece){
        for ( int i = 0; i < nextPiece.cells.Length; i++ ){
            Vector3Int tilePosition = nextPiece.cells[i] + nextPiece.position;
            this.nextTilemap.SetTile(tilePosition, nextPiece.selectTile);
        }
    }

    private void Clear(NextPiece nextPiece){
        for ( int i = 0; i < nextPiece.cells.Length; i++ ){
            Vector3Int tilePosition = nextPiece.cells[i] + nextPiece.position;
            this.nextTilemap.SetTile(tilePosition, null);
        }
    }

}
