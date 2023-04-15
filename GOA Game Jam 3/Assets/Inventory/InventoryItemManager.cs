using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryItemManager : MonoBehaviour
{
    public int index;
    public bool isSelected = false;
    private InventoryManager manager;

    public Image icon;
    public Image button;
    public TextMeshProUGUI text;
    private ScrapData data;

    public GameObject visScrap;

    private Transform player;

    public bool selectable;

    public Color selectedColor;
    private Color unselectedColor;
    

    private void Awake()
    {
        data = gameObject.GetComponent<ScrapData>();
    }

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        manager = transform.parent.GetComponent<InventoryManager>();
        unselectedColor = button.color;
    }

    private void Update()
    {
        
    }
    public void OnClick()
    {
        if (selectable)
        {
            if (!isSelected)
            {
                manager.SelectItem(index);
            }
            else
            {
                manager.SelectItem(-1);
            }
        }
    }

    public void Select()
    {
        button.color = selectedColor;
        isSelected = true;
    }

    public void Deselect()
    {
        button.color = unselectedColor;
        isSelected = false;
    }

    public void Setup()
    {
        data = GetComponent<ScrapData>();
        
        if (data != null)
        {
            icon.enabled = true;
            text.enabled = true;
            selectable = true;
            icon.sprite = data.sprite;
            text.text = data.Name();
        }
        else
        {
            icon.enabled = false;
            text.enabled = false;
            selectable = false;
        }
    }

    public void Drop()
    {
        GameObject obj = Instantiate(visScrap, player.position, new Quaternion(0f,0f,0f,0f));
        ScrapData odata = obj.GetComponent<ScrapData>();
        odata.Copy(data);
        obj.GetComponent<VisibleScrapBehavior>().Drop();
        icon.enabled = false;
        text.enabled = false;
        selectable = false;
        Deselect();
        Destroy(this.gameObject);
    }
}
