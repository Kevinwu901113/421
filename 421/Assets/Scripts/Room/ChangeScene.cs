using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour
{
    public GameObject loadScreen;
    public Slider slider;
    public Text text;
    public Image enterdialog;
    public Text textdialog;
    Color s = new Color(0f, 0f, 0f, 0f);

    //切换场景1
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            enterdialog.color = s;
            enterdialog.GetComponent<RectTransform>().position = new Vector3(0,0,0);
            textdialog.color = s;
            StartCoroutine(Loadlevel());
        }
    }
    IEnumerator Loadlevel()
    {
        loadScreen.SetActive(true);
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);

        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            slider.value = operation.progress;

            text.text = operation.progress * 100 + "%";

            if (operation.progress >= 0.9f)
            {
                slider.value = 1;

                text.text = "Press AnyKey to continue";
                if (Input.anyKeyDown)
                {
                    operation.allowSceneActivation = true;
                }
            }

            yield return null;
        }
    }
}