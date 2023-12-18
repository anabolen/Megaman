using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GuanoBarrierAnimation : MonoBehaviour {

    public SpriteRenderer[] sprites;
    [SerializeField] float zAxisRotationPerRotationTick = 22.5f;
    [SerializeField] float zAxisRotationTickDuration;
    float last_zAxisRotationTime;
    float zRotation;
    public bool guanoBarrierDeployed;
    bool guanoBarrierSwitched;
    Collider2D collider;

    private void Awake() {
        sprites = GetComponentsInChildren<SpriteRenderer>();
        collider = GetComponent<Collider2D>();
        DrawSprites();
        //System.Numerics.Vector<int> lol = new(0);
        //System.Numerics.Vector.Add(lol, new System.Numerics.Vector<int>(10));
    }

    void DrawSprites() {
        List<SpriteRenderer> newSprites = new();
        newSprites.AddRange(sprites);
        foreach (var sprite in sprites) {
            var cloningSprite = sprite;
            for (int i = 0; i < 3; i++) {
                var newObj = new GameObject();
                var newObjPivot = new GameObject();
                newObj.transform.position = cloningSprite.transform.position;
                newObjPivot.transform.position = transform.position;
                newObjPivot.transform.SetParent(transform);
                newObj.transform.SetParent(newObjPivot.transform);
                newObjPivot.transform.rotation
                    = Quaternion.Euler(0, 0, cloningSprite.transform.parent.rotation.z + 90);
                var newSprite = newObj.AddComponent<SpriteRenderer>();
                newObj.AddComponent<LockRotation>();
                newSprite.sprite = cloningSprite.sprite;
                newSprite.transform.SetParent(transform);
                newSprite.transform.name = cloningSprite.transform.name;
                Destroy(newObjPivot.gameObject);
                newSprite.transform.rotation = Quaternion.Euler(Vector2.zero);
                newSprite.sortingOrder = 1;
                newSprites.Add(newSprite);
                cloningSprite = newSprite;
            }
        }
        sprites = newSprites.ToArray();
    }

    public void GuanoBarrierSpriteSwitch(bool enabled) {
        foreach (var sprite in sprites) {
            sprite.enabled = enabled;
            collider.enabled = enabled;
        }
        guanoBarrierDeployed = enabled;
        collider.enabled = enabled;
        if (enabled)
            AudioFW.PlayLoop("GuanoBarrierSpinning");
    }

    public void Update() {

        if (!guanoBarrierDeployed) {
            return;
        }

        if (last_zAxisRotationTime + zAxisRotationTickDuration < Time.time) { 
            zRotation += zAxisRotationPerRotationTick;
            last_zAxisRotationTime = Time.time;
        }

        transform.rotation = Quaternion.Euler(0, 0, zRotation);
    }
}
