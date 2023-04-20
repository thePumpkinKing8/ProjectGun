using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{

    [SerializeField]
    private int _MaxNumberofHearts = 8; //Players Full health, Give player 1 extra health to sovle bug. 

    [SerializeField]
    private int _NumberofHearts = 8; // Must match _MaxNumberofHearts.

    [SerializeField]
    private float HeartWidth = 50.0f; // Size of the Image to remove a heart

    private RectTransform _rect;

    // Start is called before the first frame update


    public int NumberofHearts
    {
        get => _NumberofHearts;
       private set
        {

            if (value == 0) //when the player loses all hearts
            {
                SceneManager.LoadScene("SampleScene");
            }

            _NumberofHearts = Mathf.Clamp(value, min:0, max:_MaxNumberofHearts);
            _rect.sizeDelta = new Vector2(x:HeartWidth * _NumberofHearts, _rect.sizeDelta.y); // alter number of hearts on the screen
        }
    }


    private void Awake()
    {
        _rect = transform as RectTransform;

        this.RemoveHeart(1); //Remove the extra heart from the player to prevent bug
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
