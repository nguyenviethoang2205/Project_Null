using System.Collections;
using DG.Tweening;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ZoneSelect : MonoBehaviour
{
    [SerializeField] private bool completed; //false
    [SerializeField] private GameObject uncompleteOj;
    [SerializeField] private static GameObject player;
    [SerializeField] private GameObject startZone;
    [SerializeField] public GameObject selectionZone;
    public new Collider collider;
    public Path path;
    private bool isMove = false;

    #region Data
    [SerializeField] private Character character = new Character();
    private IDataService DataService = new JsonDataService();
    private bool EncryptionEnable;
    // private long loadTime;
    #endregion


    private void Awake()
    {
        Character data = DataService.LoadData<Character>("/characters.json", EncryptionEnable);
        JsonConvert.SerializeObject(data);
        
        path = GetComponentInParent<Path>();
        player = GameObject.Find(data.name);
        startZone = GameObject.FindGameObjectWithTag("Respawn");
        player.transform.position = startZone.transform.position;
    }

    public void SerializeJson(){
        
    }
    private void Start()
    {
        UpdateZone();
    }

    public void UpdateZone()
    {
        if (!completed)
        {
            uncompleteOj.SetActive(true);
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

        if (selectionZone.name == "Zone_1")
        {
            path.zone[2].SetActive(true);
            path.zone[3].SetActive(true);
            SceneManager.LoadScene("Tetris", LoadSceneMode.Additive);
            collider.enabled = false;
        }

        #region Path1
        if (selectionZone.name == "Zone_2")
        {
            path.zone[4].SetActive(true);
            path.zone[3].SetActive(false);
            SceneManager.LoadScene("GetItems", LoadSceneMode.Additive);
            collider.enabled = false;
        }

        if (selectionZone.name == "Zone_4")
        {
            path.zone[7].SetActive(true);
            SceneManager.LoadScene("Tetris", LoadSceneMode.Additive);
            collider.enabled = false;
        }

        #endregion

        #region Path2
        if (selectionZone.name == "Zone_3")
        {
            path.zone[5].SetActive(true);
            path.zone[6].SetActive(true);
            path.zone[2].SetActive(false);
            SceneManager.LoadScene("Tetris", LoadSceneMode.Additive);
            collider.enabled = false;
        }

        if (selectionZone.name == "Zone_5")
        {
            path.zone[7].SetActive(true);
            path.zone[6].SetActive(false);
            SceneManager.LoadScene("Tetris", LoadSceneMode.Additive);
            collider.enabled = false;
        }

        if (selectionZone.name == "Zone_6")
        {
            path.zone[8].SetActive(true);
            path.zone[5].SetActive(false);
            SceneManager.LoadScene("Tetris_Elite", LoadSceneMode.Additive);
            collider.enabled = false;
        }
        if (selectionZone.name == "Zone_8")
        {
            path.zone[7].SetActive(true);
            SceneManager.LoadScene("GetItems", LoadSceneMode.Additive);
            collider.enabled = false;
        }
        #endregion

        if (selectionZone.name == "Zone_7")
        {
            path.zone[9].SetActive(true);
            SceneManager.LoadScene("Tetris_Elite", LoadSceneMode.Additive);
            collider.enabled = false;
        }

        if (selectionZone.name == "BossZone")
        {
            SceneManager.LoadScene("Tetris_Boss", LoadSceneMode.Additive);
            collider.enabled = false;
        }
        isMove = false;
    }



}

