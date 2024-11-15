using System;
using System.Collections;
using System.Net;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class APIManager : MonoBehaviour
{
    public static APIManager instance;

    private void Awake()
    {
        // Singleton pattern to ensure there's only one instance of APIManager
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // This makes sure the object is not destroyed between scenes
        }
        else if (instance != this)
        {
            Destroy(gameObject); // Destroy duplicates if they exist
        }
    }

    /*string SetupUri(string subUri) => string.Format("{0}/{1}", StaticDataVR.domain, subUri);*/
    private string SetupUri(string subUri) => $"{StaticDataVR.domain}/{subUri}";

    public IEnumerator PostDataCoroutine(string subUri, string jsonData, Action<string> SetDataEvent = null)
    {
        string url = SetupUri(subUri);
        Debug.Log("POST URL: " + url);
        Debug.Log("POST Data: " + jsonData); 

        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
        UnityWebRequest request = new UnityWebRequest(url, "POST");

        request.SetRequestHeader("Content-Type", "application/json");
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error: " + request.error); 
        }
        else
        {
            Debug.Log("API Response: " + request.downloadHandler.text);  
            SetDataEvent?.Invoke(request.downloadHandler.text); 
        }
    }


    /* public IEnumerator PostDataCoroutine(string subUri, string jsonData, Action<string> SetDataEvent = null)
     {
         string url = SetupUri(subUri);
         //yield return new WaitUntil(() => !string.IsNullOrEmpty(DataHandler.instance.GetUserTicket()));
         yield return null;

         byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
         UnityWebRequest request = new UnityWebRequest(url, "POST");

         request.SetRequestHeader("Content-Type", "application/json");
         request.uploadHandler = new UploadHandlerRaw(bodyRaw);
         request.downloadHandler = new DownloadHandlerBuffer();

         yield return request.SendWebRequest();
         if (request.result != UnityWebRequest.Result.Success)
             Debug.LogError("Error: " + request.error);
         else
             SetDataEvent?.Invoke(request.downloadHandler.text);
     }*/

    public IEnumerator PostDataWithTokenCoroutine(string subUri, string jsonData, Action<string> SetDataEvent = null)
    {
        string url = SetupUri(subUri);
        //yield return new WaitUntil(() => !string.IsNullOrEmpty(DataHandler.instance.GetUserTicket()));
        yield return null;

        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
        var request = new UnityWebRequest(url, "POST");

        string authHeaderValue = $"Bearer {StaticData.token}";
        request.SetRequestHeader("Authorization", authHeaderValue);
        request.SetRequestHeader("Content-Type", "application/json");

        request.method = UnityWebRequest.kHttpVerbPOST;
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();

        yield return request.SendWebRequest();
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(request.error);
            // errorPanel.SetActive(true);
        }
        else
        {
            SetDataEvent?.Invoke(request.downloadHandler.text);
        }
    }

    public IEnumerator PatchDataCoroutine(string subUri, string jsonData, Action<string> SetDataEvent = null)
    {
        string url = SetupUri(subUri);
        //yield return new WaitUntil(() => !string.IsNullOrEmpty(DataHandler.instance.GetUserTicket()));
        yield return null;

        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
        var request = new UnityWebRequest(url, "PATCH");

        request.SetRequestHeader("Content-Type", "application/json");
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();

        yield return request.SendWebRequest();
        if (request.result != UnityWebRequest.Result.Success)
            Debug.LogError("Error: " + request.error);
        else
            SetDataEvent(request.downloadHandler.text);
    }

    public IEnumerator GetDataCoroutine(string subUri, Action<string> SetDataEvent = null)
    {
        string url = SetupUri(subUri);
        //yield return new WaitUntil(() => !string.IsNullOrEmpty(DataHandler.instance.GetUserTicket()));
        yield return null;

        using UnityWebRequest request = UnityWebRequest.Get(url);

        yield return request.SendWebRequest();
        if (request.result != UnityWebRequest.Result.Success)
            Debug.LogError("Error: " + request.error);
        else
            SetDataEvent(request.downloadHandler.text);
    }

    public IEnumerator GetDataCoroutineWithToken(string subUri, string token, Action<string> SetDataEvent = null)
    {
        string url = SetupUri(subUri);
        yield return null;

        using UnityWebRequest request = UnityWebRequest.Get(url);
        request.SetRequestHeader("Authorization", "Bearer " + token);

        yield return request.SendWebRequest();
        if (request.result != UnityWebRequest.Result.Success)
            Debug.LogError("Error: " + request.error);
        else
            SetDataEvent(request.downloadHandler.text);
    }

    public IEnumerator DownloadImageCoroutine(string subUri, Action<Sprite> SetDataEvent = null)
    {
        string url = SetupUri(subUri);
        //yield return new WaitUntil(() => !string.IsNullOrEmpty(DataHandler.instance.GetUserTicket()));
        yield return null;

        using UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error downloading image: " + request.error);
        }
        else
        {
            Texture2D texture = DownloadHandlerTexture.GetContent(request);

            Sprite sprite = Sprite.Create(
                texture,
                new Rect(0, 0, texture.width, texture.height),
                new Vector2(0.5f, 0.5f)
            );

            SetDataEvent(sprite);
        }
    }
}