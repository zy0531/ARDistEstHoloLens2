using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.WorldLocking.Core;

public class WorldLockingCustomize: MonoBehaviour
{
    public Transform cubeTransform;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Save World Locking Tools State 
    public void SaveWorldLock()
    {
        //GetWorldAnchor();
        WorldLockingManager.GetInstance().Save();
    }

    // Reset World Locking Tools State 
    public void ResetWorldLock()
    {
        WorldLockingManager.GetInstance().Reset();
        ResetCubeTransform();
    }

    public void ResetCubeTransform()
    {
        cubeTransform.position = new Vector3(0, 0, 1);
        cubeTransform.rotation = Quaternion.identity;
    }
}
