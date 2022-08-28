using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

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
        playerRB =  GetComponent<Rigidbody2D>();
        playerRB.gravityScale = 0;

        playerAnimator = GetComponent<Animator>();

        playerSprite = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Walk();

        if (playerRB.velocity.x == 0 && playerRB.velocity.y == 0)
        {
            IdleAnimation();
        }
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();

        Debug.Log(moveInput);
    }
    void Walk()
    {
        playerRB.velocity = moveInput * walkSpeed;
        float x = moveInput.x;
        float y = moveInput.y;

        if (playerRB.velocity == Vector2.zero) return;
        WalkAnimation(playerRB.velocity);
    }
    void WalkAnimation(Vector2 direction)
    {
        Vector2 up = new Vector2(0, 3f);
        Vector2 right = new Vector2(3f, 0);

        switch (direction)
        {
            case Vector2 i when i.Equals(up):
                playerAnimator.SetBool("isWalkingBack", true);
                lastFacingDirection = new Vector2((playerRB.velocity.x), (playerRB.velocity.y));
                break;

                case Vector2 i when i.Equals(-up):
                playerAnimator.SetBool("isWalkingFront", true);
                lastFacingDirection = new Vector2((playerRB.velocity.x), (playerRB.velocity.y));
                break;

            case Vector2 i when i.Equals(right):
                lastFacingDirection = new Vector2((playerRB.velocity.x), (playerRB.velocity.y));
                playerAnimator.SetBool("isWalkingSide", true);
                break;

            case Vector2 i when i.Equals(-right):
                lastFacingDirection = new Vector2((playerRB.velocity.x), (playerRB.velocity.y));
                playerAnimator.SetBool("isWalkingSide", true);
               

                // flipping the sprite X
                if (playerRB.velocity.x > 0)
                {
                    return;
                }
                playerSprite.flipX = true;
                break;
        }
        Debug.Log("Last facing direction : " + lastFacingDirection);
    }
    void IdleAnimation()
    {
        playerSprite.flipX = false;
        playerAnimator.SetBool("isWalkingFront", false);
        playerAnimator.SetBool("isWalkingBack", false);
        playerAnimator.SetBool("isWalkingSide", false);
    }


}
