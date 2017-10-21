using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour {
    public float zReg;
    public float zBack;

    public float pos1X;

    public float pos2X;


    private Vector3 pos1_Reg;
    private Vector3 pos1_Back;
    private Vector3 pos2_Reg;
    private Vector3 pos2_Back;

    private float elapsedTime;

    private enum TablePosState
    {
        tablePos1,
        tablePos2,
        tablePos1To2,
        tablePos2To1
    }

    private TablePosState posState;

    public float posMoveTime;

    private bool movedBack;
	// Use this for initialization
	void Start () {
        movedBack = false;
        elapsedTime = 0;
        posState = TablePosState.tablePos1;

        pos1_Reg = new Vector3(pos1X, -1, zReg);
        pos1_Back = new Vector3(pos1X, -1, zBack);

        pos2_Reg = new Vector3(pos2X, -1, zReg);
        pos2_Back = new Vector3(pos2X, -1, zBack);
	}
	
	// Update is called once per frame
	void Update () {
		switch (posState)
        {
            case TablePosState.tablePos1To2:
                gameObject.transform.position = Vector3.Lerp(pos1_Back, pos2_Back, elapsedTime / posMoveTime);
                elapsedTime += Time.deltaTime;
                if (elapsedTime >= posMoveTime)
                {
                    gameObject.transform.position = pos2_Reg;
                    posState = TablePosState.tablePos2;
                }
            break;
            case TablePosState.tablePos2To1:
                gameObject.transform.position = Vector3.Lerp(pos2_Back, pos1_Back, elapsedTime / posMoveTime);
                elapsedTime += Time.deltaTime;
                if (elapsedTime >= posMoveTime)
                {
                    gameObject.transform.position = pos1_Reg;
                    posState = TablePosState.tablePos1;
                }
                break;
        }
	}

    private void GoToPos2()
    {
        gameObject.transform.position = pos1_Back;
        elapsedTime = 0;
        posState = TablePosState.tablePos1To2;
    }

    private void GoToPos1()
    {
        gameObject.transform.position = pos2_Back;
        elapsedTime = 0;
        posState = TablePosState.tablePos2To1;
    }

    public void SwitchTablePos()
    {
        if (posState == TablePosState.tablePos1)
        {
            GoToPos2();
        }
        else if (posState == TablePosState.tablePos2)
        {
            GoToPos1();
        }
    }
}
