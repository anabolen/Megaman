using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAnimationScript : MonoBehaviour {
    SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Death() {
        spriteRenderer.enabled = false;
    }
}
