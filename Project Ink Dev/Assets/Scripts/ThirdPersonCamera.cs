using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;


public class ThirdPersonCamera : MonoBehaviour
{
    
    class CameraState
    {
        public float roll;
        public float pitch;
        public float yaw;
        

        public void setFromTransform(Transform t)
        {
            roll = t.eulerAngles.z;
            pitch = t.eulerAngles.x;
            yaw = t.eulerAngles.y;
        }

        public void Translate(Vector3 translation)
        {
            float x = 0, y = 0, z = 0;
            Vector3 offsetValue = Quaternion.Euler(pitch, yaw, roll) * translation;

            x += offsetValue.x;
            y += offsetValue.y;
            z += offsetValue.z;
            
        }

        public void LerpTowards(CameraState camera,float rotationLerpPct)
        {
            yaw = Mathf.Lerp(yaw,camera.yaw,rotationLerpPct);
            pitch = Mathf.Lerp(pitch,camera.pitch,rotationLerpPct);
            //row = Mathf.Lerp(roo,camera.roll,rotationLerPct);
        }

        public void updateTransform(Transform t)
        {
            t.eulerAngles = new Vector3(pitch,yaw,roll);
        }
    }


    [Header("旋转设置")]
    private AnimationCurve mouseSensitivityCurve = new AnimationCurve(new Keyframe(0f, 0.5f, 0f, 5f), new Keyframe(1f, 2.5f, 0f, 0f));

    [Header("旋转插值时间")]
    public float rotationLerpTime = 0.01f;

    [Header("反转Y轴")]
    public bool invertY = false;

    [Header("俯仰角设置")]
    [Range(0,90)]
    public int angle = 45;

    [Header("灵敏度")]
    [Range(0f,5.0f)]
    public float flx = 1.0f;

    [Header("把相机挂上来")]
    public Camera mainCamera;

    [TextArea]
    [Header("说明看这里")]
    public string helper = "脚本挂到角色上，再把相机挂载到角色下，使用前需要在编辑器中将相机对准角色";

    

    

    CameraState targetCameraState = new CameraState();
    CameraState interpolatingCameraState = new CameraState();




    void OnEnable()
    {
        targetCameraState.setFromTransform(transform);
        interpolatingCameraState.setFromTransform(transform);   
        //mainCamera.transform.forward = transform.forward;
        Cursor.lockState = CursorLockMode.Locked;


    }

    void SetInputTranslationDirection()
    {
        //rotation
            Vector2 mouseMovement = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")*(invertY?1:-1));
            float mouseSensitivityFactor = mouseSensitivityCurve.Evaluate(mouseMovement.magnitude);

            targetCameraState.yaw += mouseMovement.x * mouseSensitivityFactor * flx;
            targetCameraState.pitch += mouseMovement.y * mouseSensitivityFactor * flx;
            targetCameraState.pitch = Mathf.Clamp(targetCameraState.pitch,-1*angle,angle);
    }


#if UNITY_EDITOR
    static float i = 0;
    void DashStorage()
	{
        i += Time.deltaTime;
        if (i >= 2.0f)
            i = 2.0f;
        Debug.Log(i);
	}

#endif

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        if(Input.GetMouseButtonDown(0))
        {
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
        }



		if (Input.GetKey(KeyCode.BackQuote))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
        }

        if(Input.GetKey(KeyCode.F1))
        {
            SceneManager.LoadScene("SampleScene2");
            DOTween.Clear(true);
        }
#endif

        if (Input.GetMouseButton(1))
        {
            /*var camera = Camera.main;
			var screenRay = (camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0)));
			Ray ray = new Ray(screenRay.origin + screenRay.direction * 4.3f, screenRay.direction);
			RaycastHit hitInfo;
			if (Physics.Raycast(ray, out hitInfo, 20.0f))
			{
				//print(hitInfo.transform);
				transform.DOMove(hitInfo.point - ray.direction * 1.0f, 1.5f);
			}
			else
			{
				transform.DOMove(ray.origin + ray.direction * 20.0f, 1.5f);
			}
			//Debug.Log();
			Debug.DrawLine(ray.origin, ray.origin + ray.direction * 20.0f, Color.red);
			//Debug.Log(Screen.width);
			//transform.DOMove(ray.origin + ray.direction * 15.0f, 1.5f);*/

            Camera.main.DOFieldOfView(60.0f, 0.5f);

            var camera = Camera.main;
            var screenRay = (camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0)));
            Ray ray = new Ray(screenRay.origin + screenRay.direction * 4.3f, screenRay.direction);
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * 20, Color.red);
            DashStorage();
        }

        if (Input.GetMouseButtonUp(1))
        {
            Camera.main.DOFieldOfView(45.0f, 0.5f);
            i = 0f;
        }

        SetInputTranslationDirection();

        float rotationLerpPct = 1.0f - Mathf.Exp((Mathf.Log(1.0f-0.99f) / rotationLerpTime) * Time.deltaTime);
        interpolatingCameraState.LerpTowards(targetCameraState,rotationLerpPct);

        interpolatingCameraState.updateTransform(transform);
        
    }
}




