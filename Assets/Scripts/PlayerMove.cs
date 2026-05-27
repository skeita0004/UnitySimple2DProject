using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("移動設定")]
    public float moveSpeed = 8f;        // 左右の移動速度
    public float jumpForce = 12f;       // ジャンプの力

    [Header("マリオ風の操作感調整")]
    public float fallMultiplier = 2.5f;  // 落下中の重力倍率
    public float lowJumpMultiplier = 2f; // ボタンを離した時の重力倍率

    [Header("接地判定")]
    public Transform groundCheck;       // 足元の空オブジェクト
    public float checkRadius = 0.2f;    // 判定の半径
    public LayerMask groundLayer;       // 地面レイヤー

    private Rigidbody2D rb;
    private float horizontalInput;
    private bool isGrounded;
    private bool isFacingRight = true;

    // ボタンが押されているかを判定するフラグ
    private bool isJumpPressed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 1. 新しい方式での入力取得
        // キーボードのA/Dや矢印キー、またはゲームパッドのスティックを自動判定します
        if (Keyboard.current != null)
        {
            horizontalInput = 0f;
            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) horizontalInput = 1f;
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) horizontalInput = -1f;
        }

        // 2. 接地判定
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);

        // 3. ジャンプ入力（Spaceキーが押された瞬間 ＆ 地面にいるとき）
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        // ジャンプボタンが「今押されているか」を記録（可変ジャンプ用）
        isJumpPressed = Keyboard.current != null && Keyboard.current.spaceKey.isPressed;

        // 4. 向き反転
        if (horizontalInput > 0 && !isFacingRight) Flip();
        else if (horizontalInput < 0 && isFacingRight) Flip();

        // 5. マリオ風の挙動
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
        // スペースキーが離されたら上昇をストップ
        else if (rb.linearVelocity.y > 0 && !isJumpPressed)
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

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