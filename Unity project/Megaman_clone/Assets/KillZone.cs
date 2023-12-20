using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer != 7)
            return;
        collision.gameObject.GetComponent<PlayerManager>().playerHp = 0;
        collision.gameObject.GetComponent<PlayerController>().PlayerHitCheck(0, 0);
    }

}
