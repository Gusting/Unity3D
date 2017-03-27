using UnityEngine;
using System.Collections;
using System;
using Mono.Data.Sqlite;
using System.Data;

[AddComponentMenu("MyGame/GameManager")]
public class GameManager : MonoBehaviour {

	public static GameManager Instance;
	//得分
	public int m_score;
	//最高分
	public static float m_hiscore;
	//主角
	protected Player m_player;
	//背景音乐
	public AudioClip m_musicClip;
	//声音源
	protected AudioSource m_audio;
    //数据库连接
    SqliteDbHelper db;

    //历史前五的成绩
    public ArrayList score = new ArrayList();
    int[] IntScore;
    bool buttonDown = false;

	void Awake(){
		Instance = this;
	}
	// Use this for initialization
	void Start () {
		m_audio = GetComponent<AudioSource> ();

		//得到主角
		GameObject obj = GameObject.FindGameObjectWithTag("Player");
		if (obj != null) {
			m_player = obj.GetComponent<Player> ();
		}
        //连接数据库
        db = new SqliteDbHelper("Data Source=C:/sqlite/database/plane.db");
        Debug.Log(db.ToString());

        //获取前五的成绩
        if (GetScore())
        {
            IntScore = new int[score.Count];
            score.CopyTo(IntScore);
        }
	}
	
	// Update is called once per frame
	void Update () {
		//循环播放
		if (!m_audio.isPlaying) {
			m_audio.clip = m_musicClip;
			m_audio.Play ();
		}
		//暂停游戏
		if (Time.timeScale > 0 && Input.GetKeyDown (KeyCode.Escape)) {
			Time.timeScale = 0;
		}
	}
	void OnGUI(){
		if (Time.timeScale == 0) {
			if (GUI.Button (new Rect (Screen.width * 0.5f - 50, Screen.height * 0.4f, 100, 30), "继续游戏")) {
				Time.timeScale = 1;
			}
			//退出游戏
			if (GUI.Button (new Rect (Screen.width * 0.5f - 50, Screen.height * 0.6f, 100, 30), "退出游戏")) {
				Application.Quit();
			}



		}

		int life = 0;
		if (m_player != null) {
			life = (int)m_player.m_life;
		} else {
           
			//放大字体
			GUI.skin.label.fontSize = 50;

			//提示游戏失败
			GUI.skin.label.alignment=TextAnchor.LowerCenter;
			GUI.Label (new Rect (0, Screen.height * 0.2f, Screen.width, 60), "游戏失败");


            GUI.skin.label.fontSize = 20;
            //显示按钮
            if (GUI.Button(new Rect(Screen.width * 0.5f - 50, Screen.height * 0.5f + 150, 100, 30), "再试一次"))
            {
                Application.LoadLevel(Application.loadedLevelName);
            }


           //获取最小成绩
            int min = IntScore[0];
            for (int i = 0; i < IntScore.Length; i++)
            {
                if (IntScore[i] < min)
                    min = IntScore[i];
            }
            //如果进入前五 提示
            if (m_score >= min)
            {
                //放大字体
                GUI.skin.label.fontSize = 50;
                //提示游戏失败
                GUI.skin.label.alignment = TextAnchor.LowerCenter;
                GUI.Label(new Rect(0, Screen.height * 0.5f - 50, Screen.width, 60), "您破了纪录");
                GUI.skin.label.fontSize = 30;

                //将高分代替低分
                db.UpdateInto("five", m_score, min);
                db.CloseSqlConnection();
                Debug.Log("close table ok");

                //在数组中重新排位
                for (int i = 0; i < IntScore.Length; i++)
                {
                    if (IntScore[i] == min)
                    {
                        IntScore[i] = m_score;
                        break;
                    }
                }
    

            }
            if (GUI.Button(new Rect(Screen.width * 0.5f - 50, Screen.height * 0.5f + 50, 100, 30), "查看记录"))
            {
                buttonDown = true;
                //endConnection();
                Debug.Log(score.Count);

            }
    

		}
        if (buttonDown == true)
        {
            for (int i = 0; i < IntScore.Length; i++)
            {
                GUI.skin.label.fontSize = 30;
                GUI.skin.label.alignment = TextAnchor.LowerCenter;
                GUI.Label(new Rect(Screen.width * 0.5f + 150, Screen.height * 0.5f + 50, 100, (i + 1) * 40), IntScore[i].ToString());
            }

            //db.CloseSqlConnection();

        }
		GUI.skin.label.fontSize = 15;
		//显示生命值
		GUI.Label(new Rect(5,5,100,30),"装甲  "+life);
		//show the highScore
		GUI.skin.label.alignment = TextAnchor.LowerCenter;
		GUI.Label (new Rect (0, 5, Screen.width, 30), "记录" + m_hiscore);
		//shou the score you get
		GUI.Label(new Rect (0,25,Screen.width,30), "得分" + m_score);

	}

	//add score
	public void AddScore( int point ){
		m_score += point;

		//更新高分record
		if (m_hiscore < m_score) {
			m_hiscore = m_score;
		}
	}

    //获取历史前五记录
    bool GetScore()
    {
        IDataReader sqReader = db.SelectWhere("five");
        while (sqReader.Read())
        {
            //Debug.Log(sqReader.GetString(sqReader.GetOrdinal("score")));
            score.Add( sqReader.GetInt32(sqReader.GetOrdinal("score")));
        
        }

        if (score != null)
            return true;

        return false;
    }
    void endConnection()
    {
        db.CloseSqlConnection();
        Debug.Log("close table ok");
    }
}
