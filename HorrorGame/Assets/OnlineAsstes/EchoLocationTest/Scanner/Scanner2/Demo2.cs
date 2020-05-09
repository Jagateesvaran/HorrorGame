//using System.Collections;
//using UnityEngine;

//public class Demo2 : MonoBehaviour
//{
//	public enum ScanMode { SCAN_DIR = 0, SCAN_SPH };
//	[Header("Parameters")]
//	public Scanner.ScannerObject.FxType m_FxType = Scanner.ScannerObject.FxType.FT_None;
//	public ScanMode m_ScanMode = ScanMode.SCAN_DIR;
//	public GameObject m_Emitter;
//	public Vector4 m_Dir = new Vector4(1, 0, 0, 0);
//	[Range(0f, 2f)] public float m_Amplitude = 0f;
//	[Range(0f, 16f)] public float m_Exp = 3f;
//	[Range(0f, 64f)] public float m_Interval = 20f;
//	[Range(0f, 32f)] public float m_Speed = 10f;
//	public float m_Range = 10.0f;
//	[Header("Internal")]
//	public Scanner.ScannerObject[] m_Fxs;


//	void Start()
//	{
//		m_Fxs = GameObject.FindObjectsOfType<Scanner.ScannerObject>();
//		for (int i = 0; i < m_Fxs.Length; i++)
//			m_Fxs[i].Initialize();

//		m_FxType = Scanner.ScannerObject.FxType.FT_TransparencyTextured;

//		m_Amplitude = 1;
//		m_Range = 10.0f;

//	}
//	void Update()
//	{


//		float temp = 1000 * MicInput.MicLoudness;



//		//if (temp >= 1.0f  && temp <= 100.0f)
//		//{
//		//	Debug.Log(temp.ToString());
//		//	//m_Amplitude = 1;
//		//	//m_Range = 1.0f;
//		//}
//	    if (temp > 90.0f)
//		{
//			Debug.Log("Too loud");
//			//m_Amplitude = 2;
//			//m_Range = 10.0f;

//			StartCoroutine(ExampleCoroutine());
//		}

//		//if (temp >= 400.0f) // whisper
//		//{
//		//	Debug.Log("Shouting" + temp.ToString());
//		//	m_Amplitude = 2;
//		//	m_Range = 20.0f;
//		//}
//		//else if (temp >= 1.0f && temp <= 200.0f)
//		//{
//		//	Debug.Log("Whispering");
//		//	m_Amplitude = 1;
//		//	m_Range = 5.0f;
//		//}

//		for (int i = 0; i < m_Fxs.Length; i++)
//		{
//			m_Fxs[i].ApplyFx(m_FxType);
//			m_Fxs[i].UpdateSelfParameters();
//			if (ScanMode.SCAN_DIR == m_ScanMode)
//			{
//				m_Fxs[i].ApplyDirectionalScan(m_Dir);
//				m_Fxs[i].SetMaterialsVector("_LightSweepVector", m_Dir);
//			}
//			else if (ScanMode.SCAN_SPH == m_ScanMode)
//			{
//				m_Fxs[i].ApplySphericalScan();
//				m_Fxs[i].SetMaterialsVector("_LightSweepVector", m_Emitter.GetComponent<Transform>().position);
//			}

//			m_Fxs[i].SetMaterialsFloat("_LightSweepAmp", m_Amplitude);
//			m_Fxs[i].SetMaterialsFloat("_LightSweepExp", m_Exp);
//			m_Fxs[i].SetMaterialsFloat("_LightSweepInterval", m_Interval);
//			m_Fxs[i].SetMaterialsFloat("_LightSweepSpeed", m_Speed);
//			m_Fxs[i].SetMaterialsFloat("_LightSweepRange", m_Range);

//		}
//	}


//	IEnumerator ExampleCoroutine()
//	{

//		m_Amplitude = 2;
//		m_Range = 20.0f;

//		//Print the time of when the function is first called.
//		Debug.Log("Started Coroutine at timestamp : " + Time.time);

//		//yield on a new YieldInstruction that waits for 5 seconds.
//		yield return new WaitForSeconds(5);

//		//After we have waited 5 seconds print the time again.
//		Debug.Log("Finished Coroutine at timestamp : " + Time.time);

//		m_Amplitude = 1;
//		m_Range = 10.0f;

//	}



//}

//// Credit to Elson : for _LightSweepRange
///

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
	public float m_Range = 30.0f;
	[Header("Internal")]
	public Scanner.ScannerObject[] m_Fxs;

	public WebPlayerMic mic;

	void Start()
	{
		m_Fxs = GameObject.FindObjectsOfType<Scanner.ScannerObject>();
		for (int i = 0; i < m_Fxs.Length; i++)
			m_Fxs[i].Initialize();

		m_FxType = Scanner.ScannerObject.FxType.FT_TransparencyTextured;

		m_Range = 30;



	}
	void Update()
	{

		//m_Range = mic.volumeinput + 10;

		//if (mic.volumeinput > 10)
		//{
		//	StartCoroutine(ExampleCoroutine());
		//}

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
				m_Fxs[i].SetMaterialsVector("_LightSweepVector", m_Emitter.GetComponent<Transform>().position);
			}

			m_Fxs[i].SetMaterialsFloat("_LightSweepAmp", m_Amplitude);
			m_Fxs[i].SetMaterialsFloat("_LightSweepExp", m_Exp);
			m_Fxs[i].SetMaterialsFloat("_LightSweepInterval", m_Interval);
			m_Fxs[i].SetMaterialsFloat("_LightSweepSpeed", m_Speed);
			m_Fxs[i].SetMaterialsFloat("_LightSweepRange", m_Range);

		}
	}

	IEnumerator ExampleCoroutine()
	{

		m_Range = mic.volumeinput + 20.0f ;

		//Print the time of when the function is first called.
		Debug.Log("Started Coroutine at timestamp : " + Time.time);

		//yield on a new YieldInstruction that waits for 5 seconds.
		yield return new WaitForSeconds(10);

		//After we have waited 5 seconds print the time again.
		Debug.Log("Finished Coroutine at timestamp : " + Time.time);

		m_Range = 10.0f;

	}


}

