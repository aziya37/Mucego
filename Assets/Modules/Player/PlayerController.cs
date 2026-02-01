using System.Data.Common;
using UnityEditor.Callbacks;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public Rigidbody2D playerRb;
    public InputManager inputManager;
    public PlayerProfile currentProfile;
    public static PlayerController Instance {get; private set;}

    [Header("GROUND SETTINGS")]
    public Transform footPivot;
    private float coyoteTimeCounter;
    private float startScale;
    private float defaultGravity;
    private bool fallController;

    private void Awake() 
    {
        if(Instance == null)
            Instance = this;
            
        startScale = transform.localScale.x;
        defaultGravity = playerRb.gravityScale;
    }
    private void Move(float input)
    {
        float dir = Mathf.Sign(input);
        float t = Mathf.Pow(Mathf.Abs(input), currentProfile.power);
        float target = dir * t * currentProfile.maxSpeed;
        float diff = target - playerRb.linearVelocity.x;

        float rate = Mathf.Abs(target) > .001f ? currentProfile.acc : currentProfile.dcc;
        playerRb.AddForce(Vector2.right * rate * diff, ForceMode2D.Force);

    }

    private void Fall()
    {
        bool grounded = OnGround();

        if(grounded)
        {
            fallController = false;
            playerRb.gravityScale = defaultGravity;
            return;
        }

        if(!fallController && playerRb.linearVelocityY < 0f)
        {
            fallController = true;
            playerRb.gravityScale = defaultGravity * currentProfile.gravityMultplier;
        }
    }
    
    private void Jump()
    {
        if(coyoteTimeCounter > 0f)
        {
            playerRb.linearVelocityY = 0;
            playerRb.AddForce(Vector2.up * currentProfile.jumpForce, ForceMode2D.Impulse);
            coyoteTimeCounter = 0f;
        }
    }
    void Start()
    {
        inputManager.OnJump += Jump;
    }
    void Update()
    {
        transform.localScale = inputManager.moveInput.x > 0?  Vector3.one * startScale : inputManager.moveInput.x < 0? new Vector3(-startScale,startScale,startScale) : transform.localScale;

        if(OnGround())
            coyoteTimeCounter = currentProfile.coyoteTime;
        else
            coyoteTimeCounter -= Time.deltaTime;

    }
    void FixedUpdate()
    {
        Move(inputManager.moveInput.x);
        Fall();
    }
    // funcao que verifica se estamos no chao
    private bool OnGround()
    {
        return Physics2D.OverlapCircle(footPivot.position, .2f, currentProfile.GroundLayer);
    }
}
