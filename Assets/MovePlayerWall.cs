using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayerWall : MonoBehaviour
{
    [SerializeField ]GameObject Wall;
    private void OnCollisionEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.transform.position = Wall.transform.GetChild(0).transform.position;
        }
    }
}
