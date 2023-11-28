using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerInventory : MonoBehaviour
{
    public List<ISpecialAbilities> specialAbilities = new();
    public int currentAbilityID = 0;
    public GameObject inventoryMenu;
    public bool paused;
    string currentAbilityString;
    [SerializeField] float incrementTime = 0.05f;
    float incrementTimer;
    int previousIncrementDirection = 0;

    public void AddItem(ISpecialAbilities ability) {
        int ID = ability.AbilityID();
        while (specialAbilities.Count < ID+1) 
        {
            specialAbilities.Insert(specialAbilities.Count, null);
        }
        specialAbilities.Insert(ID, ability);
    }

    void Awake() {
        AddItem(new FoxJumpAbility());
        AddItem(new GunAbility());
        currentAbilityString = specialAbilities[currentAbilityID].AbilityName();
        print(currentAbilityString);
        paused = false;
        inventoryMenu.SetActive(false);
    }

    void Update() {

        if (Input.GetKeyDown(KeyCode.F) && paused)
        {
            specialAbilities[currentAbilityID].AbilitySequence();
        }

        int changeDirection = (int)Input.GetAxisRaw("Vertical");

        if (incrementTimer > 0 && changeDirection == previousIncrementDirection && changeDirection != 0) { 
            incrementTimer -= Time.deltaTime;
            return;
        }

        incrementTimer = 0;

        if (changeDirection != 0 && currentAbilityID + changeDirection 
            == Mathf.Clamp(currentAbilityID + changeDirection, 0, specialAbilities.Count - 1)) 
        {
            ChangeCurrentAbilitySelection(changeDirection);
            incrementTimer = incrementTime;
            previousIncrementDirection = changeDirection;
        }
        if (Input.GetKeyDown(KeyCode.P) && paused == false) {
            OpenPauseMenu();
        } else if (Input.GetKeyDown(KeyCode.P) && paused == true) {
            ClosePauseMenu();
        }
    }

    void ChangeCurrentAbilitySelection(int changeDirection) {
        int previousAbilityID = currentAbilityID;
        currentAbilityID += changeDirection;
        while (specialAbilities[currentAbilityID] == null && currentAbilityID + changeDirection
               == Mathf.Clamp(currentAbilityID + changeDirection, 0, specialAbilities.Count - 1))
        {
            currentAbilityID += changeDirection;
        }
        if (specialAbilities[currentAbilityID] == null)
            currentAbilityID = previousAbilityID;
        currentAbilityString = specialAbilities[currentAbilityID].AbilityName();
        print(currentAbilityString);
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
