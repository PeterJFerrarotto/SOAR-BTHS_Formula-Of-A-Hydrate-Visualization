using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServicesListener : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (!UIManager.Ping() || !VisualizationManager.Ping())
        {
            throw new System.Exception("Either UI Manager or Visualization Manager does not exist!");
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
