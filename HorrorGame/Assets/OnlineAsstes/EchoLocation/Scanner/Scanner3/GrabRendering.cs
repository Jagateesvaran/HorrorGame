using UnityEngine;

namespace Scanner
{
	[RequireComponent(typeof(Camera))]
	public class GrabRendering : MonoBehaviour
	{
		public LayerMask m_Layer;
		public RenderTexture m_Rt;
		Camera m_Cam;
		
		void Start ()
		{
			m_Rt = new RenderTexture(Screen.width, Screen.height, 24, RenderTextureFormat.ARGB32);
			m_Rt.name = "[Grab]";
	
			GameObject obj = new GameObject("[Grab]");
			obj.transform.parent = transform;
			obj.transform.localPosition = new Vector3(0, 0, 0);
			obj.transform.localRotation = new Quaternion(0, 0, 0, 1);
	
			m_Cam = obj.AddComponent<Camera>();
			m_Cam.targetTexture = m_Rt;
			m_Cam.cullingMask = m_Layer;
			m_Cam.useOcclusionCulling = false;
			Camera cam = GetComponent<Camera>();
			SyncCameraParameters(cam, m_Cam);
		}
		void Update ()
		{
			if (!enabled)
				return;
			
			Camera cam = GetComponent<Camera>();
			if (cam)
			{
				SyncCameraParameters(cam, m_Cam);
				m_Cam.Render();
			}
			Shader.SetGlobalTexture("_Global_GrabTex", m_Rt);
		}
//		void OnGUI ()
//		{
//			float sz = 64;
//			GUI.DrawTextureWithTexCoords(new Rect(10 + sz * 0 + 0, 10, sz, sz), m_Rt, new Rect(0, 0, 1, 1));
//		}
		void SyncCameraParameters(Camera src, Camera dst)
		{
			if (dst == null)
				return;
			dst.clearFlags = src.clearFlags;
			dst.backgroundColor = src.backgroundColor;
			dst.farClipPlane = src.farClipPlane;
			dst.nearClipPlane = src.nearClipPlane;
			dst.orthographic = src.orthographic;
			dst.fieldOfView = src.fieldOfView;
			dst.aspect = src.aspect;
			dst.orthographicSize = src.orthographicSize;
		}
	}
}