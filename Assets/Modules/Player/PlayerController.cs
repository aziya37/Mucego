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
    public float jumpForce;

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
        if(OnGround())
            playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

    }
    void Start()
    {
        inputManager.OnJump += Jump;
    }
    void Update()
    {
        Move(inputManager.moveInput.x);
        grounded = OnGround();
    }
    // funcao que verifica se estamos no chao
    private bool OnGround()
    {
        return Physics2D.OverlapCircle(footPivot.position, .2f, GroundLayer);
    }
}
