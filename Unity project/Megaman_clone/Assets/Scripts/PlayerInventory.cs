using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<ISpecialAbilities> items = new();

    public void AddItem(ISpecialAbilities ability) {
        int ID = ability.AbilityID();
        if (items[ID] == null)
            items.Insert(ID, ability);
        else
            print("Ability slot taken!");
    }

    void Awake() {
        AddItem(new FoxJumpAbility());
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Y)) {
            items[0].AbilitySequence();
        }
    }

}
