using UnityEngine;

public class ActivateCCC : MonoBehaviour
{
    /// <summary>
    /// CCC anzeigen oder nicht?
    /// </summary>
    [Tooltip(("CCC beim Start anzeigen?"))]
    public bool Show = false;

    /// <summary>
    /// GameObject CCC
    /// </summary>
    protected GameObject TheCCC;
    
    /// <summary>
    /// Verbindung zu CCC in der Szene herstellen.
    /// </summary>
    protected void FindTheCCC()
    {
        TheCCC = GameObject.Find("CCC");
        if (!TheCCC)
            Debug.Log("No CCC found!");
    }
}
