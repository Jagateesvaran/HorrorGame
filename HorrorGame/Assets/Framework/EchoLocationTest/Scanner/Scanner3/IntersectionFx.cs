using UnityEngine;
using System.Collections;

namespace Scanner
{
	public class IntersectionFx : MonoBehaviour
	{
		Material[] m_Mats;
		Coroutine m_CoroWave;
	
		void Start()
		{
			Renderer rd = GetComponent<Renderer>();
			m_Mats = rd.materials;
		}
		void Update()
		{
			// this is just a demonstrate, you can extend this wave usage as bullet collision for example.
			if (Input.GetMouseButtonDown(0))
			{
				RaycastHit hit;
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				if (Physics.Raycast(ray, out hit))
				{
					Vector3 pos = hit.point;
					for (int i = 0; i < m_Mats.Length; i++)
						m_Mats[i].SetVector("_CollisionPos", pos);

					if (m_CoroWave != null)
						StopCoroutine(m_CoroWave);
					m_CoroWave = StartCoroutine(OneShotOfWave());
				}
			}
		}
		IEnumerator OneShotOfWave()
		{
			float i = 0f;
			float rate = 2f;
			while (i < 1f)
			{
				i += Time.deltaTime * rate;
				for (int n = 0; n < m_Mats.Length; n++)
					m_Mats[n].SetFloat("_WaveScale", 1f - i);
				yield return 0;
			}
			for (int n = 0; n < m_Mats.Length; n++)
				m_Mats[n].SetFloat("_WaveScale", 0f);
		}
	}
}