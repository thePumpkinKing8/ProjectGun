using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] protected float _recoil;
    [SerializeField] protected int _ammoCap;
    [SerializeField] protected float _reloadTime;
    protected float _time;
    protected int _ammo;
    protected float _damage;
    protected Rigidbody2D _rb;
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
        Vector3 direction = mousePosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        
        if(Input.GetMouseButtonDown(0))
        {
            _shoot(angle);
        }
    }

    protected virtual void _shoot(float angle)
    {
       if(_ammo >= 0)
        {
            //trig equation to calculate the how much force to place in the x and y direction and then applies it to the player
            Vector2 direction = (new Vector2(-(_recoil * Mathf.Cos(angle * Mathf.Deg2Rad)), -(_recoil * Mathf.Sin(angle * Mathf.Deg2Rad))));
            _rb.AddForce(direction);
            //_ammo -= 1;
            Debug.Log(angle);
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

   
    
    
        
    
}
