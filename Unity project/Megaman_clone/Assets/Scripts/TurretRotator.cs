using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretRotator : MonoBehaviour
{
    TurretEnemy turretScript;

    void Start() {
        turretScript = GetComponentInParent<TurretEnemy>();
    }

    public void Turning() {
        turretScript.turning = true;
    }

    public void TurningOver() {
        turretScript.turning = false;
    }
}
