using System.Collections;
using DG.Tweening;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ZoneSelect : MonoBehaviour
{
    [SerializeField] public bool isCompleted; //false
    [SerializeField] private GameObject uncompleteOj;
    [SerializeField] private static GameObject player;
    [SerializeField] private GameObject startZone;
    [SerializeField] public GameObject selectionZone;
    public new Collider collider;
    public Path path;
    private bool isMove = false;

    #region Data
    private Character character;
    private IDataService DataService = new JsonDataService();
    private bool EncryptionEnable;
    // private long loadTime;
    #endregion


    private void Awake()
    {
        Character charData = DataService.LoadData<Character>("/characters.json", EncryptionEnable);

        if(player == null){
            player = Instantiate(Resources.Load("Prefabs/Player/" + charData.name, typeof(GameObject))) as GameObject;
        }
        
        path = GetComponentInParent<Path>();
        startZone = GameObject.FindGameObjectWithTag("Respawn");
        player.transform.position = startZone.transform.position;
        selectionZone = gameObject;
    }

    private void Start()
    {
        UpdateZone();
    }

    public void SaveData()
    {

    }

    public void UpdateZone()
    {
        if (!isCompleted)
        {
            uncompleteOj.SetActive(true);
        }
        else
        {
            uncompleteOj.SetActive(false);
            collider.enabled = false;
        }
    }

    private void OnMouseDown()
    {
        if (isMove == false)
        {
            isMove = true;
            Move();
        }
    }

    public IEnumerator Completed()
    {
        yield return new WaitForSeconds(1);
        uncompleteOj.SetActive(false);

    }

    public void Move()
    {
        selectionZone = gameObject;
        player.transform.DOMove(gameObject.transform.position, 1);
        StartCoroutine(Completed());
        StartCoroutine(ZoneSelected());

    }

    public IEnumerator ZoneSelected()
    {
        yield return new WaitForSeconds(1);
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        switch (selectionZone.name)
        {
            case "Zone_1":
                path.zone[2].SetActive(true);
                path.zone[3].SetActive(true);
                SceneManager.LoadScene("Tetris", LoadSceneMode.Additive);
                break;

            case "Zone_2":
                path.zone[4].SetActive(true);
                path.zone[3].SetActive(false);
                SceneManager.LoadScene("GetItems", LoadSceneMode.Additive);
                break;

            case "Zone_3":
                path.zone[7].SetActive(true);
                path.zone[6].SetActive(false);
                SceneManager.LoadScene("Tetris", LoadSceneMode.Additive);
                break;

            case "Zone_4":
                path.zone[7].SetActive(true);
                SceneManager.LoadScene("Tetris", LoadSceneMode.Additive);
                break;

            case "Zone_5":
                path.zone[7].SetActive(true);
                path.zone[6].SetActive(false);
                SceneManager.LoadScene("Tetris", LoadSceneMode.Additive);
                break;

            case "Zone_6":
                path.zone[8].SetActive(true);
                path.zone[5].SetActive(false);
                SceneManager.LoadScene("Tetris_Elite", LoadSceneMode.Additive);
                break;

            case "Zone_7":
                path.zone[9].SetActive(true);
                SceneManager.LoadScene("Tetris_Elite", LoadSceneMode.Additive);
                break;

            case "Zone_8":
                path.zone[7].SetActive(true);
                SceneManager.LoadScene("GetItems", LoadSceneMode.Additive);
                break;

            case "BossZone":
                SceneManager.LoadScene("Tetris_Boss", LoadSceneMode.Additive);
                break;
        }

        isMove = false;
    }



}

