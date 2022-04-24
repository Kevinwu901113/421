using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterDialog : MonoBehaviour
{
    public GameObject enterDialog;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            enterDialog.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision) {
        if(collision.CompareTag("Player"))
        {
            enterDialog.SetActive(false);
        } 
    }
}
