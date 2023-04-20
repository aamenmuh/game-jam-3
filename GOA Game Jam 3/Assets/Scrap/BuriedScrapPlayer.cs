using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuriedScrapPlayer : MonoBehaviour
{
    public KeyCode digKey;
    public float radius;
    public LayerMask buriedLayer;

    // Update is called once per frame
    void Update()
    {
        MyInput();
    }

    void MyInput()
    {
        if (Input.GetKeyDown(digKey)) StartDig();
    }

    void StartDig()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius, buriedLayer);

        foreach(Collider2D col in colliders)
        {
            //Animation should be placed here, will use animation event
            //The following is for debug purposes
            GameObject currentScrap = col.gameObject;
            Dig(currentScrap);
        }
    }

    void Dig(GameObject scrap)
    {
        BuriedScrap buried = scrap.GetComponent<BuriedScrap>();
        buried.GenerateScrap();
        buried.AddToInventory();
        Destroy(scrap);
    }
}
