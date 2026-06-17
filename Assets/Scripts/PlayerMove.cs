using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("SE")]
    public AudioClip jumpSE;
    public AudioClip landSE;

    private AudioSource audioSource;

    // 前フレームの接地状態
    private bool wasGrounded;

    [Header("移動設定")]
    public float moveSpeed = 8f;
    public float jumpForce = 12f;

    [Header("マリオ風の操作感調整")]
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    [Header("接地判定")]
    public Transform groundCheck;
    public float checkRadius = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private float horizontalInput;
    private bool isFacingRight = true;

    static bool playerIsGrounded = false;

    // ボタンが押されているかを判定するフラグ
    private bool isJumpPressed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
        // ボタンが押された瞬間
        if (value.isPressed && playerIsGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            
            // ジャンプSE
            audioSource.PlayOneShot(jumpSE);
        }

        // ボタンが今押されているか（可変ジャンプの判定用）
        isJumpPressed = value.isPressed;
    }

    void Update()
    {
        // 前フレームの状態を保存
        wasGrounded = playerIsGrounded;

        // 接地判定
        playerIsGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);

        // 着地した瞬間
        if (!wasGrounded && playerIsGrounded)
        {
            audioSource.PlayOneShot(landSE);
        }

        // 向き反転
        if (horizontalInput > 0 && !isFacingRight) Flip();
        else if (horizontalInput < 0 && isFacingRight) Flip();

        // マリオ風の挙動
        BetterJump();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);
    }

    void BetterJump()
    {
        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.linearVelocity.y > 0 && !isJumpPressed)
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    // 
    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
        }
    }
}