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

    protected PlayerHealth playerhealth;


   
   
    void Start()
    {
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
        
    }

     public void Move(float move, bool jump)
     {
        Vector3 targetVelocity = new Vector2(move * 10f, _rb.velocity.y);
        _rb.velocity = Vector3.SmoothDamp(_rb.velocity, targetVelocity, ref _velocity, _movementSmoothing);
        if(_isGrounded && jump)
        {
            _rb.AddForce(new Vector2(0f, _jumpForce));
          
        }
        if(move > 0)
        {
            _sprite.flipX = true;
        }
        else if(move < 0)
        {
            _sprite.flipX = false;
        }			
      }
      public void SetPosition(Vector3 Position)
      {
        this.transform.position = Position;
      }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        this.playerhealth.RemoveHeart(1);
    }

}
     
    

