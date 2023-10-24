using System.Collections;
using Cinemachine;
using DG.Tweening;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.SceneManagement;
using Spine.Unity;
using Spine;

public class ZoneSelect : MonoBehaviour
{
    public bool isCompleted; //false
    [SerializeField] private GameObject uncompleteOj;
    [SerializeField] private static GameObject player;
    [SerializeField] private GameObject startZone;
    [SerializeField] private GameObject selectionZone;
    private Vector3 currentPos;
    public new Collider collider;
    public Path path;

    #region Data
    private CharacterCore character;
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
        SkeletonAnimation skeletonAnimation = player.GetComponentInChildren<SkeletonAnimation>();
        skeletonAnimation.gameObject.GetComponent<MeshRenderer>().sortingOrder = -1;
        selectionZone = gameObject;
    }

    private void Start()
    {   
        Completed();
        UpdateZone();
    }

    public void UpdateZone()
    {
        if (!isCompleted)
        {
            uncompleteOj.SetActive(true);
        }
        else
        {
            Completed();
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
        uncompleteOj.SetActive(false);
        isCompleted = false;
        collider.enabled = false;
    }

    public void Move()
    {
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
                    SceneManager.LoadScene("Tetris", LoadSceneMode.Additive);
                    break;

                case "Zone_2":
                    path.zone[4].SetActive(true);
                    path.zone[3].SetActive(false);
                    SceneManager.LoadScene("GetItems",LoadSceneMode.Additive);
                    break;

                case "Zone_3":
                    path.zone[6].SetActive(true);
                    path.zone[5].SetActive(true);
                    path.zone[2].SetActive(false);
                    SceneManager.LoadScene("Tetris", LoadSceneMode.Additive);
                    // SceneManager.LoadScene("Tetris");
                    break;

                case "Zone_4":
                    path.zone[7].SetActive(true);
                    SceneManager.LoadScene("Tetris", LoadSceneMode.Additive);
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
                    SceneManager.LoadScene("Tetris_Elite_2", LoadSceneMode.Additive);
                    // SceneManager.LoadScene("Tetris_Elite");
                    break;

                case "Zone_7":
                    path.zone[9].SetActive(true);
                    SceneManager.LoadScene("Tetris_Elite", LoadSceneMode.Additive);
                    // SceneManager.LoadScene("Tetris_Elite");
                    break;

                case "Zone_8":
                    path.zone[7].SetActive(true);
                    SceneManager.LoadScene("GetItems",LoadSceneMode.Additive);
                    // SceneManager.LoadScene("GetItems");
                    break;

                case "BossZone":
                    SceneManager.LoadScene("Tetris_Boss", LoadSceneMode.Additive);
                    // SceneManager.LoadScene("Tetris_Boss");
                    break;
            }

        path.isMove = false;
    }



}

