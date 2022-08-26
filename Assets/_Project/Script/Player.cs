using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Vector2 moveInput;
    Rigidbody2D playerRB;

    [SerializeField]
    float walkSpeed;
    void Start()
    {
        playerRB =  GetComponent<Rigidbody2D>();
        playerRB.gravityScale = 0;
        
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

        Debug.Log("X : " + x);
        Debug.Log("Y : " + y);
    }

}
