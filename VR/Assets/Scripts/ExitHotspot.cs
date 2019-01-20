using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using StreamBED.Backend.Helper;
using StreamBED.Backend.Models.ProtocolModels;
using System.IO;
public class ExitHotspot : Hotspot
{
    public override void HotspotTrigger()
    {
        SerializePhotos();
        SceneManager.LoadScene("MenuScene");
    }

    public void SerializePhotos()
    {
        //Map feature name in Unity to Keyword in Backend
        var keywordMap = new Dictionary<string, Keyword>();
        keywordMap["CobbleSome"] = EpifaunalSubstrateModel.Keywords.Cobble;
        keywordMap["SteepBank"] = BankStabilityModel.Keywords.SteepBank;
        keywordMap["GentlySlopingBank"] = BankStabilityModel.Keywords.GentlySlopingBank;

        /*
         * EpifaunalSubstrateModel
        public static readonly Keyword Snags;
        public static readonly Keyword SubmergedLogs;
        public static readonly Keyword UndercutBanks;
        public static readonly Keyword Cobble;

        BankStabilityModel
    public static readonly Keyword SteepBank;
    public static readonly Keyword GentlySlopingBank;
    public static readonly Keyword BankFailure;
    public static readonly Keyword CrumblingBank;
    public static readonly Keyword ErosionalScars;
    public static readonly Keyword ExposedSoil;
    public static readonly Keyword ExposedTreeRoot;
    */

        var handle = GameObject.FindObjectOfType<GameStateHandle>();
        var state = handle.Instance;

        var ser = new ImageSerialization();
        foreach(var photo in state.Photos)
        {
            var bytes = File.ReadAllBytes(photo.ImagePath);
            var image = new ImageWithMetadata(bytes);
            image.Location = photo.AreaName;
            foreach(var tag in photo.Tags)
            {
                if (keywordMap.ContainsKey(tag))
                {
                    image.AddKeyword(keywordMap[tag]);
                }
                else
                {
                    Debug.LogErrorFormat("Missing keyword in keywordMap: [{0}]", tag);
                }
            }
            ser.AddImage(image);
        }
        try
        {
            ser.SerializeImage();
        }
        catch (System.Runtime.Serialization.SerializationException ex)
        {
            Debug.LogException(ex);
        }
    }
}
