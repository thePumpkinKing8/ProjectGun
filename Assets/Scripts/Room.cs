using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public void Unload()
    {
        Destroy(this.gameObject);
    }
}
