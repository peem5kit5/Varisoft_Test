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
    [SerializeField] private CharacterSprite characterRenderer;
    [SerializeField] private Transform shootingPoint;

    [Header("Input Assets")]
    [SerializeField] private InputActionReference inputMovement;
    [SerializeField] private InputActionReference inputMagicShoot;

    [Header("Status")]
    public float Speed;
    public float ShootMaxCooldown;
    public bool IsShooted;

    private float shootCooldown;
    private Vector2 moveDirection;
    private Vector2 inputVector;
    private Vector2 movementPosition;
    private Vector2 newPosition;

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (!rb)
            rb = GetComponent<Rigidbody2D>();

        if (!health)
            health = GetComponent<Health>();
    }
#endif

    public void Init()
    {
        inputMagicShoot.action.performed += MagicShoot;

        health.OnHpChange += OnDeath;
    }

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        moveDirection = inputMovement.action.ReadValue<Vector2>();
        inputVector = new Vector2(moveDirection.x, moveDirection.y);
        inputVector = Vector2.ClampMagnitude(inputVector, 1);

        movementPosition = inputVector * Speed;
        newPosition = rb.position + movementPosition * Time.fixedDeltaTime;

        rb.MovePosition(newPosition);
        characterRenderer.SetDirection(moveDirection);

        if (moveDirection != Vector2.zero)
        {
            float _angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            shootingPoint.rotation = Quaternion.Euler(new Vector3(0, 0, _angle));

            Vector3 _shootingPointOffset = moveDirection.normalized * 3;
            shootingPoint.position = rb.transform.position + _shootingPointOffset;
        }
    }

    private void MagicShoot(InputAction.CallbackContext _context)
    {
        if (!IsShooted)
        {
            GameObject _projectile = Instantiate(magicBullet, transform.position, shootingPoint.rotation);

            Rigidbody2D _projectileRb = _projectile.GetComponent<Rigidbody2D>();

            Vector2 _shootDirection = shootingPoint.right;
            _projectileRb.velocity = _shootDirection * 10;

            Destroy(_projectile, 1);
            StartCoroutine(CountingMagicCooldown());
        }
    }

    private IEnumerator CountingMagicCooldown()
    {
        IsShooted = true;
        yield return new WaitForSeconds(ShootMaxCooldown);
        IsShooted = false;
    }

    private void OnDeath()
    {
        if (health.HP <= 0)
            GameManager.Instance.GameOver();
    }
}
