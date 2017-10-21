using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunsenBurner : MonoBehaviour {
    public GameObject intakeRing;
    public float minIntakeRingRot;
    public float maxIntakeRingRot;

    [SerializeField]
    private ParticleSystem flameCore;

    [SerializeField]
    private ParticleSystem flameOuter;

    public float baseLifeTimeCore;
    public float baseLifeTimeOuter;

    public float coreLifeTimeAdd;
    public float outerLifeTimeAdd;

    public float outerRenderCutOff;

    //air intake from 0 to 1
    private float airIntakeRate;

    private bool outerRenderOn;

    public bool OuterFlameOn
    {
        get
        {
            return flameOuter.isPlaying;
        }
    }

    public float AirIntakeRate
    {
        get
        {
            return airIntakeRate;
        }
        set
        {
            if (value >= 0 && value <= 1)
            {
                airIntakeRate = value;
            }
        }
    }

	// Use this for initialization
	void Start () {
        airIntakeRate = 0;
        intakeRing.transform.localRotation = new Quaternion(0, minIntakeRingRot, 0, 0);
        flameCore.Stop();
        flameOuter.Stop();
	}
	
	// Update is called once per frame
	void Update () {
        //intakeRing.transform.localRotation = Quaternion.Lerp(new Quaternion(0, minIntakeRingRot, 0, 0), new Quaternion(0, maxIntakeRingRot, 0, 0), airIntakeRate);
        intakeRing.transform.eulerAngles = new Vector3(0, minIntakeRingRot + airIntakeRate * maxIntakeRingRot, 0);
        ParticleSystem.MainModule mod = flameCore.main;
        mod.startLifetime = baseLifeTimeCore + airIntakeRate * coreLifeTimeAdd;

        flameOuter.gameObject.SetActive(airIntakeRate >= outerRenderCutOff);
        mod = flameOuter.main;
        mod.startLifetime = baseLifeTimeOuter + airIntakeRate * outerLifeTimeAdd;
	}

    public void IgniteBurner()
    {
        flameCore.Play();
        flameOuter.Play();
    }

    public void TurnOffBurner()
    {
        flameCore.Stop();
        flameOuter.Stop();
    }

    public void IncreaseIntakeRate(float increaseBy)
    {
        AirIntakeRate += increaseBy;
    }

    public void DecreaseIntakeRate(float decreaseBy)
    {
        AirIntakeRate -= decreaseBy;
    }
}
