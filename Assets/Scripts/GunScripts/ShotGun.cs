using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : Gun
{

    
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        _recoil = 2500f;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    
}
