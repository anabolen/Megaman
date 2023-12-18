using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class LevelSelectScript : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public VideoClip clip;

    void Update() {
        if (Input.GetButtonDown("Start")) {
            videoPlayer.Play();
        }  
    }


}
