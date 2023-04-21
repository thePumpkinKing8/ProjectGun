using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterEnemyAi : MonoBehaviour
{
    [SerializeField] private float _jumpForce = 10f;
    [SerializeField] private float _movementSmoothing = 0.5f;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Transform _groundCheck;
    const float _groundCheckRadius = .2f;
    private bool _isGrounded;
    private bool _canBeHit;
    private Vector3 _velocity = Vector3.zero;
    private Rigidbody2D _rb;
    private bool _jumped;
    private SpriteRenderer _sprite;
    public float speed = 3f;
    [SerializeField] private float _health = 8;
    private Player_Controller _player;
    private Transform _playerLocation;
    private float BaseKnockBack = 100;
    private float distance;
    private bool _isAttacking;
    private bool _isActive;


    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
        _player = GameManager.Instance.Player;
        _playerLocation = _player.GetComponent<Transform>();
        _canBeHit = true;
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

        
    }


    IEnumerator HitDelay()
    {
        yield return new WaitForSeconds(.5f);
        _sprite.color = Color.white;
        _canBeHit = true;
    }




    IEnumerator JumpAttack()
    {
        _isAttacking = true;
        _sprite.color = Color.yellow;
        float speedContainer = speed;
        speed = 0;
        yield return new WaitForSeconds(.5f);
        Vector2 enemyToPlayer = _playerLocation.position - this.transform.position;
        enemyToPlayer.Normalize();
        _rb.AddForce(enemyToPlayer * _jumpForce);
        yield return new WaitForSeconds(2f);
        _sprite.color = Color.white;
        speed = speedContainer;
        yield return new WaitForSeconds(3f);
        _isAttacking = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(_isAttacking)
        {
            if(collision.transform.tag == "Player")
            {
                collision.transform.GetComponent<Player_Controller>()._takeDamage();
                //_rb.AddForce(-_rb.velocity);
            }
        }
    }



    // Update is called once per frame
    void Update()
    {

        distance = Vector2.Distance(transform.position, _playerLocation.transform.position);
        Debug.Log(distance);
        Vector2 direction = _playerLocation.transform.position - transform.position;
        if(distance < 5f)
        {
            _isActive = true;
        }
        if(_isActive == true)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, _playerLocation.transform.position, speed * Time.deltaTime);
        }
        
        if(distance <= 3f && _isAttacking == false) 
        {
            StartCoroutine(JumpAttack());
        }
    }

    
   
    public void TakeDamage(float angle, float damage)
    {
        if(_canBeHit == true)
        {
            _health -= damage;
            Vector2 direction = (new Vector2(-((BaseKnockBack * damage) * Mathf.Cos(angle * Mathf.Deg2Rad)), -((BaseKnockBack * damage) * Mathf.Sin(angle * Mathf.Deg2Rad))));
            _rb.AddForce(-direction);
            _sprite.color = Color.red;
            _canBeHit = false;
            if(_health <= 0)
            {
                _die();
            }
        StartCoroutine(HitDelay());
        }
    }

    private void _die()
    {
        StopAllCoroutines();
        Destroy(this.gameObject);
    }

}
