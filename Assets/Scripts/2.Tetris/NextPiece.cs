using UnityEngine;
using UnityEngine.Tilemaps;

public class NextPiece : MonoBehaviour
{
    public Tilemap nextPieceTilemap;
    public Piece nextPiece;
    private Vector3Int nextPiecePosition;
    public void Awake()
    {
        this.nextPieceTilemap = GetComponentInChildren<Tilemap>();
        this.nextPiece = GetComponentInChildren<Piece>();
    }
    public void NextPieceShow(TetrominoData data) 
    {
        this.nextPiece.Initialize(nextPiecePosition, data);
        Set(nextPiece);
    }

    public void Set(Piece piece)
    {
        for (int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int tilePosition = piece.cells[i] + piece.position;
            this.nextPieceTilemap.SetTile(tilePosition, piece.selectTile);
        }
    }
}
