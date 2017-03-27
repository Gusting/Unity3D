using UnityEngine;
using System.Collections;
[AddComponentMenu("MyGame/Enemy")]
public class Enemy : MonoBehaviour {
	public float m_speed = 2;
	protected float m_rotSpeed = 30;
	protected float m_timer = 1.5f;

	public int m_point = 10;

	public float m_life = 10;//生命zhi

	protected Transform m_transform;

	protected Transform m_bound;
	//set 爆炸效果
	public Transform m_explosionFX;
	// Use this for initialization

	void Awake(){
		GameObject obj = GameObject.FindGameObjectWithTag ("Plane");
		if (obj != null)
			m_bound = obj.transform;
	}

	void Start () {
		m_transform = this.transform;

	}
	
	// Update is called once per frame
	void Update () {
		UpDateMove ();
	}
	protected virtual void UpDateMove(){//virtual 虚方法 继承后可以重写
		m_timer -= Time.deltaTime;

		if (m_timer <= 0) {
			m_timer = 3;
			m_rotSpeed = -m_rotSpeed;
		}

		//旋转方向
		m_transform.Rotate(Vector3.up, m_rotSpeed*Time.deltaTime,Space.World);

		//前进
		m_transform.Translate(new Vector3(0,0,-m_speed*Time.deltaTime));

		if (m_transform.position.z <= -(m_bound.position.z + m_bound.lossyScale.z/2*10)+1) //
			Destroy (this.gameObject);
		if (m_transform.position.x <= -(m_bound.position.x + m_bound.lossyScale.x / 2 * 10)+1)
			Destroy (this.gameObject);
		if (m_transform.position.z >= (m_bound.position.z + m_bound.lossyScale.z / 2 * 10)-1) 
			Destroy (this.gameObject);
		if (m_transform.position.x >= (m_bound.position.x + m_bound.lossyScale.x / 2 * 10)-1)
			Destroy (this.gameObject);
	}

	void OnTriggerEnter(Collider Other){
		if (Other.tag.CompareTo ("PlayerRocket") == 0) {
			Rocket rocket = Other.GetComponent<Rocket> ();
			if (rocket != null) {
				m_life -= rocket.m_power;
				if (m_life <= 0) {
					GameManager.Instance.AddScore (m_point);
					Instantiate (m_explosionFX, m_transform.position, Quaternion.identity);
					Destroy (this.gameObject);
				}
			}
		} else if (Other.tag.CompareTo ("Player") == 0) {
			m_life = 0;
			Instantiate (m_explosionFX, m_transform.position, Quaternion.identity);
			Destroy (this.gameObject);
		}// else if (Other.tag.CompareTo ("bound") == 0) {
			//m_life = 0;
			//Destroy (this.gameObject);
		//}
	}
}
