using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private ARRaycastManager aRRaycastManager;

    List<ARRaycastHit> _hits = new List<ARRaycastHit>();

    [SerializeField]
    GameObject placementIndicator;
    [SerializeField]
    GameObject arObjectToSpawn;


    private GameObject spawnedObject;

    bool Spawnar;

    private void Awake() {
    }

    private void Start()
    {
        spawnedObject = null;
        Spawnar = true;
    }

    void Update() {

        if (Spawnar)
        {
            if (Input.touchCount == 0)
                return;

            if (aRRaycastManager.Raycast(Input.GetTouch(0).position, _hits))
            {
                if(Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    SpawnPrefab(_hits[0].pose.position);
                    Spawnar = false;
                }
                else if (Input.GetTouch(0).phase == TouchPhase.Moved && spawnedObject == null)
                {
                    spawnedObject.transform.position = _hits[0].pose.position;
                }

                if (Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    spawnedObject = null;
                }
            }

        }


        //if (spawnedObject == null && placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) {
        //    ARPlaceObject(arObjectToSpawn);
        //}

        

        //UpdatePlacementPose();
        //UpdatePlacementIndicator();
       
    }

    void SpawnPrefab(Vector3 spawnPosition)
    {
        spawnedObject = Instantiate(arObjectToSpawn, spawnPosition, Quaternion.identity);
    }

 //   private void UpdatePlacementIndicator()
 //   {
 //       if (placementPoseIsValid) {
 //           placementIndicator.SetActive(true);
 //           placementIndicator.transform.SetPositionAndRotation(PlacementPose.position, PlacementPose.rotation);
 //       }else{
 //           placementIndicator.SetActive(false);
	//	}
	//}

 //   private void UpdatePlacementPose()
	//{
 //       var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
 //       var hits = new List<ARRaycastHit>();
 //       aRRaycastManager.Raycast(screenCenter, hits, TrackableType.Planes);
 //       placementPoseIsValid = hits.Count > 0;

 //       if (placementPoseIsValid) {
 //           PlacementPose = hits[0].pose;
            
	//	}
	//}

    //public void ARPlaceObject(GameObject ARObjt ) {
        
    //    if(spawnedObject == null) {
    //        spawnedObject = Instantiate(ARObjt, PlacementPose.position, Quaternion.identity);
    //        placementPoseIsValid = false;
    //    }//else {
    //    //     Destroy(spawnedObject);
    //    //     spawnedObject = null;
    //    //      spawnedObject = Instantiate(ARObjt, PlacementPose.position, PlacementPose.rotation);
    //    // }

    //}


    //void UpdateObjectPosition() {
        
    //    spawnedObject.transform.position = new Vector3(PlacementPose.position.x, PlacementPose.position.y, PlacementPose.position.z);
        
    //}

    
}