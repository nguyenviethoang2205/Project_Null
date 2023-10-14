using System.Collections;
using DG.Tweening;
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
        player.transform.position = startZone.transform.position;
        UpdateZone();
    }

    private void Update() {
        selectionZone = gameObject;
    }

    public void UpdateZone(){
        if (!completed){ 
            uncompleteOj.SetActive(true);
        }
    }

    private void OnMouseDown() {
         Move();        
    }

    public IEnumerator Completed(){
        yield return new WaitForSeconds(1);
        uncompleteOj.SetActive(false);
        
    }

    public void Move(){
        player.transform.DOMove(gameObject.transform.position, 1);
        StartCoroutine(Completed());
        StartCoroutine(FindZone());

    }

    public IEnumerator FindZone(){
        yield return new WaitForSeconds(1);

        

        
    }

}
    
