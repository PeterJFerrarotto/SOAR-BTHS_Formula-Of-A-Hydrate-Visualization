using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scale : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponentInParent<Crucible>())
        {
            Crucible.CrucibleState state = collision.gameObject.GetComponentInParent<Crucible>().State;
            UIManager.DataField field = UIManager.DataField.emptyCrucibleWeight;
            switch (state)
            {
                case Crucible.CrucibleState.Empty:
                    field = UIManager.DataField.emptyCrucibleWeight;
                    break;
                case Crucible.CrucibleState.Hydrated:
                    field = UIManager.DataField.hydrateCrucibleWeight;
                    break;
                case Crucible.CrucibleState.DeHydrated:
                    field = UIManager.DataField.deHydrateCrucibleWeight;
                    break;
            }
            float weight = collision.gameObject.GetComponentInParent<Crucible>().Weight;
            UIManager.Instance.DisplayData(weight, field);
        }
    }
}
