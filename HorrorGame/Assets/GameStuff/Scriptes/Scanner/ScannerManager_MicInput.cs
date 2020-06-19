using Boo.Lang;
using System.Collections;
using System.ComponentModel.Design.Serialization;
using UnityEngine;


public class ScannerManager_MicInput : MonoBehaviour
{
	public const int EMITTER_COUNT = 5;

	public enum ScanMode { SCAN_DIR = 0, SCAN_SPH };
	[Header("Parameters")]
	public Scanner.ScannerObject.FxType m_FxType = Scanner.ScannerObject.FxType.FT_None;
	public ScanMode m_ScanMode = ScanMode.SCAN_DIR;

	[Header("Internal")]
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
	[Range(0,5)]
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
		m_Range = 5f;
	}

	void Update()
	{
		m_Fxs = GameObject.FindObjectsOfType<Scanner.ScannerObject>();
		for (int i = 0; i < m_Fxs.Length; i++)
			m_Fxs[i].Initialize();


		if (EnableMic == true)
		{
			if (mic.volumeinput >= 10 && m_Range != 30)
			{
				StartCoroutine(CoroutineIncreaseRange());
			}
			else if (b_reduceRange == false)
			{
				StartCoroutine(CoroutineDecreaseRange());
			}


			if (Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d"))
			{
				m_Range = Mathf.Lerp(m_Range, 10, lerpTime * Time.deltaTime);
			}
		}
		else if (EnableMic == false)
		{

			m_Amplitude = 2f;
			m_Exp = 3f;
			m_Interval = 5f;
			m_Speed = 5f;
			m_Range = 25;

		}



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


				//Debug.Log(m_Emitters[1].emitter.gameObject.GetComponent<Rigidbody>().velocity);

				for (int j = 1; j < m_Emitters.Length; j++)
				{
					// when the object has no velocity it set the range to zero and if moving the range to 20
					if (m_Emitters[1].emitter.gameObject.GetComponent<Rigidbody>().velocity == new Vector3(0, 0, 0))
					{
						StartCoroutine(CoroutineDecreaseRangeObjects(1));
					}
					else
					{
						StartCoroutine(CoroutineIncreaseRangeObject(1));
					}
				}
				
				

				for (int j = 0; j < EMITTER_COUNT; ++j)
				{
					if (j >= m_Emitters.Length) break;
					emitterPositions.Add(m_Emitters[j].emitter.transform.position);
					emitterRanges.Add(m_Emitters[j].range);
				}
				m_Fxs[i].SetMaterialsVectorArray("_LightEmitters", emitterPositions.ToArray());
				m_Fxs[i].SetMaterialsFloatArray("_LightEmitterRange", emitterRanges.ToArray());

				//for(int i = 0; )
				//m_Fxs[i].SetMaterialsVector("_LightSweepVector", m_Emitter.GetComponent<Transform>().position);
				//m_Fxs[i].SetMaterialsVector("_LightSweepVector2", m_Emitter2.GetComponent<Transform>().position);
			}

			m_Fxs[i].SetMaterialsFloat("_LightSweepAmp", m_Amplitude);
			m_Fxs[i].SetMaterialsFloat("_LightSweepExp", m_Exp);
			m_Fxs[i].SetMaterialsFloat("_LightSweepInterval", m_Interval);
			m_Fxs[i].SetMaterialsFloat("_LightSweepSpeed", m_Speed);
			m_Fxs[i].SetMaterialsFloat("_LightSweepRange", m_Range);

		}
	}



	// Set Coroutine For Main Player EchoLocation
	IEnumerator CoroutineIncreaseRange()
	{
		b_reduceRange = true;

		//m_Range = Mathf.Lerp(m_Range, 30, lerpTime * Time.deltaTime);
		m_Range = Mathf.Lerp(m_Range, 30, lerpTime * Time.deltaTime);

		//Print the time of when the function is first called.
		//Debug.Log("Started Coroutine at timestamp : " + Time.time);

		//yield on a new YieldInstruction that waits for 5 seconds.
		yield return new WaitForSeconds(10);

		//After we have waited 5 seconds print the time again.
		//Debug.Log("Finished Coroutine at timestamp : " + Time.time);
		b_reduceRange = false;
	}

	IEnumerator CoroutineDecreaseRange()
	{
		m_Range = Mathf.Lerp(m_Range, 0, lerpTime * Time.deltaTime);

		//Print the time of when the function is first called.
		//Debug.Log("Started Coroutine at timestamp : " + Time.time);

		//yield on a new YieldInstruction that waits for 5 seconds.
		yield return new WaitForSeconds(1);

		//After we have waited 5 seconds print the time again.
		//Debug.Log("Finished Coroutine at timestamp : " + Time.time);

	}


	// Set Coroutine For Other Throwable Object

	IEnumerator CoroutineIncreaseRangeObject(int objectIndex)
	{
		m_Emitters[objectIndex].range = Mathf.Lerp(m_Emitters[objectIndex].range, 5, lerpTime * Time.deltaTime);
		yield return new WaitForSeconds(3);
	}

	IEnumerator CoroutineDecreaseRangeObjects(int objectIndex)
	{
		m_Emitters[objectIndex].range = Mathf.Lerp(m_Emitters[objectIndex].range, 0, lerpTime * Time.deltaTime);
		yield return new WaitForSeconds(1);
	}



}

