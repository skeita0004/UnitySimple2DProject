using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Camera mainCamera;

    [Header("SE")]
    public AudioClip jumpSE;
    public AudioClip landSE;

    private AudioSource audioSource;

    // 前フレームの接地状態
    private bool wasGrounded;

    [Header("移動設定")]
    public float moveSpeed = 8f;
    public float jumpForce = 10f;

    [Header("マリオ風の操作感調整")]
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 4f;

    [Header("接地判定")]
    public Transform groundCheck;
    public float checkRadius = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private float horizontalInput;
    private bool isFacingRight = true;

    private InputAction jumpAction;

    static bool playerIsGrounded = false;

    // ボタンが押されているかを判定するフラグ
    private bool isJumpPressed;

    void Start()
    {
        Vector3 initPos = Vector3.zero;
        Vector3 initPosCam = Vector3.zero;

        // これは、とんでもない最低のコードですわよ！流石平民というべきかしら？
        if ( GoalPole.clearNum == 0)
        {
            initPos = new Vector3(-5.8f, -2.6f, 0f);
            initPosCam = new Vector3(-0.81f, 0f, -10f);
        }
        else if (GoalPole.clearNum == 1 ) 
        {
            initPos = new Vector3(308f, -1.0f, 0f);
            initPosCam = new Vector3(313.2f, 0f, -10f);
        }

        transform.position = initPos;
        mainCamera.transform.position = initPosCam; 

        rb = GetComponent<Rigidbody2D>();

        jumpAction = GetComponent<PlayerInput>().actions["Jump"];
        audioSource = GetComponent<AudioSource>();
    }

    // ★Player Inputコンポーネントから自動で呼び出される移動処理
    public void OnMove(InputValue value)
    {
        // スティックやキーボードの左右の値を読み込む (-1 ～ 1)
        Vector2 moveVector = value.Get<Vector2>();
        horizontalInput = moveVector.x;
    }

    // ★Player Inputコンポーネントから自動で呼び出されるジャンプ処理
    public void OnJump(InputValue value)
    {
        bool pressed = value.isPressed;
        Debug.Log("Jump input: " + pressed);

        // ボタンが押された瞬間
        if (pressed && playerIsGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            
            // ジャンプSE
            audioSource.PlayOneShot(jumpSE);
        }

        if (!pressed && rb.linearVelocityY > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
        }

        if (!pressed && rb.linearVelocityY > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
        }

        // ボタンが今押されているか（可変ジャンプの判定用）
    }

    void Update()
    {
        // 前フレームの状態を保存
        wasGrounded = playerIsGrounded;

        // 接地判定
        playerIsGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);

        isJumpPressed = jumpAction.IsPressed();

        // 着地した瞬間
        if (!wasGrounded && playerIsGrounded)
        {
            audioSource.PlayOneShot(landSE);
        }

        // 向き反転
        if (horizontalInput > 0 && !isFacingRight) Flip();
        else if (horizontalInput < 0 && isFacingRight) Flip();

    }

    void FixedUpdate()
    {
        // 水平移動
        rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);

        // マリオ風の挙動
        BetterJump();
    }

    void BetterJump()
    {
        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += ((Vector2.up * Physics2D.gravity.y) * fallMultiplier) * Time.fixedDeltaTime;
        }
        else if (rb.linearVelocity.y > 0 && !isJumpPressed)
        {
            rb.linearVelocity += ((Vector2.up * Physics2D.gravity.y) * lowJumpMultiplier) * Time.fixedDeltaTime;
        }
    }

    // キャラクター反転
    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    // groundcheckの位置を表示するやつ
    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
        }
    }
}