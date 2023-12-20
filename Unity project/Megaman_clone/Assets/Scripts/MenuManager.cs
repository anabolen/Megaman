using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEngine.Video;
using UnityEditor.SearchService;

public class MenuManager : MonoBehaviour
{
    enum menuItems {start, credits, quit}
    menuItems menuSelected;
    Image arrow;
    public RectTransform startPos;
    public RectTransform creditsPos;
    public RectTransform quitPos;
    GameObject credits;
    bool creditsOn;
    bool videoStarted;
    public VideoPlayer videoPlayer;
    public GameObject canvas;
    void Start()
    {
        menuSelected = menuItems.start;
        arrow = GameObject.Find("Arrow").GetComponent<Image>();
        credits = GameObject.Find("Credits");
        credits.SetActive(false);
        creditsOn = false;
        videoPlayer.loopPointReached += CheckOver;
        videoStarted = false;
        AudioFW.PlayLoop("MainMenuMusic");
    }

    void Update()
    {
        if (creditsOn == false) {
            if (Input.GetButtonDown("Select") && menuSelected == menuItems.start) {
                menuSelected = menuItems.credits;
            } else if (Input.GetButtonDown("Select") && menuSelected == menuItems.credits) {
                menuSelected = menuItems.quit;
            } else if (Input.GetButtonDown("Select") && menuSelected == menuItems.quit) {
                menuSelected = menuItems.start;
            }
            if (menuSelected == menuItems.start) {
                arrow.rectTransform.position = startPos.position;
            } else if (menuSelected == menuItems.credits) {
                arrow.rectTransform.position = creditsPos.position;
            } else if (menuSelected == menuItems.quit) {
                arrow.rectTransform.position = quitPos.position;
            }
            if (Input.GetButtonDown("Start") && menuSelected == menuItems.start && videoStarted == false) {
                canvas.SetActive(false);
                videoPlayer.Play();
                videoStarted = true;
            } else if (Input.GetButtonDown("Start") && videoStarted == true) {
                SceneManager.LoadScene(1);
            }
            else if (Input.GetButtonDown("Start") && menuSelected == menuItems.quit) {
                Application.Quit();
                Debug.Log("Quitting game");
            } else if (Input.GetButtonDown("Start") && menuSelected == menuItems.credits) {
                credits.SetActive(true);
                creditsOn = true;
            } 
        } else if (Input.GetButtonDown("Start") && creditsOn == true) {
            credits.SetActive(false);
            creditsOn = false;
        } 
    }
    void CheckOver(UnityEngine.Video.VideoPlayer vp) {
        Debug.Log("Video Over");
    }
}
