using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class EnemyAIProto : MonoBehaviour
{
    private Transform player; 

    public float speed = 1;

    //private float distance;

    public float FloatRange = 4;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);

        if (distanceFromPlayer > FloatRange)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
        }

        

        Vector2 direction = player.transform.position - transform.position;

    }


    private void OnDrawGizmosSelceted()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, FloatRange);


    }

}
