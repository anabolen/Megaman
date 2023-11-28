using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryMenuScript : MonoBehaviour
{
    public bool paused;
    public GameObject inventoryMenu;

    void Start()
    {
        paused = false;
        inventoryMenu.SetActive(false);

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && paused == false) {
            OpenPauseMenu();
        } else if (Input.GetKeyDown(KeyCode.P) && paused == true) {
            ClosePauseMenu();
        }
    }

    void OpenPauseMenu() {
        print("Paused");
        Time.timeScale = 0;
        paused = true;
        inventoryMenu.SetActive(true);
    }

    void ClosePauseMenu() {
        print("Unpaused");
        Time.timeScale = 1;
        paused = false;
        inventoryMenu.SetActive(false);
    }
}
