using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Grid Tools")]
    public int _gridCount;
    public int _blockUnit;

    private GameObject _gridCanvas;
    private Image _gridImage;
    private GameObject _cube;
    private GameObject _cubeTwo;

    private int _row;
    private int _column;
    private Vector3 _blockpos;
    [Header("Rows and Columns")]
    public List<GameObject> Row1 = new List<GameObject>();
    public List<GameObject> Row2 = new List<GameObject>();
    public List<GameObject> Row3 = new List<GameObject>();
    public List<GameObject> Row4 = new List<GameObject>();
    public List<GameObject> Row5 = new List<GameObject>();
    public List<GameObject> Row1Reverse = new List<GameObject>();
    public List<GameObject> Row2Reverse = new List<GameObject>();
    public List<GameObject> Row3Reverse = new List<GameObject>();
    public List<GameObject> Row4Reverse = new List<GameObject>();
    public List<GameObject> Row5Reverse = new List<GameObject>();
    public List<GameObject> Column1 = new List<GameObject>();
    public List<GameObject> Column2 = new List<GameObject>();
    public List<GameObject> Column3 = new List<GameObject>();
    public List<GameObject> Column4 = new List<GameObject>();
    public List<GameObject> Column5 = new List<GameObject>();
    public List<GameObject> Column1reverse = new List<GameObject>();
    public List<GameObject> Column2reverse = new List<GameObject>();
    public List<GameObject> Column3reverse = new List<GameObject>();
    public List<GameObject> Column4reverse = new List<GameObject>();
    public List<GameObject> Column5reverse = new List<GameObject>();

    void Awake()
    {
        instance = this;
        _gridCanvas = GameObject.Find("GridCanvas");
        _gridCanvas.GetComponent<GridLayoutGroup>().constraintCount = _gridCount;
        _gridImage = Resources.Load<Image>("Image");
        _cube = Resources.Load<GameObject>("Cube");
        _cubeTwo = Resources.Load<GameObject>("CubeTwo");
        CreateGrid();
    }

    void Update()
    {
        #region Create Cubes
        if (Input.GetMouseButtonUp(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (_blockUnit == 2) _blockpos = new Vector3(hit.transform.position.x, hit.transform.position.y, hit.transform.position.z + 0.5f);
                if (_blockUnit == 3) _blockpos = new Vector3(hit.transform.position.x, hit.transform.position.y, hit.transform.position.z);
                if (_blockUnit == 4) _blockpos = new Vector3(hit.transform.position.x, hit.transform.position.y, hit.transform.position.z - 0.5f);

                if (hit.transform.CompareTag("LeftGrid") && hit.transform.GetComponent<GridController>()._isUse)
                {
                    if (_blockUnit == 2) _blockpos = new Vector3(hit.transform.position.x - 0.5f, hit.transform.position.y, hit.transform.position.z);
                    if (_blockUnit == 3) _blockpos = new Vector3(hit.transform.position.x - 1f, hit.transform.position.y, hit.transform.position.z);
                    if (_blockUnit == 4) _blockpos = new Vector3(hit.transform.position.x - 1.5f, hit.transform.position.y, hit.transform.position.z);

                    if (hit.transform.GetComponent<GridController>()._queueCount >= _blockUnit)
                    {
                        var Block = Instantiate(_cubeTwo, _blockpos, Quaternion.identity);
                        StartCoroutine(SetPositionBlockLeft(Block, hit.transform.GetComponent<GridController>()._queueCount));
                        SetDestroyGridLastChild(_blockUnit, hit.transform.gameObject);
                    }
                }

                if (hit.transform.CompareTag("RightGrid") && hit.transform.GetComponent<GridController>()._isUse)
                {
                    if (_blockUnit == 2) _blockpos = new Vector3(hit.transform.position.x + 0.5f, hit.transform.position.y, hit.transform.position.z);
                    if (_blockUnit == 3) _blockpos = new Vector3(hit.transform.position.x + 1f, hit.transform.position.y, hit.transform.position.z);
                    if (_blockUnit == 4) _blockpos = new Vector3(hit.transform.position.x + 1.5f, hit.transform.position.y, hit.transform.position.z);

                    if (hit.transform.GetComponent<GridController>()._queueCount >= _blockUnit)
                    {
                        var Block = Instantiate(_cubeTwo, _blockpos, Quaternion.identity);
                        StartCoroutine(SetPositionBlockRight(Block, hit.transform.GetComponent<GridController>()._queueCount));
                        SetDestroyGridLastChild(_blockUnit, hit.transform.gameObject);
                    }
                }

                if (hit.transform.CompareTag("DownGrid") && hit.transform.GetComponent<GridController>()._isUse)
                {
                    if (_blockUnit == 2) _blockpos = new Vector3(hit.transform.position.x, hit.transform.position.y, hit.transform.position.z + 0.5f);
                    if (_blockUnit == 3) _blockpos = new Vector3(hit.transform.position.x, hit.transform.position.y, hit.transform.position.z);
                    if (_blockUnit == 4) _blockpos = new Vector3(hit.transform.position.x, hit.transform.position.y, hit.transform.position.z - 0.5f);

                    if (hit.transform.GetComponent<GridController>()._queueCount >= _blockUnit)
                    {          
                        var Block = Instantiate(_cubeTwo, _blockpos, Quaternion.Euler(0,90,0));
                        StartCoroutine(SetPositionBlockDown(Block, hit.transform.GetComponent<GridController>()._queueCount));
                        SetDestroyGridLastChild(_blockUnit, hit.transform.gameObject);
                    }
                }

                if (hit.transform.CompareTag("UpGrid") && hit.transform.GetComponent<GridController>()._isUse)
                {
                    if (_blockUnit == 2) _blockpos = new Vector3(hit.transform.position.x, hit.transform.position.y, hit.transform.position.z + 0.5f);
                    if (_blockUnit == 3) _blockpos = new Vector3(hit.transform.position.x, hit.transform.position.y, hit.transform.position.z + 1f);
                    if (_blockUnit == 4) _blockpos = new Vector3(hit.transform.position.x, hit.transform.position.y, hit.transform.position.z  + 1.5f);

                    if (hit.transform.GetComponent<GridController>()._queueCount >= _blockUnit)
                    {
                        var Block = Instantiate(_cubeTwo, _blockpos, Quaternion.Euler(0,90,0));
                        StartCoroutine(SetPositionBlockUp(Block, hit.transform.GetComponent<GridController>()._queueCount));
                        SetDestroyGridLastChild(_blockUnit, hit.transform.gameObject);
                    }
                }

            }
        }
        #endregion
    }

    // creating the locations of the cubes at the beginning of the game
    void CreateGrid()
    {
        for (int i = 0; i < _gridCount; i++)
        {
            for (int a = 0; a < _gridCount; a++)
            {
                var gridField = Instantiate(_gridImage);
                gridField.transform.parent = _gridCanvas.transform;
                gridField.transform.localRotation = Quaternion.Euler(0, 0, 0);
                gridField.transform.name = _row + " Grid " + _column;
                SetRowAndColumn(gridField.gameObject);
                _column++;
            }
            _row++;
            _column = 0;
        }
    }

    //destroying the places where the formed cubes settle
    void SetDestroyGridLastChild(int unit, GameObject hitCube)
    {
        if (unit == 2)
        {
            hitCube.GetComponent<GridController>().myCheck[hitCube.GetComponent<GridController>()._queueCount - 1].GetComponent<CubeController>()._isMotion = false;
            hitCube.GetComponent<GridController>().myCheck.RemoveAt(hitCube.GetComponent<GridController>()._queueCount - 1);

            hitCube.GetComponent<GridController>().myCheck[hitCube.GetComponent<GridController>()._queueCount - 2].GetComponent<CubeController>()._isMotion = false;
            hitCube.GetComponent<GridController>().myCheck.RemoveAt(hitCube.GetComponent<GridController>()._queueCount - 2);
        }

        if (unit == 3)
        {
            hitCube.GetComponent<GridController>().myCheck[hitCube.GetComponent<GridController>()._queueCount - 1].GetComponent<CubeController>()._isMotion = false;
            hitCube.GetComponent<GridController>().myCheck.RemoveAt(hitCube.GetComponent<GridController>()._queueCount - 1);

            hitCube.GetComponent<GridController>().myCheck[hitCube.GetComponent<GridController>()._queueCount - 2].GetComponent<CubeController>()._isMotion = false;
            hitCube.GetComponent<GridController>().myCheck.RemoveAt(hitCube.GetComponent<GridController>()._queueCount - 2);

            hitCube.GetComponent<GridController>().myCheck[hitCube.GetComponent<GridController>()._queueCount - 3].GetComponent<CubeController>()._isMotion = false;
            hitCube.GetComponent<GridController>().myCheck.RemoveAt(hitCube.GetComponent<GridController>()._queueCount - 3);
        }

        if (unit == 4)
        {
            hitCube.GetComponent<GridController>().myCheck[hitCube.GetComponent<GridController>()._queueCount - 1].GetComponent<CubeController>()._isMotion = false;
            hitCube.GetComponent<GridController>().myCheck.RemoveAt(hitCube.GetComponent<GridController>()._queueCount - 1);

            hitCube.GetComponent<GridController>().myCheck[hitCube.GetComponent<GridController>()._queueCount - 2].GetComponent<CubeController>()._isMotion = false;
            hitCube.GetComponent<GridController>().myCheck.RemoveAt(hitCube.GetComponent<GridController>()._queueCount - 2);

            hitCube.GetComponent<GridController>().myCheck[hitCube.GetComponent<GridController>()._queueCount - 3].GetComponent<CubeController>()._isMotion = false;
            hitCube.GetComponent<GridController>().myCheck.RemoveAt(hitCube.GetComponent<GridController>()._queueCount - 3);

            hitCube.GetComponent<GridController>().myCheck[hitCube.GetComponent<GridController>()._queueCount - 4].GetComponent<CubeController>()._isMotion = false;
            hitCube.GetComponent<GridController>().myCheck.RemoveAt(hitCube.GetComponent<GridController>()._queueCount - 4);
        }

    }

    #region Set Button units
    public void SetTwoButton()
    {
        _cubeTwo = Resources.Load<GameObject>("CubeTwo");
        _blockUnit = 2;
    }

    public void SetThreeButton()
    {
        _cubeTwo = Resources.Load<GameObject>("CubeThree");
        _blockUnit = 3;
    }

    public void SetFourButton()
    {
        _cubeTwo = Resources.Load<GameObject>("CubeFour");
        _blockUnit = 4;
    }
    #endregion

    # region cubes move
    IEnumerator SetPositionBlockRight(GameObject SelectionBlock, int Queue)
    {
        for (int i = 0; i < Queue; i++)
        {
            yield return new WaitForSeconds(0.2f);
            SelectionBlock.transform.Translate(Vector3.left * 1f);
        }
    }

    IEnumerator SetPositionBlockLeft(GameObject SelectionBlock, int Queue)
    {
        for (int i = 0; i < Queue; i++)
        {
            yield return new WaitForSeconds(0.2f);
            SelectionBlock.transform.Translate(Vector3.right * 1f);
        }
    }

    IEnumerator SetPositionBlockDown(GameObject SelectionBlock, int Queue)
    {
        for (int i = 0; i < Queue - 1; i++)
        {
            yield return new WaitForSeconds(0.2f);

           SelectionBlock.transform.position = new Vector3(SelectionBlock.transform.position.x, SelectionBlock.transform.position.y, SelectionBlock.transform.position.z + 1f);
            
        }
    }

    IEnumerator SetPositionBlockUp(GameObject SelectionBlock, int Queue)
    {
        for (int i = 0; i < Queue; i++)
        {
            yield return new WaitForSeconds(0.2f);
            SelectionBlock.transform.Translate(Vector3.right * 1f);
        }
    }
    #endregion

    // cubes add their own fields
    void SetRowAndColumn(GameObject gridField)
    {
        if (_row == 0)
        {
            Row1.Add(gridField.gameObject);
            Row1Reverse.Add(gridField.gameObject);
        }
        if (_row == 1) { 
            Row2.Add(gridField.gameObject);
            Row2Reverse.Add(gridField.gameObject);
        }
        if (_row == 2)
        {
            Row3.Add(gridField.gameObject);
            Row3Reverse.Add(gridField.gameObject);
        }
        if (_row == 3)
        {
            Row4.Add(gridField.gameObject);
            Row4Reverse.Add(gridField.gameObject);
        }
        if (_row == 4)
        {
            Row5.Add(gridField.gameObject);
            Row5Reverse.Add(gridField.gameObject);
        }


        if (_column == 0)
        {
            Column1.Add(gridField.gameObject);
            Column1reverse.Add(gridField.gameObject);
        }
        if (_column == 1)
        {
            Column2.Add(gridField.gameObject);
            Column2reverse.Add(gridField.gameObject);
        }
        if (_column == 2)
        {
            Column3.Add(gridField.gameObject);
            Column3reverse.Add(gridField.gameObject);
        }
        if (_column == 3)
        {
            Column4.Add(gridField.gameObject);
            Column4reverse.Add(gridField.gameObject);
        }
        if (_column == 4)
        {
            Column5.Add(gridField.gameObject);
            Column5reverse.Add(gridField.gameObject);
        }

    }
}
