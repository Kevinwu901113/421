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
    //切换场景1
    public void LoadNextLevel()
    {
        Debug.Log("1");
        StartCoroutine(Loadlevel());
    }
    IEnumerator Loadlevel()
    {
        loadScreen.SetActive(true);
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);

        operation.allowSceneActivation = false;

        while(!operation.isDone)
        {
            slider.value = operation.progress;

            text.text = operation.progress * 100 + "%";

            if(operation.progress >= 0.9f)
            {
                slider.value = 1;

                text.text = "Press AnyKey to continue";
                if(Input.anyKeyDown)
                {
                    operation.allowSceneActivation = true;
                }
            }

            yield return null;
        }
    }
    //切换场景2
}
