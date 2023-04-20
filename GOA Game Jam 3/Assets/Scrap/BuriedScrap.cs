using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuriedScrap : MonoBehaviour
{
    public GameObject scrapPrefab, VisibleScrap;

    GameObject currentScrap;
    ScrapData data;

    public void GenerateScrap()
    {
        currentScrap = Instantiate(scrapPrefab);
        data = currentScrap.GetComponent<ScrapData>();
        data.Randomize();
    }

    public void AddToInventory()
    {
        InventoryManager manager = GameObject.FindWithTag("InventoryManager").GetComponent<InventoryManager>();
        if (manager.Add(currentScrap)) Destroy(this.currentScrap);
        else
        {
            GameObject obj = Instantiate(VisibleScrap, GameObject.Find("Player").transform.position, new Quaternion(0f, 0f, 0f, 0f));
            ScrapData odata = obj.GetComponent<ScrapData>();
            odata.Copy(data); //the data variable here should be a reference to the scrapdata component //of this piece of scrap
            obj.GetComponent<VisibleScrapBehavior>().Drop();
        }
    }
}
