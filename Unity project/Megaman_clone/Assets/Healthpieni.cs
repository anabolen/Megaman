using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthpieni : MonoBehaviour
{
    public GameObject lyhtyosumapieni11;
    public GameObject lyhtyidleISO21;
    public GameObject Healthpieniprefab;
    private void Start()
    {

    }

    //void Update()
    //{
    //    transform.Rotate(new Vector3(0, 0.3f, 0));
    //}
    public void InstantiateHealth() {
        Destroy(lyhtyidleISO21);
        Destroy(lyhtyosumapieni11);
        var g = Instantiate(Healthpieniprefab);

        g.transform.position = transform.position;
    }
}
