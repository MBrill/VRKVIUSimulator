//========= 2020 - 2023 - Copyright Manfred Brill. All rights reserved. ===========
using UnityEngine;

    /// <summary>
    /// Abstrakte Basisklasse für die Fortbewegung  in VR.
    /// </summary>
    public abstract class Locomotion : MonoBehaviour
    {
        /// <summary>
        /// Festlegen der Bewegungsrichtung.
        /// </summary>
        /// <remarks>
        /// Bewegungsrichtung als normalisierte Vector3-Instanz.
        /// Wenn diese Funktion nicht überschrieben wird verwenden
        /// wir forward des GameObjects, an dem die Komponente
        /// hängt.
        /// </remarks>
        protected virtual void InitializeDirection()
        {
            Direction = transform.forward;
        }

        /// <summary>
        /// Orientierung initialiseren. Wir überschreiben diese
        /// Funktion in den abgeleiteten Klassen und rufen
        /// diese Funktion in Locomotion::Awake  auf.
        /// </summary>
        protected virtual void InitializeOrientation()
        {
            Orientation = new Vector3(0.0f, 0.0f, 0.0f);
        }
        
        /// <summary>
        /// Update  der Bewegungsrichtung.
        /// </summary>
        protected virtual void UpdateDirection()
        {
            Direction = transform.forward;
        }
        
        /// <summary>
        /// Berechnung der Geschwindigkeit der Fortbewegung
        /// </summary>
        protected abstract void UpdateSpeed();

        /// <summary>
        /// Orientierung für die Bewegung als Eulerwinkel.
        /// </summary>
        /// <remarks>
        /// Orientierungen als Instanz von Vector3.
        /// </remarks>
        protected abstract void UpdateOrientation();

        /// <summary>
        /// Geschwindigkeit initialiseren. Wir überschreiben diese
        /// Funktion in den abgeleiteten Klassen und rufen
        /// diese Funktionin Locomotion::Awake auf.
        /// </summary>
        protected abstract void InitializeSpeed();

        /// <summary>
        /// Initialisieren
        /// </summary>
        protected virtual void Awake()
        {
            // Bewegungsrichtung, Orientierung und Bahngeschwindigkeit initialisieren
            InitializeDirection();
            InitializeOrientation();
            InitializeSpeed();
        }

        /// <summary>
        /// Die Bewegung durchführen.
        /// </summary>
        /// <remarks>
        /// Die Bewegung wird durchgeführt, wenn eine in dieser Klasse
        /// deklarierte logische Variable true ist.
        /// 
        /// Wir bewegen uns in Richtung des Vektors Direction,
        /// er typischer Weise auf forward des GameObjects gesetzt wird.
        ///
        /// Wir orientieren das Objekt mit Hilfe der Eulerwinkel in Orientation
        /// und führen anschließend eine Translation in Richtung Direction durch.
        /// <remarks>
        protected virtual void Move()
        {
            if (m_moving)
            {
                transform.eulerAngles = Orientation;
                transform.Translate(Speed * Time.deltaTime * Direction);
            }
        }

        /// <summary>
        /// Bewegung ist durch einen Trigger ausgelöst worden.
        /// <remarks>
        /// Ob die Bewegung mit Hilfe eines gedrückten Buttons erfolgt
        /// oder durch zwei Button-Clicks ausgelöst und beendet wird müssen die
        /// davon abgeleiteten Klassen entscheiden!
        /// </remarks>
        /// </summary>
        private bool m_moving;

        protected bool Moving
        {
            get => m_moving;
            set => m_moving = value;
        }

        /// <summary>
        /// Betrag der Geschwindigkeit für die Bewegung
        /// <remarks>
        /// Einheit dieser Variable ist m/s.
        /// </remarks>
        /// </summary>
        protected float Speed;

        /// <summary>
        /// Vektor mit den Eulerwinkeln für die Kamera
        /// </summary>
        protected Vector3 Orientation;

        /// <summary>
        /// Klasse für die Verwaltung der Bahngeschwindigkeit.
        /// </summary>
        protected ScalarProvider Velocity;

        /// <summary>
        /// Normierter Richtungsvektor für die Fortbewegung.
        /// </summary>
        /// <remarks>
        /// In den VR-Varianten wird die Richtung direkt
        /// aus dem forward-Vektor des Orientierungsobjekts
        /// gesetzt.
        /// </remarks>
        protected Vector3 Direction;
}

