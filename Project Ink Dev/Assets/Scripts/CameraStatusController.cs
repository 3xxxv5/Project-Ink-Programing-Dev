using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumSpace;

public class CameraStatusController
{
	 private static CameraStatusController instance = null;
	 private static readonly object obj = new object();
	 private CameraStatusController() { }
	 public static CameraStatusController Instance()
	 {
		 if (instance == null)
		 {
			 lock (obj)
			 {
				 if (instance == null)
				 {
					 instance = new CameraStatusController();
				 }
			 }
		 }

		 return instance;
	 }

	private CameraStatus cameraStatues = CameraStatus.Common;

	public CameraStatus GetCameraStatus()
	{
		return cameraStatues;
	}

	public void SetShock()
	{
		cameraStatues = CameraStatus.Shock;
	}

	public void SetCommon()
	{
		cameraStatues = CameraStatus.Common;
	}
}
