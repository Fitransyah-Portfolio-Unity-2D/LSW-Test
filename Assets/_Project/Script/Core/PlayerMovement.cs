using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using LSWTest.UI;

namespace LSWTest.Core
{
    public enum GameMode
    {
        Play,
        Shop
    }
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] ShowHideUI showHideUi;
        
        Vector2 moveInput;
        Vector2 lastFacingDirection;
        Rigidbody2D playerRB;
        Animator playerAnimator;
        SpriteRenderer playerSprite;

        public GameMode gameMode = GameMode.Play;

        [SerializeField]
        float walkSpeed;
        float cachedWalkspeed;
        void Start()
        {
            cachedWalkspeed = walkSpeed;

            playerRB = GetComponent<Rigidbody2D>();
            playerRB.gravityScale = 0;

            playerAnimator = GetComponent<Animator>();

            playerSprite = GetComponent<SpriteRenderer>();

            showHideUi.onShopActivity += ChangeGameMode;
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

        void ChangeGameMode(GameObject uiContainer)
        {
            if (uiContainer.activeSelf)
            {
                gameMode = GameMode.Shop;
                walkSpeed = 0;
            }
            else
            {
                gameMode = GameMode.Play;
                walkSpeed = cachedWalkspeed;
            }
        }
    }
}

