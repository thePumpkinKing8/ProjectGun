using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    protected float _recoil;
    protected int _ammoCap;
    protected float damage;
    protected Rigidbody2D _rb;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        _rb = GetComponentInParent<Rigidbody2D>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
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
       
        Vector2 direction = (new Vector2(-(_recoil * Mathf.Cos(angle * Mathf.Deg2Rad)), -(_recoil * Mathf.Sin(angle * Mathf.Deg2Rad))));
        _rb.AddForce(direction);
        Debug.Log(angle);
       
    }    
    
    
        
    
}
