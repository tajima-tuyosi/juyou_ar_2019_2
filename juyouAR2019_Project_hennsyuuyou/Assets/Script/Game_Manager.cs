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

        //スクショ用
    public GameObject content;
    private RectTransform camerafinder_rect;
    public Camera camera;
    private float scale_ratio;
    private Vector2 camerafinder_senter;
    private Rect rect;
    private Vector2 pivot;


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

        camerafinder_rect = GameObject.Find("CameraFinder").GetComponent<RectTransform>();

        //canvasとスクリーンの縮尺比を出す。
        scale_ratio = Screen.width / GameObject.Find("Canvas").GetComponent<RectTransform>().sizeDelta.x;
        camerafinder_senter = new Vector2(Screen.width / 2 + camerafinder_rect.anchoredPosition.x * scale_ratio, Screen.height / 2 + camerafinder_rect.anchoredPosition.y * scale_ratio);
        //左下を軸に、横幅と縦幅を左上に向けて伸ばす
        rect = new Rect(camerafinder_senter.x - camerafinder_rect.sizeDelta.x / 2 * scale_ratio, camerafinder_senter.y - camerafinder_rect.sizeDelta.y / 2 * scale_ratio, camerafinder_rect.sizeDelta.x * scale_ratio, camerafinder_rect.sizeDelta.y * scale_ratio);
        pivot = new Vector2(0.5f, 0.5f); //画像中心にピボットを置く


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

    //スクショを乗せるオブジェクトを引数にしてスクショ撮影、保存する
    public void CaptureScreen(GameObject obj)
    {
        Texture2D screenShot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        RenderTexture rt = new RenderTexture(screenShot.width, screenShot.height, 24);
        RenderTexture prev = camera.targetTexture; //カメラ映像に値を戻せるようにprevに保存
        camera.targetTexture = rt; //スクショ用変数を代入
        camera.Render(); // カメラの画像をrtに取得
        camera.targetTexture = prev;//画面をカメラに戻す
        RenderTexture.active = rt;//rtのみアクティブ状態にする。
        screenShot.ReadPixels(new Rect(0, 0, screenShot.width, screenShot.height), 0, 0);
        screenShot.Apply(); //ReadPixcelを反映

        //必要なくなったrtをメモリ削減のため削除
        Destroy(rt);
        Sprite sprite = Sprite.Create(screenShot, rect, pivot);
        //メモリを圧迫しないようにTexture2Dの破棄
        //Debug.Log("後: " + screenShot);
        content.GetComponent<Content>().Input_ScreenShot_Image(obj.name, sprite);
        //Destroy(screenShot);
        //Destroy(sprite);
    }


}
