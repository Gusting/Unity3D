 using UnityEngine;
using System.Collections;

[AddComponentMenu("MyGame/SuperEnemy")]
public class SuperEnemy : Enemy {

	public Transform m_rocket;
	protected float m_rocketRate = 0;
	protected Transform m_player;

	void Awake(){
		GameObject obj = GameObject.FindGameObjectWithTag ("Player");
		if (obj != null) {
			m_player = obj.transform;
		}
		GameObject obj2 = GameObject.FindGameObjectWithTag ("Plane");
		if (obj != null)
			m_bound = obj2.transform;
	}
	protected override void UpDateMove(){
		m_transform.Translate (new Vector3(0,0,-m_speed*Time.deltaTime));

		m_rocketRate -= Time.deltaTime;
		if (m_rocketRate <= 0 && m_player != null) {
			m_rocketRate = 2.0f;
			Vector3 relativePos = m_transform.position - m_player.position;
			Instantiate (m_rocket, m_transform.position, Quaternion.LookRotation(relativePos));
		}

		if (m_transform.position.z <= -(m_bound.position.z + m_bound.lossyScale.z/2*10)+1) //
			Destroy (this.gameObject);
		if (m_transform.position.x <= -(m_bound.position.x + m_bound.lossyScale.x / 2 * 10)+1)
			Destroy (this.gameObject);
		if (m_transform.position.z >= (m_bound.position.z + m_bound.lossyScale.z / 2 * 10)-1) 
			Destroy (this.gameObject);
		if (m_transform.position.x >= (m_bound.position.x + m_bound.lossyScale.x / 2 * 10)-1)
			Destroy (this.gameObject);
	}
}
