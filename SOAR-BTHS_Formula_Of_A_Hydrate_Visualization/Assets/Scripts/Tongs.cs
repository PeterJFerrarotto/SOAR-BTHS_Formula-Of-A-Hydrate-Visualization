using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tongs : MonoBehaviour {
    [SerializeField]
    private GameObject pickupLoc;

    private float distance;
    public enum TongsState
    {
        Tongs_Closed,
        Tongs_Open,
    }

    public TongsState state;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponentInParent<Crucible>())
        {
            if (state == TongsState.Tongs_Closed)
            {
                if (!other.gameObject.GetComponentInParent<Crucible>().BeingHeld)
                {
                    other.gameObject.GetComponentInParent<Crucible>().Hold(pickupLoc.transform);
                }
            }
            else if (state == TongsState.Tongs_Open)
            {
                if (other.gameObject.GetComponentInParent<Crucible>().BeingHeld)
                {
                    other.gameObject.GetComponentInParent<Crucible>().Drop();
                }
            }
        }
    }

    //private void OnCollisionExit(Collision collision)
    //{
    //    if (collision.gameObject.GetComponent<Crucible>())
    //    {
    //        if (state == TongsState.Tongs_Opening)
    //        {
    //            collision.gameObject.GetComponent<Crucible>().Drop();
    //        }
    //    }
    //}

    // Use this for initialization
    void Start () {
        state = TongsState.Tongs_Open;
        gameObject.transform.eulerAngles = new Vector3(0, 13.7f, 0);
	}
	
	// Update is called once per frame
	void Update () {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 rayPoint = ray.GetPoint(distance);
        rayPoint.z = -7.4f;
        transform.position = rayPoint;

        if (Input.GetMouseButtonDown(0))
        {
            if (state == TongsState.Tongs_Open)
            {
                state = TongsState.Tongs_Closed;
            }
            else if (state == TongsState.Tongs_Closed)
            {
                state = TongsState.Tongs_Open;
            }
            
        }
    }

    public void Open()
    {

    }

}
