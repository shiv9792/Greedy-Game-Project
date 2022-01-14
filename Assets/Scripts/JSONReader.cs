using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class JSONReader : MonoBehaviour
{
    [Tooltip("type some Input to show it on RunTime")]
    [SerializeField] InputField userInput;

    [Tooltip("Result text which will be rendered as output of given Input")]
    [SerializeField] Text resultTxt;
    [SerializeField] Image frameImg;

    [Tooltip(" reference of Json text(.text file) which is kept in Resources")]
    [SerializeField] TextAsset jsonTxt;
    const string textType = "text";
    // const string frameType = "frame";

    RectTransform _finalTransform;

    public AllLayersList layersList = new AllLayersList();
    void Start()
    {
        resultTxt.gameObject.SetActive(false);
        layersList = JsonUtility.FromJson<AllLayersList>(jsonTxt.text);
        Debug.Log("list available : " + layersList.layers[0].type);
    }

    public void onClickBtn() => parseJson();

    void parseJson()
    {
        foreach (Layers layer in layersList.layers)
        {
            if (layer.type == "")
            {
                Debug.LogError("type is undefined");
                return;
            }
            _finalTransform = getActiveRectTransform(layer.type);
            if (layer.operations != null)
            {
                foreach (Operations operation in layer.operations)
                {
                    parseJsonOperations(operation.name, operation.argument, layer.type);
                }
            }
            if (layer.path != null)
            {
                StartCoroutine(StaticRefs.DownloadImage(layer.path, frameImg));
            }
            foreach (Placement _placement in layer.placement)
            {
                setRectsPositions(_placement.position.x, _placement.position.y, _placement.position.height, _placement.position.width, _finalTransform);
            }



        }
    }



    RectTransform getActiveRectTransform(string _type)
    {
        resultTxt.text = userInput.text;
        RectTransform _activeTransform = _type.Equals(textType) ? resultTxt.rectTransform : frameImg.rectTransform;
        if (_activeTransform == resultTxt.rectTransform)
        {
            resultTxt.gameObject.SetActive(true);
            setRectsPositions(0, 0, Screen.height, Screen.width, frameImg.rectTransform);
        }
        else if (_activeTransform == frameImg.rectTransform)
        {
            resultTxt.gameObject.SetActive(false);
        }
        return _activeTransform;
    }

    void setRectsPositions(float _x, float _y, float _height, float _width, RectTransform _rect)
    {
        if (_height > Screen.height || _width > Screen.width)
        {
            Debug.LogError("Either height or width is above the range of screen");
            return;
        }
        _rect.localPosition = new Vector3(_x, _y, 0);
        _rect.sizeDelta = new Vector2(_width, _height);
    }

    void parseJsonOperations(string _operationName, string _arg, string _type)
    {
        switch (_operationName)
        {
            case "color":
                setRectColor(_type, _arg);
                break;

            default:
                setRectColor(_type, _arg);
                break;
        }
    }

    void setRectColor(string _type, string _arg)
    {
        if (_type.Equals(textType))
        {
            resultTxt.GetComponent<Text>().color = StaticRefs.getColorFromString(_arg);
        }
        else
        {
            frameImg.color = StaticRefs.getColorFromString(_arg);
            Debug.Log("color changed");
        }
    }


}