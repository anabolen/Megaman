using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.GraphicsBuffer;

public class Buzzsaw : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float launchForce;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        var x = Random.Range(-1f, 1f);
        rb.AddForce(new Vector2(x, 0) * launchForce, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    //    void Update()
    //    {
    //        OnTriggerEnter2D()
    //    }
}
