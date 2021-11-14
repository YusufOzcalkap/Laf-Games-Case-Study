using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public static GridController instance;

    [Header("Ýf row Grid")]
    public int _queueRow;
    [Header("Ýf Column Grid")]
    public int _queueColumn;
    [Header("normal 1 reverse 2")]
    public int _queueReverse;
    public int _queueCount;

    public List<GameObject> myCheck = new List<GameObject>();
    public bool _isUse;

    void Start()
    {
        instance = this;
        CheckGrids();
        if (_queueReverse == 2) myCheck.Reverse();
        
    }

    void Update()
    {
        SetQueueReverse();
    }

    // cubes add their own fields
    void CheckGrids()
    {
        if (_queueRow == 1) myCheck = GameManager.instance.Row1;
        if (_queueRow == 2) myCheck = GameManager.instance.Row2;
        if (_queueRow == 3) myCheck = GameManager.instance.Row3;
        if (_queueRow == 4) myCheck = GameManager.instance.Row4;
        if (_queueRow == 5) myCheck = GameManager.instance.Row5;
        if (_queueRow == 1 && _queueReverse == 2) myCheck = GameManager.instance.Row1Reverse;
        if (_queueRow == 2 && _queueReverse == 2) myCheck = GameManager.instance.Row2Reverse;
        if (_queueRow == 3 && _queueReverse == 2) myCheck = GameManager.instance.Row3Reverse;
        if (_queueRow == 4 && _queueReverse == 2) myCheck = GameManager.instance.Row4Reverse;
        if (_queueRow == 5 && _queueReverse == 2) myCheck = GameManager.instance.Row5Reverse;

        if (_queueColumn == 1) myCheck = GameManager.instance.Column1;
        if (_queueColumn == 2) myCheck = GameManager.instance.Column2;
        if (_queueColumn == 3) myCheck = GameManager.instance.Column3;
        if (_queueColumn == 4) myCheck = GameManager.instance.Column4;
        if (_queueColumn == 5) myCheck = GameManager.instance.Column5;
        if (_queueColumn == 1 && _queueReverse == 2) myCheck = GameManager.instance.Column1reverse;
        if (_queueColumn == 2 && _queueReverse == 2) myCheck = GameManager.instance.Column2reverse;
        if (_queueColumn == 3 && _queueReverse == 2) myCheck = GameManager.instance.Column3reverse;
        if (_queueColumn == 4 && _queueReverse == 2) myCheck = GameManager.instance.Column4reverse;
        if (_queueColumn == 5 && _queueReverse == 2) myCheck = GameManager.instance.Column5reverse;
    }

    void SetQueueReverse()
    {
        if (_queueReverse == 1 || _queueReverse == 2)
        {
            if (myCheck[0].GetComponent<CubeController>()._isMotion) _isUse = true; else _isUse = false;

            if (myCheck[0].GetComponent<CubeController>()._isMotion) _queueCount = 1;
            if (myCheck[0].GetComponent<CubeController>()._isMotion && myCheck[1].GetComponent<CubeController>()._isMotion) _queueCount = 2;
            if (myCheck[0].GetComponent<CubeController>()._isMotion && myCheck[1].GetComponent<CubeController>()._isMotion && myCheck[2].GetComponent<CubeController>()._isMotion) _queueCount = 3;
            if (myCheck[0].GetComponent<CubeController>()._isMotion && myCheck[1].GetComponent<CubeController>()._isMotion && myCheck[2].GetComponent<CubeController>()._isMotion
                && myCheck[3].GetComponent<CubeController>()._isMotion) _queueCount = 4;
            if (myCheck[0].GetComponent<CubeController>()._isMotion && myCheck[1].GetComponent<CubeController>()._isMotion && myCheck[2].GetComponent<CubeController>()._isMotion
                && myCheck[3].GetComponent<CubeController>()._isMotion && myCheck[4].GetComponent<CubeController>()._isMotion) _queueCount = 5;
        }
    }
}
