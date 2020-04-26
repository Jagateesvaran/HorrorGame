using System.Collections;
using UnityEngine;

public class Demo2 : MonoBehaviour
{
	public enum ScanMode { SCAN_DIR = 0, SCAN_SPH };
	[Header("Parameters")]
	public Scanner.ScannerObject.FxType m_FxType = Scanner.ScannerObject.FxType.FT_None;
	public ScanMode m_ScanMode = ScanMode.SCAN_DIR;
	public GameObject m_Emitter;
	public Vector4 m_Dir = new Vector4(1, 0, 0, 0);
	[Range(0f, 2f)] public float m_Amplitude = 0f;
	[Range(0f, 16f)] public float m_Exp = 3f;
	[Range(0f, 64f)] public float m_Interval = 20f;
	[Range(0f, 32f)] public float m_Speed = 10f;
	public float m_Range = 10.0f;
	[Header("Internal")]
	public Scanner.ScannerObject[] m_Fxs;

	public MicControl mic;
	float currCountdownValue;

	void Start()
	{
		m_Fxs = GameObject.FindObjectsOfType<Scanner.ScannerObject>();
		for (int i = 0; i < m_Fxs.Length; i++)
			m_Fxs[i].Initialize();

		m_FxType = Scanner.ScannerObject.FxType.FT_TransparencyAdd;

		m_Amplitude = 1;
		m_Range = 5.0f;

	}
	void Update()
	{


		for (int i = 0; i < m_Fxs.Length; i++)
		{
			if (mic.loudness >= 10)
			{
				StartCoroutine(StartCountdown());
			}
			else if (mic.loudness <= 0.1)
			{
				m_Amplitude = 0;
				m_Range = 0f;
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
				m_Fxs[i].SetMaterialsVector("_LightSweepVector", m_Emitter.GetComponent<Transform>().position);
			}

			m_Fxs[i].SetMaterialsFloat("_LightSweepAmp", m_Amplitude);
			m_Fxs[i].SetMaterialsFloat("_LightSweepExp", m_Exp);
			m_Fxs[i].SetMaterialsFloat("_LightSweepInterval", m_Interval);
			m_Fxs[i].SetMaterialsFloat("_LightSweepSpeed", m_Speed);
			m_Fxs[i].SetMaterialsFloat("_LightSweepRange", m_Range);

		}
	}

	public IEnumerator StartCountdown(float countdownValue = 20)
	{
		m_Amplitude = 2;
		m_Range = 20.0f;

		currCountdownValue = countdownValue;
		while (currCountdownValue > 0)
		{
			Debug.Log("Countdown: " + currCountdownValue);
			yield return new WaitForSeconds(1.0f);
			currCountdownValue--;
		}
	}


}

// Credit to Elson : for _LightSweepRange