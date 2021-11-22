using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GateInformation : MonoBehaviour
{
    Camera camera;

    TMP_Text[] gateNames;
    public float offsetValue;
    public RectTransform truthTableContainer;
    public GameObject cellPrefab;
    AbstractGate activeGate;

    void Awake()
    {
        gateNames = GetComponentsInChildren<TMP_Text>();
        activeGate = FindObjectOfType<AbstractGate>();
    }

    void Start()
    {
        camera = Camera.main;
        UpdatePosition(false);

        TruthTableRow[] truthTable = activeGate.GenerateTruthTable();
        // Gets the entire Row length
        SetGridLayoutConstraintCount(truthTable[0].Inputs.Length + truthTable[0].Outputs.Length);
        for (int i = 0; i < truthTable.Length; i++)
        {
            foreach (bool value in truthTable[i].Inputs)
            {
               SetTruthtableCells(value); 
               Instantiate(cellPrefab, truthTableContainer.transform);
            }
            
            foreach (bool value in truthTable[i].Outputs)
            {
               SetTruthtableCells(value); 
               Instantiate(cellPrefab, truthTableContainer.transform);
            }
        }
    }

    void Update() => UpdatePosition(true);

    void OnEnable() => Update();

    public void ShowGateInformation(bool show, string gateName)
    {
        gameObject.SetActive(show);
        SetGateName(gateName);
    }


    // Sets the tables heading
    void SetGateName(string gateName) => gateNames[1].SetText(gateName);

    void SetTruthtableCells(bool value)
    {
        var cellValue = value ? "1" : "0";
        cellPrefab.GetComponentInChildren<TMP_Text>().SetText(cellValue);
    }

    void SetGridLayoutConstraintCount(int size)
    {
        truthTableContainer.GetComponent<GridLayoutGroup>().constraintCount = size;
    }

    void UpdatePosition(bool lerpPosition)
    {
        Vector3 offset =
            Vector3.Cross(camera.transform.right, (camera.transform.position - transform.position)).normalized *
            offsetValue;
        transform.position = lerpPosition
            ? Vector3.Lerp(transform.position, transform.parent.position + offset, 0.5f)
            : transform.parent.position + offset;
        transform.LookAt(camera.transform.position);
    }
}