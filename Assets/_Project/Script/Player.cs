using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Vector2 moveInput;
    Rigidbody2D playerRB;
    Animator playerAnimator;

    [SerializeField]
    float walkSpeed;
    void Start()
    {
        playerRB =  GetComponent<Rigidbody2D>();
        playerRB.gravityScale = 0;

        playerAnimator = GetComponent<Animator>();
        
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

        if (y > 0)
        {
            playerAnimator.SetBool("isWalkingBack", true);
        }
        else if (y < 0)
        {
            playerAnimator.SetBool("isWalkingFront", true);
        }
        else
        {
            playerAnimator.SetBool("isWalkingFront", false);
            playerAnimator.SetBool("isWalkingBack", false);
        }
    }

}
