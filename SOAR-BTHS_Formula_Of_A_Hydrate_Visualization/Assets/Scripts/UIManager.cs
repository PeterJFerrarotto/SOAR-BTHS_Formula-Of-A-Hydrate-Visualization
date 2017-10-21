using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public Image saltUI;
    public Image bunsenBurnerUI;

    public bool atSalt;

    public enum DataField
    {
        emptyCrucibleWeight,
        hydrateCrucibleWeight,
        deHydrateCrucibleWeight,
    }
    public Image resetText;
    private static UIManager _instance;

    public Text crucibleWeightEmpty;

    public Text crucibleWeightFull_Hydrate;

    public Text crucibleWeightFull_DeHydrated;

    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<UIManager>();
            }

            if (_instance == null)
            {
                Debug.Log("Didn't find any instance of UIManager - making new one...");
                GameObject obj = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/UIManager"));
                _instance = obj.GetComponent<UIManager>();
            }

            return _instance;
        }
    }
	// Use this for initialization
	void Start () {
        resetText.gameObject.SetActive(false);
        atSalt = true;
        saltUI.gameObject.SetActive(true);
        bunsenBurnerUI.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static bool Ping()
    {
        bool pinged = Instance != null;
        return pinged;
    }

    public void ShowResetDialog()
    {
        resetText.gameObject.SetActive(true);
    }

    public void ConfirmReset()
    {
        VisualizationManager.Instance.ResetSim();
        crucibleWeightEmpty.text = "";
        crucibleWeightFull_Hydrate.text = "";
        crucibleWeightFull_DeHydrated.text = "";
        resetText.gameObject.SetActive(false);
    }

    public void DisplayData(object data, DataField field)
    {
        switch (field)
        {
            case DataField.emptyCrucibleWeight:
                crucibleWeightEmpty.text = data.ToString();
                break;
            case DataField.hydrateCrucibleWeight:
                crucibleWeightFull_Hydrate.text = data.ToString();
                break;
            case DataField.deHydrateCrucibleWeight:
                crucibleWeightFull_DeHydrated.text = data.ToString();
                break;
            default:
                throw new System.Exception("Unknown data field!");
                break;
        }
    }

    public void SwitchUI()
    {
        if (atSalt)
        {
            saltUI.gameObject.SetActive(false);
            bunsenBurnerUI.gameObject.SetActive(true);
            atSalt = false;
        }
        else
        {
            bunsenBurnerUI.gameObject.SetActive(false);
            saltUI.gameObject.SetActive(true);
            atSalt = true;
        }
    }
}
