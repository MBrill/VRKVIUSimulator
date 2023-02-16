//========= 2021 - 2023 Copyright Manfred Brill. All rights reserved. ===========
using HTC.UnityPlugin.Vive;

/// <summary>
/// Walk mit Start und Stop mit Hilfe des Button mit VIU
/// </summary>
public class WalkSteady : Walk
{
        /// <summary>
        /// Walk wird so lange durchgeführt bis der Trigger-Button
        /// wieder gedrückt wird.
        /// </summary>
        protected override void Trigger()
        {
            if (ViveInput.GetPressUp(moveHand, moveButton))
                Moving = !Moving;
        }
}
