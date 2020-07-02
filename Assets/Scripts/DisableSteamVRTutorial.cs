using UnityEngine;
using Valve.VR.InteractionSystem;

public class DisableSteamVRTutorial : MonoBehaviour
{

    void Update()
    {
        Teleport.instance.CancelTeleportHint();
    }
}
