using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisualizationManager : MonoBehaviour {
    private static VisualizationManager _instance;

    public static VisualizationManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<VisualizationManager>();
            }

            if (_instance == null)
            {
                Debug.Log("No instance of visualization manager found - making new one...");
                if (_instance == null)
                {
                    GameObject obj = Instantiate(Resources.Load<GameObject>("Prefabs/VisualizationManager"));
                    _instance = obj.GetComponent<VisualizationManager>();
                }
            }

            return _instance;
        }
    }

	// Use this for initialization
	void Start () {
        useTongsBtn.interactable = true;
        noUseTongsBtn.interactable = false;
        usingTongs = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (GameObject.FindObjectOfType<Crucible>().IsBroken)
        {
            UIManager.Instance.ShowResetDialog();
        }
	}

    public Button useTongsBtn;
    public Button noUseTongsBtn;

    private bool usingTongs;

    public bool UsingTongs
    {
        get
        {
            return usingTongs;
        }
    }

    public static bool Ping()
    {
        bool pinged = Instance != null;
        return pinged;
    }

    public void PickupTongs()
    {
        usingTongs = true;
        GameObject tongs = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Tongs"));
        useTongsBtn.interactable = false;
        noUseTongsBtn.interactable = true;
    }

    public void PutDownTongs()
    {
        usingTongs = false;
        useTongsBtn.interactable = true;
        noUseTongsBtn.interactable = false;
        if (GameObject.FindObjectsOfType<Tongs>().Length > 0)
        {
            Destroy(GameObject.FindObjectOfType<Tongs>().gameObject);
        }
    }


    public void ResetSim()
    {
        GameObject.FindObjectOfType<Crucible>().Reset();
        PutDownTongs();
    }



}
