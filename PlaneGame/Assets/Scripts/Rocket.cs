using UnityEngine;
using System.Collections;

[AddComponentMenu("MyGame/Rocket")]
public class Rocket : MonoBehaviour {
	public float m_speed;
	public float m_liveTime;
	public float m_power;

	protected Transform m_transform;
	// Use this for initialization
	void Start () {
		m_transform = this.transform;
	}
	
	// Update is called once per frame
	void Update () {
		m_liveTime -= Time.deltaTime;
		if (m_liveTime < 0) {
			Destroy(this.gameObject);
		}

		m_transform.Translate (new Vector3(0,0,-m_speed*Time.deltaTime));
	}

	void OnTriggerEnter(Collider other){
		if (other.tag.CompareTo ("Enemy") == 0) { 
			Destroy (this.gameObject);
		}
	}
}
