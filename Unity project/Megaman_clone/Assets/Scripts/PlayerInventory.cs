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
    public GameObject UISprite;
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
        AddItem(new SuperGunAbility());
        AddItem(new NormalGunAbility());
        UISprite = GameObject.Find("CurrentlySelectedAbility");
        currentAbilityString = specialAbilities[currentAbilityID].AbilityName();
        paused = false;
        inventoryMenu.SetActive(false);
        UpdatePauseMenuSprite();
    }

    void Update() {

        if (Input.GetKeyDown(KeyCode.P) && !paused) {
            OpenPauseMenu();
        }
        else if (Input.GetKeyDown(KeyCode.P) && paused) {
            ClosePauseMenu();
        }

        int changeDirection = (int)Input.GetAxisRaw("Vertical");

        if (incrementTimer > 0 && changeDirection == previousIncrementDirection && changeDirection != 0) { 
            incrementTimer -= Time.unscaledDeltaTime;
            return;
        }

        incrementTimer = 0;

        if (changeDirection != 0 && currentAbilityID + changeDirection 
            == Mathf.Clamp(currentAbilityID + changeDirection, 0, specialAbilities.Count - 1) && paused) 
        {
            ChangeCurrentAbilitySelection(changeDirection);
            incrementTimer = incrementTime;
            previousIncrementDirection = changeDirection;
            return;
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

    void UpdatePauseMenuSprite() {
        var spriteRenderer = UISprite.GetComponent<UnityEngine.UI.Image>();
        spriteRenderer.sprite = specialAbilities[currentAbilityID].UIAbilitySprite();
    }

    void ChangeCurrentAbilitySelection(int changeDirection) {
        int previousAbilityID = currentAbilityID;
        currentAbilityID += changeDirection;
        while (specialAbilities[currentAbilityID] == null && currentAbilityID + changeDirection
               == Mathf.Clamp(currentAbilityID + changeDirection, 0, specialAbilities.Count - 1))
        {
            currentAbilityID += changeDirection;
        }
        if (specialAbilities[currentAbilityID] == null) { 
            currentAbilityID = previousAbilityID;
        }
        currentAbilityString = specialAbilities[currentAbilityID].AbilityName();
        UpdatePauseMenuSprite();
        print(currentAbilityString);
    }


}
