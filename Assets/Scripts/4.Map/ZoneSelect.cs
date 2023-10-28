using System;
using System.Collections;
using Cinemachine;
using DG.Tweening;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using Spine.Unity;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

[JsonObject(MemberSerialization.OptIn)]
public class ZoneSelect : MonoBehaviour
{
    [SerializeField] private static GameObject player;
    [SerializeField] private GameObject uncompleteOj;
    [SerializeField] public GameObject selectionZone;
    [SerializeField] private UILevelMapScreen uiLevelMapScreen;
    [JsonProperty] private float[] currentPos;
    [JsonProperty] public bool isCompleted; //false
    public string currentZone;

    public new Collider2D collider;
    public Collider2D playerCollider;
    public Path path;

    #region Data
    private IDataService DataService = new JsonDataService();
    private bool EncryptionEnable;
    #endregion


    private void Awake()
    {
        this.selectionZone = this.gameObject;
        LoadChar();
        path = GetComponentInParent<Path>();
        playerCollider = player.GetComponent<Collider2D>();
        uiLevelMapScreen = GetComponent<UILevelMapScreen>();
        LoadPos();

    }
    private void Start()
    {
        SkeletonAnimation skeletonAnimation = player.GetComponentInChildren<SkeletonAnimation>();
        skeletonAnimation.gameObject.GetComponent<MeshRenderer>().sortingOrder = -1;
        StartCoroutine(ZoneSelected());
    }
    private void Update()
    {
        UpdateZone();
    }

    private void OnMouseDown()
    {
        if (path.isMove == false && path.isPause == false)
        {
            path.isMove = true;
            Move();
            currentZone = selectionZone.name;
            SaveZone();
        }
    }
    public void UpdateZone()
    {
        switch (isCompleted)
        {
            case false:
                uncompleteOj.SetActive(true);
                break;

            case true:
                uncompleteOj.SetActive(false);
                collider.enabled = false;
                break;

        }

    }
    public void Completed()
    {
        // yield return new WaitForSeconds(1);
        isCompleted = true;
        path.SaveStatus();
        CurrentPos(player);
        Debug.Log(player.transform.position);
        SavePos();
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


        }
        else
        {
            Debug.LogError("Could not save the file!");
        }
    }

    public void SaveZone()
    {
        JsonConvert.SerializeObject(currentZone, Formatting.Indented,
                new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

        if (DataService.SaveData("/zone.json", currentZone, EncryptionEnable))
        {

            Debug.Log(currentZone);
        }
        else
        {
            Debug.LogError("Could not save the file!");
        }
    }

    public void LoadZone()
    {
        string zoneData = DataService.LoadData<string>("/zone.json", EncryptionEnable);
        currentZone = zoneData;
    }

    public void LoadChar()
    {
        Character charData = DataService.LoadData<Character>("/characters.json", EncryptionEnable);

        if (player == null)
        {
            player = Instantiate(Resources.Load("Prefabs/Player/" + charData.name, typeof(GameObject))) as GameObject;
        }
    }

    public void LoadPos()
    {
        float[] posData = DataService.LoadData<float[]>("/position.json", EncryptionEnable);
        Vector3 position;
        position.x = posData[0];
        position.y = posData[1];
        position.z = posData[2];
        player.transform.position = position;
    }

    public float[] CurrentPos(GameObject _player)
    {
        currentPos = new float[3];
        currentPos[0] = _player.transform.position.x;
        currentPos[1] = _player.transform.position.y;
        currentPos[2] = _player.transform.position.z;
        return currentPos;
    }
    public void Move()
    {

        player.transform.DOMove(selectionZone.transform.position, 1);
        // StartCoroutine(Completed());
        StartCoroutine(ZoneSelected());
    }

    public IEnumerator ZoneSelected()
    {
        yield return new WaitForSeconds(2);
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        LoadZone();
        switch (currentZone)
        {

            case "Zone_1":
                path.zone[2].SetActive(true);
                path.zone[3].SetActive(true);
                break;

            case "Zone_2":
                path.zone[4].SetActive(true);
                path.zone[3].SetActive(false);
                break;

            case "Zone_3":
                path.zone[6].SetActive(true);
                path.zone[5].SetActive(true);
                path.zone[2].SetActive(false);
                break;

            case "Zone_4":
                path.zone[7].SetActive(true);
                break;

            case "Zone_5":
                path.zone[7].SetActive(true);
                path.zone[6].SetActive(false);
                break;

            case "Zone_6":
                path.zone[8].SetActive(true);
                path.zone[5].SetActive(false);
                break;

            case "Zone_7":
                path.zone[9].SetActive(true);
                break;

            case "Zone_8":
                path.zone[9].SetActive(true);
                break;

            case "BossZone":
                break;
        }
        Debug.Log(currentZone);
        path.isMove = false;
    }


    private IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        yield return new WaitForSeconds(1);

        if (!isCompleted)
        {
            Completed();
            if (currentZone == "Zone_1")
            {
                SceneManager.LoadScene("Tetris");
            }

            if (currentZone == "Zone_2")
            {
                SceneManager.LoadScene("GetItems");
            }

            if (currentZone == "Zone_3")
            {
                SceneManager.LoadScene("Tetris_4");
            }

            if (currentZone == "Zone_4")
            {
                SceneManager.LoadScene("Tetris_3");
            }

            if (currentZone == "Zone_5")
            {
                SceneManager.LoadScene("Tetris_2");
            }

            if (currentZone == "Zone_6")
            {
                SceneManager.LoadScene("Tetris_Elite_2");
            }

            if (currentZone == "Zone_7")
            {
                SceneManager.LoadScene("Tetris_Elite");
            }

            if (currentZone == "Zone_8")
            {
                SceneManager.LoadScene("GetItems");
            }

            if (currentZone == "BossZone")
            {
                SceneManager.LoadScene("Tetris_Boss");
            }
        }


    }

}

