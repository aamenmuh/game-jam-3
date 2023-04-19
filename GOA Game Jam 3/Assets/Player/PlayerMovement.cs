using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float Acceleration;
    [SerializeField]
    private float MaxSpeed;
    private Rigidbody2D rb;
    public bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //IF IN DIALOGUE SEQUENCE- DO NOT ACCEPT ANY OTHER INPUTS FROM MOVEMENT
        if(DialogueManager.isActive == true)
        {
            return;
        }

        Vector2 NewVelocity = rb.velocity;
        NewVelocity.x += Input.GetAxisRaw("Horizontal") * Acceleration* Time.deltaTime;
        NewVelocity.y += Input.GetAxisRaw("Vertical") * Acceleration * Time.deltaTime;
        if (NewVelocity.magnitude > MaxSpeed) NewVelocity = NewVelocity.normalized * MaxSpeed;
        if (canMove) rb.velocity = NewVelocity;
    }


}