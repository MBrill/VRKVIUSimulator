//========= 2021 - 2023 Copyright Manfred Brill. All rights reserved. ===========
using HTC.UnityPlugin.Vive;

/// <summary>
/// Walk mit Start und Stop mit Hilfe des Button mit VIU
/// </summary>
public class WalkSteady : Walk
{
        /// <summary>
        /// Walk wird so lange durchgef�hrt bis der Trigger-Button
        /// wieder gedr�ckt wird.
        /// </summary>
        protected override void Trigger()
        {
            if (ViveInput.GetPressUp(moveHand, moveButton))
                Moving = !Moving;
        }
}
