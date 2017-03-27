using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour {

	protected Transform m_transform;
	public Transform m_enemy;
	public float liveTime = 5;
	protected Transform m_player;
	// Use this for initialization
	void Awake(){
		GameObject obj = GameObject.FindGameObjectWithTag ("Player");
		if (obj != null) {
			m_player = obj.transform;
		}
	}
	void Start () {
		m_transform = this.transform;
	}
	
	// Update is called once per frame
	void Update () {
		liveTime -= Time.deltaTime;
		if (liveTime <= 0 && m_player != null) {
			liveTime = Random.value*15;
			if (liveTime < 5)
				liveTime = 5;
			Vector3 relativePos = m_transform.position - m_player.position;
			Instantiate (m_enemy, m_transform.position, Quaternion.LookRotation(relativePos));
		}
	}

	void OnDrawGizmos(){
		Gizmos.DrawIcon (transform.position,"item.png",true);
	}
}
