using UnityEngine;
using UnityEngine.UI;
public class GridPlacer : MonoBehaviour
{
    public GameObject objectToPlace;
    public GridManager gridManager; // assign in Inspector
    public LayerMask groundLayer;
    public GameObject[] objectDifferentation;
    private int colorIndex = 0;
    private int objectSlots = 4; 
    int i = 0;
    bool buildingMode = true;
    bool isDragging = false;
    private GameObject itemClicked;

    public GameObject[] Images;
    public GameObject buildMode;
    public GameObject moveMode;
    private void Start()
    {
        objectToPlace = objectDifferentation[i];
        Images[0].GetComponent<Image>().color = Color.grey;
    }
    void Update()
    {
        // Right click: cycle the color index for next placed object
        if (buildingMode)
        {
            buildMode.SetActive(true);
            moveMode.SetActive(false);
            if (Input.GetKeyDown(KeyCode.F))
            {
                i++;
                for(int f = 0; f <= objectSlots; f++)
                {
                    if (f== i)
                    {
                        Images[f].GetComponent<Image>().color = Color.grey;
                    }
                    else
                    {
                        Images[f].GetComponent<Image>().color = Color.white;
                    }
                }
                objectToPlace = objectDifferentation[i];

                if (i == objectSlots) { i = -1; }
            }

            // Left click: place the object on grid and set color
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, 100f, groundLayer))
                {
                    Vector3 gridPos = gridManager.GetNearestPointOnGrid(hit.point);

                    // Instantiate the object at the snapped grid position
                    Instantiate(objectToPlace, gridPos, Quaternion.identity);


                }
            }

            if (Input.GetKeyDown(KeyCode.Q)) { buildingMode = false; }

        }
        else
        {
            buildMode.SetActive(false);
            moveMode.SetActive(true);
            if (Input.GetMouseButton(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, 100f))
                {
                    isDragging = true;
                    itemClicked = hit.collider.gameObject;

                    if (itemClicked.CompareTag("PlacedObject")) 
                    {
                        Renderer rendClick = itemClicked.GetComponentInChildren<Renderer>();

                        Vector3 gridPos = gridManager.GetNearestPointOnGrid(hit.point);
                        itemClicked.transform.position = gridPos;

                        if (rendClick != null)
                            rendClick.material.color = Color.red;
                    }

                    if (Input.GetKeyDown(KeyCode.G) && itemClicked.CompareTag("PlacedObject"))
                    {
                        Destroy(itemClicked);
                    }

                    if (Input.GetMouseButtonDown(1))
                    {
                        itemClicked.transform.Rotate(0, 90, 0);
                    }
                }
            }
            if(Input.GetMouseButtonUp(0)) { isDragging = false; }
            if (!isDragging)
            {
                Renderer rend = itemClicked.GetComponentInChildren<Renderer>();

                if (rend != null)
                    rend.material.color = Color.white;
            }
            if (Input.GetKeyDown(KeyCode.Q)) { buildingMode = true; }

        }
    }


}