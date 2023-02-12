using HTC.UnityPlugin.ColliderEvent;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Handling der Events HoverEnter, HoverExit,
/// PressEnter und PressExit für einen einzelnen Cube in CCC.
/// </summary>
/// <remarks>
/// Voraussetzung: Das Prefab ViveColliders ist in der Szene enthalten.
/// Der Contoller-Button der für die Press-Events eingesetzt
/// wird wird im übergeordneten GameObject CCC gesetzt
/// und hier abgefragt.
/// </remarks>
public class CCCubeEventManager : MonoBehaviour, 
    IColliderEventHoverEnterHandler,
    IColliderEventHoverExitHandler,
    IColliderEventPressEnterHandler,
    IColliderEventPressExitHandler
{
    /// <summary>
    /// Event, der im Inspektor dem Würfel zugeordnet werden kann.
    /// </summary>
    /// <remarks>
    /// Event wird aufgerufen, wenn wir in PressExit gehen.
    /// </remarks>
    [Tooltip("Event für diesen Button")]
    public UnityEvent MyEvent = new UnityEvent();
    
    /// <summary>
   ///  Funktion, die bei HoverEnter aufgerufen wird
   /// </summary>
   /// <param name="eventData"></param>
    public void OnColliderEventHoverEnter(ColliderHoverEventData eventData)
    {
        Debug.Log("Berührung hat begonnen!");
    }

   /// <summary>
   ///  Funktion, die bei HoverExit aufgerufen wird
   /// </summary> 
   public void OnColliderEventHoverExit(ColliderHoverEventData eventData)
    {
        Debug.Log("Berührung ist beendet!");
    }
    
   /// <summary>
   ///  Funktion, die bei PressEnter aufgerufen wird
   /// </summary> 
   public void OnColliderEventPressEnter(ColliderButtonEventData eventData)
    {
        Debug.Log("Selektion hat stattgefunden!");
    }

   /// <summary>
   ///  Funktion, die bei PressExit aufgerufen wird
   /// </summary> 
   public void OnColliderEventPressExit(ColliderButtonEventData eventData)
    {
        Debug.Log("Event wird ausgelöst!");
        MyEvent.Invoke();
        m_logEvent.Invoke();
    }

   /// <summary>
   /// Initialisieren
   /// </summary>
   void Awake()
   {
       // Im übergeordneten GameObject CCC nachsehen, welchen
       // Button wir eingestellt haben.
       var m_triggerButton = GetComponentInParent<ColliderButtonEventData.InputButton>();
       // Callbacks registrieren
       MyEvent.AddListener(m_Logging);
       m_logEvent.AddListener(m_Logging);
   }
   
   /// <summary>
   /// Button, der in CCC eingestellt ist.
   /// </summary>
   private ColliderButtonEventData.InputButton m_triggerButton;
   /// <summary>
   /// Unity-Event mit einem Callback, der mit Hilfe von Log4net
   /// die Events protokolliert. Ob die Protokollierung auf der Konsole
   /// oder eine Datei durchgeführt wird entscheiden wir in einer
   /// Konfigurationsdatei.
   /// </summary>
   private UnityEvent m_logEvent = new UnityEvent();

   private void m_Logging()
   {
       // Logging in log4back.
       Debug.Log("Event ausgelöst!");
   }

}
