using UnityEngine;
using System.Collections;
[AddComponentMenu("MyGame/EnemyRocket")]
public class EnemyRocket : MonoBehaviour{
	public float m_speed;
	public float m_liveTime;
	public float m_power;
	private Transform m_transform;
	void Start(){
		m_transform = this.transform;
	}

	void Update(){
		m_transform.Translate (0,0,-m_speed*Time.deltaTime);

		m_liveTime -= Time.deltaTime;
		if (m_liveTime < 0) {
			Destroy(this.gameObject);
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.tag.CompareTo ("Player") == 0) {
			Destroy (this.gameObject);
		}
	}
}
