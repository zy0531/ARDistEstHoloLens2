using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacement : MonoBehaviour
{
    [SerializeField]
    GameObject mainCamera;

    [SerializeField]
    [Tooltip("Set a gameObject to be instantiate when hit the floor")]
    GameObject m_gameObjectPrefab;

    [SerializeField]
    [Tooltip("Set the starting position when hit the floor")]
    GameObject m_startingPosPrefab;

    public Vector3 hitPoint { get; set; }
    public Vector3 hitNormal { get; set; }
    public Vector3 forwardDirection { get; set; }

    bool isStartingPosVisualized;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // call SetObjectPlacement when saying "All Set"
    public void SetObjectPlacement()
    {
        SetHitPoint();
        SetHitNormal();
        SetForwardDirection();
        VisualizeStartingPosition();
    }

    void SetHitPoint()
    {
        hitPoint = m_gameObjectPrefab.transform.position;
    }

    void SetHitNormal()
    {
        hitNormal = m_gameObjectPrefab.transform.up;
    }

    void SetForwardDirection()
    {
        Vector3 planeNormal = m_gameObjectPrefab.transform.up;
        Vector3 facingDirection = m_gameObjectPrefab.transform.position - mainCamera.transform.position;
        forwardDirection = Vector3.Normalize(Vector3.ProjectOnPlane(facingDirection, planeNormal));
    }

    void VisualizeStartingPosition()
    {
        if(!isStartingPosVisualized)
        {
            Vector3 hitpos = m_gameObjectPrefab.transform.position;
            Vector3 pos = new Vector3(hitpos.x, hitpos.y - 0.11f, hitpos.z);
            Vector3 planeNormal = m_gameObjectPrefab.transform.up;
            Vector3 facingDirection = m_gameObjectPrefab.transform.position - mainCamera.transform.position;
            GenerateGameObject(pos, m_startingPosPrefab, planeNormal, facingDirection);
            isStartingPosVisualized = true;
        }
        
    }


    void GenerateGameObject(Vector3 hitpoint, GameObject objectToGenerate, Vector3 planeNormal, Vector3 forwardDirection)
    {
        Vector3 pos = new Vector3(mainCamera.transform.position.x, hitpoint.y, mainCamera.transform.position.z);
        // Calculate the rotation based on the camera's forward direction
        Quaternion rot = Quaternion.LookRotation(forwardDirection, Vector3.up);
        Instantiate(objectToGenerate, pos, rot);
    }
}
