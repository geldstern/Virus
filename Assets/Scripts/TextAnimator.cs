using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TextAnimator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Text text;
    private Behaviour halo;
    private Color COLOR_NORMAL, COLOR_HIGHLIGHT;

    private bool isPointerOver;

    void Start()
    {

        text = GetComponent<Text>();
        if (text != null)
        {
            COLOR_NORMAL = text.color;
            float h, s, v;
            Color.RGBToHSV(COLOR_NORMAL, out h, out s, out v);
            COLOR_HIGHLIGHT = Color.HSVToRGB(h, s * 0.8f, v * 1.2f);

        }

        halo = (Behaviour)gameObject.GetComponent("Halo");
        isPointerOver = false;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        /*
        if (isPointerOver)
        {

        }
        */
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        /*
        if (halo != null)
            halo.enabled = true;
            */
        if (text != null)
        {
            text.color = COLOR_HIGHLIGHT;
            text.transform.localScale = new Vector3(1.3f, 1.3f, 1);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        /*
        if (halo != null)
            halo.enabled = false;
            */
        if (text != null)
        {
            text.color = COLOR_NORMAL;
            text.transform.localScale = new Vector3(1f, 1f, 1);
        }
    }
}
