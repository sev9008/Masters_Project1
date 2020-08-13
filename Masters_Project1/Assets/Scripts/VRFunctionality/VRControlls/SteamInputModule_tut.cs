﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class SteamInputModule_tut : VRInputModule
{
    public SteamVR_Input_Sources TargetSource;
    public SteamVR_Action_Boolean ClickAction = null;

    public override void Process()
    {
        base.Process();

        if (ClickAction.GetStateDown(TargetSource))
        {
            Press();
        }

        if (ClickAction.GetStateUp(TargetSource))
        {
            Release();
        }        
    }
}
