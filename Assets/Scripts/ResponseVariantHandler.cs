using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ResponseVariantHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public float localPositionY = 0;
    
    private Color defaultColor;
    private Color hoverColor;
    public void Start()
    {
        defaultColor = transform.Find("Text").GetComponent<Text>().color;
        hoverColor = new Color(defaultColor.r, defaultColor.g + 0.23f, defaultColor.b, defaultColor.a);
        StartCoroutine(Resize());
    }

    private IEnumerator Resize()
    {
        Text indexText = transform.Find("Index").GetComponent<Text>();
        RectTransform indexRectTransform = indexText.GetComponent<RectTransform>();

        RectTransform textRectTransform = transform.Find("Text").GetComponent<RectTransform>();
        Text text = transform.Find("Text").GetComponent<Text>();
        
        RectTransform responseVariantRectTransform = GetComponent<RectTransform>();

        yield return new WaitForFixedUpdate();//wait a fixed update because otherwise preferredHeight does shit 
        
        indexRectTransform.sizeDelta = new Vector2(indexText.preferredWidth, indexText.preferredHeight);
        
        textRectTransform.sizeDelta = new Vector2(textRectTransform.sizeDelta.x - indexText.preferredWidth / 2, text.preferredHeight);
        text.transform.localPosition = new Vector3(indexText.preferredWidth / 2, 0, 0);
        
        responseVariantRectTransform.sizeDelta = new Vector2(responseVariantRectTransform.sizeDelta.x, text.preferredHeight);
        responseVariantRectTransform.transform.localPosition = new Vector3(5, -localPositionY - 5, 0);
    }
    
    //Hover response variants
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.Find("Text").GetComponent<Text>().color = hoverColor;
        transform.Find("Text").GetComponent<Text>().fontSize += 2;
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        transform.Find("Text").GetComponent<Text>().color = defaultColor;
        transform.Find("Text").GetComponent<Text>().fontSize -= 2;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Click");
        TextField textField = gameObject.AddComponent<TextField>();
        if (transform.Find("Index").GetComponent<Text>().text == "1. ")
        {
            
            textField.CreateTextNode("First", "This is what you get after choosing first variant.");
        }
        else
        {
            textField.CreateTextNode("Second", "This is what you get after choosing second variant.");
        }
    }
}
