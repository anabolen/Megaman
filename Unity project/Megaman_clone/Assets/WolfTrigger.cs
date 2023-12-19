using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEngine;

public class WolfTrigger : MonoBehaviour {
    [SerializeField] GameObject wolf;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer != 7)
            return;
        wolf.SetActive(true);
    }
}
