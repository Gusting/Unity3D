using UnityEngine;
using System.Collections;
[AddComponentMenu("MyGame/Player")]
public class Player : MonoBehaviour {
	public float m_speed;
	protected Transform m_transform;//提高效率

	public Transform m_rocket;
	public float m_rocketRate = 0;//子弹发射频率

	public float m_life = 3;

	//set 声音
	protected AudioSource m_audio;

	public AudioClip m_shootClip;

	//爆炸特效
	public Transform m_explosionFX;

	protected Transform m_plane;

	// Use this for initialization
	void Awake(){
		GameObject obj = GameObject.FindGameObjectWithTag ("Plane");
		if (obj != null) {
			m_plane = obj.transform;
		}

	}
	void Start () {
		m_transform = this.transform;
		m_audio = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		float movev = 0;
		float moveh = 0;
		Vector3 vecBottom = new Vector3 (0, 0, 0.1f);
		Vector3 vecUp = new Vector3 (0, 0, -0.1f);
		Vector3 vecLeft = new Vector3 (0.1f, 0, 0);
		Vector3 vecRight = new Vector3 (-0.1f, 0, 0); 

		if (Input.GetKey (KeyCode.UpArrow)||Input.GetKey(KeyCode.W)) {
			movev -= m_speed * Time.deltaTime;
		}
		if (Input.GetKey (KeyCode.DownArrow)||Input.GetKey(KeyCode.S)) {
			movev += m_speed * Time.deltaTime;
		}

		if (Input.GetKey (KeyCode.LeftArrow)||Input.GetKey(KeyCode.A)) {
			moveh += m_speed * Time.deltaTime;	
		}
		if (Input.GetKey (KeyCode.RightArrow)||Input.GetKey(KeyCode.D)) {
			moveh -= m_speed * Time.deltaTime;		
		}

		if (m_transform.position.z <= (m_plane.position.z - m_plane.lossyScale.z / 2 * 10)+1) {
			m_transform.position = m_transform.position + vecBottom;
		} else if (m_transform.position.z >= (m_plane.position.z + m_plane.lossyScale.z / 2 * 10)-1) {
			m_transform.position = m_transform.position + vecUp;
		} else if (m_transform.position.x <= (m_plane.position.x - m_plane.lossyScale.x / 2 * 10)+1) {
			m_transform.position = m_transform.position + vecLeft;
		} else if (m_transform.position.x >= (m_plane.position.x + m_plane.lossyScale.x / 2 * 10)-1) {
			m_transform.position = m_transform.position + vecRight;
		} else {
			m_transform.Translate (moveh, 0, movev);
		}


		m_rocketRate -= Time.deltaTime;
		if (m_rocketRate <= 0) {
			m_rocketRate = 0.1f;
			if (Input.GetKey (KeyCode.Space) || Input.GetMouseButton (0)) {
				Instantiate (m_rocket, m_transform.position, m_transform.rotation);
				//播放射ji声音
				m_audio.PlayOneShot (m_shootClip);
			}
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.tag.CompareTo ("Enemy") == 0) {
			m_life -= 1;
			if (m_life <= 0) {
				Destroy (this.gameObject);
				Instantiate (m_explosionFX, m_transform.position, Quaternion.identity);
			}
		} else if (other.tag.CompareTo ("EnemyRocket") == 0) {
			EnemyRocket rocket = other.GetComponent<EnemyRocket>();
			m_life -= rocket.m_power;

			if (m_life <= 0) {
				Destroy (this.gameObject);
				Instantiate (m_explosionFX, m_transform.position, Quaternion.identity);
			}
		}
	}


}
