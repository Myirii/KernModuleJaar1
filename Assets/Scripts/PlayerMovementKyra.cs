using System.Collections;

using UnityEngine;

public class PlayerMovementKyra : MonoBehaviour
{
    public float baseSpeed; // character starts with this speed 
    public float dashBonus; //The bonus speed/ distance the player gets whenever they dash
    [SerializeField] private float maxSpeed; //though i don't know what SerializeField stands for, its a private float so people cant change this. Its also the maximum speed the player can get
    [SerializeField] private float speedIncrease; // again private so people can't change this easily. It increases the speed of the player.
    [SerializeField] private SpriteRenderer shield; //*Shows the shield of the character?*

    private Rigidbody2D rb2d;  //A rigidbody is for applying physics to the character rb2d is a shorter version of the word
    private BoxCollider2D boxColl; //The boxcollider around the player, this makes sure the character doesn't fall through the floor or platforms.

    [HideInInspector] public int shieldStrength { get; private set; } = 0; //!I'm guessing this is the different set in if the shield is weak/strong though i don't know how it works

    private float dashTime = 0.0f; //the amound of force is put on the object after it dashes?
    private float maxDashTime = 0.5f; //the most force that is put on the object will be 0.5?

    private float dashCooldownTime = 0.0f; //you can dash anytime you want there wont be a cooldown on the use
    private float maxDashCooldownTime = 0.5f; //when dashing through thicker walls the cooldown on when you can dash again will be higher than when you dash through a thinner wall

    public bool dashing { get; private set; } = false; // makes the variable of dashing

    private void Awake() //awake is called whenever the script is loaded
    {
        rb2d = GetComponent<Rigidbody2D>(); //use Getcomponent to acces rigidbody2D
        boxColl = GetComponent<BoxCollider2D>(); //load box collider code
    }

    // Update is called once per frame
    void Update()
    {
        rb2d.velocity = new Vector2(Mathf.Min(baseSpeed, maxSpeed), rb2d.velocity.y); //the velocity of the rigidbody has a min base speed and a maxspeed?
        //left
        Debug.DrawLine(new Vector2(transform.position.x - (boxColl.size.x / 2), transform.position.y - (boxColl.size.y / 2) - 0.01f), new Vector2(transform.position.x - (boxColl.size.x / 2), transform.position.y - (boxColl.size.y / 2) - 0.21f), Color.red);
        //.....thats a lot of code...
        //right 
        Debug.DrawLine(new Vector2(transform.position.x - (boxColl.size.x / 2), transform.position.y - (boxColl.size.y / 2) - 0.01f), new Vector2(transform.position.x - (boxColl.size.x / 2), transform.position.y - (boxColl.size.y / 2) - 0.21f), Color.blue);
        //.....thats a lot of code...

        if (Input.GetKeyDown(KeyCode.Space) && !dashing && dashCooldownTime <= 0) //when you press space you dash
        {
            dashing = true;
            baseSpeed += dashBonus; //the dash bonus is added to the base speed
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            decreasingShield = true; //when q is pressed the shield will 'shrink'
        }

        if (Input.GetKeyUp(KeyCode.Q))
        {
            decreasingShield = false; //when q is released nothing will change
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            increasingShield = true; //when E is pressed the shield will grow
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            increasingShield = false; //when E is released the shield will do nothing
        }
    }

    private void FixedUpdate() //why fixed again?
    {
        baseSpeed += speedIncrease; //when you dash ur base speed gets faster

        if (dashing)
        {
            dashTime += Time.fixedDeltaTime; //this so its the same for every pc and not faster on the computer with better specs

            if (dashTime > maxDashTime) //If the dash time is bigger than the max (player continuously gets speed until it hits the max), do the following
            {
                dashCooldownTime = maxDashCooldownTime; //the cd becomes the max cd for the dash
                dashTime = 0;
                dashing = false;
                baseSpeed -= dashBonus;
            }
        }

        dashCooldownTime -= Time.fixedDeltaTime;
        shieldHitstunDuration -= Time.fixedDeltaTime; //the stun is the same for anyone playing the game

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
        //Im guessing these are for figuring out if the player is still on the ground. And because there are diagonal platforms the rectangle needed to have 2 sides because otherwise it wouldn't register that the player was on the ground anymore.
        RaycastHit2D raycastHiRight = Physics2D.Linecast(new Vector2(transform.position.x - (boxColl.size.x / 2), transform.position.y - (boxColl.size.y / 2) - 0.01f), new Vector2(transform.position.x + (boxColl.size.x / 2), transform.position.y - (boxColl.size.y / 2) - 0.11f));

        if (raycastHitLeft.collider != null || raycastHiRight.collider != null) //the raycast can never be equal to 0 
        {
            return true; //if the number returned is something other than 0 its true
        }

        return false; //otherwise this number is false
    }

    private void DecreaseShield()
    {
        shieldStrength = Mathf.Min(Mathf.Max(shieldStrength - 4, 0), 200); //ye no sorry
        shield.color = new Color(shield.color.r, shield.color.g, shield.color.b, shieldStrength / 255f); //when do you use Color or color?
    }

    private void IncreaseShield()
    {
        shieldStrength = Mathf.Min(Mathf.Max(shieldStrength + 4, 0), 200); //again, que? 
        shield.color = new Color(shield.color.r, shield.color.g, shield.color.b, shieldStrength / 255f);
    }

    public void CreateShieldHitstun(int _bulletStrength) //is this like calling it from another script when using _ ?
    {

    }

    public void SetDashBonus(int _dashBonus)
    {

    }

    public void SetJumpStrength(int _jumpStrength)
    {

    }

    public void SetShieldStrength(int _shieldStrength)
    {

    }

    // I don't know how terrible or good my interpretations of the code is, but I'm thinking I'll need to practice using this anyway. It's like ill never really understand when what is supposed to go where
    // which is kinda frustrating. You learn this by practicing tho.
    //****THANK YOU LARS  FOR HELPING ME <3 :) ****
}
