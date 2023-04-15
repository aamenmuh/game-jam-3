using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public List<GameObject> inventoryItems;
    public GameObject itemPrefab;
    //public GameObject selector;
    public TextMeshProUGUI description;
    public int selected = -1;
    public int inventoryLimit = 6;

    void Start()
    {
        inventoryItems = new List<GameObject>();
        InitializeInventory();
    }
    
    void Update()
    {

    }

    void InitializeInventory()
    {
        ClearInventory();
        for(int i = 0; i < inventoryLimit; i++)
        {
            GameObject obj = Instantiate(itemPrefab, transform);
            obj.GetComponent<ScrapData>().Randomize();
            obj.GetComponent<InventoryItemManager>().index = i;
            obj.GetComponent<InventoryItemManager>().Setup();
            inventoryItems.Add(obj);
        }
    }

    public void SelectItem(int index)
    {
        if (selected != -1) inventoryItems[selected].GetComponent<InventoryItemManager>().Deselect();
        selected = index;
        if (index != -1)
        {
            inventoryItems[index].GetComponent<InventoryItemManager>().Select();
            description.text = inventoryItems[index].GetComponent<ScrapData>().Description();
        }
        else
        {
            description.text = "";
        }
    }

    void ClearInventory()
    {
        foreach(GameObject go in inventoryItems)
        {
            Destroy(go);
        }
    }

    public void Drop()
    {
        if (selected == -1) return;
        int temp = selected;
        inventoryItems[selected].GetComponent<InventoryItemManager>().Drop();
        selected = -1;
        for (int i = temp + 1; i < inventoryItems.Count; i++)
        {
                inventoryItems[i].GetComponent<InventoryItemManager>().index -= 1;
                inventoryItems[i - 1] = inventoryItems[i];
                inventoryItems[i] = null;
        }
        inventoryItems.RemoveAt(inventoryItems.Count - 1);
        SelectItem(-1);
    }

 
        
    
    public bool Add(GameObject go)
    {
        ScrapData data;
        if (go.GetComponent<ScrapData>() == null) return false;
        if (inventoryItems.Count == inventoryLimit) return false;
        GameObject obj = Instantiate(itemPrefab, transform);
        obj.GetComponent<ScrapData>().Copy(go.GetComponent<ScrapData>());
        obj.GetComponent<InventoryItemManager>().index = inventoryItems.Count;
        obj.GetComponent<InventoryItemManager>().Setup();
        inventoryItems.Add(obj);
        return true;
    }

}
