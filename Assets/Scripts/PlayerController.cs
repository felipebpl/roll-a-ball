using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
	
	public float speed;
	public TextMeshProUGUI countText;
	public float gameTime = 60.0f;
	private float currentTime;
	public TextMeshProUGUI timerText;

	private float movementX;
	private float movementY;

	private Rigidbody rb;
	private int count;

	public AudioClip pickupSound;
	private AudioSource audioSource;

	private float fallLimit = -1.0f;

	public AudioClip ticTacSound;
	private float lastTicTacTime;


	void Start () {
		rb = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource>();
		count = 0;
		lastTicTacTime = -1f;
		currentTime = gameTime;
		timerText.text = "Time: " + Mathf.Round(currentTime).ToString() + "s";
		SetCountText ();
	}

	void FixedUpdate () {
		Vector3 movement = new Vector3 (movementX, 0.0f, movementY);
		rb.AddForce (movement * speed);

		currentTime -= Time.fixedDeltaTime;

		if (currentTime <= 10) {
			timerText.color = Color.red;

			if (Mathf.Floor(currentTime) != lastTicTacTime) {
				audioSource.PlayOneShot(ticTacSound);
				lastTicTacTime = Mathf.Floor(currentTime);
			}
		} else {
			timerText.color = Color.white;
		}
				timerText.text = "Time: " + Mathf.Round(currentTime).ToString() + "s";

		if (currentTime <= 0 || transform.position.y < fallLimit) {
		    EndGame();
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("PickUp")) {
			other.gameObject.SetActive (false);
			count = count + 1;
			SetCountText ();
			audioSource.PlayOneShot(pickupSound);
		}
	}

	void OnMove(InputValue value) {
        	Vector2 v = value.Get<Vector2>();
        	movementX = v.x;
        	movementY = v.y;
        }

	void SetCountText() {
		countText.text = "Count: " + count.ToString();

		if (count >= 13) {
			SceneManager.LoadScene("WinMenu");
		}
	}

	void EndGame() {
	    SceneManager.LoadScene("DeadEnd");
	}
}
