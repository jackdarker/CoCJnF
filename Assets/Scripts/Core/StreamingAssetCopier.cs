using UnityEngine;
using System.Collections;
using System;
using System.IO;

public class StreamingAssetCopier : MonoBehaviour
{
    string streamingAssetFilePath;
    string destinationFilePath;
    Action onComplete;

    public static void CopyToPersistentDataPath(string streamingAssetFilePath, string destinationFilePath, Action onComplete)
    {
        GameObject obj = new GameObject(Path.GetFileNameWithoutExtension(destinationFilePath));
        StreamingAssetCopier copier = obj.AddComponent<StreamingAssetCopier>();
        copier.streamingAssetFilePath = streamingAssetFilePath;
        copier.destinationFilePath = destinationFilePath;
        copier.onComplete = onComplete;
        copier.StartCoroutine(copier.Copy());
    }

    IEnumerator Copy()
    {
        WWW www = new WWW(streamingAssetFilePath);
        yield return www;
        File.WriteAllBytes(destinationFilePath, www.bytes);
        if (onComplete != null)
            onComplete();
        Destroy(gameObject);
    }
}