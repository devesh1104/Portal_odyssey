using UnityEngine;
using System.Collections;

public class FireBoss : MonoBehaviour {

    [SerializeField] float      m_speed = 4.0f;
    [SerializeField] float      m_jumpForce = 7.5f;
    [SerializeField] float      m_rollForce = 6.0f;
   // [SerializeField] bool       m_noBlood = false;
    [SerializeField] GameObject m_slideDust;
    public GameObject attackpoint;
    public float attackRange=0.5f;
    public LayerMask enemyLayers;
    [SerializeField] float PlayerHealth=100.0f;
    [SerializeField] float currenthealth=0.0f;
    private int damage;

    private Animator            m_animator;
    private Rigidbody2D         m_body2d;
    private Sensor_HeroKnight   m_groundSensor;
    private Sensor_HeroKnight   m_wallSensorR1;
    private Sensor_HeroKnight   m_wallSensorR2;
    private Sensor_HeroKnight   m_wallSensorL1;
    private Sensor_HeroKnight   m_wallSensorL2;
    private bool                m_isWallSliding = false;
    private bool                m_grounded = false;
    private bool                m_rolling = false;
    private int                 m_facingDirection = 1;
    private int                 m_currentAttack = 0;
    private float               m_timeSinceAttack = 0.0f;
    private float               m_delayToIdle = 0.0f;
    private float               m_rollDuration = 8.0f / 14.0f;
    private float               m_rollCurrentTime;
    private bool                doubleJump;
    [SerializeField] float attackCooldown = 1.0f; // Cooldown time between attacks
    private float timeSinceLastAttack = 0.0f; // Timer to track time since the last attack



    // Use this for initialization
    

    void Start ()
    {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_HeroKnight>();
        m_wallSensorR1 = transform.Find("WallSensor_R1").GetComponent<Sensor_HeroKnight>();
        m_wallSensorR2 = transform.Find("WallSensor_R2").GetComponent<Sensor_HeroKnight>();
        m_wallSensorL1 = transform.Find("WallSensor_L1").GetComponent<Sensor_HeroKnight>();
        m_wallSensorL2 = transform.Find("WallSensor_L2").GetComponent<Sensor_HeroKnight>();
        m_animator = GetComponent<Animator>();
        currenthealth=PlayerHealth;
    }

    // Update is called once per frame
    void Update ()
    {
        // Increase timer that controls attack combo
        m_timeSinceAttack += Time.deltaTime;

        // Increase timer that checks roll duration
        if(m_rolling)
            m_rollCurrentTime += Time.deltaTime;

        // Disable rolling if timer extends duration
        if(m_rollCurrentTime > m_rollDuration)
            m_rolling = false;

        //Check if character just landed on the ground
        if (!m_grounded && m_groundSensor.State())
        {
            m_grounded = true;
            m_animator.SetBool("Grounded", m_grounded);
        }

        //Check if character just started falling
        if (m_grounded && !m_groundSensor.State())
        {
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
        }

        // -- Handle input and movement --
        float inputX = Input.GetAxis("Horizontal");

        // Swap direction of sprite depending on walk direction
        if (inputX > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            m_facingDirection = 1;
        }
            
        else if (inputX < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            m_facingDirection = -1;
            //attackpoint = 
        }

        // Move
        if (!m_rolling )
            m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);

        //Set AirSpeed in animator
        m_animator.SetFloat("AirSpeedY", m_body2d.velocity.y);

        // -- Handle Animations --
        //Wall Slide
        m_isWallSliding = (m_wallSensorR1.State() && m_wallSensorR2.State()) || (m_wallSensorL1.State() && m_wallSensorL2.State());
        m_animator.SetBool("WallSlide", m_isWallSliding);

        // if(PlayerHealth<currenthealth)
        // {
        //     currenthealth=PlayerHealth;
        //    // anim.SetTrigger("Attacked");
        // }
        // if(PlayerHealth<=0)
        // {
        // }

        //Attack
         if(Input.GetMouseButtonDown(0) && !m_rolling)
        {
            // Reset the timer since we are initiating a new attack
            timeSinceLastAttack = 0.0f;

            m_currentAttack=1;
            m_animator.SetTrigger("Attack1");
            damage=20;
            
            //attack();
        }
        // Reset Attack combo if time since last attack is too large
        if (Input.GetKeyDown("q") && !m_rolling)
        {   
            timeSinceLastAttack = 0.0f; 
            m_currentAttack = 2;
             m_animator.SetTrigger("Attack2");
             damage=40;
            // attack();
        }
           
            

        if(Input.GetKeyDown("r")  && !m_rolling)
        {   
            timeSinceLastAttack = 0.0f;
            m_currentAttack = 3;
            m_animator.SetTrigger("Attack3");
            damage=50;
           // attack();
        }   
        // Block
        if (Input.GetMouseButtonDown(1) && !m_rolling)
        {
            m_animator.SetTrigger("Block");
            m_animator.SetBool("IdleBlock", true);
        }

        else if (Input.GetMouseButtonUp(1))
            m_animator.SetBool("IdleBlock", false);

        // Roll
        else if (Input.GetKeyDown("left shift") && !m_rolling && !m_isWallSliding)
        {
            m_rolling = true;
            m_animator.SetTrigger("Roll");
            m_body2d.velocity = new Vector2(m_facingDirection * m_rollForce, m_body2d.velocity.y);
        }
            

        //Jump
        if(m_grounded&&!Input.GetKeyDown("space"))
        {
            doubleJump=false;
        }
        if (Input.GetKeyDown("space"))
        {
            if((m_grounded&& !m_rolling)||doubleJump)
            {
                m_animator.SetTrigger("Jump");
                m_body2d.velocity=new Vector2(m_body2d.velocity.x, m_jumpForce);
                doubleJump=!doubleJump;
            }
        }
        if (Input.GetKeyDown("space") && m_grounded && !m_rolling)
        {
            m_animator.SetTrigger("Jump");
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
            m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce); 
            m_groundSensor.Disable(0.2f);

        }
        

        //Run
        else if (Mathf.Abs(inputX) > Mathf.Epsilon)
        {
            // Reset timer
            m_delayToIdle = 0.05f;
            m_animator.SetInteger("AnimState", 1);
        }

        //Idle
        else
        {
            // Prevents flickering transitions to idle
            m_delayToIdle -= Time.deltaTime;
                if(m_delayToIdle < 0)
                    m_animator.SetInteger("AnimState", 0);
        }
    }

    // Animation Events
    // Called in slide animation.
    void AE_SlideDust()
    {
        Vector3 spawnPosition;

        if (m_facingDirection == 1)
            spawnPosition = m_wallSensorR2.transform.position;
        else
            spawnPosition = m_wallSensorL2.transform.position;

        if (m_slideDust != null)
        {
            // Set correct arrow spawn position
            GameObject dust = Instantiate(m_slideDust, spawnPosition, gameObject.transform.localRotation) as GameObject;
            // Turn arrow in correct direction
            dust.transform.localScale = new Vector3(m_facingDirection, 1, 1);
        }
    }
   void attack()
{
    // Determine the direction of the attack
    float attackDirection = (m_facingDirection == 1) ? 1f : -1f;

    // Calculate the position of the attack point
    Vector3 attackPosition = attackpoint.transform.position + new Vector3(attackDirection * attackRange, 0f, 0f);

    // Perform the attack
    Collider2D[] enemies = Physics2D.OverlapCircleAll(attackpoint.transform.position, attackRange, enemyLayers);
    foreach (Collider2D enemyCollider in enemies)
    {
        Debug.Log("Hit enemy");
        EnemyHealth enemyHealth = enemyCollider.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(damage);
        }
    }
}
}

