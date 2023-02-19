//========= 2021 - 2023 - Copyright Manfred Brill. All rights reserved. ===========

/// <summary>
/// Fly als Locomotion in einer VR-Anwendung, mit zwei Objekten für
/// die Definition der Bewegungsrichtung.
/// </summary>
/// <remarks>
/// Fly bedeutet, dass wir die Bewegungsrichtung in allen drei
/// Koordinatenachsen verändern können.
///
/// Wir verwenden einen Trigger-Button. So lange dieser Button
/// gedrückt ist wird die Bewegung ausgeführt.
/// 
/// Als Bewegungsrichtung verwenden wir den Differenzvektor
/// zweier Objekte, typischer Weise die Controller. Möglich ist
/// auch den Kopf als einer der Objekte zu verwenden.
///
/// Die Geschwindigkeit wird mit Buttons auf einem Controller
/// verändert.
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
