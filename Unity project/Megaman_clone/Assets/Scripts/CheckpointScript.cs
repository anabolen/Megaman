using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointScript : MonoBehaviour
{
    PlayerController playerCon;
    //[SerializeField] bool used;
    void Start()
    {
        playerCon = FindObjectOfType<PlayerController>();
        //used = false;
    }

    void OnTriggerEnter2D(Collider2D collision) {
        playerCon.checkpoint++;
        GetComponent<BoxCollider2D>().enabled = false;
    }
}
