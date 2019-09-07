using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelinePlayer : CameraBase
{
    public CinemaController cinemaController;
    public GameObject destroyObject;
    public override void PlayCamera()
    {
        ProductionCameraManager.Instance.cinemachine[0].gameObject.SetActive(true);
        ProductionCameraManager.Instance.cinemachine[1].gameObject.SetActive(true);
    }
    public override  CinemaController GetController()
    {
        return cinemaController;
    }
    public override void DestroyCamera()
    {
        ProductionCameraManager.Instance.cinemachine[0].gameObject.SetActive(false);
        ProductionCameraManager.Instance.cinemachine[1].gameObject.SetActive(false);
        Destroy(destroyObject);
    }
    private void Update() 
    {
        if(!cinemaController.isPlay)
        {
            DestroyCamera();
        }
    }

}
