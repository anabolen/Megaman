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
    

    void Awake() {
        hp = maxHp;
        lives = 3;
        ammo = 0;
    }


    void Update()
    {
        if (hp > maxHp) {                
            hp = maxHp;
        }
        if (ammo > maxAmmo) {
            ammo = maxAmmo;
        }

    }

    void OnTriggerEnter2D(Collider2D coll) {
        if (coll.gameObject.layer == 8) {
            //Enemy collision
        }
        if (coll.gameObject.layer == 9) {
            //PickUpCollision
            var pickupScript = coll.gameObject.GetComponent<PickUpScript>();
            print(pickupScript.pickupType);
            if(pickupScript.pickupType == PickUpScript.PickUpType.Hp || 
               pickupScript.pickupType == PickUpScript.PickUpType.Shp) {
                hp += pickupScript.hpAmount;
            }
            else if (pickupScript.pickupType == PickUpScript.PickUpType.Ammo ||
                     pickupScript.pickupType == PickUpScript.PickUpType.Sammo) {
                ammo += pickupScript.ammoAmount;
            } else if (pickupScript.pickupType == PickUpScript.PickUpType.ExtraLife) {
                lives++;
            }
 

        }
            
    }
}
