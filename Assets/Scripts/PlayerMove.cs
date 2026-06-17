using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
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
        rb = GetComponent<Rigidbody2D>();

        jumpAction = GetComponent<PlayerInput>().actions["Jump"];
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
        if (isJumpPressed && playerIsGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        if (!pressed && rb.linearVelocityY > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
        }

        // ボタンが今押されているか（可変ジャンプの判定用）
    }

    void Update()
    {
        // 接地判定
        playerIsGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);

        isJumpPressed = jumpAction.IsPressed();

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