using Didenko.Sayollo.VideoAds.Extensions;
using Didenko.Sayollo.VideoAds.Networking.Data;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class AdsTestScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       StartCoroutine(GetVideo());
    }

    private IEnumerator GetVideo()
    {
        var request = UnityWebRequest.Get("https://6u3td6zfza.execute-api.us-east-2.amazonaws.com/prod/ad/vast");
        yield return request.SendWebRequest();

        string result = request.downloadHandler.text;

        result.DeserializeXml<VAST>();
    }
}

public class GetRequestSender
{
    //public void GetXml<T>()
}