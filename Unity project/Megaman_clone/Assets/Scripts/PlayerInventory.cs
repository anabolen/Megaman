using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerInventory : MonoBehaviour
{
    public List<ISpecialAbilities> specialAbilities = new();
    int currentAbilityID = 0;
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
    }

    private void Update() {

        if (Input.GetKeyDown(KeyCode.Y))
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
}
