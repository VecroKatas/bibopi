using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TextNodeHandler : MonoBehaviour
{
    public float localPositionY;
    public void Start()
    {
        StartCoroutine(Resize());
    }

    private IEnumerator Resize()
    {
        RectTransform textFieldRectTransform = transform.parent.GetComponent<RectTransform>();
        
        Text titleText = transform.Find("Title").GetComponent<Text>();
        RectTransform titleRectTransform = titleText.GetComponent<RectTransform>();

        RectTransform textRectTransform = transform.Find("Text").GetComponent<RectTransform>();
        Text text = transform.Find("Text").GetComponent<Text>();
        
        GameObject spaceAfterAuthor = transform.Find("SpaceAfterAuthor").gameObject;
        RectTransform SAARectTransform = transform.Find("SpaceAfterAuthor").GetComponent<RectTransform>();
        Text SAAText = spaceAfterAuthor.AddComponent<Text>();
        SAAText.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
        SAAText.fontSize = 4;
        SAAText.text = "\n";

        GameObject spaceBetweenNods = transform.Find("SpaceBetweenNods").gameObject;
        RectTransform SBNRectTransform = transform.Find("SpaceBetweenNods").GetComponent<RectTransform>();
        Text SBNText = spaceBetweenNods.AddComponent<Text>();
        SBNText.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
        SBNText.fontSize = 9;
        SBNText.text = "\n";
        
        RectTransform textNodeRectTransform = GetComponent<RectTransform>();

        yield return new WaitForFixedUpdate();//wait a fixed update because otherwise preferredHeight does shit 
        
        titleRectTransform.sizeDelta = new Vector2(textFieldRectTransform.sizeDelta.x, titleText.preferredHeight);
        titleText.transform.localPosition = new Vector3(0, 0, 0);
        
        SAARectTransform.sizeDelta = new Vector2(textFieldRectTransform.sizeDelta.x, SAAText.preferredHeight);
        SAAText.transform.localPosition = new Vector3(0, -titleText.preferredHeight, 0);
        
        textRectTransform.sizeDelta = new Vector2(textRectTransform.sizeDelta.x, text.preferredHeight);
        text.transform.localPosition = new Vector3(0, -titleText.preferredHeight - SAAText.preferredHeight, 0);
        
        SBNRectTransform.sizeDelta = new Vector2(textFieldRectTransform.sizeDelta.x, SBNText.preferredHeight);
        SBNText.transform.localPosition = new Vector3(0, -titleText.preferredHeight - SAAText.preferredHeight - text.preferredHeight, 0);
        
        textNodeRectTransform.sizeDelta = new Vector2(textNodeRectTransform.sizeDelta.x, titleText.preferredHeight + text.preferredHeight + SBNText.preferredHeight + SAAText.preferredHeight);
        transform.localPosition = new Vector3(0, -localPositionY, 0);
    }
}
