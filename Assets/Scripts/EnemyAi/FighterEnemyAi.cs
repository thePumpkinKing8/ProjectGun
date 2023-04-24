using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterEnemyAi : MonoBehaviour
{
    [SerializeField] private float _jumpForce = 10f;
    private bool _canBeHit;
    private Rigidbody2D _rb;
    private SpriteRenderer _sprite;
    public float speed = 3f;
    [SerializeField] private float _health = 8;
    private Player_Controller _player;
    private Transform _playerLocation;
    private float _baseKnockBack = 100;
    private float _distance;
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


    IEnumerator HitDelay() //prevents ai from getting stunlocked
    {
        yield return new WaitForSeconds(.5f);
        _sprite.color = Color.white;
        _canBeHit = true;
    }




    IEnumerator JumpAttack() //ai stops and charges an attack before lunging at the player
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

    void OnCollisionEnter2D(Collision2D collision) //checks if attack onnects with player
    {
        if(_isAttacking == true)
        {
            if(collision.transform.tag == "Player")
            {
                collision.transform.GetComponent<Player_Controller>()._takeDamage();
                _rb.AddForce(-_rb.velocity);
            }
        }
    }



    // Update is called once per frame
    void Update()
    {

        _distance = Vector2.Distance(transform.position, _playerLocation.transform.position);
        Vector2 direction = _playerLocation.transform.position - transform.position;
        if(_distance < 5f) //spider will only attack player if player is close enough
        {
            _isActive = true;
        }
        if(_isActive == true)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, _playerLocation.transform.position, speed * Time.deltaTime);
        }
        
        if(_distance <= 3f && _isAttacking == false) 
        {
            StartCoroutine(JumpAttack());
        }
    }

    
   
    public void TakeDamage(float angle, float damage) //deals damage to the spider and knocks it backwards
    {
        if(_canBeHit == true)
        {
            _health -= damage;
            Vector2 direction = (new Vector2(-((_baseKnockBack * damage) * Mathf.Cos(angle * Mathf.Deg2Rad)), -((_baseKnockBack * damage) * Mathf.Sin(angle * Mathf.Deg2Rad))));
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

    private void _die() //kills the spider when called
    {
        StopAllCoroutines();
        Destroy(this.gameObject);
    }

}
