using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.GraphicsBuffer;

public class Buzzsaw : MonoBehaviour
{
    private float waitTime = 3.0f;
    private float timer = 0.0f;
    Rigidbody2D rb;
    [SerializeField] float launchForce;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5);
        rb = GetComponent<Rigidbody2D>();
        var x = Random.Range(-1f, 1f);
        rb.AddForce(new Vector2(x, 0) * launchForce, ForceMode2D.Impulse);
        AudioFW.Play("BUZZSAW");
    }

    
}
