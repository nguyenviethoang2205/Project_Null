using System.Collections;
using Cinemachine;
using DG.Tweening;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

[JsonObject(MemberSerialization.OptIn)]
public class ZoneSelect : MonoBehaviour
{
    public bool isCompleted; //false
    [SerializeField] private GameObject uncompleteOj;
    [SerializeField] private static GameObject player;
    [SerializeField] private GameObject startZone;
    [SerializeField] private GameObject selectionZone;
    [JsonProperty] private float[] currentPos;

    public float[] CurrentPos(GameObject _player)
    {
        currentPos = new float[3];
        currentPos[0] = _player.transform.position.x;
        currentPos[1] = _player.transform.position.y;
        currentPos[2] = _player.transform.position.z;
        return currentPos;
    }
    public new Collider collider;
    public Path path;

    #region Data
    private Character character;
    private IDataService DataService = new JsonDataService();
    private bool EncryptionEnable;
    #endregion


    private void Awake()
    {
        Character charData = DataService.LoadData<Character>("/characters.json", EncryptionEnable);

        if (player == null)
        {
            player = Instantiate(Resources.Load("Prefabs/Player/" + charData.name, typeof(GameObject))) as GameObject;
        }

        path = GetComponentInParent<Path>();
        startZone = GameObject.FindGameObjectWithTag("Respawn");
        player.transform.position = startZone.transform.position;
        selectionZone = gameObject;
    }

    private void Start()
    {

    }

    private void Update()
    {
        UpdateZone();
    }

    public void UpdateZone()
    {
        foreach (var i in path.zone)
        {

            if (!isCompleted)
            {
                uncompleteOj.SetActive(true);
            }
            if (isCompleted)
            {
                i.SetActive(true);
                uncompleteOj.SetActive(false);
                collider.enabled = false;
            }
        }

    }

    private void OnMouseDown()
    {
        if (path.isMove == false)
        {
            path.isMove = true;
            Move();

        }
    }

    public IEnumerator Completed()
    {
        yield return new WaitForSeconds(1);
        gameObject.SetActive(true);
        uncompleteOj.SetActive(false);
        collider.enabled = false;
    }

    public void SavePos()
    {
        JsonConvert.SerializeObject(currentPos, Formatting.Indented,
                new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

        if (DataService.SaveData("/position.json", currentPos, EncryptionEnable))
        {

            Debug.Log(currentPos);
        }
        else
        {
            Debug.LogError("Could not save the file!");
        }
    }
    public void Move()
    {
        CurrentPos(player);
        SavePos();
        player.transform.DOMove(selectionZone.transform.position, 1);
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
                // SceneManager.LoadScene("Tetris");
                break;

            case "Zone_2":
                path.zone[4].SetActive(true);
                path.zone[3].SetActive(false);
                // SceneManager.LoadScene("GetItems");
                break;

            case "Zone_3":
                path.zone[6].SetActive(true);
                path.zone[5].SetActive(true);
                path.zone[2].SetActive(false);
                // SceneManager.LoadScene("Tetris");
                break;

            case "Zone_4":
                path.zone[7].SetActive(true);
                // SceneManager.LoadScene("Tetris");
                break;

            case "Zone_5":
                path.zone[7].SetActive(true);
                path.zone[6].SetActive(false);
                // SceneManager.LoadScene("Tetris");
                break;

            case "Zone_6":
                path.zone[8].SetActive(true);
                path.zone[5].SetActive(false);
                // SceneManager.LoadScene("Tetris_Elite");
                break;

            case "Zone_7":
                path.zone[9].SetActive(true);
                // SceneManager.LoadScene("Tetris_Elite");
                break;

            case "Zone_8":
                path.zone[7].SetActive(true);
                // SceneManager.LoadScene("GetItems");
                break;

            case "BossZone":
                // SceneManager.LoadScene("Tetris_Boss");
                break;
        }

        path.isMove = false;
    }



}

