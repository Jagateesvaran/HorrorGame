using UnityEngine;
using System.Collections;

// Realtime Hearing AI -
//
// This is the Web Player version, it is no different than any other version, however, as this deals with Web Players,
// We have certain legal requirements we have to meet, and that is authorization.
// This just allows the user to authorize permission to use the Microphone. If they choose not to use it, then it won't turn on.

[RequireComponent(typeof(AudioSource))] // ---------- You must have an Audio SOURCE or this will not work.
public class WebPlayerMic : MonoBehaviour {
	
	// This is the bools section.
	public bool WebPlayerTrue; // -------------- If this is the webplayer, and is true, then start audio.
	//private bool micSelected = false; // ------- Have we selected a mic yet?

	// public Variables ---------------------------------------- Don't touch these unless you know what you are doing.
	public int detail = 500; // -------------------------------- How much detail is in the sound?
	public float minValue = 0.5f; // ----- ----------------------What's the minimum value?
	public float amp = 0.5f; // -------------------------------- How much do we want to multiply the Package Data?
	public string selectedDevice { get; private set; }	// ----- Search for a selected device;

	// Audio Variables INTERNAL ONLY (Change at your own risk).
	public AudioSource PlayerMicInput;// ---------------- This is where we drag and drop our Audio Source for microphone Input.
	public int volume; // ------------------------------- If you don't know what Valume is, you shouldn't be programming :P.
	private Vector3 startPos; //  ----------------------- Don't touch this. Well unless you know what you are doing.
	public float sensitivity = 100; // ------------------ How sensitive does the mic need to be to hear noise that you make?
	public float loudness { get; private set; } // ------ DON'T CHANGE THIS, UNLESS YOU KNOW WHAT YOU ARE DOING!!
	private float ClearRamTimer = 30; // --------------------- How often to clear memory.
	private bool micSelected = false;
	public bool GuiSelectDevice = true;

	public float volumeinput;

	//---------------------------------------------------------------------------------------------------------------------------------





	IEnumerator Start () {
// We use this to allow the user to authenticate the Microphone usage, after it is authenticated, we call InitializeFirstTimeMic and make sure the scale of the sphere is correct.
				yield return Application.RequestUserAuthorization (UserAuthorization.Microphone);
				if (Application.HasUserAuthorization (UserAuthorization.Microphone)) {
				InitializeFirstTimeMic ((Screen.width/2)-150, (Screen.height/2)-75, 300, 100, 10, -300);
				startPos = transform.localScale;
			}
		}
	// We use this to Initialize permission for the first time the Microphone is accessed by player.
	// This is due to Web Player security, we must do this (Legally Speaking).
	void InitializeFirstTimeMic(float left, float top, float width, float height, float buttonSpaceTop, float buttonSpaceLeft) {
		
		if (Microphone.devices.Length > 1 && GuiSelectDevice == true || micSelected == false)//If there is more than one device, choose one.
			for (int i = 0; i < Microphone.devices.Length; ++i)
			if (GUI.Button(new Rect(left + ((width + buttonSpaceLeft) * i), top + ((height + buttonSpaceTop) * i), width, height), Microphone.devices[i].ToString())) {
				
				if (PlayerMicInput == null) { // ----- If there is no Audio Source in the Player Mic Input, we need to add one.
					Debug.LogWarning (" There is no Audio Source in the Player Mic Input, you should add one. Then I shall work :)");
					return;
				}
				
				StopMicrophone();
				selectedDevice = Microphone.devices[i].ToString();
				StartMicrophone();
				micSelected = true;
			}
	}


	public void StartMicrophone () {
		WebPlayerTrue = true;
		PlayerMicInput.clip = Microphone.Start(selectedDevice,true, 30, 44100);//Starts recording
		//while (!(Microphone.GetPosition(selectedDevice) > 0)){} // Wait until the recording has started
		PlayerMicInput.PlayDelayed (0.2f); // ---------- We purposely create a delay of 0.2 Seconds, if you don't the Microphone and Audio can't keep up and becomes glitchy!
		PlayerMicInput.Play(); // Play the audio source!
	}
	
	public void StopMicrophone () {
		WebPlayerTrue = false;
		PlayerMicInput.Stop();//Stops the audio
		Microphone.End(selectedDevice);//Stops the recording of the device	
	}	
	
	void OnGUI() {
		if (Input.GetKey("z"))
		{
			InitializeFirstTimeMic((Screen.width / 2) - 150, (Screen.height / 2) - 75, 300, 100, 10, -300);
		}
		else if(Input.GetKey("x"))
		{
			InitializeFirstTimeMic(0, 0, 0, 0, 0, 0);
		}

	
		if (Microphone.IsRecording(selectedDevice)) {
			ClearRamTimer += Time.deltaTime;
			ClearRam();
		}
	}


	
	void Update () {
			Logic();   // ------------ Logic deals with all the important stuff.
		    ClearRam (); // ---------- This is where we clear the ram so the system doesn't overload memory past playablilty.

		
	}

	// This is where we clear our memory at.
	void ClearRam(){
				if (WebPlayerTrue) {
						ClearRamTimer -= Time.deltaTime;
						if (ClearRamTimer <= 0) {
								Microphone.End ("Built-in Microphone");
								PlayerMicInput.clip = Microphone.Start ("Built-in Microphone", true, 30, 44100);
								PlayerMicInput.PlayDelayed (0.2f);
								ClearRamTimer = 30;
						}
				}
	}

	// This is where all the magic happens :).
	void Logic(){
				// Internal use only.
				if (WebPlayerTrue) {
						// This section deals with the Internal Components (Volume, ETC).
						GetComponent<AudioSource>().volume = (volume);
						loudness = volume * sensitivity;// *(volume/10);

	
						// This section of code deals with how far the detection zone goes, the louder something is, the further it can be heard.
			            //----------------------------------------------------------------------------------------------------------------
			            float[] info = new float[detail]; // ---------------- We Declare a new float and name it Detail.
						PlayerMicInput.GetOutputData (info, 0); // ---------- We call the Microphone Input and ask for the DataOutput.
						float packagedData = .5f; // ------------------------ we give this Dataoutput a float of .5f.
		                //----------------------------------------------------------------------------------------------------------------
						for (int x = 0; x <info.Length; x++) { // ---------------- We scan through the data.
						packagedData += System.Math.Abs (info [x]);//-------- ---- Doing a little math with it.
						}
			            //----------------------------------------------------------------------------------------------------------------
						transform.localScale = Vector3.one * packagedData; // ---- We are getting ready to multiply the axis of the sphere.
						packagedData = transform.localScale.x; // ---------------- Again, a little more setting up the axis of sphere.
						transform.localScale = new Vector3 (minValue, (packagedData * amp) + startPos.y, minValue); // ----- This is how we Multiply the sphere size based on Audio.
						volumeinput = transform.localScale.y;
						//Debug.Log(transform.localScale.y.ToString());            
			//----------------------------------------------------------------------------------------------------------------
			Debug.Log ("Microphone is now admiting sound into your game, you may delete this debug log in code if you no longer need it.");
				}
		}
}