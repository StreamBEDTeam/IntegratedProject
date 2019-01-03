using StreamBED.Backend.Helper;
using StreamBED.Backend.Models.ProtocolModels;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

    public GameObject discardButton;

    public GameObject saveButton;

	// Use this for initialization
	void Start () {
        displayFeatureList();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnEnable()
    {
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(discardButton);
    }

    private void displayFeatureList()
    {
        Keyword[] bKey = BankStabilityModel.GetKeywords();
        Keyword[] eKey = EpifaunalSubstrateModel.GetKeywords();
        Keyword[] featureNames = new Keyword[bKey.Length + eKey.Length];
        bKey.CopyTo(featureNames, 0);
        eKey.CopyTo(featureNames, bKey.Length);

        // Add the features
        int len = featureNames.Length; 
        GameObject[] featureObjs = new GameObject[len];
        Toggle[] featureToggles = new Toggle[len];
        float parentHeight = transform.GetComponent<RectTransform>().sizeDelta.y;
        float distBetween = -1 * (parentHeight / len);

        // Adding image features
        for (int i = 1; i < len - 1; i++)   
        {
            featureObjs[i] = (GameObject) Instantiate(Resources.Load("ImageFeature"));
            featureObjs[i].transform.SetParent(transform, false);
            featureObjs[i].transform.localScale = new Vector3(1, 1, 1);
            featureObjs[i].transform.localPosition = featureObjs[i].transform.localPosition + new Vector3(0, -56 + distBetween * (i) * 0.9f, 0);
            featureToggles[i] = featureObjs[i].GetComponent<Toggle>();
            featureToggles[i].GetComponentInChildren<Text>().text = featureNames[i].Content;
        }

    }

}
