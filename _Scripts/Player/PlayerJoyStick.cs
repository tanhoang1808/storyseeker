using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJoyStick : PlayerMove
{
    //[SerializeField] public Rigidbody2D rb;
    //[SerializeField] public FixedJoystick joystick;
    //[SerializeField] public Animator anim;
    public float horizontal;
    public bool CanJump = true;
    public PlayerAttack playerAtk;
    [SerializeField] private float movespeed = 9f;
    public bool slash = false;
    private void Awake()
    {
        playerAtk = GetComponent<PlayerAttack>();
    }

    private void FixedUpdate()
    {


        Move();
        CheckGravity();
       
    }

    protected override void Move()
    {
       
            horizontal = joystick.Horizontal;
            rb.velocity = new Vector2(horizontal * this.movespeed, rb.velocity.y);
            if (horizontal != 0)
            {
                if (horizontal <= 0)
                {
                    PlayerCtrl.Instance.transform.localScale = new Vector2(-1, 1);
                }
                else
                {
                    PlayerCtrl.Instance.transform.localScale = new Vector2(1, 1);
                }
            }
        if (slash)
        {
            StartCoroutine(ChangeSlash());
        }
       
    }
    public void OnJumpButtonClicked()
    {
       
        if(isGround == false)
        {
            CanJump = false;
           
        }
        CanJump = true;
        SetJump(CanJump);
    }

    public void OnDashButtonClick()
    {
        SetDash(true);
    }


    public void OnAttackButtonClick()
    {
        slash = true;
        playerAtk.setSlash(slash);


    }

    public IEnumerator ChangeSlash()
    {
        
        yield return new WaitForSeconds(0.5f);
        slash = false;
        playerAtk.setSlash(slash);

    }

   

}

