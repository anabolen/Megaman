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
    TMP_Text startText;
    TMP_Text quitText;
    TMP_Text creditsText;
    void Start()
    {
        menuSelected = menuItems.start;
        startText = GameObject.Find("StartText").GetComponent<TMP_Text>();
        quitText = GameObject.Find("QuitText").GetComponent<TMP_Text>();
        creditsText = GameObject.Find("CreditsText").GetComponent<TMP_Text>();
    }

    void Update()
    {
            if (Input.GetButtonDown("Select") && menuSelected == menuItems.start ) {
                menuSelected = menuItems.credits;
            } else if (Input.GetButtonDown("Select") && menuSelected == menuItems.credits) {
                menuSelected = menuItems.quit;
            } else if (Input.GetButtonDown("Select") && menuSelected == menuItems.quit) {
               menuSelected = menuItems.start;
            }
            if (menuSelected == menuItems.start) {
                startText.color = Color.red;
                quitText.color = Color.white;
            } else if (menuSelected == menuItems.credits) {
                creditsText.color = Color.red;
                startText.color = Color.white;
            } else if (menuSelected == menuItems.quit) {
                quitText.color = Color.red;
                creditsText.color = Color.white;
            }
            if (Input.GetButtonDown("Start") && menuSelected == menuItems.start) {
                SceneManager.LoadScene(1);
            } else if (Input.GetButtonDown("Start") && menuSelected == menuItems.quit) {
                Application.Quit();
                Debug.Log("Quitting game");
            } else if (Input.GetButtonDown("Start") && menuSelected == menuItems.credits) {
                // Credits scene load
            }
    }
}
