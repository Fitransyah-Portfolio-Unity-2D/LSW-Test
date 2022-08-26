using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Vector2 moveInput;
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

        Vector2 lastFacingDirection = new Vector2();

        if (y > 0)
        {

            playerAnimator.SetBool("isWalkingBack", true);

            lastFacingDirection = new Vector2((playerRB.velocity.x), (playerRB.velocity.y));
        }
        else if (y < 0)
        {

            playerAnimator.SetBool("isWalkingFront", true);

            lastFacingDirection = new Vector2((playerRB.velocity.x), (playerRB.velocity.y));
        }
        else if (x > 0)
        {

            playerAnimator.SetBool("isWalkingSide", true);

            lastFacingDirection = new Vector2((playerRB.velocity.x), (playerRB.velocity.y));
        }
        else if (x < 0)
        {
            playerSprite.flipX = true;
            playerAnimator.SetBool("isWalkingSide", true);

            lastFacingDirection = new Vector2((playerRB.velocity.x), (playerRB.velocity.y));
        }
        else
        {
            playerSprite.flipX = false;
            playerAnimator.SetBool("isWalkingFront", false);
            playerAnimator.SetBool("isWalkingBack", false);
            playerAnimator.SetBool("isWalkingSide", false);

            switch (lastFacingDirection)
            {
                // TO DO :
                // Set default sprite to last facing direction
                // stop idle animation if character facing right, left or up
            }
        }

        Debug.Log("Last facing direction : " + lastFacingDirection);
    }

}
