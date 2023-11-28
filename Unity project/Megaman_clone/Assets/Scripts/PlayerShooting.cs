using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject projectile;
    public List<GameObject> projectiles;
    public int maxprojectiles;
    public float playerOrientation;
    public Vector3 defaultProjectileOffset;
    public Vector3 projectileOffset;
    InventoryMenuScript invScript;
    void Awake() 
    {
        new List<GameObject>();
        invScript = FindObjectOfType<InventoryMenuScript>();
        projectileOffset = defaultProjectileOffset;
    }

    void Update() {
        foreach (GameObject p in projectiles) {
            if (p == null) {
                projectiles.Remove(p);
                return;
            }
        }

        playerOrientation = Input.GetAxisRaw("Horizontal");

        if (playerOrientation != 0) { 
            projectileOffset = new Vector3(defaultProjectileOffset.x * playerOrientation, defaultProjectileOffset.y, defaultProjectileOffset.z);
        }

        if (projectiles.Count < maxprojectiles && invScript.paused == false) {
            if (Input.GetKeyDown(KeyCode.F)) {
                var playerController = gameObject.GetComponent<PlayerController>();
                StartCoroutine(playerController.ShootingAnimations());
                Instantiate(projectile, transform.position + projectileOffset, transform.rotation);
            }
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + projectileOffset);
    }
}
