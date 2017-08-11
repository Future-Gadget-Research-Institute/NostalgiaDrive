using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SphereJump : MonoBehaviour
{
	public Rigidbody rb;
	public Vector3 com;
	public GameObject explosion;

	public Text scoreText;
	public GameObject gameOverPanel;
	public GameObject startPanel;

	public Button restart;
	public Button exit;

	public Material[] SkyBoxSeries;

	int score = 0;

	void Start ()
	{
		rb = GetComponent<Rigidbody> ();
		rb.centerOfMass = com;

		rb.isKinematic = true;

		SetScoreText();
		gameOverPanel.SetActive(false);

		Button btn = restart.GetComponent<Button>();
		btn.onClick.AddListener(Restart);

		Button btn1 = exit.GetComponent<Button>();
		btn1.onClick.AddListener(Exit);
	}

	void FixedUpdate () 
	{
		if (Input.GetKeyUp ("space"))
		{
			rb.AddForce (new Vector3 (36.0f, 36.0f, 0), ForceMode.Impulse);
		}

		int temp = (int)this.transform.position.x;
		score = (int) temp / 21;
		SetScoreText ();

		if (score > 4)
		{
			RenderSettings.skybox = SkyBoxSeries[score / 4 % 4];
		}
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.name == "UpperUnits" || col.gameObject.name == "LowerUnits" || col.gameObject.name == "Cubes")
		{
			GameObject expl = Instantiate (explosion, transform.position, Quaternion.identity) as GameObject;
			this.gameObject.SetActive(false);
			Destroy (expl, 2.0f);
			gameOverPanel.SetActive(true);
		}
	}

	public void SetScoreText ()
	{
		scoreText.text = "Score: " + score.ToString ();
	}

	public void StartPanel()
	{
		startPanel.SetActive (false);
		rb.isKinematic = false;
	}

	public void Restart()
	{
		Application.LoadLevel(Application.loadedLevel);
	}

	public void Exit()
	{
		Application.Quit();
	}
}
