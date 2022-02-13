using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Siccity.GLTFUtility;
using System.IO;

public class GlbLoader : MonoBehaviour
{
    string filePath;

    public static GlbLoader Instance { get; private set; }
    void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
    }

    private void Start()
    {
        filePath = $"{Application.persistentDataPath}/Files/";
    }
    public void DownloadFile(string url, Transform parent)
    {
        string path = GetFilePath(url);
        if (File.Exists(path))
        {
            Debug.Log("Found file locally, loading...");
            LoadModel(path, parent);
            return;
        }

        StartCoroutine(GetFileRequest(url, (UnityWebRequest req) =>
        {
            if (req.result == UnityWebRequest.Result.ConnectionError || req.result == UnityWebRequest.Result.ProtocolError)
            {
                // Log any errors that may happen
                Debug.Log($"{req.error} : {req.downloadHandler.text}");
            }
            else
            {
                // Save the model into a new wrapper
                LoadModel(path, parent);
            }
        }));
    }

    string GetFilePath(string url)
    {
        string[] pieces = url.Split('/');
        string filename = pieces[pieces.Length - 1];

        return $"{filePath}{filename}";
    }

    void LoadModel(string path, Transform parent)
    {
        //ResetWrapper();
        AnimationClip[] clips;
        ImportSettings settings = new ImportSettings();
        settings.animationSettings.useLegacyClips = true;
        settings.animationSettings.looping = true;
        GameObject model = Importer.LoadFromFile(path,settings,out clips);
        OnGLTFImportComplete(model,clips);
        model.transform.SetParent(parent);
        model.transform.localPosition = Vector3.zero;
        model.transform.localScale = model.transform.localScale * 0.5f;
        GameManager.Instance.Loaded(model);
    }

    IEnumerator GetFileRequest(string url, Action<UnityWebRequest> callback)
    {
        using (UnityWebRequest req = UnityWebRequest.Get(url))
        {
            req.downloadHandler = new DownloadHandlerFile(GetFilePath(url));
            UnityWebRequestAsyncOperation operation = req.SendWebRequest();
            while (!operation.isDone)
            {
                float downloadDataProgress = req.downloadProgress * 100;

                UiManager.Instance.Loading(downloadDataProgress / 100.0f);

                yield return null;
            }
            callback(req);
        }
    }

    private void OnGLTFImportComplete(GameObject result, AnimationClip[] clips)
    {
        if (clips != null)
        {
            if (clips.Length > 0)
            {
                Debug.Log(clips.Length);
                Animation animation = result.AddComponent<Animation>();
                clips[0].legacy = true;
                animation.AddClip(clips[0], clips[0].name);
                animation.clip = animation.GetClip(clips[0].name);
                animation.Play();
                animation.wrapMode = WrapMode.Loop;
            }
        }
    }
}
