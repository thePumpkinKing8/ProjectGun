using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] protected float _recoil;
    [SerializeField] protected int _ammoCap;
    [SerializeField] protected float _reloadTime;
    [SerializeField] protected GameObject _barrel;
    protected float _time;
    protected int _ammo;
    protected float _damage;
    protected Rigidbody2D _rb;
    protected Vector3 _direction;
    [SerializeField] protected GameObject _sprite;
    protected bool _active;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        _rb = GetComponentInParent<Rigidbody2D>();
        _ammo = _ammoCap;
    }

    // Update is called once per frame
    protected virtual void Update()
    {


        

        //finds the angle of the mouse relative to the gun and rotates it
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _direction = mousePosition - transform.position;
        float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        
        if(Input.GetMouseButtonDown(0) && _active == true)
        {
            _shoot(angle);
        }

        if(_active == false) //will only reload when gun is not active
        {
            _reload();
        }
    }

    protected virtual void _shoot(float angle)
    {
       if(_ammo >= 0)
        {
           RaycastHit2D hit = Physics2D.Raycast(_barrel.transform.position, _direction, Mathf.Infinity, ~(1 << 8));
            if(hit.collider != null)
            {
                Debug.Log(hit.transform.name);
            }
           
             
            //trig equation to calculate the how much force to place in the x and y direction and then applies it to the player
            Vector2 direction = (new Vector2(-(_recoil * Mathf.Cos(angle * Mathf.Deg2Rad)), -(_recoil * Mathf.Sin(angle * Mathf.Deg2Rad))));
            _rb.AddForce(direction);
            _ammo -= 1;
        }
       
    }
    //reloads gun after set time
    protected void _reload()
    {
        _time += Time.deltaTime; 
        if(_time >= _reloadTime)
        {
            _ammo = _ammoCap;
            _time = 0;
        }
    }

    // sets the gun to be active
    public void Activate()
    {
        _sprite.SetActive(true);
        _active = true;
    }
    public void Deactivate()
    {
        _sprite.SetActive(false);
        _active = false;
    }

   
    
    
        
    
}
