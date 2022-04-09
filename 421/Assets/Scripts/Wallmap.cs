using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallmap : MonoBehaviour
{
    GameObject mapSprite;

    private void OnEnable() {
        mapSprite = transform.parent.GetChild(0).gameObject;

        mapSprite.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player"))
            mapSprite.SetActive(true);
    }
}
