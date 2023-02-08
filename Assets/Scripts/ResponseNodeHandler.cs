using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResponseNodeHandler : MonoBehaviour
{
    public float localPositionY;
    public GameObject responseVariantPrefab;
    
    private List<GameObject> responseVariants;
    private float _first = 0;
    public void Start()
    {
        responseVariants = new List<GameObject>();
        CreateResponseVariant("Let's choose first variant and see what it will show us.");
        CreateResponseVariant("Let's choose second variant and see what happens.");
        StartCoroutine(Resize());
    }

    private void CreateResponseVariant(string responseText)
    {
        GameObject responseVariant = Instantiate(responseVariantPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        responseVariant.transform.SetParent(transform, false);
        responseVariant.name = "ResponseVariant" + responseVariants.Count;
        responseVariants.Add(responseVariant);
        responseVariant.transform.Find("Index").GetComponent<Text>().text = responseVariants.Count + ". ";
        responseVariant.transform.Find("Text").GetComponent<Text>().text = responseText;
        
        if (_first == 0) _first = responseVariant.GetComponent<RectTransform>().sizeDelta.y;
        
        if (responseVariants.Count == 0)
            responseVariant.GetComponent<ResponseVariantHandler>().localPositionY = 0;
        else
            responseVariant.GetComponent<ResponseVariantHandler>().localPositionY = CalculateHeight() - _first;
    }

    private float CalculateHeight()
    {
        float sum = 0;
        for (int i = 0; i < responseVariants.Count; i++)
        {
            sum += responseVariants[i].GetComponent<RectTransform>().sizeDelta.y + 5;
        }

        return sum;
    }

    private IEnumerator Resize()
    {
        RectTransform responseNodeRectTransform = GetComponent<RectTransform>();

        responseNodeRectTransform.sizeDelta = new Vector2(responseNodeRectTransform.sizeDelta.x, CalculateHeight() + 10);
        
        yield return new WaitForFixedUpdate();//wait a fixed update because otherwise preferredHeight does shit 

        transform.localPosition = new Vector3(0, -localPositionY, 0);
    }
}
