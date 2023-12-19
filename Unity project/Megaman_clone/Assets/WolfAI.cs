using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfAI : MonoBehaviour {
    
    [SerializeField] GameObject smallProjectile;
    [SerializeField] GameObject longProjectile;
    [SerializeField] Vector2 offSet;
    [SerializeField] Vector2 longOffSet;
    public float wolfDirection;
    public bool linearHorizontalShot;
    int shootingIndex;

    void Awake() {
        SwitchDirection();    
        longOffSet = new Vector2(longOffSet.x * wolfDirection, longOffSet.y);
        offSet = new Vector2(offSet.x * wolfDirection, offSet.y);
    }

    public void SwitchDirection() {
        transform.rotation = Quaternion.Euler(0, 180 * Mathf.Clamp(-wolfDirection, 0, 1), 0);
    }

    public void Shoot() {
        if (shootingIndex < 2) {
            ShootSmall();
            shootingIndex++;
        }
        else {
            ShootLong();
            shootingIndex = 0;
        }

    }

    public void ShootSmall() {
        var proj = Instantiate(smallProjectile);
        AudioFW.Play("WolfShortSoundWave");
        proj.transform.position = (Vector2)transform.position + offSet;
    }

    public void ShootLong() {
        var proj = Instantiate(longProjectile);
        AudioFW.Play("WolfLongSoundWave");
        proj.transform.position = (Vector2)transform.position + longOffSet;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.TransformPoint(longOffSet));
    }
}
