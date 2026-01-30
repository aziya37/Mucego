using System.Data.Common;
using UnityEditor.Callbacks;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public Rigidbody2D playerRb;
    public InputManager inputManager;
    public float maxSpeed;
    public float power;
    public float acc;
    public float dcc;

    [Header("JUMP SETTINGS")]

    public float jumpForce;
    public float coyoteTime = .13f;
    private float coyoteTimeCounter;

    [Header("GROUND SETTINGS")]
    public Transform footPivot;
    public LayerMask GroundLayer;
    public bool grounded;

    private void Move(float input)
    {
        float dir = Mathf.Sign(input);
        float t = Mathf.Pow(Mathf.Abs(input), power);
        float target = dir * t * maxSpeed;
        float diff = target - playerRb.linearVelocity.x;

        float rate = Mathf.Abs(target) > .001f ? acc : dcc;
        playerRb.AddForce(Vector2.right * rate * diff, ForceMode2D.Force);

    }
    
    private void Jump()
    {
        if(coyoteTimeCounter > 0f)
        {
            playerRb.linearVelocityY = 0;
            playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            coyoteTimeCounter = 0f;
        }
    }
    void Start()
    {
        inputManager.OnJump += Jump;
    }
    void Update()
    {
        Move(inputManager.moveInput.x);
        grounded = OnGround();

        transform.localScale = inputManager.moveInput.x > 0?  Vector3.one : inputManager.moveInput.x < 0? new Vector3(-1,1,1) : transform.localScale;
     
        if(grounded)
            coyoteTimeCounter = coyoteTime;
        else
            coyoteTimeCounter -= Time.deltaTime;
    }
    // funcao que verifica se estamos no chao
    private bool OnGround()
    {
        return Physics2D.OverlapCircle(footPivot.position, .2f, GroundLayer);
    }
}
