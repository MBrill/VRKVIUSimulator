//========= 2021 - 2023 - Copyright Manfred Brill. All rights reserved. ===========

/// <summary>
/// Fly als Locomotion in einer VR-Anwendung, mit zwei Objekten f�r
/// die Definition der Bewegungsrichtung.
/// </summary>
/// <remarks>
/// Fly bedeutet, dass wir die Bewegungsrichtung in allen drei
/// Koordinatenachsen ver�ndern k�nnen.
///
/// Wir verwenden einen Trigger-Button. So lange dieser Button
/// gedr�ckt ist wird die Bewegung ausgef�hrt.
/// 
/// Als Bewegungsrichtung verwenden wir den Differenzvektor
/// zweier Objekte, typischer Weise die Controller. M�glich ist
/// auch den Kopf als einer der Objekte zu verwenden.
///
/// Die Geschwindigkeit wird mit Buttons auf einem Controller
/// ver�ndert.
/// </remarks>
public class DifferenceWalk : TwoObjectsDirection
{
        /// <summary>
        /// Bewegungsrichtung auf den forward-Vektor des Orientierungsobjekts setzen.
        /// </summary>
        protected override void UpdateDirection()
        {
            m_Direction = endObject.transform.position - startObject.transform.position;
            m_Direction.y = 0.0f;
            m_Direction.Normalize();
        }

        protected override void UpdateOrientation()
        {
            throw new System.NotImplementedException();
        }
}
