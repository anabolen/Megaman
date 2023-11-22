using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject projectile;
    public List<GameObject> projectiles;
    public int maxprojectiles;
    public float firingHeight;
    Vector3 heightVec;
    void Awake()
    {
        new List<GameObject>();
        heightVec.y = firingHeight;
    }

    void Update() {
        foreach (GameObject p in projectiles) {
            if (p == null) {
                projectiles.Remove(p);
                return;
            }
        }

        if (projectiles.Count < maxprojectiles) {
            if (Input.GetKeyDown(KeyCode.F)) {
                Instantiate(projectile, transform.position + heightVec, transform.rotation);
            }
        }
    }
}
