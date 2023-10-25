using UnityEngine; 
using UnityEngine.Tilemaps;
using Spine.Unity;
using Spine;
using System.Collections;
using System.Collections.Generic;
public class Boards : MonoBehaviour {

    #region Data
    private Character character;
    private IDataService DataService = new JsonDataService();
    private bool EncryptionEnable;
    public GameObject player;
    #endregion
    public InventoryManager inventoryManager;
    public EnemyCore enemyCore;
    public NextBox nextBox;
    public HealthBar healthbar;
    public EnemyAnimation characterAnimation;
    public AnimationCharacter animationCharacter;
    public GameOverScreen overScreen;
    public VictoryScreen victoryScreen;
    public LevelAudioPlayer levelAudioPlayer;
    public LevelAnimationUIManager levelAnimationUIManager;
    public Tilemap tilemap {get; private set;}
    public Piece activePiece {get; private set;}
    public TetrominoData[] tetrominoes;
    public Vector3Int spawnPosition;
    public Vector2Int boardSize = new Vector2Int(9, 18);
 
    public static int currentHealth;
    public static int maxHealth;
    public static int damage;
    public int additionDamage;

    public float dropSpeed = 1f;
    public float currnetTime;
    public float addSpeedTime = 15f;

    public int totalLinesClear = 0;
    public int totalLines = 0;
    public int totalCombo = 0;
    public int comboLost = 4;
    public int damageLastTurn = 0;
    public int totalDamageWithCombo = 0;
    private int activePieceIndex = -1;
    private int activePieceColor = -1;

    private int itemBuffATK = 0;

    public bool isAnimationRun = false;
    // Kiểm tra trò chơi có kết thúc không? 
    public bool isGameOver = false;

    public bool nearEnd = false;
    public bool nearEndAudioPlayer = false;
    // Lấy vị trí trung tâm của bảng
    public RectInt Bounds{
        get{
            Vector2Int position = new Vector2Int( -this.boardSize.x / 2 , -this.boardSize.y / 2);
            return new RectInt(position, this.boardSize);
        }
    }

    private void Awake(){
        Character charData = DataService.LoadData<Character>("/characters.json", EncryptionEnable);

        if(player == null){
            player = Instantiate(Resources.Load("Prefabs/Player/" + charData.name, typeof(GameObject)), new Vector3(-6, (float)-3.5f, -5), Quaternion.Euler(0,180f,0)) as GameObject;
        }
        animationCharacter = player.GetComponent<AnimationCharacter>();
        Debug.Log(animationCharacter);

        levelAudioPlayer.PlayThemeAudio();
        this.tilemap = GetComponentInChildren<Tilemap>();
        this.activePiece  = GetComponentInChildren<Piece>();
        for ( int i = 0; i < this.tetrominoes.Length; i++ ){
            this.tetrominoes[i].Initialize();
        }
    }
    
    private void Start(){
        inventoryManager.isGameStart = true;
        isGameOver = false;
        maxHealth = enemyCore.EnemyHealth;
        currentHealth = enemyCore.EnemyHealth;
        additionDamage = 0;
        healthbar.SetMaxHealth(maxHealth);;
        healthbar.SetHealth(maxHealth);
        this.currnetTime = Time.time;
        SpawmPiece();  
    }

    private void Update(){
        if (inventoryManager.isGameStart == true && inventoryManager.playerInventory.isGetItem == true){
                if (Input.GetKeyDown(KeyCode.Return)){
                    inventoryManager.UseItems(this);
                }
            }
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
            if (Time.time - this.currnetTime >= addSpeedTime)
                updateSpeed();
            this.activePiece.Initialize(this, this.spawnPosition, data, dropSpeed);
            
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
    public void SpawmPiece(Vector3Int certentPosition)
    {
        activePieceColor = nextBox.nextPieceColor;
        nextBox.ClearPiece();
        nextBox.SpawmPiece();
        TetrominoData data;
        data = this.tetrominoes[activePieceIndex];
        activePieceIndex = nextBox.nextPieceIndex;
        this.activePiece.Initialize(this, certentPosition, data, dropSpeed);
        this.activePiece.GetColorTile(activePieceColor);
        Set(this.activePiece);
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
                    totalLines++;
                } else {
                    // Kiểm tra hàng tiếp theo
                    row++;
                }
            }
            // characterCore.CheckBeforeClearLine(totalLinesClear);
            if (totalLinesClear == 0){
                comboLost = comboLost - 1;
                levelAnimationUIManager.UpdateComboWait(comboLost);
                if (comboLost == 0){
                    comboLost = 4;
                    totalCombo = 0;
                    totalDamageWithCombo = 0;
                }
                levelAudioPlayer.PlayPieceDownSound();
            } else {
                totalCombo++;
                levelAudioPlayer.PlayPieceClearSound();
                // Kiểm tra thiệt hại và ghi nó vào thanh máu
                CalculateDamage(totalLinesClear);
                currentHealth = currentHealth - damage;
                totalDamageWithCombo += damage;
                levelAnimationUIManager.ChooseDamageToShow();
                CheckHealthStatus();
                healthbar.SetHealth(currentHealth);
            }
            // characterCore.CheckAfterClearLine(totalLinesClear);
            levelAnimationUIManager.ShowDamageCombo();
            activePieceColor = nextBox.nextPieceColor;
            nextBox.ClearPiece();
            nextBox.SpawmPiece();
            
            // Check Skill
            enemyCore.CheckSkillClearLine();
            CheckNearEnd();
            CheckVictory();           
        }
    }

    private void CheckNearEnd(){
        if (nearEnd == true && nearEndAudioPlayer == false){
            levelAudioPlayer.StopThemeAudio();
            levelAudioPlayer.PlayNearEndTheme();
            nearEndAudioPlayer = true;
        }
    }

    private void CheckVictory(){
        if (currentHealth <= 0){
            StartCoroutine(Victory());
            isGameOver = true;
        }
    }

    private void CheckHealthStatus(){
        if (currentHealth > (maxHealth * 2 / 3))
                healthbar.TurnGreen();
            else if (currentHealth <= (maxHealth / 3)){
                healthbar.TurnRed();
                nearEnd = true;
            }
            else
                healthbar.TurnYellow();
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
        levelAudioPlayer.PlayPlayerAttackSound();
        animationCharacter.PlayerDoAttackAction();
        characterAnimation.EnemyDoDefenseAction();
        comboLost = 4;
        damage = (lines * 5) + (10 * (lines - 1));
        if (totalCombo > 1)
        {
            checkComboDamage();
        }
        damage = damage + itemBuffATK;
        // damage = damage + 2 * (totalCombo - 1);
        // D = 1.1 wait 4
        // C = 1.3 wait 3
        // B = 1.65 wait 2
        // A = 2 wait 1
        damageLastTurn = damage;
    }
    // Hàm tính combo
    private void checkComboDamage(){
        if (totalCombo <= 5 ){
            levelAnimationUIManager.UpdateMaxComboWait(4);
            levelAnimationUIManager.ComboTurnWhite();
            comboLost = 4;
            damage = (int)((damage + totalCombo - 1)* 1.1);
        } else if (totalCombo <= 10) {
            levelAnimationUIManager.UpdateMaxComboWait(3);
            levelAnimationUIManager.ComboTurnYellow();
            comboLost = 3;
            damage = (int)((damage + totalCombo - 1)* 1.3);
        } else if (totalCombo <= 15) {
            levelAnimationUIManager.UpdateMaxComboWait(2);
            levelAnimationUIManager.ComboTurnOrange();
            comboLost = 2;
            damage = (int)((damage + totalCombo - 1)* 1.65);
        } else {
            levelAnimationUIManager.UpdateMaxComboWait(1);
            levelAnimationUIManager.ComboTurnRed();
            comboLost = 1; 
            damage = (int)((damage + totalCombo - 1 )* 2);
        }
    }

    public void DestroyPlayer(){
        DestroyImmediate(player);
    }

    // ----------------- Hiệu ứng Items ảnh hưởng tới map ------- //
    private void PlayerUseItemAnimation(){
        levelAudioPlayer.PlayItemSound();
        animationCharacter.PlayerDoAttackAction();
        characterAnimation.EnemyDoDefenseAction();
    }
    public void ItemsDealDamage(int damage){
        PlayerUseItemAnimation();
        if (currentHealth - damage < 1){
            currentHealth = 1;
        }  else {
            currentHealth = currentHealth - damage;
        }
        healthbar.SetHealth(currentHealth);
        CheckHealthStatus();
        CheckNearEnd();
    }

    public void ItemsDestroyLine(){
        PlayerUseItemAnimation();
        Clear(this.activePiece);
        RectInt bounds = this.Bounds;        
        LineClear(bounds.yMin);
        Set(this.activePiece);
    }

    public void ItemsReduceSkill(){
        PlayerUseItemAnimation();
        enemyCore.skillWait = 0;
        enemyCore.skillBar.SetSkillValue(enemyCore.skillWait);
    }

    public void ItemsInsertDamage(int buff){
        PlayerUseItemAnimation();
        itemBuffATK = itemBuffATK + buff;
    }

    // ----------------- Hiệu ứng Skill ảnh hưởng tới map ------- //
    // Hàm tính thiệt hại thêm từ skill, item
    public void calculateAdditionDamage(int additionPercent)
    {
        if (additionPercent != -1)
            damage = damage + (damage * additionPercent) / 100;
    }
    // Thay đổi piece tiếp theo
    public void ChangeNextPiece(int pieceIndexChange)
    {
        this.activePieceIndex = pieceIndexChange;
    }
    // Hồi máu cho quái
    public void Heal(int percent){
        currentHealth = currentHealth + (maxHealth / 100)*percent;
        if (currentHealth >= maxHealth)
            currentHealth = maxHealth;
        healthbar.SetHealth(currentHealth);
    }
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
    // tăng tốc độ rơi
    private void updateSpeed()
    {
        this.dropSpeed = this.dropSpeed - this.dropSpeed * 0.05f;
        this.currnetTime = Time.time;
    }

    public void MoveSound(){
        levelAudioPlayer.PlayPieceMoveSound();
    }

    // thực hiện hành động khi thất bại
    public void DoEnemyAttack(){
        StartCoroutine(DoEnemyAttackAnimation());
    }

    IEnumerator GameOver(){
        inventoryManager.isGameStart = false;
        isAnimationRun = true;
        if (nearEnd == true){
            levelAudioPlayer.StopNearEndTheme();
        } else {
            levelAudioPlayer.StopThemeAudio();
        }
        animationCharacter.PlayerDoLoseAction();
        characterAnimation.EnemyDoVictoryAction();
        levelAudioPlayer.PlayPlayerLoseSound();
        levelAudioPlayer.PlayDefenseVictorySound();
        yield return new WaitForSeconds(3);
        overScreen.Setup();
        levelAudioPlayer.PlayGameOverAudio();
        isAnimationRun = false;
    }

    // thực hiện hành động khi chiến thắng
    IEnumerator Victory(){
        inventoryManager.isGameStart = false;
        isAnimationRun = true;
        levelAudioPlayer.StopNearEndTheme();
        levelAudioPlayer.PlayDefenseLoseSound();
        characterAnimation.EnemyDoLoseAction();
        yield return new WaitForSeconds(1);
        animationCharacter.PlayerDoVictoryAction();
        yield return new WaitForSeconds(1);
        levelAudioPlayer.PlayPlayerVictorySound();
        yield return new WaitForSeconds(2);
        victoryScreen.Setup();
        levelAudioPlayer.PlayVictoryAudio();
        isAnimationRun = false;
    }

    IEnumerator DoEnemyAttackAnimation(){   
        levelAudioPlayer.PlayDefenseAttackSound(); 
        characterAnimation.EnemyDoAttackAction();
        yield return new WaitForSecondsRealtime(0.2f);
        animationCharacter.PlayerDoDefenseAction();
    }


}