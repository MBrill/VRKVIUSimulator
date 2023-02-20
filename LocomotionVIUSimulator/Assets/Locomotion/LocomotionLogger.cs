//========= 2021 - 2023 Copyright Manfred Brill. All rights reserved. ===========

using HTC.UnityPlugin.Vive;
using UnityEngine;

public class LocomotionLogger : MonoBehaviour
{
    public GameObject PlayAreaPosition;
    
    /// <summary>
    /// Dateiname für die Logs
    /// </summary>
    public string FileName = "locomotion.csv";
    
    /// <summary>
    /// Initialisierung
    ///
    /// Wir stellen den LogHander ein und
    /// erzeugen anschließend Log-Ausgaben in LateUpdate.
    void Awake()
    {
        csvLogHandler = new CustomLogHandler(FileName);
        object[] args = {"Zeit",
            "Kopf.x",
            "Kopf.y",
            "Kopf.z",
            "Real.x",
            "Real.y",
            "Real.z"
        };
        s_Logger.LogFormat(LogType.Log, gameObject,
            "{0:G};{1:G};{2:G}, {3:G}; {4:G}; {5:G}; {6:G}", args);
        
        deviceIndex = role.GetDeviceIndex();
    }

    // Update is called once per frame
    void Update()
    {
        var pos = VivePose.GetPose(role).pos;
        var headPos =  PlayAreaPosition.transform.position;
        object[] args = {Time.time,
            PlayAreaPosition.transform.position.x,
            PlayAreaPosition.transform.position.y,
            PlayAreaPosition.transform.position.z,
            PlayAreaPosition.transform.localPosition.x,
            PlayAreaPosition.transform.localPosition.y,
            PlayAreaPosition.transform.localPosition.z,
            /*pos.x,
            pos.y,
            pos.z*/
        };
        s_Logger.LogFormat(LogType.Log, gameObject,
            "{0:G};{1:G};{2:G}, {3:G}; {4:G}; {5:G}; {6:G}", args);
    }

    private void OnDisable()
    {
        csvLogHandler.CloseTheLog();
    }
    
    private ViveRoleProperty role = ViveRoleProperty.New(BodyRole.Head);
    private uint deviceIndex;

    /// <summary>
    /// Eigener LogHandler
    /// </summary>
    private CustomLogHandler csvLogHandler;

    /// <summary>
    /// Instanz des Default-Loggers in Unity
    /// </summary>
    private static readonly ILogger s_Logger = Debug.unityLogger;
}
