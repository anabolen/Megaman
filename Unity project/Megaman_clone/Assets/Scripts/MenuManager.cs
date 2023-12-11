using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    bool startSelected;
    bool levelSelectOpen;
    TMP_Text startText;
    TMP_Text quitText;
    Image levelSelectSprite;
    void Start()
    {
        startSelected = true;
        startText = GameObject.Find("StartText").GetComponent<TMP_Text>();
        quitText = GameObject.Find("QuitText").GetComponent<TMP_Text>();
    }

    void Update()
    {
            if (Input.GetButtonDown("Select") && startSelected) {
                startSelected = false;
            } else if (Input.GetButtonDown("Select") && startSelected == false) {
                startSelected = true;
            }
            if (startSelected == true) {
                startText.color = Color.red;
                quitText.color = Color.white;
            } else if (startSelected == false) {
                quitText.color = Color.red;
                startText.color = Color.white;
            }
            if (Input.GetButtonDown("Start") && startSelected == true) {
                SceneManager.LoadScene(1);
            } else if (Input.GetButtonDown("Start") && startSelected == false) {
                Application.Quit();
                Debug.Log("Quitting game");
            }
    }
}
