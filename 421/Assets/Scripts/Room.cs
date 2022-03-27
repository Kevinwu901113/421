using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Room : MonoBehaviour
{
    public GameObject doorLeft, doorRight, doorUp, doorDown;
    public GameObject wallLeft, wallRight, wallUp, wallDown;

    public Text text;

    public bool roomLeft, roomRight, roomUp, roomDown;
    public int stepToStart;

    public int doorNumber; 

    public float x, y;


    // Start is called before the first frame update
    void Start()
    {
        doorLeft.SetActive(roomLeft);
        doorRight.SetActive(roomRight);
        doorUp.SetActive(roomUp);
        doorDown.SetActive(roomDown);
        wallLeft.SetActive(!roomLeft);
        wallRight.SetActive(!roomRight);
        wallUp.SetActive(!roomUp);
        wallDown.SetActive(!roomDown);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateRoom()
    {
        stepToStart = (int)(Mathf.Abs(transform.position.x / x) + Mathf.Abs(transform.position.y / y));
        text.text = stepToStart.ToString();

        if(roomUp)
            doorNumber++;
        if (roomDown)
            doorNumber++;
        if (roomLeft)
            doorNumber++;
        if (roomRight)
            doorNumber++;
    }
}
