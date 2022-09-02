using UnityEngine;
using HTC.UnityPlugin.Vive;

/// <summary>
/// Highlighter f�r ein GameObject,
/// abh�ngig von einem Tastendruck auf einem Controller
/// als erstes Beispiel f�r eine Interaktion mit VIU.
/// <remarks>
/// In dieser Version kann ausgew�hlt werden, ob wir den rechten
/// oder den linken Controller verwenden.
/// </remarks>
/// </summary>
public class HighlightWithHand : MonoBehaviour
{
    /// <summary>
    /// Die Farbe dieses Materials wird f�r die ge�nderte Farbe verwendet.
    /// </summary>
    [Tooltip("Material f�r das Highlight")]
    public Material highlightMaterial;

    /// <summary>
    /// Welche Hand wollen wir verwenden?
    /// </summary>
    /// <remarks>
    ///Default ist die rechte Hand.
    /// </remarks>
    [Tooltip("Welcher Controller (links/rechts) soll f�r das Highlight verwendet werden?")]
    public HandRole MainHand = HandRole.RightHand;
    
    /// <summary>
    /// Button, der den Event ausl�st.
    /// 
    /// Default ist der Trigger des Controllers.
    /// </summary>
    [Tooltip("Welcher Button auf dem Controller soll verwendet werden?")]
    public ControllerButton theButton = ControllerButton.Trigger;

    private bool m_status = false;

    /// <summary>
    /// Variable, die das Original-Material des Objekts enth�lt
    /// </summary>
    private Material myMaterial;
    /// <summary>
    /// Wir fragen die Materialien ab und speichern die Farben als Instanzen
    /// der Klasse Color ab.
    /// </summary>
    private Color originalColor, highlightColor;

    /// <summary>
    /// Wir fragen das Material und die Farbe ab und setzen
    /// die Highlight-Farbe aus dem zugewiesenen Material.
    /// </summary>
    private void Awake()
    {
        myMaterial = GetComponent<Renderer>().material;
        originalColor = myMaterial.color;
        highlightColor = highlightMaterial.color;
    }

    /// <summary>
    /// Listener f�r den Controller registrieren
    /// </summary>
    private void OnEnable()
    {
        ViveInput.AddListenerEx(MainHand,
                                theButton,
                                ButtonEventType.Down,
                                changeColor);

        ViveInput.AddListenerEx(MainHand,
                                theButton,
                                ButtonEventType.Up,
                                changeColor);
    }

    /// <summary>
    /// Listener wieder aus der Registrierung
    /// herausnehmen beim Beenden der Anwendung
    /// </summary>
    private void OnDestroy()
    {
        ViveInput.RemoveListenerEx(MainHand,
                                   theButton,
                                   ButtonEventType.Down,
                                   changeColor);

        ViveInput.RemoveListenerEx(MainHand,
                                   theButton,
                                   ButtonEventType.Up,
                                   changeColor);
        
    }
    
    private void changeColor()
    { 
        if (!m_status)
            myMaterial.color = highlightColor;
        else
            myMaterial.color = originalColor; 
        
         m_status = !m_status;
    }
}