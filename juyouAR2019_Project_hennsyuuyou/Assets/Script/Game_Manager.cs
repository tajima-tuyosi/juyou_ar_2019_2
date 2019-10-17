using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TagGet;



public class Game_Manager : MonoBehaviour
{
    //オブジェクトとその取得状況管理用のDictionary。Sortedで自動昇順ソート
    private SortedDictionary<string, bool> collection_table = new SortedDictionary<string, bool>();
    //プロパティ化して他のスクリプトで呼び出せるようにする
    public SortedDictionary<string, bool> Collection_Table
    {
        get { return this.collection_table; }
        set { this.collection_table = value; }
    }
    public void Collection_Table_Set(string name, bool torf)
    {
        collection_table[name] = torf;
    }

    public string[] Collection1_name = {""};

    GameObject menu_UI;
    GameObject collection_UI;


    void Awake()
    {
        int count = 0;
        //コレクション一覧をハッシュに登録。
        //GameObject[] collections = GameObject.FindGameObjectsWithTag("Image_Target");
        GameObject[] collections = TagUtility.getParentTagObjects("Image_Target");
        foreach (GameObject collection in collections)
        {
            //マーカーではなく、3Dオブジェクトの名前を格納
            collection_table.Add(collection.transform.GetChild(0).name, false);
        }

        //add
        GameObject[] collection1s = TagUtility.getChildTagObjects("Collection1");
        System.Array.Resize(ref Collection1_name, collection1s.Length);
        for (int i = 0 ; i< collection1s.Length ; i++)
        {
            //マーカーではなく、3Dオブジェクトの名前を格納
            Collection1_name[i] = collection1s[i].transform.GetChild(0).name;
        }


        //非アクティブのメニュー、コレクション一覧UIを一度起動してスクリプトを動かしておく。
        menu_UI = GameObject.Find("Canvas").transform.Find("Collection_Check_UI").gameObject;
        collection_UI = GameObject.Find("Canvas").transform.Find("Menu_UI").gameObject;
        menu_UI.SetActive(true);
        menu_UI.SetActive(false);
        collection_UI.SetActive(true);
        collection_UI.SetActive(false);

    }

    void Update()
    {

    }


    public void Reset_Collection_Table()
    {

        //直接変更するとエラーになるので添付を経由
        SortedDictionary<string, bool> temp_table = new SortedDictionary<string, bool>();
        foreach (KeyValuePair<string, bool> collection in collection_table)
        {
            temp_table[collection.Key] = false;
        }
        collection_table = temp_table;
    }

}
