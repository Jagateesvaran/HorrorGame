using UnityEngine;
using System.Collections;

public class Demo1 : MonoBehaviour
{
	[Header("Mouse Click")]
	public Camera m_Camera;
	public Scanner.ScannerCameraEffect m_Scanner;
	public float m_ScanVelocity = 50f;
	[Range(1f, 20f)] public float m_ScanWidth = 10f;
	public Color m_Leading;
	public Color m_Middle;
	public Color m_Trail;
	public Color m_HorizontalBar;
	float m_ScanDistance;
	bool m_Scanning;

	//[Header("Object Halo")]
	//public Renderer m_RdTerrain;
	//public Transform m_TransObject;

	void Update ()
	{
		if (m_Scanning)
			m_ScanDistance += Time.deltaTime * m_ScanVelocity;
		if (Input.GetMouseButtonDown (1))
		{
			Ray ray = m_Camera.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit))
			{
				m_Scanning = true;
				m_ScanDistance = 0f;
				m_Scanner.m_Origin = hit.point;
			}
		}
		m_Scanner.SetScanDistance (m_ScanDistance);
		m_Scanner.m_Material.SetFloat ("_ScanWidth", m_ScanWidth);
		m_Scanner.m_Material.SetColor ("_LeadColor", m_Leading);
		m_Scanner.m_Material.SetColor ("_MidColor", m_Middle);
		m_Scanner.m_Material.SetColor ("_TrailColor", m_Trail);
		m_Scanner.m_Material.SetColor ("_HBarColor", m_HorizontalBar);

		//m_RdTerrain.material.SetVector ("_Center", m_TransObject.position);
	}
}
