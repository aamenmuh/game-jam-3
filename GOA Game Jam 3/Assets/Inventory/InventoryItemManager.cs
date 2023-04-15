using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryItemManager : MonoBehaviour
{
    public int index;
    public bool isSelected = false;
    //private RectTransform selector;
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
        //selector = GameObject.FindWithTag("UISelector").GetComponent<RectTransform>();
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
        //selector.gameObject.GetComponent<Image>().enabled = true;
        //selector.position = this.GetComponent<RectTransform>().position;
        button.color = selectedColor;
        //manager.SelectItem(index);
        isSelected = true;
    }

    public void Deselect()
    {
        //selector.gameObject.GetComponent<Image>().enabled = false;
        button.color = unselectedColor;
        //manager.SelectItem(-1);
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
        icon.enabled = false;
        text.enabled = false;
        selectable = false;
        Deselect();
        Destroy(this.gameObject);
    }
}
