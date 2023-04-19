using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    [SerializeField]
    private int _MaxNumberofHearts = 8;

    [SerializeField]
    private int _NumberofHearts = 8;

    [SerializeField]
    private float HeartWidth = 45.0f;

    private RectTransform _rect;

    // Start is called before the first frame update


    public int NumberofHearts
    {
        get => _NumberofHearts;
       private set
        {

            if (value < 0) //when the player loses all hearts
            {

            }

            _NumberofHearts = Mathf.Clamp(value, min:0, max:_MaxNumberofHearts);
            _rect.sizeDelta = new Vector2(x:HeartWidth * _NumberofHearts, _rect.sizeDelta.y); // alter number of hearts on the screen
        }
    }


    private void Awake()
    {
        _rect = transform as RectTransform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddHeart(int num = 1) //Heal
    {
        NumberofHearts += num;
    }

    public void RemoveHeart(int num = 1) //Take damage
    {
        NumberofHearts -= num;
    }

}
