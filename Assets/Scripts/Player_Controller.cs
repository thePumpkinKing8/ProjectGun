using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    [SerializeField] private float _jumpForce = 400f;
    [SerializeField] private float _movementSmoothing = 0.5f;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Transform _groundCheck;
    const float _groundCheckRadius = .2f;
    private bool _isGrounded;
    private SpriteRenderer _sprite;
    private Vector3 _velocity = Vector3.zero;
    private Rigidbody2D _rb;
    private bool _jumped;
    public float Speed = 40f;
    private Animator _animator;

    protected PlayerHealth playerhealth;


   
   
    void Start()
    {
        _animator = GetComponent<Animator>();
       _rb = GetComponent<Rigidbody2D>();
       _sprite = GetComponent<SpriteRenderer>();

        playerhealth = GameObject.Find("HealthCounter").GetComponent<PlayerHealth>();
    }

     void FixedUpdate() 
    {
        //creates circle collider that checks to see if any gameobjects within its radius are part of the ground layer and sets grounded to true if it does
		Collider2D collider = Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _groundLayer);
        if(collider != null)
        {
            if (collider.gameObject != gameObject)
		    {
				_isGrounded = true;
		    }
            else
            {
                _isGrounded = false;
            }
        }
        else
        {
            _isGrounded = false;
        }
        _animator.SetBool("Grounded", _isGrounded);
		
		Move((Input.GetAxisRaw("Horizontal") * Speed) * Time.fixedDeltaTime, _jumped);
        _jumped = false;
    }

    // Update is called once per frame
     void Update()
    {
        
       if(Input.GetKeyDown(KeyCode.Space))
       {
        _jumped = true;
       }
       if((Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x > 0) && this.transform.localScale.x < 0)
       {
        _animator.SetFloat("Direction", 1);
        this.transform.localScale = new Vector3 (-this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z);
        
       }
       else if((Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x < 0 ) && this.transform.localScale.x > 0)
       {
        _animator.SetFloat("Direction", -1);
        this.transform.localScale = new Vector3 (-this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z);
       }	
    }

     public void Move(float move, bool jump)
     {
        Vector3 targetVelocity = new Vector2(move * 10f, _rb.velocity.y);
        _rb.velocity = Vector3.SmoothDamp(_rb.velocity, targetVelocity, ref _velocity, _movementSmoothing);
        if(_isGrounded && jump)
        {
            _rb.AddForce(new Vector2(0f, _jumpForce));
          
        }	
        
        if(move != 0)
        {
            _animator.SetBool("Moving", true);
            _animator.SetFloat("Velocity", Mathf.Sign(_rb.velocity.x));	
        }
        else
        {
            
            _animator.SetBool("Moving", false);
            _animator.SetFloat("Velocity",0);
        }
        
      }
      public void SetPosition(Vector3 Position)
      {
        this.transform.position = Position;
      }
      IEnumerator TakeDamage()
    {
        this.playerhealth.RemoveHeart(1);
        _sprite.color = Color.red;
        yield return new WaitForSeconds(1f);
        _sprite.color = Color.white;
    }

    public void _takeDamage()
    {
        StartCoroutine(TakeDamage());
    }
    

}
     
    

