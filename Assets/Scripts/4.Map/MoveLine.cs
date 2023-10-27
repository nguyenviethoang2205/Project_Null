using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLine : MonoBehaviour
{
    LineRenderer link;
    public Transform[] pos;
    public float speed = 5f;

    private void Start()
    {
        link = gameObject.GetComponent<LineRenderer>();
    }

    private void Update()
    {
       KeLine(pos);
    }

    private void KeLine(Transform[] pos)
    {
        link.SetPosition(0, pos[0].position);
        link.SetPosition(1, Vector3.MoveTowards(transform.position, pos[1].position, speed * Time.time));
        link.SetPosition(2, Vector3.MoveTowards(transform.position, pos[2].position, speed * Time.time));
        link.SetPosition(3, Vector3.MoveTowards(transform.position, pos[3].position, speed * Time.time));
        link.SetPosition(4, Vector3.MoveTowards(transform.position, pos[4].position, speed * Time.time));
        link.SetPosition(5, Vector3.MoveTowards(transform.position, pos[5].position, speed * Time.time));
    }
}
