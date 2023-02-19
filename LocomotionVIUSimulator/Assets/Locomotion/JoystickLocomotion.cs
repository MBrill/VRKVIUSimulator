//========= 2020 -  2023 - Copyright Manfred Brill. All rights reserved. ===========
using HTC.UnityPlugin.Vive;
using UnityEngine;

/// <summary>
/// Abstrakte Basisklasse für dien kontinuierliche Fortbewegung
/// in immersiven Anwendungen auf der Basis von VIU.
 /// </summary>
/// <remarks>
/// Diese Klasse ist von VRKL.MBU.Locomotion abgeleitet.
/// Dort sind bereits abstrakte Funktionen für die Fortbewegung
/// vorgesehen, die wir in den abgeleiteten Klassen einsetzen.
/// In der Basisklasse ist eine Variable ReverseButton vorgesehen,
/// die aber in der VR-Version nicht verwendet wird. Das kann man noch tun,
/// dann können wir einen Rückwärtsgang realisieren. Ob der wirklich
/// gebraucht wird in VR sehen wir dann noch.
///
/// In dieser Klasse kommen Geräte und Einstellungen für den
/// Inspektor dazu.
///
/// Nochmals auftrennen und die VIU-Funktionalität in eine
/// Klasse "Controller" auslagern, wie bei WIM und anderen
/// Techniken.
/// Dann ist es einfach später UXR zu verwenden!
/// </remarks>
public abstract class JoystickLocomotion : Locomotion
{
        [Header("Trigger Device")]
        /// <summary>
        /// Welchen Controller verwenden wir für das Triggern der Fortbewegung?
        /// </summary>
        /// <remarks>
        /// Als Default verwenden wir den Controller in der rechten Hand,
        /// also "RightHand" im "ViveCameraRig".
        /// </remarks>
        [Tooltip("Rechter oder linker Controller für den Trigger?")]
        public HandRole moveHand = HandRole.RightHand;

        /// <summary>
        /// Der verwendete Button, der die Bewegung auslöst, kann im Editor mit Hilfe
        /// eines Pull-Downs eingestellt werden.
        /// </summary>
        /// <remarks>
        /// Default ist "Trigger"
        /// </remarks>
        [Tooltip("Welchen Button verwenden wir als Trigger der Fortbewegung?")]
        public ControllerButton moveButton = ControllerButton.Trigger;

        [Header("Anfangsgeschwindigkeit")]
        /// <summary>
        /// Geschwindigkeit für die Bewegung der Kamera in km/h
        /// </summary>
        [Tooltip("Geschwindigkeit")]
        [Range(0.1f, 20.0f)]
        public float InitialSpeed = 5.0f; 
        
        /// <summary>
        /// Maximal mögliche Geschwindigkeit in km/h.
        /// </summary>
        [Tooltip("Maximal mögliche Geschwindigkeit")]
        [Range(0.001f, 20.0f)]
        public float MaximumSpeed = 10.0f;

        /// <summary>
        /// Delta für das Verändern der Geschwindigkeit in km/h.
        /// </summary>
        [Tooltip("Delta für die Veränderung der Bahngeschwindigkeit")]
        [Range(0.001f, 2.0f)]
        public float DeltaSpeed = 0.2f;
        
        /// <summary>
        /// Button auf dem Controller für das Abbremsen der Fortbewegung.
        /// </summary>
        /// <remarks>
        /// Default ist "Pad"
        /// </remarks>
        [Tooltip("Button für das Verkleinern der Bahngeschwindigkeit")] 
        public ControllerButton DecButton = ControllerButton.Pad;

        /// <summary>
        /// Button auf dem Controller für das Beschleunigen der Fortbewegung.
        /// </summary>
        /// <remarks>
        /// Default ist "Grip"
        /// </remarks>
        [Tooltip("Button für das Vergrößern der Bahngeschwindigkeit")]
        public ControllerButton AccButton = ControllerButton.Grip;
        

        ///<summary>
        /// Richtung, Geschwindigkeit aus der Basisklasse initialisieren und weitere
        /// Initialisierungen durchführen, die spezifisch für VR sind.
        /// </summary>
        /// <remarks>
        /// Die Callbacks für Beschleunigung und Abbremsen in der VIUregistrieren.
        /// </remarks>
        protected void OnEnable()
        {
            ViveInput.AddListenerEx(moveHand, DecButton, 
                                                 ButtonEventType.Down,  
                                                 m_Velocity.Decrease);
            ViveInput.AddListenerEx(moveHand, AccButton, 
                                                 ButtonEventType.Down,
                                                 m_Velocity.Increase);
        }

        /// <summary>
        /// Die Callbacks in der VIU wieder abhängen.
        /// </summary>
        protected void OnDisable()
        {
             ViveInput.RemoveListenerEx(moveHand, DecButton, 
                                                         ButtonEventType.Down,  
                                                         m_Velocity.Decrease);
            ViveInput.RemoveListenerEx(moveHand, AccButton, 
                                                        ButtonEventType.Down, 
                                                        m_Velocity.Increase);
        }
        
        /// <summary>
        /// Update aufrufen und die Bewegung ausführen.
        /// </summary>
        /// <remarks>
        ///Wir verwenden den forward-Vektor des
        /// Orientierungsobjekts als Bewegungsrichtung.
        /// </remarks>
        protected virtual void Update()
        {
            UpdateDirection();
            UpdateSpeed();
            Trigger();
            Move();
        }

        /// <summary>
        /// Die Bewegung durchführen.
        /// </summary>
        /// <remarks>
        /// Die Bewegung wird durchgeführt, wenn eine in dieser Klasse
        /// deklarierte logische Variable true ist.
        /// <remarks>
        protected override void Move()
        {
            if (Moving)
                transform.Translate(m_Speed * Time.deltaTime * m_Direction);
        }
        
        /// <summary>
        /// Berechnung der Geschwindigkeit der Fortbewegung
        /// </summary>
        /// <remarks>
        /// Wir rechnen die km/h aus dem Interface durch Division
        /// mit 3.6f in m/s um.
        /// </remarks>
        protected override void UpdateSpeed()
        {
            m_Speed = m_Velocity.Value/3.6f;
        }
        
        /// <summary>
        /// Geschwindigkeit initialiseren. Wir überschreiben diese
        /// Funktion in den abgeleiteten Klassen und rufen
        /// diese Funktion in Locomotion::Awake auf.
        /// </summary>
        protected override void InitializeSpeed()
        {
            m_Velocity = new LinearBlend(InitialSpeed, DeltaSpeed, 
                                                                      0.0f, MaximumSpeed);
            m_Speed = m_Velocity.Value/3.6f;
        }

        /// <summary>
        ///Die von JoystickLocomotion abgeleiteten Klassen entscheiden wie die Bewegung
        /// getriggert wird. Mit einem gehaltenen Button, zwischen zwei Button-
        /// Clicks oder mit Hilfe anderer Dinge wie Bewegungen und Gesten.
        /// </summary>
        /// <remarks>
        /// Als Default-Behaviour implementieren wir das bisher verwendete
        /// Verhalten - die Bewegung findet so lange statt, wie ein ebenfalls
        /// in dieser Klasse deklariertes Trigger-Device und ein Button darauf
        /// gedrückt ist.
        /// </remarks>
        protected virtual void Trigger()
        {
            Moving = ViveInput.GetPress(moveHand, moveButton);
        }
}
