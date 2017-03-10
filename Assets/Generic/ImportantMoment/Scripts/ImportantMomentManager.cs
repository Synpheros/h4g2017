using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

namespace ImportantManager{
public class ImportantMomentManager : MonoBehaviour {

	public GameObject im, opts, white;
	GameObject canvas, mainworld, grayscaleworld;
	Grayscale g;

	enum IMState { NONE, FLASHING, COUNTING, HIDING, OPTIONS}

	IMState state = IMState.NONE;
	bool tofade = false;

	// Use this for initialization
	void Start () {
		g = Camera.main.GetComponent<Grayscale> ();
		g.enabled = false;
		canvas = GameObject.Find ("Canvas");
		opts.SetActive(false);
		im.SetActive (false);
		white.SetActive (false);
	}
	
	// Update is called once per frame
	float flashtime = -1;
	float showtime = 4f, time_to_show = 0;
	bool up = true;
	void Update () {
		if (tofadewhite) {
			foreach(Transform t in mainworld.transform){
				Fade f = t.GetComponent<Fade> ();
				if (f != null && f.image != null) {
					tofade = false;
					f.FadeOut ();
				}
			}
		}

		switch (state) {
		case IMState.FLASHING:
			if (up) {
				flashtime += Time.deltaTime * 4f;
				g.rampOffset = flashtime > 1f ? 1f : flashtime;
				if (flashtime > 1f) {
					up = false;
					flashtime = 1f;
				}
			} else {
				flashtime -= Time.deltaTime * 4f;

				g.rampOffset = flashtime < 0f ? 0f : flashtime;
				if(g.rampOffset == 0f)
					state = IMState.COUNTING;
				time_to_show = 0;
			}
			break;
		case IMState.COUNTING:
			time_to_show += Time.deltaTime;
			if (time_to_show > showtime) {
				showOptions ();
				state = IMState.OPTIONS;
			}
			break;
		case IMState.HIDING:
			if (up) {
				flashtime += Time.deltaTime * 4f;
				g.rampOffset = flashtime > 1f ? 1f : flashtime;
				if (flashtime > 1f) {
					up = false;
					flashtime = 1f;
					state = IMState.NONE;
					g.enabled = false;
					white.SetActive (true);
					white.GetComponent<Image> ().CrossFadeAlpha (0f, 0.5f, false);
				}
			}
			break;
		default:
			break;
		}
	}

	public void hide(){
		im.gameObject.SetActive (false);
		g.rampOffset = -1;
		deflash ();
	}

	public void show(){
		g.enabled = true;
		im.gameObject.SetActive (true);
		g.rampOffset = -1;
		flash ();
	}

	public void showOptions (){
		opts.SetActive (true);
		opts.GetComponent<Options> ().fade_out_instant ();
		opts.GetComponent<Options> ().fade_in ();
	}

	public void flash(){
		state = IMState.FLASHING;
		up = true;
		flashtime = -1;
	}

	public void deflash(){
		state = IMState.HIDING;
		up = true;
		flashtime = 0;
	}

	bool tofadewhite = false;
	public void fadewhite(){
		tofadewhite = true;
	}

	public void duplicateWorld(){
		/*mainworld = GameObject.Find ("MainWorld");

		grayscaleworld = GameObject.Instantiate (mainworld);

		grayscaleworld.transform.SetParent (canvas.transform);

		grayscaleworld.GetComponent<RectTransform> ().localPosition = new Vector3(0,0,0);

		foreach(Transform t in grayscaleworld.transform){
			Image i = t.GetComponent<Image> ();
			if (i != null) {
				i.material = new Material (grayscale);
				i.color = new Color(1,1,1,0);
			}
		}

		foreach(Transform t in mainworld.transform){
			Image i = t.GetComponent<Image> ();
			if (i != null) {
				Fade f = t.gameObject.AddComponent<Fade> ();
				f.FadeRate = 0.5f;
			}
		}

		mainworld.transform.SetParent (Camera.main.transform);
		mainworld.transform.SetParent (canvas.transform);
		tofade = true;*/
	}
}
}