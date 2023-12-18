using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

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
    void Start()
    {
        menuSelected = menuItems.start;
        arrow = GameObject.Find("Arrow").GetComponent<Image>();
        credits = GameObject.Find("Credits");
        credits.SetActive(false);
        creditsOn = false;
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
            if (Input.GetButtonDown("Start") && menuSelected == menuItems.start) {
                SceneManager.LoadScene(1);
            } else if (Input.GetButtonDown("Start") && menuSelected == menuItems.quit) {
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
}
