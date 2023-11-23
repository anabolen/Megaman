using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpScript : MonoBehaviour
{
    public enum PickUpType { BigHp,SmallHp,ExtraLife,BigAmmo,SmallAmmo }
    public PickUpType pickupType;
    public Vector2 gCheckVector;
    public LayerMask solids;
    public float dropSpeed;
    public int bigHpPickUp;
    public int smallHpPickUp;
    public int bigAmmoPickUp;
    public int smallAmmoPickUp;
    public int pickUpAmmoAmount;
    public int pickUpHpAmount;

    void Awake() {
        if (pickupType == PickUpType.BigHp) {
            pickUpHpAmount = bigHpPickUp;
        } else if (pickupType == PickUpType.SmallHp) {
            pickUpHpAmount = smallHpPickUp;
        }
        if (pickupType == PickUpType.BigAmmo) {
            pickUpAmmoAmount = bigAmmoPickUp;
        } else if (pickupType == PickUpType.SmallAmmo) {
            pickUpAmmoAmount = smallAmmoPickUp;
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
