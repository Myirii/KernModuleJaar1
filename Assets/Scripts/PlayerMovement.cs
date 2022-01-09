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

    private int shieldStrength = 0;

    private float dashTime = 0.0f;
    private float maxDashTime = 0.5f;

    private float dashCooldownTime = 0.0f;
    private float maxDashCooldownTime = 1.0f;

    public bool dashing { get; private set; } = false;

    private bool decreasingShield = false;
    private bool increasingShield = false;

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

        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && IsGrounded() && !dashing)
        {
            rb2d.AddForce(new Vector2(rb2d.velocity.x, 800/*has to be arduino jump sensor input*/));
        }

        if (Input.GetKeyDown(KeyCode.Space) && !dashing && dashCooldownTime <= 0)//Has to be arduino dash sensor input
        {
            dashing = true;
            baseSpeed += dashBonus;//Has to be arduino dash sensor input
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            decreasingShield = true;
        }

        if (Input.GetKeyUp(KeyCode.Q))
        {
            decreasingShield = false;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            increasingShield = true;
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            increasingShield = false;
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
            }
        }

        dashCooldownTime -= Time.fixedDeltaTime;

        if (decreasingShield)
        {
            DecreaseShield();
        }

        if (increasingShield)
        {
            IncreaseShield();
        }
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

    private void DecreaseShield()
    {
        shieldStrength = Mathf.Min(Mathf.Max(shieldStrength - 4, 0), 200);
        shield.color = new Color(shield.color.r, shield.color.g, shield.color.b, shieldStrength / 255f);
    }

    private void IncreaseShield()
    {
        shieldStrength = Mathf.Min(Mathf.Max(shieldStrength + 4, 0), 200);
        shield.color = new Color(shield.color.r, shield.color.g, shield.color.b, shieldStrength / 255f);
    }

    //Old code for dashing
    //private IEnumerator Dash()
    //{
    //    dashing = true;
    //    float dashTime = Time.time + 0.8f;
    //    Vector3 startPosition = transform.position;
    //    Vector3 lookPosition = lookPoint.localPosition;
    //    Vector3 dashEndPosition = transform.position + new Vector3(5/*calculation of dash sensor input*/, 0, 0);
    //    Vector3 lookPointPosition = lookPoint.localPosition - new Vector3(5/*calculation of dash sensor input*/, 0, 0);
    //    yield return new WaitForSeconds(0.02f);

    //    while (Time.time <= dashTime)
    //    {
    //        transform.position = Vector2.Lerp(transform.position, dashEndPosition, 0.4f / Vector2.Distance(transform.position, dashEndPosition));
    //        lookPoint.localPosition = Vector2.Lerp(lookPoint.localPosition, lookPointPosition,
    //                                  0.4f / Vector2.Distance(lookPoint.localPosition, lookPointPosition));

    //        if (Vector2.Distance(lookPoint.localPosition, lookPointPosition) < 0.1f)
    //        {
    //            dashTime -= 0.5f;
    //        }

    //        yield return new WaitForSeconds(0.02f);
    //    }

    //    float returnTime = Time.time + 0.8f;

    //    while (Time.time <= returnTime)
    //    {
    //        transform.position = Vector2.Lerp(transform.position, startPosition, 0.4f / Vector2.Distance(transform.position, startPosition));
    //        lookPoint.localPosition = Vector2.Lerp(lookPoint.localPosition, lookPosition, 0.4f / Vector2.Distance(lookPoint.localPosition, lookPosition));

    //        if (Vector2.Distance(lookPoint.localPosition, lookPosition) < 0.1f)
    //        {
    //            returnTime -= 0.5f;
    //        }

    //        yield return new WaitForSeconds(0.02f);
    //    }

    //    dashing = false;
    //}
}
