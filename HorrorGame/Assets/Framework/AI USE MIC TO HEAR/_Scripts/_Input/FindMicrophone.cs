using UnityEngine;
using System.Collections;


[RequireComponent(typeof(AudioSource))]
public class FindMicrophone : MonoBehaviour {

	public enum micType
		{
		AlwaysOn,
		HoldToTalk,
		PushToTalk,
		SensitivityRange // if this is selected, audio will be on always, but wont be heard unless loud enough.
		}
	public enum buildType{
		Standalone,
		WebPlayer,
		MobileDevice
	}

	public buildType BuildType;
	public micType MicType;

	public float MicSensitivity = 100;
	public float MicVolume;
	public float RamDiposalSpeed; // Faster this is the faster ram is cleared for other things.
	public string DeviceSelected { get; private set; }
	public bool micSelected = false;
	private float RamTimer;
	//private float AudioSamples = 256; // Increase to get better average sound, but will decrease performance.
	private float minFreq, maxFreq;
	public bool WebPlayerTrue;

	// Internal Variables.

	public int detail = 500;
	public float minValue = 0.5f;
	public float amplitude = 0.5f;
	private Vector3 startPos;


	IEnumerator Start(){
		startPos = transform.localScale;

				if (BuildType == buildType.WebPlayer && MicType == micType.AlwaysOn) {
						yield return Application.RequestUserAuthorization (UserAuthorization.Microphone);
						if (Application.HasUserAuthorization (UserAuthorization.Microphone)) {
				AudioSource aud = GetComponent<AudioSource> ();
				aud.clip = Microphone.Start ("Built-in Microphone", true, 10, 44100);
				aud.Play ();
				aud.mute = true;

				WebPlayerTrue = true;
				} 
			}
		}
		void Update(){

		if (WebPlayerTrue && BuildType == buildType.WebPlayer && MicType == micType.AlwaysOn) {

			
			// Internal
			float[] info = new float[detail];
			AudioListener.GetOutputData(info, 0);
			float packagedData = 0.1f;
			
			for (int x = 0; x <info.Length; x++){
				packagedData += System.Math.Abs(info[x]);
			}
			transform.localScale = new Vector3(minValue, (packagedData * amplitude) + startPos.y, minValue);
			transform.localScale = new Vector3(minValue, (packagedData * amplitude) + startPos.x, minValue);
			transform.localScale = new Vector3(minValue, (packagedData * amplitude) + startPos.z, minValue);
			Debug.Log ("Listening to Web Player Audio, but muting from player");
		}



		if (BuildType == buildType.Standalone && MicType == micType.AlwaysOn) {

			AudioSource aud = GetComponent<AudioSource>();
			aud.clip = Microphone.Start("Built-in Microphone", true, 10, 44100);
			aud.Play();
			aud.mute = true;
			Debug.Log("Listening to Standalone Audio, but muting from player");
		}

		if (BuildType == buildType.MobileDevice && MicType == micType.AlwaysOn) {

			AudioSource aud = GetComponent<AudioSource>();
			aud.clip = Microphone.Start("Built-in Microphone", true, 10, 44100);
			aud.Play();
			aud.mute = true;
			Debug.Log("Listening to Mobile Audio Device");
		}
  }
}

