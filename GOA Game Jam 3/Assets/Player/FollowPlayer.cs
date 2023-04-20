using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    public float xFollow, yFollow, zFollow;

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + new Vector3(xFollow, yFollow, zFollow);
    }
}
