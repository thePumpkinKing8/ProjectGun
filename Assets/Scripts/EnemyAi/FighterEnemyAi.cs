using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterEnemyAi : MonoBehaviour
{
    [SerializeField] private float _jumpForce = 400f;
    [SerializeField] private float _movementSmoothing = 0.5f;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Transform _groundCheck;
    const float _groundCheckRadius = .2f;
    private bool _isGrounded;
    private Vector3 _velocity = Vector3.zero;
    private Rigidbody2D _rb;
    private bool _jumped;
    public float speed = 3f;

    private Transform player;

    private float distance;


    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

       player = GameObject.Find("Player").transform;

    }

    void FixedUpdate()
    {
        //creates circle collider that checks to see if any gameobjects within its radius are part of the ground layer and sets grounded to true if it does
        Collider2D collider = Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _groundLayer);
        if (collider != null)
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

        Move((Input.GetAxisRaw("Horizontal") * speed) * Time.fixedDeltaTime, _jumped);
        _jumped = false;
    }

    // Update is called once per frame
    void Update()
    {

        distance = Vector2.Distance(transform.position, player.transform.position);

        Vector2 direction = player.transform.position - transform.position;

        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);

    }

    public void Move(float move, bool jump)
    {
        Vector3 targetVelocity = new Vector2(move * 10f, _rb.velocity.y);
        _rb.velocity = Vector3.SmoothDamp(_rb.velocity, targetVelocity, ref _velocity, _movementSmoothing);
        if (_isGrounded && jump)
        {
            _rb.AddForce(new Vector2(0f, _jumpForce));
            Debug.Log("jumped");
        }
    }

}
