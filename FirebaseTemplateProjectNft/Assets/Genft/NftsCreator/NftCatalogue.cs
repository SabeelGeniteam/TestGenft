using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;
[System.Serializable]
public enum MintStatus{minted,notminted}
[System.Serializable]
[FirestoreData]
public class NftData
{
    [SerializeField]
    private MintStatus mintStatus=MintStatus.notminted;
    [SerializeField]
    private Texture2D nftImage;
    [SerializeField]
    private string nftURI;
    [SerializeField]
    private string name;
    [SerializeField]
    private string description;
    [SerializeField]
    private string level;
    [FirestoreProperty]
    public string NftURI {get {return nftURI;}set { nftURI=value;}}
    [FirestoreProperty]
    public string Name {get {return name;}set { name=value;}}
    [FirestoreProperty]
    public string Discription {get {return description;}set { description=value;}}
    [FirestoreProperty]
    public string Level {get {return level;}set { level=value;}}
    public Texture2D NftImage{get;set;}
}

[CreateAssetMenu(fileName ="NFtsCatalogue", menuName ="MyNftsCatalogue")]
public class NftCatalogue : ScriptableObject
{

    public NftData[] nftData;
}
