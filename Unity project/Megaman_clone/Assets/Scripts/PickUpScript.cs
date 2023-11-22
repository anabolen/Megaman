using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpScript : MonoBehaviour
{
    public enum PickUpType { Hp,Shp,ExtraLife,Ammo,Sammo }
    public PickUpType pickupType;
    public Vector2 gCheckVector;
    public LayerMask solids;
    public float dropSpeed;
    public int bigHp;
    public int smallHp;
    public int bigAmmo;
    public int smallAmmo;
    public int ammoAmount;
    public int hpAmount;

    void Awake() {
        if (pickupType == PickUpType.Hp) {
            hpAmount = bigHp;
        } else if (pickupType == PickUpType.Shp) {
            hpAmount = smallHp;
        }
        if (pickupType == PickUpType.Ammo) {
            ammoAmount = bigAmmo;
        } else if (pickupType == PickUpType.Sammo) {
            ammoAmount = smallAmmo;
        }       

    }

    void OnTriggerEnter2D(Collider2D collision) {
        Destroy(gameObject);
    }

    void FixedUpdate() {
        if (Physics2D.OverlapBox(transform.position, gCheckVector,0 , solids) == null) {
            transform.position += -transform.up * dropSpeed * Time.deltaTime;
        }
        
    }
    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, gCheckVector);
    }


}
