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
       
        Move();
        Animate();
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
        float x = moveInput.x;
        float y = moveInput.y;

        playerAnimator.SetFloat("AnimMoveX", x);
        playerAnimator.SetFloat("AnimMoveY", y);

    }



}
