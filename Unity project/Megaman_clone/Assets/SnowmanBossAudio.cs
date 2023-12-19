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

    public void SlamAttack() {
        AudioFW.Play("SnowmanBossSlamAttack");
    }

    public void WhirlAttack() {
        AudioFW.Play("SnowmanBossWhirlAttack");
    }

    public void CarrotLaunch() {
        AudioFW.Play("SnowmanBossCarrotLaunch");
    }

    public void CarrotQuickLaunch() {
        AudioFW.Play("SnowmanBossCarrotLaunchQuick");

    }

}
