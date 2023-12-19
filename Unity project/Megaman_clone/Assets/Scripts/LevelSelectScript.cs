using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class LevelSelectScript : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public RawImage levelSelect;
    void Start() {
        videoPlayer.loopPointReached += CheckOver;
    }

    void Update() {
        if (Input.GetButtonDown("Start")) {
            videoPlayer.Play();
            levelSelect.enabled = false;
        }
    }
    void CheckOver(UnityEngine.Video.VideoPlayer vp) {
        SceneManager.LoadScene(2);
        Debug.Log("Video Over");
    }


}
