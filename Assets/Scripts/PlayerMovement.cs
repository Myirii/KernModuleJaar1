using System.Collections;

using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float baseSpeed;
    public float dashBonus;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float speedIncrease;
    [SerializeField] private SpriteRenderer shield;

    private Rigidbody2D rb2d;
    private BoxCollider2D boxColl;

    [HideInInspector] public int shieldStrength { get; private set; } = 0;

    private float dashTime = 0.0f;
    private float maxDashTime = 0.5f;

    private float dashCooldownTime = 0.0f;
    private float maxDashCooldownTime = 0.75f;

    private float shieldHitstunDuration = 0.0f;

    public bool dashing { get; private set; } = false;

    private float previousDashDistance = 0.0f;
    private float highestDashPower = 0.0f;

    private int jumpForce = 500;

    private void Awake() //awake is called whenever the script is loaded
    {
        rb2d = GetComponent<Rigidbody2D>(); //use GetComponent to acces rigidbody2D
        boxColl = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        rb2d.velocity = new Vector2(Mathf.Min(baseSpeed, maxSpeed), rb2d.velocity.y);
        //Left
        Debug.DrawLine(new Vector2(transform.position.x - (boxColl.size.x / 2), transform.position.y - (boxColl.size.y / 2) - 0.01f), new Vector2(transform.position.x - (boxColl.size.x / 2), transform.position.y - (boxColl.size.y / 2) - 0.21f), Color.red);
        //Right
        Debug.DrawLine(new Vector2(transform.position.x + (boxColl.size.x / 2), transform.position.y - (boxColl.size.y / 2) - 0.01f), new Vector2(transform.position.x + (boxColl.size.x / 2), transform.position.y - (boxColl.size.y / 2) - 0.21f), Color.blue);

        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && IsGrounded() && shieldHitstunDuration <= 0)
        {
            rb2d.AddForce(new Vector2(rb2d.velocity.x, jumpForce/*has to be arduino jump sensor input*/));
            jumpForce = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space) && !dashing && dashCooldownTime <= 0)//Has to be arduino dash sensor input
        {
            dashing = true;
            dashBonus = Mathf.Ceil(highestDashPower / 3);
            baseSpeed += dashBonus;//Has to be arduino dash sensor input
            highestDashPower = 0;
        }
    }

    private void FixedUpdate()
    {
        baseSpeed += speedIncrease;

        if (dashing)
        {
            dashTime += Time.fixedDeltaTime;

            if (dashTime > maxDashTime)
            {
                dashCooldownTime = maxDashCooldownTime;
                dashTime = 0;
                dashing = false;
                baseSpeed -= dashBonus;
                dashBonus = 1;
            }
        }

        dashCooldownTime -= Time.fixedDeltaTime;
        shieldHitstunDuration -= Time.fixedDeltaTime;
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHitLeft = Physics2D.Linecast(new Vector2(transform.position.x - (boxColl.size.x / 2), transform.position.y - (boxColl.size.y / 2) - 0.01f), new Vector2(transform.position.x - (boxColl.size.x / 2), transform.position.y - (boxColl.size.y / 2) - 0.11f));
        RaycastHit2D raycastHitRight = Physics2D.Linecast(new Vector2(transform.position.x + (boxColl.size.x / 2), transform.position.y - (boxColl.size.y / 2) - 0.01f), new Vector2(transform.position.x + (boxColl.size.x / 2), transform.position.y - (boxColl.size.y / 2) - 0.11f));

        if (raycastHitLeft.collider != null || raycastHitRight.collider != null)
        {
            return true;
        }

        return false;
    }

    public void CreateShieldHitstun(int _bulletStrength)
    {
        int leftoverShieldToSeconds = 100;//Voor Kyra om te tweaken
        shieldHitstunDuration = (shieldStrength - _bulletStrength) / leftoverShieldToSeconds;
    }

    public void SetDashBonus(int _dashBonus)
    {
        if (_dashBonus < previousDashDistance)
        {
            if(previousDashDistance - _dashBonus > highestDashPower)
            {
                highestDashPower = previousDashDistance - _dashBonus;
            }
        }

        previousDashDistance = _dashBonus;
    }

    public void SetJumpStrength(int _jumpStrength)
    {
        int jumpBoost = 30;//Voor Kyra om te tweaken
        jumpForce = _jumpStrength * jumpBoost;
    }

    public void SetShieldStrength(int _shieldStrength)
    {
        shieldStrength = Mathf.Abs(Mathf.RoundToInt(_shieldStrength * 6.666666f) - 200); //
        shield.color = new Color(shield.color.r, shield.color.g, shield.color.b, shieldStrength / 255f);
    }
}
