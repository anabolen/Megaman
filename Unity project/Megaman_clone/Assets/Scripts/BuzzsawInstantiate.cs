using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuzzsawInstantiate : MonoBehaviour

{
    public GameObject buzzsawprefab;
    public void SpawnBuzzsaw()
    {
     var g =   Instantiate(buzzsawprefab);

        g.transform.position = transform.position;

        print("TOIMII");
    }   
    
// Start is called before the first frame update
void Start()
    {

        //GameObject.Find("Buzzsaw1");
        //Instantiate<Buzzsaw1>
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
