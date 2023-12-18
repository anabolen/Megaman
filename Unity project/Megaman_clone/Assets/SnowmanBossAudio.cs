using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowmanBossAudio : MonoBehaviour {

    public void ShotChargeLong() {
        AudioFW.Play("SnowmanBossShotChargeLong");
    }

    public void ShotChargeShort() {
        AudioFW.Play("SnowmanBossShotChargeShort");
    }

}
