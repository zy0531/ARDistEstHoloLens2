using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Microsoft.MixedReality.SampleQRCodes;
using Microsoft.MixedReality.Toolkit.Utilities;
using System.Linq;


// using SpatialGraphNode = Microsoft.MixedReality.SampleQRCodes.WindowsXR.SpatialGraphNode;

public class QRCodePose : MonoBehaviour
{ 
    [SerializeField] GameObject referenceCube;
    [SerializeField] QRCodesVisualizer qRCodesVisualizer;

    GameObject QRCodeObject;

    // private QRCodesManager qRCodesManager;
    // private SpatialGraphNode node;
    public bool IsAligned { get; set; }


    void Start()
    {
        IsAligned = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (QRCodeObject == null)
        {
            if (qRCodesVisualizer.GetqrCodesObjectsList().Count > 0)
            {
                // Using LINQ's Last() method to get the last element
                var lastElement = qRCodesVisualizer.GetqrCodesObjectsList().Last();
                // Accessing the value of the first element using its key
                QRCodeObject = qRCodesVisualizer.GetqrCodesObjectsList()[lastElement.Key];
            }
        }
        else
        {
            if (!IsAligned)
            {
                // *** 1. Rotation Adjustment *** //
                referenceCube.transform.rotation = QRCodeObject.transform.rotation;
                // Rotate the object 90 degrees along the X-axis
                referenceCube.transform.Rotate(90f, 0f, 0f);


                // *** 2. Position Adjustment *** //
                referenceCube.transform.position = QRCodeObject.transform.position;
                // Get the scales of the reference cube along each axis
                float offsetX = referenceCube.transform.localScale.x / 2.0f;
                float offsetY = referenceCube.transform.localScale.y / 2.0f;
                float offsetZ = referenceCube.transform.localScale.z / 2.0f;
                // Get the position of the reference object
                Vector3 referencePosition = referenceCube.transform.position;
                // Translate the object's position by the specified offset along its local x-axis
                referenceCube.transform.Translate(Vector3.right * offsetX, Space.Self);
                // Translate the object's position by the specified offset along its local y-axis
                referenceCube.transform.Translate(Vector3.up * offsetY, Space.Self);
                // Translate the object's position by the specified offset along its local y-axis
                referenceCube.transform.Translate(Vector3.forward * -1f * offsetZ, Space.Self);


                //// *** 3. Set IsAligned as true *** //
                //// make sure it stay at the same place after the alignment 
                //IsAligned = true;

                // *** 3. Make the QRCodeObject Invisible *** //
                // QRCodeObject.SetActive(false)
                // Disable rendering for this GameObject and its children
                Renderer[] renderers = QRCodeObject.GetComponentsInChildren<Renderer>();
                foreach (Renderer renderer in renderers)
                {
                    renderer.enabled = false;
                }
            }
        }
    }
}
