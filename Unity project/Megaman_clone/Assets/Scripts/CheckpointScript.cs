using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointScript : MonoBehaviour
{
    PlayerController playerCon;
    void Start()
    {
        playerCon = FindObjectOfType<PlayerController>();
    }

    void OnTriggerEnter2D(Collider2D collision) {
        playerCon.checkpoint++;
        GetComponent<BoxCollider2D>().enabled = false;
    }
}
