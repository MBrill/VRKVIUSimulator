using UnityEngine;
using HTC.UnityPlugin.Vive;

public class WiMVIUController : WiM
{
    /// <summary>
    /// Der verwendete Button kann im Editor mit Hilfe
    /// eines Pull-Downs eingestellt werden.
    /// 
    /// Default ist der Trigger des Controllers.
    /// </summary>
    [Tooltip("Welcher Button auf dem Controller soll verwendet werden?")]
    public ControllerButton TheButton = ControllerButton.Trigger;
    
    /// <summary>
    ///Die Listerner registrieren.
    /// </summary>
    /// <remarks>
    ///In dieser Version registrieren wir beide Controller.
    /// </remarks>
    private void OnEnable()
    {
        ViveInput.AddListenerEx(HandRole.RightHand,
            TheButton,
            ButtonEventType.Down,
            ToggleShow);
        

        ViveInput.AddListenerEx(HandRole.LeftHand,
            TheButton,
            ButtonEventType.Down,
            ToggleShow);
    }
    
    /// <summary>
    /// Listener wieder aus der Registrierung
    /// herausnehmen beim Beenden der Anwendung
    /// </summary>
    private void OnDestroy()
    {
        ViveInput.RemoveListenerEx(HandRole.RightHand,
            TheButton,
            ButtonEventType.Down,
            ToggleShow);

        ViveInput.RemoveListenerEx(HandRole.RightHand,
            TheButton,
            ButtonEventType.Up,
            ToggleShow);

        ViveInput.RemoveListenerEx(HandRole.LeftHand,
            TheButton,
            ButtonEventType.Down,
            ToggleShow);

        ViveInput.RemoveListenerEx(HandRole.LeftHand,
            TheButton,
            ButtonEventType.Up,
            ToggleShow);
    }
}
