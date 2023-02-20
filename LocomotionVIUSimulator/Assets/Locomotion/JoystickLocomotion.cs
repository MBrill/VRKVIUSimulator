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
    [Header("Locomotion")]
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
        /// Die abgeleiteten Klassen entscheiden, wann die Locomotion
        /// getriggert werden.
        /// </summary>
        protected virtual void Trigger() { }
}
