using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TextAnimator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Text text;
    private Behaviour halo;

    private bool isPointerOver;

    void Start()
    {
        text = GetComponent<Text>();
        halo = (Behaviour)gameObject.GetComponent("Halo");
        isPointerOver = false;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (isPointerOver)
        {

        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (halo != null)
            halo.enabled = true;
        if (text != null)
            text.transform.localScale = new Vector3(1.3f, 1.3f, 1);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (halo != null)
            halo.enabled = false;
        if (text != null)
            text.transform.localScale = new Vector3(1f, 1f, 1);
    }
}
