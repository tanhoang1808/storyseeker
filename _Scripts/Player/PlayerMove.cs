using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Rigidbody2D rb;
    [SerializeField]
   public float horizontalInput;
     public float speed = 7f;
     public float timeJumpEffect = 1f;
    public float dashDelay = 0.4f;
    public float dashDistance = 4f;
    public const float dashTime = 0.3f;
    public float dashCounter;
    public float JumpForceLerp = 25f;
    public Animator anim;
    public bool jump = false;
    public float timeCounterJump;
    private Vector2 boxsize = new Vector2(1, 1);
    
    public bool fall = false;
    public bool dash = false;
    public CapsuleCollider2D _playercollider;
    public bool isGround = false;
    private bool crouch;
    [SerializeField] public FixedJoystick joystick;
    public PlayerJoyStick playerjoy;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        this.rb.mass = 0.25f;
        _playercollider = GetComponentInChildren<CapsuleCollider2D>();
        playerjoy = GetComponent<PlayerJoyStick>();

    }

    private void Start()
    {
        anim.SetTrigger("StartGame");
    }

    private void Update()
    {
       
        Move();
        Flip();
        Run();
        
        Fall();
        Dash();
        Crouch();
        CheckGravity();
        DurationJump();
       
    }


    protected virtual void Move()
    {
        if(isGround == true)
        {
            horizontalInput = Input.GetAxis("Horizontal");
           
            this.rb.velocity = new Vector2(horizontalInput * speed, 0);
        }
    }

    protected virtual void Flip()
    {
        if (horizontalInput > 0.01f) this.transform.localScale = new Vector3(1, 1, 1);
        else if (horizontalInput < -0.01f) this.transform.localScale = new Vector3(-1, 1, 1);
        if (horizontalInput == 0)
        {
           
            this.transform.localScale = new Vector3(this.transform.localScale.x, 1, 1);
        }
    }

    protected virtual void Run()
    {
        //anim.SetBool("run", horizontalInput != 0 && jump == false && fall == false && isGround == true);
        anim.SetBool("run", joystick.Horizontal != 0 && jump == false && fall == false && isGround);
    }
    
    //public virtual void Jump()
    //{
        
    //    if ((Input.GetKeyDown(KeyCode.Space) && jump == false) )
    //    {
           
    //        jump = true;
    //        // Smooth Jump
    //        StartCoroutine(JumpCoroutine());

    //    }
        
    //    anim.SetBool("jump", jump);

    //}
    public virtual IEnumerator JumpCoroutine()
    {
       
        float jumpTime = 0f;
        while (jumpTime < timeJumpEffect)
        {
            float jumpForce = Mathf.Lerp(JumpForceLerp, 0f, jumpTime / timeJumpEffect);
            this.rb.velocity = new Vector2(this.rb.velocity.x, jumpForce);
            jumpTime += Time.deltaTime;
           
            yield return null;
        }

    }
    public virtual void DurationJump()
    {
        if (jump == true)
        {
            timeCounterJump += Time.deltaTime;
            if (timeCounterJump >= timeJumpEffect)
            {
                fall = true;

                timeCounterJump = 0;
            }
        }
    }

    // jump for button in mobile
    public void SetJump(bool canjump)
    {
        if (isGround && canjump && jump == false)
        {
            jump = true;
            isGround = false;
            StartCoroutine(JumpCoroutine());
            
        }
        anim.SetBool("jump", jump);

    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("ground"))
        {
            jump = false;
            fall = false;
            SceneCtrl.Instance.buttonHit = false;
            timeCounterJump = 0;
            isGround = true;
            this.rb.gravityScale = 6f;
            anim.SetBool("jump", false);
        }
      
    }

    protected virtual void CheckGravity()
    {
        if (fall || jump)
        {
            this.rb.gravityScale = 5f * timeCounterJump/timeJumpEffect;
        }
    }
    protected virtual void Fall()
    {
               
            anim.SetBool("fall", fall);
        
    }

    protected virtual void Dash()
    {
        dashCounter += Time.deltaTime;

        if (dashCounter <= dashDelay) return;

        if (Input.GetKeyDown(KeyCode.LeftShift) && dash == false)
        {
            dashCounter = 0;
            StartCoroutine(DashCoroutine());
        }
    }

    public virtual void SetDash(bool canDash)
    {
        dashCounter += Time.deltaTime;

        if (dashCounter <= dashDelay) return;
        if (dash == false && canDash)
        {
            dashCounter = 0;
            StartCoroutine(DashCoroutine());
        }
    }



    protected virtual IEnumerator DashCoroutine()
    {

        Debug.Log("isCalled");
        // Lưu lại vị trí hiện tại
        Vector2 startPos = transform.position;
        // Vi tri muon dash toi
        //Vector2 endPos = startPos + new Vector2(transform.localScale.x * dashDistance, 0f);
        Vector2 dashDirection = new Vector2(transform.localScale.x, 0f);
        RaycastHit2D hit = Physics2D.BoxCast(this.transform.position,boxsize,0f,Vector2.right * transform.localScale.x,10f,LayerMask.GetMask("Ground"));
        if(hit)
        {
            Debug.Log(hit.collider.name);
            rb.position = hit.point;
        }

        else
        {
            float elapsedTime = 0f;
            while (elapsedTime < dashTime)
            {
                dash = true;
                anim.SetBool("dash", dash);
                // Tính toán vị trí mới dựa trên khoảng cách và thời gian dash
               
                transform.Translate((dashDistance / dashTime) * Time.deltaTime * dashDirection);
              
                elapsedTime += Time.deltaTime;
                
                yield return null;
            }
        }

        // Đặt lại isDashing
        dash = false;
        anim.SetBool("dash", dash);

    }



    protected virtual void Crouch()
    {
        if (Input.GetKey(KeyCode.Z) && jump == false && fall == false && isGround == true )
        {
            crouch = true;
            if (crouch == true)
            {
                horizontalInput = 0;
                this.rb.velocity = new Vector2(0, 0);
            }
            _playercollider.size = new Vector2(0.5f, 1.3f);
            _playercollider.offset = new Vector2(0, 0);

        }
        else
        {
            crouch = false;
            _playercollider.size = new Vector2(0.5f, 1.7f);
            _playercollider.offset = new Vector2(0, 0.3f);
        }
        
        anim.SetBool("crouch", crouch);
    }
}
