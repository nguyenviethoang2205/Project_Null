
using UnityEngine;
using UnityEngine.SceneManagement;

public class ZoneSelect : MonoBehaviour
{
    [SerializeField] private bool completed; //false
    [SerializeField] private GameObject uncompleteOj; 
    [SerializeField] private GameObject selectionZone;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject startZone;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        startZone = GameObject.FindGameObjectWithTag("Respawn");
        StartZone();
        UpdateZone();
    }

    public void UpdateZone(){
        if (!completed){ 
            uncompleteOj.SetActive(true);
        }
    }

    private void OnMouseDown() {
        player.transform.position = gameObject.transform.position;
        // SceneManager.LoadScene("Tetris");
    }


    public void StartZone(){
        player.transform.position = startZone.transform.position;
    }
}
    
