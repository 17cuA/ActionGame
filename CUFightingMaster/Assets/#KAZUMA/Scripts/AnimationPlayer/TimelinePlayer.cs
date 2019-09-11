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
        CameraController.Instance.boxCollider1.enabled = true;
        CameraController.Instance.boxCollider2.enabled = true;
        CameraController.Instance.cinemaController = null;
        //プレイヤー個々のカメラを消す
        foreach (var cam in ProductionCameraManager.Instance.player1Camera)
        {
            cam.gameObject.SetActive(true);
        }
        foreach (var cam in ProductionCameraManager.Instance.player2Camera)
        {
            cam.gameObject.SetActive(true);
        }
        // ProductionCameraManager.Instance.cinemachine[0].gameObject.SetActive(false);
        // ProductionCameraManager.Instance.cinemachine[1].gameObject.SetActive(false);
        Destroy(destroyObject);
    }
    private void Start() 
    {
        CameraController.Instance.boxCollider1.enabled = false;
        CameraController.Instance.boxCollider2.enabled = false;
        CameraController.Instance.cinemaController = cinemaController;
        //プレイヤー個々のカメラを消す
        foreach(var cam in ProductionCameraManager.Instance.player1Camera)
        {
            cam.gameObject.SetActive(false);
        }
        foreach(var cam in ProductionCameraManager.Instance.player2Camera)
        {
            cam.gameObject.SetActive(false);
        }
        ProductionCameraManager.Instance.cinemachine[0].gameObject.SetActive(true);
        ProductionCameraManager.Instance.cinemachine[1].gameObject.SetActive(true);
    }
    private void Update() 
    {
        if(CameraController.Instance.boxCollider1.enabled == true)
        {
            CameraController.Instance.boxCollider1.enabled = false;
        }
        if (CameraController.Instance.boxCollider2.enabled == true)
        {
            CameraController.Instance.boxCollider2.enabled = false;
        }
        if(!cinemaController.isPlay)
        {
            DestroyCamera();
        }
    }

}
