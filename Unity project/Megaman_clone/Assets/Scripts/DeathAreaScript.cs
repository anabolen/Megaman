using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAreaScript : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D coll) {
        if (coll.gameObject.layer == 7) {
            coll.gameObject.GetComponent<PlayerManager>().UpdatePlayerHp(-100);
        }    
    }
}
