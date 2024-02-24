using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Damageable
{
    [SerializeField] 
    private InputActionAsset actions;
    public InputAction moveAction;

    private Vector2 moveAmount;
    
    // Start is called before the first frame update
    void Start()
    {
        InitializeComponents();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector2 moveVector = moveAction.ReadValue<Vector2>();
        Vector3 moveDir = new Vector3(moveAction.ReadValue<Vector2>().x, moveAction.ReadValue<Vector2>().y, 0);
        transform.Translate(moveDir * speed * Time.deltaTime, Space.Self);
        
    }

    public override void InitializeComponents()
    {
        actions.FindActionMap("Base").Enable();
        moveAction = actions.FindActionMap("Base").FindAction("Walk");
        spriteControls = GetComponent<SpriteRenderer>();
        moveAction.performed += OnMove;
    }
    

    void OnMove(InputAction.CallbackContext context)
    {
        faceLeft = moveAction.ReadValue<Vector2>().x < 0 ; //If moving Left, faceLeft is false
        spriteControls.flipX = faceLeft; 
    }
}
