using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class StaticRefs
{
    private static int HexToDec(string _hex)
    {
        int dec = System.Convert.ToInt32(_hex, 16);
        return dec;
    }

    private static float hexToFloatNormalized(string _hex)
    {
        return HexToDec(_hex) / 255f;
    }
    public static Color getColorFromString(string _hexToString)
    {
        if (_hexToString == "")
            return new Color(1, 1, 1, 1);

        float _red = hexToFloatNormalized(_hexToString.Substring(1, 2));
        float _green = hexToFloatNormalized(_hexToString.Substring(3, 2));
        float _blue = hexToFloatNormalized(_hexToString.Substring(5, 2));

        float _alpha = 1f;
        if (_hexToString.Length >= 9)
        {
            _alpha = hexToFloatNormalized(_hexToString.Substring(7, 2));
        }

        // Debug.Log($"red is {_red}, green is {_green}, blue is {_blue} ");

        return new Color(_red, _green, _blue, _alpha);
    }


    public static IEnumerator DownloadImage(string MediaUrl, Image ProfileImg)
    {
        string url = MediaUrl.Replace(@"\", string.Empty);
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log($"<color=red> {request.error}</color>");
        }
        else
        {
            Texture2D tex = ((DownloadHandlerTexture)request.downloadHandler).texture;
            Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(tex.width / 2, tex.height / 2));
            Debug.Log("sprite is : " + sprite);
            ProfileImg.GetComponent<Image>().sprite = sprite;

        }
    }

}
