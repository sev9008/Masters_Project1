using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nonVRInputModule : nonVRInputModuleb
{
    public override void Process()
    {
        base.Process();

        if (Input.GetButtonDown("Fire1"))
        {
            Press();
        }

        if (Input.GetButtonUp("Fire1"))
        {
            Release();
        }
    }
}
