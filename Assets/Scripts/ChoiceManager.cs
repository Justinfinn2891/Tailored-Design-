using UnityEngine;
using Dummiesman;
using SimpleFileBrowser;
public class ChoiceManager : MonoBehaviour
{
    public GameObject[] buttons;
    public Material fallbackMaterial; // Assign this in the Inspector
    public GameObject GridSystem;
    public Vector3 gridPos = new Vector3(0, 0, 0);

    public void CreateButton()
    {
        Instantiate(GridSystem, gridPos, Quaternion.identity);
        buttons[1].SetActive(true);
        Destroy(buttons[0]);
        Destroy(buttons[2]);
    }
    
    public void ImportButton(){
        OpenFileExplorer();
    }

    public void OpenFileExplorer()
    {
        FileBrowser.SetFilters(true, new FileBrowser.Filter("OBJ Files", ".obj"));
        FileBrowser.ShowLoadDialog(OnFileSelected, null, FileBrowser.PickMode.Files, false, null, null, "Select OBJ File", "Select");
    }
 
    void OnFileSelected(string[] paths)
    {
        if (paths.Length > 0)
        {
            Debug.Log("Selected file: " + paths[0]);
            LoadOBJ(paths[0]);
        }
    }
 
void LoadOBJ(string path)
{
    GameObject obj = new OBJLoader().Load(path);
    
    Vector3 grid = new Vector3(
        GridSystem.transform.position.x,
        1,
        GridSystem.transform.position.z
    );
    
    obj.transform.position = grid;


    float gridScale = 10f; 
    ScaleToFit(obj, gridScale);


    Instantiate(GridSystem, gridPos, Quaternion.identity);


    ApplyMaterialToAllRenderers(obj);


    buttons[2].SetActive(true);
    Destroy(buttons[0]);
    Destroy(buttons[1]);
}
 
    void ApplyMaterialToAllRenderers(GameObject obj)
    {
        foreach (var renderer in obj.GetComponentsInChildren<Renderer>())
        {
            renderer.material = fallbackMaterial;
        }
    }
    void ScaleToFit(GameObject obj, float targetSize)
    {
        Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();
        if (renderers.Length == 0) return;

        Bounds bounds = renderers[0].bounds;
        foreach (Renderer rend in renderers)
        {
            bounds.Encapsulate(rend.bounds);
        }

        float maxSize = Mathf.Max(bounds.size.x, bounds.size.y, bounds.size.z);
        if (maxSize == 0) return;

        float scaleFactor = targetSize / maxSize;
        obj.transform.localScale *= scaleFactor;
    }
}