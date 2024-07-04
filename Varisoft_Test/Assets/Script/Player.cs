using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof (Health))]
[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Health health;
    [SerializeField] private GameObject magicBullet;
    [SerializeField] private CharacterSprite isometricCharacterRenderer;

    [SerializeField] private InputActionReference inputMovement;
    [SerializeField] private InputActionReference inputMagicShoot;

    [Header("Status")]
    public float Speed;

    private Vector2 moveDirection;
    private Vector2 inputVector;
    private Vector2 movementPosition;
    private Vector2 newPosition;

    public void Init()
    {
        inputMovement.action.performed += Movement;
        inputMovement.action.canceled += Movement;

        inputMagicShoot.action.performed += MagicShoot;
    }


    private void Movement(InputAction.CallbackContext _context)
    {
        moveDirection = _context.ReadValue<Vector2>();
        inputVector = new Vector2(moveDirection.x, moveDirection.y);
        inputVector = Vector2.ClampMagnitude(inputVector, 1);

        movementPosition = inputVector * Speed;
        newPosition = rb.position + movementPosition * Time.fixedDeltaTime;

        rb.MovePosition(newPosition);
        isometricCharacterRenderer.SetDirection(moveDirection);
    }

    private void MagicShoot(InputAction.CallbackContext _context)
    {
        Debug.Log("Shoot");
    }
}
