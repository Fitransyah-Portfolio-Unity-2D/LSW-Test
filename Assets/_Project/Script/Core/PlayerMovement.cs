using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace LSWTest.Core
{
    public class PlayerMovement : MonoBehaviour
    {
        Vector2 moveInput;
        Vector2 lastFacingDirection;
        Rigidbody2D playerRB;
        Animator playerAnimator;
        SpriteRenderer playerSprite;

        [SerializeField]
        float walkSpeed;
        void Start()
        {
            playerRB = GetComponent<Rigidbody2D>();
            playerRB.gravityScale = 0;

            playerAnimator = GetComponent<Animator>();

            playerSprite = GetComponent<SpriteRenderer>();

        }

        // Update is called once per frame
        void Update()
        {
            Move();
            Animate();

            if (moveInput == Vector2.zero) return;
            lastFacingDirection = moveInput;
        }

        void OnMove(InputValue value)
        {
            moveInput = value.Get<Vector2>();
        }
        void Move()
        {
            playerRB.velocity = moveInput * walkSpeed;
        }
        void Animate()
        {
            playerAnimator.SetFloat("AnimMoveMagnitude", playerRB.velocity.magnitude);
            playerAnimator.SetFloat("AnimMoveX", lastFacingDirection.x);
            playerAnimator.SetFloat("AnimMoveY", lastFacingDirection.y);
        }
    }
}

