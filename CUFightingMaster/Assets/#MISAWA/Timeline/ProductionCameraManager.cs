using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using CUEngine.Pattern;

public class ProductionCameraManager : SingletonMono<ProductionCameraManager>
{
	// CameraのCinemachineBrainを保存
	public CinemachineBrain[] cinemachine = new CinemachineBrain[2];
    public Camera player1Camera = null;
    public Camera player2Camera = null;
}
