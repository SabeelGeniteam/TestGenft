using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Storage;
using System;
using Newtonsoft.Json;

public class NftMetaDataUploader : MonoBehaviour
{
    public Texture2D texture2D;
    [System.Serializable]
    public class nftData
    {
        public string name="GenftName";
        public string description="GenftDescription";
        public string image="https://firebasestorage.googleapis.com/v0/b/genfttemplate.appspot.com/o/myNftsCollection%2Fmy-files.png?alt=media&token=9fc3460d-b84a-4f0e-94ca-9fa92ce10186";
        public string meta="https://firebasestorage.googleapis.com/v0/b/genfttemplate.appspot.com/o/myNftsCollection%2Fmy-files.json?alt=media&token=7573b628-5bca-47e6-ac6d-6097a1079d17";
    }
    public nftData nftmetaData;
    public NftCatalogue nftCatalogue;
    public void UploadNftImage(Texture2D nftImage)
    {
        StartCoroutine(UploadCoroutine(nftImage));
    }
    private IEnumerator UploadCoroutine(Texture2D nftImage)
    {
        string json=JsonUtility.ToJson(nftmetaData);
        var storage=FirebaseStorage.DefaultInstance;
        //var nftImageReferance=storage.GetReference($"/Genfts/{Guid.NewGuid()}.png");

        var nftImageReferance=storage.GetReference("myNftsCollection");
        var fileRef=nftImageReferance.Child("my-metaDataFiles.json");

        //var bytes=nftImage.EncodeToPNG();
        var stringMessage=JsonConvert.SerializeObject(json);
        var bytes= System.Text.Encoding.UTF8.GetBytes(json);
        var nftMetadata= new MetadataChange()
        {
            ContentEncoding ="image/png",
            CustomMetadata = new Dictionary<string,string>()
            {
                {"Nft Name","Genft"},
                {"Special","Testing Description for meta"}
            }
        };
        nftMetadata.ContentType="image/json";
        //Debug.LogError(bytes.ToString());
        var uploadTask=fileRef.PutBytesAsync(bytes,nftMetadata);
        yield return new WaitUntil(()=>uploadTask.IsCompleted);
        if(uploadTask.Exception!=null)
        {
            Debug.LogError($"Failed to upload because{uploadTask.Exception}");
            yield break;
        }

        var getUriTask =fileRef.GetDownloadUrlAsync();
        yield return new WaitUntil(()=>getUriTask.IsCompleted);
        if(getUriTask.Exception!=null)
        {
            Debug.LogError($"Failed to get url because{uploadTask.Exception}");
            yield break;
        }
        Debug.Log($"Download from {getUriTask.Result}");
        Debug.LogError(nftImageReferance.Name);

    }
}
