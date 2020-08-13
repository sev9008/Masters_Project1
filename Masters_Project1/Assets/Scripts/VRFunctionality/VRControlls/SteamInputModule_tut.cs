using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class SteamInputModule_tut : VRInputModule
{
    public SteamVR_Input_Sources TargetSourceLeft;
    public SteamVR_Input_Sources TargetSourceRight;
    public SteamVR_Action_Boolean ClickAction = null;

    public override void Process()
    {
        base.Process();

        if (ClickAction.GetStateDown(TargetSourceLeft))
        {
            Press();
        }

        if (ClickAction.GetStateUp(TargetSourceLeft))
        {
            Release();
        }        
        if (ClickAction.GetStateDown(TargetSourceRight))
        {
            Press();
        }

        if (ClickAction.GetStateUp(TargetSourceRight))
        {
            Release();
        }
    }
}
