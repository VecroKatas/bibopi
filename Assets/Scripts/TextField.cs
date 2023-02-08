using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using UnityEngine;
using UnityEngine.UI;

public class TextField : MonoBehaviour
{
    [SerializeField]
    public GameObject TextNodePrefab;
    [SerializeField]
    public GameObject ResponseNodePrefab;

    private RectTransform rectTransform;
    private List<GameObject> textNodes = new List<GameObject>();
    private List<GameObject> responseNodes = new List<GameObject>();
    private int _textNodeIndex = -1, _responseNodeIndex = -1;
    private float _scrollValue, _height = Screen.height - 30, _firstTextNode = 0, _firstResponseNode = 0;
    private bool _textUIEmpty = true;
    private const string lorem = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam tempus nibh egestas orci mollis pharetra. Proin convallis eros a elementum commodo.";

    private void Awake()
    {
        rectTransform = transform.GetComponent<RectTransform>();
        _scrollValue = transform.parent.Find("Scrollbar").GetComponent<Scrollbar>().value;
    }

    private void Start()
    {
        ChangePositionViaScroll();
    }

    public void OpenEmptyTextUI()
    {
        if (_textUIEmpty)
        {
            NewNode();
            _textUIEmpty = false;
        }
    }

    public void NewNode()
    {
        CreateTextNode();
        ChangePositionViaScroll();
    }

    public void NewResponse()
    {
        CreateResponseNode();
        ChangePositionViaScroll();
    }
    
    public void CreateTextNode(string title = "Title", string text = lorem)
    {
        GameObject textNode = Instantiate(TextNodePrefab, new Vector3(0, 0, 0), Quaternion.identity);
        _textNodeIndex++;
        textNode.transform.SetParent(transform, false);
        textNode.name = "TextNode" + _textNodeIndex;
        textNodes.Add(textNode);

        textNode.transform.Find("Title").GetComponent<Text>().text = title;
        textNode.transform.Find("Text").GetComponent<Text>().text = text;
        
        if (_firstTextNode == 0) _firstTextNode = textNode.GetComponent<RectTransform>().sizeDelta.y;
        
        if (_textNodeIndex == 0)
            textNode.GetComponent<TextNodeHandler>().localPositionY = 0;
        else
            textNode.GetComponent<TextNodeHandler>().localPositionY = CalculateHeight() - _firstTextNode;
        
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, CalculateHeight());
    }

    public void CreateResponseNode()
    {
        GameObject responseNode = Instantiate(ResponseNodePrefab, new Vector3(0, 0, 0), Quaternion.identity);
        _responseNodeIndex++;
        responseNode.transform.SetParent(transform, false);
        responseNode.name = "ResponseNode" + _responseNodeIndex;
        responseNodes.Add(responseNode);

        if (_firstResponseNode == 0) _firstResponseNode = responseNode.GetComponent<RectTransform>().sizeDelta.y;

        responseNode.GetComponent<ResponseNodeHandler>().localPositionY = CalculateHeight() - _firstResponseNode;
    }

    private float CalculateHeight()
    {
        float sum = 0;
        for (int i = 0; i <= _textNodeIndex; i++)
            sum += textNodes[i].transform.GetComponent<RectTransform>().sizeDelta.y;

        for (int i = 0; i <= _responseNodeIndex; i++)
            sum += responseNodes[i].transform.GetComponent<RectTransform>().sizeDelta.y;
        
        return sum;
    }

    private void ChangePositionViaScroll()
    {
        if (CalculateHeight() < _height) 
            transform.localPosition = new Vector3(0, (_scrollValue - .5f) * (_height - CalculateHeight()) + CalculateHeight() / 2, 0);
        else 
            transform.localPosition = new Vector3(0, -(_scrollValue - .5f) * (_height - CalculateHeight()) + CalculateHeight() / 2, 0);
        
    }

    public void Scroll()
    {
        _scrollValue = transform.parent.Find("Scrollbar").GetComponent<Scrollbar>().value;
        ChangePositionViaScroll();
    }
}
