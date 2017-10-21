using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crucible : MonoBehaviour {
    [SerializeField]
    private float temperature;

    public Vector3 startingPos;

    private bool beingHeld;

    public float tempLoss;

    public bool BeingHeld
    {
        get
        {
            return beingHeld;
        }
    }

    [SerializeField]
    private Color tintColor;

    [SerializeField]
    private float emptyCrucibleWeight;

    [SerializeField]
    private float hydratedCrucibleWeight;

    [SerializeField]
    private float deHydratedCrucibleWeight;

    public float Weight
    {
        get
        {
            float weight = 0;
            switch (state)
            {
                case CrucibleState.Empty:
                    weight = emptyCrucibleWeight;
                    break;
                case CrucibleState.Hydrated:
                    weight = hydratedCrucibleWeight;
                    break;
                case CrucibleState.DeHydrated:
                    weight = deHydratedCrucibleWeight;
                    break;
            }
            return weight;
        }
    }

    private Transform holdPos;

    public enum CrucibleState
    {
        Empty,
        Hydrated,
        DeHydrated
    }

    private CrucibleState state;

    public CrucibleState State
    {
        get
        {
            return state;
        }
        set
        {
            switch (value)
            {
                case CrucibleState.Empty:
                    hydratedSalt.SetActive(false);
                    deHydratedSalt.SetActive(false);
                    break;
                case CrucibleState.Hydrated:
                    hydratedSalt.SetActive(true);
                    deHydratedSalt.SetActive(false);
                    break;
                case CrucibleState.DeHydrated:
                    hydratedSalt.SetActive(false);
                    deHydratedSalt.SetActive(true);
                    break;
            }
            state = value;
        }
    }

    [SerializeField]
    private float minBreakForce;

    [SerializeField]
    private float breakingTemp;

    [SerializeField]
    private float deHydrationTemp;

    [SerializeField]
    private GameObject hydratedSalt;

    [SerializeField]
    private GameObject deHydratedSalt;

    public float tempToAdd;

    public void Hold(Transform holdPos)
    {
        beingHeld = true;
        this.holdPos = holdPos;
    }

    public void Drop()
    {
        beingHeld = false;
    }

    private bool isBroken;

    public bool IsBroken
    {
        get
        {
            return isBroken;
        } 
    }

	// Use this for initialization
	void Start () {
        Reset();
	}
	
	// Update is called once per frame
	void Update () {
		if (beingHeld)
        {
            gameObject.transform.position = holdPos.position;
            gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
        }

        if (temperature > 23)
        {
            temperature -= tempLoss;
        }

        if (temperature <= 30)
        {
            gameObject.transform.Find("crucible").gameObject.GetComponent<Renderer>().material.SetColor("_Color", new Color(1, 1, 229.0f / 255.0f, 1));
        }
        else
        {
            tintColor = new Color(1, (178.0f - (110.0f * (temperature/breakingTemp))) / 255.0f, (178.0f - (110.0f * (temperature / breakingTemp))) / 255.0f, 1);

            gameObject.transform.Find("crucible").gameObject.GetComponent<Renderer>().material.SetColor("_Color", tintColor);
        }

        if (temperature >= deHydrationTemp)
        {
            State = CrucibleState.DeHydrated;
        }

        if (temperature >= breakingTemp)
        {
            isBroken = true;
        }
	}

    private void OnCollisionEnter(Collision collision)
    {
        beingHeld = false;
        if (holdPos != null)
        {
            holdPos.gameObject.GetComponentInParent<Tongs>().state = Tongs.TongsState.Tongs_Open;
        }
    }

    public void Reset()
    {
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
        gameObject.transform.position = startingPos;
        State = CrucibleState.Empty;
        beingHeld = false;
        isBroken = false;
        temperature = 23;
    }

    public void Heat(float increaseBy)
    {
        temperature += increaseBy;
    }

    public void FillCrucible()
    {
        State = CrucibleState.Hydrated;
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.GetComponentInParent<BunsenBurner>())
        {
            if (other.GetComponentInParent<BunsenBurner>().OuterFlameOn)
            {
                if (other.tag == "OuterFlame")
                {
                    temperature += tempToAdd * 2;
                }
            }
            else
            {
                if (other.tag == "InnerFlame")
                {
                    temperature += tempToAdd;
                }
            }
        }        
    }
}
