//========= 2021 - 2023 Copyright Manfred Brill. All rights reserved. ===========
using UnityEngine;

/// <summary>
///Walking-in-Place mit einem Trigger-Objekt
/// </summary>
/// <remarks>
/// Wir beobachten die y-Koordinaten eine getrackten Objekts
/// und entscheiden damit,
/// ob wir uns fortbewegen m�chten. Keine weiteren Strategien.
///
/// Implementiert LLVM WiP.
/// </remarks>
public class OneTriggerConstantSpeedWiP : InPlaceLocomotion
{
        /// <summary>
        /// Welchen Arm verwenden wir f�r das Triggern der Fortbewegung?
        /// </summary>
        [Tooltip("Welches Objekt wird f�r die Fortbewegung bewegt?")]
        public GameObject TriggerObject;
        
    
        /// <summary>
        /// Initialisierung
        ///
        /// Wir stellen den LogHander ein und
        /// erzeugen anschlie�end Log-Ausgaben in LateUpdate.
        protected override void Awake()
        {
            csvLogHandler = new CustomLogHandler(fileName);
            base.Awake();
        }
        
        /// <summary>
        /// Walk wird so lange durchgef�hrt wie das Trigger-Objekt  bewegt wird.
        /// Das entscheiden wir auf Grund der Geschwindigkeit dieser
        /// Ver�nderung, die wir
        /// mit Hilfe von numerischem Differenzieren sch�tzen.
        /// </summary>
        protected override void Trigger()
        {
            
            float position = 0.0f,
                signalVelocity = 0.0f;

            // Numerisches Differenzieren
            position = TriggerObject.transform.position.y;
            signalVelocity = Mathf.Abs((position - m_LastValue) / Time.deltaTime);
            Moving = signalVelocity > Threshold;

            if (Moving)
            {
                object[] args = {Time.time,
                    position,
                    signalVelocity            
                };
                s_Logger.LogFormat(LogType.Log, gameObject,
                    "{0:G};{1:G};{2:G}", args);

            }
            m_LastValue = position;
        }

        /// <summary>
        /// Speicher f�r den letzten Wert
        /// </summary>
        private float m_LastValue = 1.6f;
        
        /// <summary>
        /// Dateiname f�r die Logs
        /// </summary>
        private string fileName = "llcmwip.csv";
        
        /// <summary>
        /// Eigener LogHandler
        /// </summary>
        private CustomLogHandler csvLogHandler;

        /// <summary>
        /// Instanz des Default-Loggers in Unity
        /// </summary>
        private static readonly ILogger s_Logger = Debug.unityLogger;
}
