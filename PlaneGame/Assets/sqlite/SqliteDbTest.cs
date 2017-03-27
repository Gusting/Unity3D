/*using UnityEngine;
using System.Collections;
using System;
using Mono.Data.Sqlite;
using System.Data;
public class SqliteDbTest : MonoBehaviour
{

    SqliteDbHelper db;
    int id = 1;
    void Start()
    {
        db = new SqliteDbHelper("Data Source=C:/sqlite/database/sq.db");
        Debug.Log(db.ToString());
       // 
         SqliteDbAccess db = new SqliteDbAccess("data source=mydb1.db"); 
       //db.CreateTable("momo",new string[]{"name","qq","email","blog"}, 
       // new string[]{"text","text","text","text"}); 
       //db.CloseSqlConnection(); 
        //
    }  
    public string name = "";
    public string emls = "";
    void OnGUI()
    {

        if (GUILayout.Button("create table"))
        {
            db.CreateTable("mytable", new string[] { "id", "name", "email" }, new string[] { "int", "varchar(20)", "varchar(50)" });
            Debug.Log("create table ok");
        }

        if (GUILayout.Button("insert data"))
        {

            db.InsertInto("mytable",
                         new string[] { "" + (++id), "'随风去旅行" + id + "'", "'zhangj_live" + id + "@163.com'" });//),"'aaa"+id+"'","'aaa"+id+"@sohu.com'"});  

            Debug.Log("insert table ok");
        }


        if (GUILayout.Button("search database"))
        {
            IDataReader sqReader = db.SelectWhere("mytable", new string[] { "name", "email" }, new string[] { "id" }, new string[] { "=" }, new string[] { "2" });
            while (sqReader.Read())
            {

                //Debug.Log(  
                name = "name=" + sqReader.GetString(sqReader.GetOrdinal("name"));// +  
                emls = "email=" + sqReader.GetString(sqReader.GetOrdinal("email"));
                //);  
            }

        }
        if (name != "")
        {
            GUI.Label(new Rect(100, 100, 100, 100), name);
            GUI.Label(new Rect(100, 200, 100, 100), emls);
            //  GUILayout.Label(emls);  
        }
        if (GUILayout.Button("close database"))
        {
            db.CloseSqlConnection();
            Debug.Log("close table ok");
        }

    }

}*/