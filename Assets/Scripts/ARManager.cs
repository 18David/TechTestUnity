using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


[RequireComponent(typeof(ARRaycastManager))]
public class ARManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _arParent;

    private ARRaycastManager _raycastManager;

    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

    public static ARManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }

        _raycastManager = GetComponent<ARRaycastManager>();
    }


    public Transform CreateARPoint()
    {
        //Ray r = new Ray(transform.forward * 1.5f, -Vector3.up);
        if (_raycastManager.Raycast(new Vector2(Screen.safeArea.width/2,Screen.safeArea.height/2), s_Hits, TrackableType.PlaneWithinPolygon))
        {
            Pose hitPos = s_Hits[0].pose;
            GameObject parent = GameObject.Instantiate(_arParent, new Vector3(hitPos.position.x, Camera.main.transform.position.y, hitPos.position.z), Quaternion.identity/*, hitPos.rotation*/);
            //parent.transform.position = hitPos.position;
            //parent.transform.rotation = hitPos.rotation;
            return parent.transform;
        }
        Debug.LogError("Ar Point Not Found");
        return null;
    }
}
