using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryOpener : MonoBehaviour
{
    public InventoryManager manager;
    private void Update()
    {
        if (Input.GetButtonDown("ToggleInventory"))
        {
            manager.SelectItem(-1);
            transform.GetChild(0).gameObject.SetActive(!transform.GetChild(0).gameObject.activeInHierarchy);
        }
    }
}
