using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouteInputModule : MouseClickInputModuleb
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
