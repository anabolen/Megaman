using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] int hp;
    [SerializeField] int lives;
    [SerializeField] int ammo;
    public int maxHp;
    public int maxAmmo;
    PlayerController controller;
    

    void Awake() {
        hp = maxHp;
        lives = 3;
        ammo = 0;
        controller = GetComponent<PlayerController>();
    }

    void Update() {
        hp = Mathf.Clamp(hp, 0, maxHp);
        ammo = Mathf.Clamp(ammo, 0, maxAmmo);
        //Clamps hp and ammo between 0 and max so they never go above max or below 0
        if (hp == 0)
            controller.PlayerDeath();
    }

    void OnTriggerEnter2D(Collider2D coll) {
        if (coll.gameObject.layer == 8) {
            //Enemy collision
        }
        if (coll.gameObject.layer == 9) {
            //PickUpCollision
            var pickupScript = coll.gameObject.GetComponent<PickUpScript>();
            print(pickupScript.pickupType);
            if (pickupScript.pickupType == PickUpScript.PickUpType.BigHp || 
                pickupScript.pickupType == PickUpScript.PickUpType.SmallHp) {
                hp += pickupScript.pickUpHpAmount;
            }
            else if (pickupScript.pickupType == PickUpScript.PickUpType.BigAmmo ||
                     pickupScript.pickupType == PickUpScript.PickUpType.SmallAmmo) {
                ammo += pickupScript.pickUpAmmoAmount;
            } else if (pickupScript.pickupType == PickUpScript.PickUpType.ExtraLife) {
                lives++;
            }
        }
    }
}
