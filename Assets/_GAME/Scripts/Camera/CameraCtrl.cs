using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
[RequireComponent (typeof(Camera))]
public class CameraCtrl : MonoBehaviour
{
    #region SERIALIZEFIELD
    //Main Camera
    [Header("Main Camera")]
    [SerializeField] private Camera mainCam;

    //Variable contains all Character to follow
    [Header("All Target")]
    [SerializeField] private Player[] targets;


    // Variable to custom cameraLogic
    [Header("Camera Properties")]
    [SerializeField] private float minZoom = 5f;
    [SerializeField] private float zoomLerpSpeed = 5f;
    [SerializeField] private float zoomScale = 1.5f;
    #endregion

    #region FIELD
    private Vector3 positionCenter;
    #endregion

    #region PUBLIC FUNCTION
    public void Init(Player[] allChar)
    {
        //Check null
        if (allChar == null || allChar.Length <= 0) return;

        //Set all target
        targets = allChar;
    }
    #endregion

    #region UNITY

    /// <summary>
    /// Cache camera
    /// </summary>
    private void Awake()
    {
        mainCam = Camera.main;
    }

   
    // Update is called once per frame
    void LateUpdate()
    {
        if (targets == null || targets.Length <= 0) return;
       
        //Set position Cam
        if (CaculateAvgPosition(out positionCenter))
        {
            positionCenter.z = mainCam.transform.position.z;
            mainCam.transform.position = positionCenter;
        }

        //Zoom Camera
        CheckZoomCam();
    }
    #endregion

    #region PRIVATE FUNCTION

    private bool CaculateAvgPosition(out Vector3 result)
    {
        result = Vector3.zero;
        if (targets == null || targets.Length <= 0) return false;
        foreach (var player in targets)
        {
            result += player.transform.position;
        }
        result /= targets.Length;
        return true;
    }


    /// <summary>
    /// 
    /// </summary>
    private void CheckZoomCam()
    {
        if (targets == null || targets.Length <= 0) return;
        float maxDistanceX = 0f;
        float maxDistanceY = 0f;

        foreach (var player in targets)
        {
            float distanceX = Mathf.Abs(player.transform.position.x - positionCenter.x);
            float distanceY = Mathf.Abs(player.transform.position.y - positionCenter.y);
            maxDistanceX = Mathf.Max(maxDistanceX, distanceX);
            maxDistanceY = Mathf.Max(maxDistanceY, distanceY);
        }

        var targetZoom = maxDistanceX > maxDistanceY ? maxDistanceX : maxDistanceY;
        targetZoom = (targetZoom / zoomScale) > minZoom ? (targetZoom / zoomScale) : minZoom;
        mainCam.orthographicSize = Mathf.Lerp(mainCam.orthographicSize, targetZoom, Time.deltaTime * zoomLerpSpeed);
    }

    #endregion
}
