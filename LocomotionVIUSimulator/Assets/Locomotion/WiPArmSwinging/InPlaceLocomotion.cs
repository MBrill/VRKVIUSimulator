//========= 2021 - 2023 Copyright Manfred Brill. All rights reserved. ===========
using UnityEngine;

/// <summary>
/// Abstrakte Basisklasse für Locomotion-Verfahren,
/// bei denen etwas "auf der Stelle" durchgeführt wird
/// wie Walking-in-Place oder Arm-Swinging.
/// </summary>
/// <remarks>
/// Diese Klasse ist von Locomotion abgeleitet.
///
/// Welche getrackten Objekte wir verwenden und wie viele davon
/// legen wir in den von dieser Klasse abgeleiteten Klassen fest!
/// </remarks>
public abstract class InPlaceLocomotion : Locomotion
{
        [Header("Walking-in-Place")]
        /// <summary>
        /// Welches GameObject verwenden wir für die Definition der Richtung?
        /// </summary>
        /// <remarks>
        /// Sinnvoll ist bei Walking-in-Place der Kopf, oder irgend ein anderes
        /// getracktes GameObject.
        /// </remarks>
        [Tooltip("Welches Objekt definiert die Bewegungsrichtung?")]
        public GameObject orientationObject;
        
        /// <summary>
        /// Geschwindigkeit für die Bewegung der Kamera in km/h.
        /// </summary>
        [Tooltip("Geschwindigkeit in km/h")]
        public float InitialSpeed = 1.0f; 
        
        [Tooltip("Schwellwert für das Auslösen der Bewegung")] 
        [Range(0.01f, 1.0f)]
        public float Threshold = 0.05f;
        
        
        /// <summary>
        /// Update aufrufen und die Bewegung ausführen.
        /// </summary>
        /// <remarks>
        /// Wir verwenden den forward-Vektor des
        /// Orientierungsobjekts als Bewegungsrichtung.
        ///
        /// Deshalb verwenden wir hier nicht die Funktion
        /// UpdateOrientation, sondern setzen die Bewegungsrichtung
        /// direkt.
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
        /// Die Bewegung wird durchgeführt, falls die Variable
        /// Moving in der Basisklasse true ist.
        ///
        /// Darüber entscheiden wir in der Funktion Trigger..
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
            // Wir verändern die Geschwindigkeit nicht ...
            m_Velocity = new LinearBlend(InitialSpeed, 0.001f,
                0.0f, 2.0f * InitialSpeed);
            m_Speed = m_Velocity.Value/3.6f;
        }

        /// <summary>
        /// Die abgeleiteten Klassen entscheiden, wann die Locomotion
        /// getriggert werden.
        /// </summary>
        protected abstract void Trigger();

        /// <summary>
        /// Bewegungsrichtung auf den forward-Vektor des Orientierungsobjekts setzen.
        /// </summary>
        protected override void UpdateDirection()
        {
            m_Direction = orientationObject.transform.forward;
            m_Direction.y = 0.0f;
            m_Direction.Normalize();
        }
        
        /// <summary>
        /// Bewegungsrichtung auf den forward-Vektor des Orientierungsobjekts setzen.
        /// </summary>
        /// <remarks>
        /// Vorerst protected gesetzt. Mittelfristig werden wir
        /// die Geschwindigkeit aus der Bewegung auf der Stelle
        /// herauslesen.
        /// </remarks>
        protected override void InitializeDirection()
        {
            m_Direction = orientationObject.transform.forward;
            m_Direction.y = 0.0f;
            m_Direction.Normalize();
        }
        
        /// <summary>
        /// Update der Orientierung des GameObjects,
        /// das die Bewegungsrichtung definiert..
        /// </summary>
        /// <remarks>
        /// Für die Verarbeitung der Orientierung verwenden wir
        /// die Eulerwinkel der x- und y-Achse.
        ///
        /// Wird aktuell nicht verwendet, da wir die Bewegungsrichtung
        /// direkt aus dem forward-Vektor des Orientierungsobjekts
        /// ablesen.
        /// </remarks>
        protected override void UpdateOrientation()
        {
            throw new System.NotImplementedException();
        }
}
