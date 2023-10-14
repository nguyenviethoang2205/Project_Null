using UnityEngine; 
using UnityEngine.Tilemaps;
using Spine.Unity;
using Spine;
using System.Collections;
using System.Collections.Generic;
public class Boards : MonoBehaviour {
    public EnemyCore enemyCore;
    public NextBox nextBox;
    public HealthBar healthbar;
    public CharacterAnimation characterAnimation;
    public GameOverScreen overScreen;
    public VictoryScreen victoryScreen;
    public Tilemap tilemap {get; private set;}
    public Piece activePiece {get; private set;}
    public TetrominoData[] tetrominoes;
    public Vector3Int spawnPosition;
    public Vector2Int boardSize = new Vector2Int(9, 18);
 
    public static int currentHealth;
    public static int maxHealth;
    public static int damage;
    public int totalLinesClear = 0;
    private int activePieceIndex = -1;
    private int activePieceColor = -1;

    public bool isAnimationRun = false;
    // Kiểm tra trò chơi có kết thúc không? 
    public bool isGameOver = false;
    // Lấy vị trí trung tâm của bảng
    public RectInt Bounds{
        get{
            Vector2Int position = new Vector2Int( -this.boardSize.x / 2 , -this.boardSize.y / 2);
            return new RectInt(position, this.boardSize);
        }
    }

    private void Awake(){
        
        this.tilemap = GetComponentInChildren<Tilemap>();
        this.activePiece  = GetComponentInChildren<Piece>();
        for ( int i = 0; i < this.tetrominoes.Length; i++ ){
            this.tetrominoes[i].Initialize();
        }
    }
    
    private void Start(){
        isGameOver = false;
        maxHealth = enemyCore.EnemyHealth;
        currentHealth = enemyCore.EnemyHealth;
        healthbar.SetMaxHealth(maxHealth);;
        healthbar.SetHealth(maxHealth);
        SpawmPiece(); 
    }

    // Tạo khối
    public void SpawmPiece(){
        if (isGameOver == false){
            TetrominoData data;
            if (activePieceIndex == -1){
                int random = Random.Range(0, this.tetrominoes.Length);
                data = this.tetrominoes[random];
                activePieceIndex = nextBox.nextPieceIndex;
            }else{
                data = this.tetrominoes[activePieceIndex];
                activePieceIndex = nextBox.nextPieceIndex;
            }

            this.activePiece.Initialize(this, this.spawnPosition, data);
            
            if (activePieceColor == -1){
                this.activePiece.RandomTile();
            } else {
                this.activePiece.GetColorTile(activePieceColor);
            }
            
            if(IsValidPosition(this.activePiece, this.spawnPosition)){
                Set(this.activePiece);
            } else {
                StartCoroutine(GameOver());
                isGameOver = true;
            }
        }
    }

    // Xóa block cần xóa trong tilemap
    public void Clear(Piece piece){
        for ( int i = 0; i < piece.cells.Length; i++ ){
            Vector3Int tilePosition = piece.cells[i] + piece.position;
            this.tilemap.SetTile(tilePosition, null);
        }
    }

    // Đặt các ô trông  title map
    public void Set(Piece piece){
        for ( int i = 0; i < piece.cells.Length; i++ ){
            Vector3Int tilePosition = piece.cells[i] + piece.position;
            this.tilemap.SetTile(tilePosition, piece.selectTile);
        }
    }

    // Kiểm tra vị trí block có hợp lệ không
    public bool IsValidPosition(Piece piece, Vector3Int position){
        RectInt bounds = this.Bounds;
        
        for (int i = 0; i < piece.cells.Length; i++){
            Vector3Int tilePosition = piece. cells[i] + position;

            // Nằm ngoài biên
            if (!bounds.Contains((Vector2Int)tilePosition)){
                return false;
            }

            // Vị trí đã có block
            if (this.tilemap.HasTile(tilePosition)){
                return false;
            }
        }
        return true;
    }
    public int countLines = 0; 
    public int total = 0;
    // Xóa các hàng đầy 
    public void ClearLines(){
        if (isGameOver == false){
            RectInt bounds = this.Bounds;
            // Bất đầu từ dòng dưới cùng
            int row = bounds.yMin;
            totalLinesClear = 0;

            while (row < bounds.yMax){
                if (IsLineFull(row)){
                    // Xóa dòng; 
                    LineClear(row);
                    totalLinesClear++;
                    countLines++;
                    total++;
                } else {
                    // Kiểm tra hàng tiếp theo
                    row++;
                }
            }
            activePieceColor = nextBox.nextPieceColor;
            nextBox.ClearPiece();
            nextBox.SpawmPiece();
            // Kiểm tra thiệt hại và ghi nó vào thanh máu
            CalculateDamage(totalLinesClear);
            currentHealth = currentHealth - damage;
            healthbar.SetHealth(currentHealth);
            // Check Skill
            enemyCore.CheckSkillClearLine();
            // Kiểm tra xem có hoàn thành game đấu chưa?
            if (currentHealth <= 0){
                StartCoroutine(Victory());
                isGameOver = true;
            }
            if (countLines >= 3){
                countLines = countLines % 3;
            }
        }
    }

    // Kiểm tra dòng có đủ block không
    private bool IsLineFull(int row){
        RectInt bounds = this.Bounds;

        for (int col = bounds.xMin; col < bounds.xMax; col++){
            Vector3Int position = new Vector3Int(col, row, 0);
        
            if (!this.tilemap.HasTile(position)){
                return false;
            }
        }
        return true;
    }

    // Hàm xóa dòng
    private void LineClear(int row){
        RectInt bounds = this.Bounds;

        for (int col = bounds.xMin; col < bounds.xMax; col++){
            Vector3Int position = new Vector3Int(col, row, 0);
            this.tilemap.SetTile(position, null);
        }

        while(row < bounds.yMax){
            for (int col = bounds.xMin; col < bounds.xMax; col++){
                Vector3Int position = new Vector3Int(col, row + 1, 0);
                TileBase above = this.tilemap.GetTile(position);

                position = new Vector3Int(col, row, 0);
                this.tilemap.SetTile(position, above);
            }  
            
            row++;
        }
    }

    // Hàm tính thiệt hại
    private void CalculateDamage(int lines) {
        if (lines == 0)
            damage = 0;
        else{
            characterAnimation.PlayerDoAttackAction();
            characterAnimation.EnemyDoDefenseAction();
            if (lines == 1)
                damage = 5;
            else if (lines == 2)
                damage = 15;
            else if (lines == 3)
                damage = 25;
            else
                damage = 50;
        }
    }

    // ----------------- Hiệu ứng Skill ảnh hưởng tới map ------- //
    // Gây tăng một hàng
    public void MakeAGrayLine(){
        RectInt bounds = this.Bounds;
        int NullTile = Random.Range(bounds.xMin, Bounds.xMax);
        int row = bounds.yMax;
        while(row > bounds.yMin - 1){
            for (int col = bounds.xMin; col < bounds.xMax; col++){
                Vector3Int position = new Vector3Int(col, row, 0);
                TileBase above = this.tilemap.GetTile(position);

                position = new Vector3Int(col, row + 1, 0);
                this.tilemap.SetTile(position, above);
            }  
            row--;
        }
        for (int col = bounds.xMin; col < bounds.xMax; col++){
            Vector3Int position = new Vector3Int(col, bounds.yMin, 0);
            this.tilemap.SetTile(position, null);
        }


        this.activePiece.GetBlackTile();
        for (int col = bounds.xMin; col < bounds.xMax; col++){
            if (col != NullTile){
                Vector3Int position = new Vector3Int(col, bounds.yMin, 0);
                this.tilemap.SetTile(position, activePiece.blackTile);
            }    
        }

    }
    // xóa cột

    public void deleteCollum(int col) 
    {
        RectInt bounds = this.Bounds;
        for (int row = bounds.yMin; row < bounds.yMax; row++)
        {
            Vector3Int position = new Vector3Int(col, row, 0);
            this.tilemap.SetTile(position, null);
        }
    }

    // thực hiện hành động khi thất bại
    public void DoEnemyAttack(){
        StartCoroutine(DoEnemyAttackAnimation());
    }

    IEnumerator GameOver(){
        isAnimationRun = true;
        characterAnimation.PlayerDoLoseAction();
        characterAnimation.EnemyDoVictoryAction();
        
        yield return new WaitForSeconds(4);
        overScreen.Setup();
        isAnimationRun = false;
    }

    // thực hiện hành động khi chiến thắng
    IEnumerator Victory(){
        isAnimationRun = true;
        characterAnimation.EnemyDoLoseAction();
        yield return new WaitForSeconds(1);
        characterAnimation.PlayerDoVictoryAction();
        
        yield return new WaitForSeconds(4);
        victoryScreen.Setup();
        isAnimationRun = false;
    }

    IEnumerator DoEnemyAttackAnimation(){    
        characterAnimation.EnemyDoAttackAction();
        yield return new WaitForSeconds(0.1f);
        characterAnimation.PlayerDoDefenseAction();
    }
}
