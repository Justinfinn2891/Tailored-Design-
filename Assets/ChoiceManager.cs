using UnityEngine;
using Dummiesman;
using SimpleFileBrowser;
public class ChoiceManager : MonoBehaviour
{
    public GameObject[] buttons;
    public Material fallbackMaterial; // Assign this in the Inspector
    public GameObject GridSystem;
    public Vector3 gridPos = new Vector3(2, 2, 2);

    public void CreateButton()
    {
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
        buttons[2].SetActive(true);
        Destroy(buttons[0]);
        Destroy(buttons[1]);
        obj.transform.position = Vector3.zero; // or anywhere in your scene
        Instantiate(GridSystem, gridPos, Quaternion.identity);
        ApplyMaterialToAllRenderers(obj);
    }
 
    void ApplyMaterialToAllRenderers(GameObject obj)
    {
        foreach (var renderer in obj.GetComponentsInChildren<Renderer>())
        {
            renderer.material = fallbackMaterial;
        }
    }
 
}