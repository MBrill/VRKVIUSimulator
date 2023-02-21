//========= 2021 - 2023 Copyright Manfred Brill. All rights reserved. ===========

using UnityEngine;

/// <summary>
///Walking-in-Place mit einem Trigger-Objekt. 
/// </summary>
/// <remarks>
/// Wir beobachten die z-Koordinaten eines getrackten Objekts
/// und entscheiden damit,
/// ob wir uns fortbewegen m�chten. Keine weiteren Strategien.
///
/// Implementiert LLVM WiP mit Hilfe von Arm-Swinging.
/// </remarks>
public class ArmSwingingOneTrigger : InPlaceLocomotion
{
        /// <summary>
        /// Trigger-Objekt
        /// </summary>
        [Tooltip("Welches Objekt wird f�r die Fortbewegung bewegt?")]
        public GameObject TriggerObject;
        
        [Header("Protokollierung des Triggers")]
        /// <summary>
        /// Aktivieren und De-Aktivieren Protokollieren
        /// </summary>      
        [Tooltip("Protollieren?")]
        public bool Logs = false;
        
        /// <summary>
        /// Dateiname f�r die Logs
        /// </summary>      
        [Tooltip("Name der Protokoll-Datei")]
        public string fileName = "asllcmwip.csv";
    
        /// <summary>
        /// Initialisierung
        ///
        /// Wir stellen den LogHander ein und
        /// erzeugen anschlie�end Log-Ausgaben in LateUpdate.
        protected override void Awake()
        {
            csvLogHandler = new CustomLogHandler(fileName);
            if (!Logs)
                Debug.unityLogger.logEnabled = false;
            base.Awake();
        }
        
        /// <summary>
        /// Schlie�en der Protokolldatei
        /// </summary>
        private void OnDisable()
        {
            csvLogHandler.CloseTheLog();
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
            position = TriggerObject.transform.position.z;
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
        /// Eigener LogHandler
        /// </summary>
        private CustomLogHandler csvLogHandler;

        /// <summary>
        /// Instanz des Default-Loggers in Unity
        /// </summary>
        private static readonly ILogger s_Logger = Debug.unityLogger;
}
