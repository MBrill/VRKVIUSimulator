//========= 2021 - 2023 - Copyright Manfred Brill. All rights reserved. ===========
using UnityEngine;

/// <summary>
/// Abstrakte Basisklasse für die Realisierung von Locomotion-Verfahren,
/// die die Differenz zweier Objekte
/// für die Definition der Bewegungsrichtung verwenden.
/// </summary>
/// <remarks>
///Der Differenzvektor wird normalisiert.
/// </remarks>
public abstract class TwoObjectsDirection : JoystickLocomotion
{
         [Header("Two Objekts Locomotion")]  
        /// <summary>
        /// GameObject, das den Startpunkt der Bewegungsrichtung definiert
        /// </summary>
        [Tooltip("Startpunkt der Bewegungsrichtung")]
        public GameObject StartObject;

        /// <summary>
        /// GameObject, das den Endpunkt der Bewegungsrichtung definiert
        /// </summary>
        [Tooltip(" Endpunkt der Bewegungsrichtung")]
        public GameObject EndObject;
        
        [Tooltip("Schwellwert für das Auslösen der Fortbewegung")]
        [Range(0.1f, 1.5f)]
        public float Threshold = 1.0f;
        
        /// <summary>
        /// Bewegungsrichtung als Differenz der forward-Vektoren
        /// der beiden definierenden Objekte setzen.
        /// </summary>
        protected override void InitializeDirection()
        {
            m_Direction = EndObject.transform.position-StartObject.transform.position;
            m_Direction.Normalize();
        }
        
        /// <summary>
        /// Auslösen der Bewegung.
        /// </summary>
        /// <remarks>
        /// Die Fortbewegung wird ausgelöst, falls der Abstand zwischen
        /// den beiden Objekten, mit denen wir die Richtung der Fortbewegung
        /// steuern größer als ein Schwellwert ist.
        /// </remarks>
        protected override void Trigger()
        {
            var distance = Vector3.Magnitude(EndObject.transform.position - StartObject.transform.position);
            Moving = distance > Threshold;
        }

}

