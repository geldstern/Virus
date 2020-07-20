using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour
{
    // Wird aufgerufen bei Klick auf den Play Button
    public void HandlePlay()
    {
        Debug.Log("Play pressed");
        StartCoroutine(GameManager.instance.SetState(GameManager.State.PLAY));
    }

    public void HandleHowTo()
    {
        Debug.Log("HowTo pressed");
    }

    public void HandleQuit()
    {
        Debug.Log("Quit pressed");
        Application.Quit();
    }

    public void HandleBack()
    {
        Debug.Log("Back pressed");
        StartCoroutine(GameManager.instance.SetState(GameManager.State.MENU));
    }

    // Deaktiviert das Hauptmenu mit Animation:
    public IEnumerator HideMenu(int index)
    {
        Debug.Log("enter " + gameObject.name + "." + " HideMenu()");

        for (float ft = 1f; ft >= 0f; ft -= 0.05f)
        {
            foreach (Transform child in transform)
            {
                /*
                float scale;
                if (child.GetSiblingIndex() == index)
                {
                    scale = 1.1f;
                }
                else
                {
                    scale = 0.9f;
                }
                child.localScale = new Vector3(child.localScale.x * scale, child.localScale.y * scale, 1);

*/
                Text text = child.GetComponent<Text>();
                if (text != null)
                {
                    Color c = text.color;
                    c.a = ft;
                    text.color = c;
                }
            }
            yield return null;
        }

        Debug.Log("exit " + gameObject.name + "." + " HideMenu()");
        //gameObject.SetActive(false);
    }


    // Aktiviert das Hauptmenu mit Animation:
    public IEnumerator ShowMenu()
    {
        Debug.Log("enter " + gameObject.name + "." + " ShowMenu()");
        for (float ft = 0f; ft <= 1; ft += 0.05f)
        {
            foreach (Transform child in transform)
            {
                child.localScale = new Vector3(ft, ft, 1);

                Text text = child.GetComponent<Text>();
                if (text != null)
                {
                    Color c = text.color;
                    c.a = ft;
                    text.color = c;
                }
            }
            yield return null;
        }
        Debug.Log("exit " + gameObject.name + "." + " ShowMenu()");
    }
}
