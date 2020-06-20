using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoFormainMeun : MonoBehaviour
{
	public const int EMITTER_COUNT = 5;

	public enum ScanMode { SCAN_DIR = 0, SCAN_SPH };
	[Header("Parameters")]
	public Scanner.ScannerObject.FxType m_FxType = Scanner.ScannerObject.FxType.FT_None;
	public ScanMode m_ScanMode = ScanMode.SCAN_DIR;

	public ScanEmitter[] m_Emitters;

	public Vector4 m_Dir = new Vector4(1, 0, 0, 0);
	[Range(0f, 2f)] public float m_Amplitude = 0f;
	[Range(0f, 16f)] public float m_Exp = 3f;
	[Range(0f, 60f)] public float m_Interval = 20f;
	[Range(0f, 32f)] public float m_Speed = 10f;

	[HideInInspector]
	public float m_Range = 30.0f;

	[Header("Internal")]
	public Scanner.ScannerObject[] m_Fxs;

	public WebPlayerMic mic;
	public bool b_reduceRange = false;

	private List<Vector4> emitterPositions = null;
	private List<float> emitterRanges = null;

	[SerializeField]
	[Range(0, 5)]
	float lerpTime = 1f;

	public bool EnableMic;


	void Start()
	{
		EnableMic = true;

		emitterPositions = new List<Vector4>();
		emitterRanges = new List<float>();

		m_Fxs = GameObject.FindObjectsOfType<Scanner.ScannerObject>();
		for (int i = 0; i < m_Fxs.Length; i++)
			m_Fxs[i].Initialize();

		m_FxType = Scanner.ScannerObject.FxType.FT_Additional;
		m_Range = 100f;


	}

	void Update()
	{
		m_Fxs = GameObject.FindObjectsOfType<Scanner.ScannerObject>();
		for (int i = 0; i < m_Fxs.Length; i++)
			m_Fxs[i].Initialize();




		for (int i = 0; i < m_Fxs.Length; i++)
		{

			if (m_Fxs[i] == null)
			{
				i += 1;
			}

			m_Fxs[i].ApplyFx(m_FxType);
			m_Fxs[i].UpdateSelfParameters();
			if (ScanMode.SCAN_DIR == m_ScanMode)
			{
				m_Fxs[i].ApplyDirectionalScan(m_Dir);
				m_Fxs[i].SetMaterialsVector("_LightSweepVector", m_Dir);
			}
			else if (ScanMode.SCAN_SPH == m_ScanMode)
			{
				m_Fxs[i].ApplySphericalScan();
				emitterPositions.Clear();
				emitterRanges.Clear();

				/* PLAYER is at index = 0 */
				m_Emitters[0].range = m_Range;

				for (int j = 0; j < EMITTER_COUNT; ++j)
				{
					if (j >= m_Emitters.Length) break;
					emitterPositions.Add(m_Emitters[j].emitter.transform.position);
					emitterRanges.Add(m_Emitters[j].range);
				}
				m_Fxs[i].SetMaterialsVectorArray("_LightEmitters", emitterPositions.ToArray());
				m_Fxs[i].SetMaterialsFloatArray("_LightEmitterRange", emitterRanges.ToArray());

			}

			m_Fxs[i].SetMaterialsFloat("_LightSweepAmp", m_Amplitude);
			m_Fxs[i].SetMaterialsFloat("_LightSweepExp", m_Exp);
			m_Fxs[i].SetMaterialsFloat("_LightSweepInterval", m_Interval);
			m_Fxs[i].SetMaterialsFloat("_LightSweepSpeed", m_Speed);
			m_Fxs[i].SetMaterialsFloat("_LightSweepRange", m_Range);

		}
	}


	


}
