using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//contentにアタッチ
//コレクションの状況を確認するUIのスクリプト

public class Content : MonoBehaviour
{
    Game_Manager game_manager_script;
    [SerializeField] GameObject collection_image_sample; //非アクティブで見つけられないためインスペクターに表示
    [SerializeField] GameObject collection_scroll_view; //非アクティブ回避用

    GameObject get_count_top_text;
    GameObject get_count_text;

    //Complete_Collections クラスのメソッドを呼びたすための定義
    [SerializeField] GameObject Complete_Image;

    //Complete_Collections Collection1がそろったときに表示するイメージ
    [SerializeField] GameObject Complete_Image_Collection1;


    [SerializeField] int collection_num_per_line = 5; //一行に置くコレクションの予定数
    Sprite unknown_image;

    string screen_direction = "";




    void Awake()
    {
        game_manager_script = GameObject.Find("GameManager").GetComponent<Game_Manager>();
        get_count_top_text = GameObject.Find("GetCountTop").transform.Find("GetCountText").gameObject;
        get_count_text = GameObject.Find("GetCount").transform.Find("GetCountText").gameObject;
       unknown_image = collection_image_sample.GetComponent<Image>().sprite;
        //コレクションごとにUIを作成
        Create_Collection_View_Object();
    }

    private void OnEnable()
    {
        Review_Collections_Get_Status();

    }

    void Update()
    {
        Check_Screen_Direction();
    }

    public void Create_Collection_View_Object()
    {
        foreach (KeyValuePair<string, bool> collection in game_manager_script.Collection_Table)
        {

            GameObject collection_view = (GameObject)Instantiate(collection_image_sample); //オブジェクトを生成
            collection_view.SetActive(true);//生成されたサンプルは非アクティブなのでアクティブ化
            collection_view.transform.SetParent(gameObject.transform, false); // 生成したオブジェクトをUIのcanvasの子クラスに
            collection_view.name = collection.Key + "_view";
        }
        Review_Collections_Get_Status();

        Alignment_Display_Collections();
    }


    //コレクション一覧をUI上に整列させる
    private void Alignment_Display_Collections()
    {
        //表示範囲(背景UIのサイズ)
        collection_scroll_view.GetComponent<UI_Size_Manager>().Check_Screen_Direction(); //参照元の値の更新
        float frame_width = collection_scroll_view.GetComponent<RectTransform>().rect.width;
        //Debug.Log(collection_scroll_view.GetComponent<RectTransform>().rect);

        //コレクションのサイズを生成済みのオブジェクトの一つから取得
        collection_image_sample.GetComponent<UI_Size_Manager>().Check_Screen_Direction(); //値の更新
        float collection_size_x = collection_image_sample.GetComponent<RectTransform>().sizeDelta.x;
        float collection_size_y = collection_image_sample.GetComponent<RectTransform>().sizeDelta.y;


        //コレクション間の距離
        float interval_x = 0.0f;
        int one_line_num;
        //コレクションが隣のコレクションと重なる場合は一行のコレクション数を減らす
        for (one_line_num = collection_num_per_line; one_line_num > 1; one_line_num--)
        {
            //collection_num_per_line = i;
            interval_x = (frame_width - collection_size_x / 2) / one_line_num;
            if (interval_x > collection_size_x)
            {
                break;
            }
        }

        float interval_y = collection_size_y / 2 * 3;

        float y = -collection_size_y / 3 * 2;

        int collecton_num_per_line_count = 0;
        //各コレクションの配置
        foreach (KeyValuePair<string, bool> collection in game_manager_script.Collection_Table)
        {
            //表示するためのUIの作成
            if (!this.transform.Find(collection.Key + "_view"))
            {
                Debug.Log("コレクション「" + collection.Key + "_view」が見つかりませんでした。");
            }
            else
            {
                GameObject collection_view = this.transform.Find(collection.Key + "_view").gameObject;

                if (collecton_num_per_line_count >= one_line_num)
                {
                    y -= interval_y;
                    collecton_num_per_line_count = 0;
                }

                //コレクションを表示する座標(左から順に並べる)
                float x = -one_line_num * (interval_x / 2) + (collecton_num_per_line_count * interval_x) + (interval_x / 2);
                //anchoredPosition?
                collection_view.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);

                collecton_num_per_line_count += 1;
            }

        }


        Review_Collections_Get_Status();

        //スクロールバーの縦移動範囲制限のためにcontentオブジェクトの長さを調節
        this.GetComponent<RectTransform>().sizeDelta = new Vector2(0, Mathf.Abs(y - interval_y));

    }


    //コレクションすべての所持判定
    public void Review_Collections_Get_Status()
    {
        int complete = 0;
        foreach (KeyValuePair<string, bool> collection in game_manager_script.Collection_Table)
        {
            //complete = を追加
            complete += Review_Collection_Get_Status(collection.Key, collection.Value);
        }

        if (complete == game_manager_script.Collection_Table.Count)
        {
            Complete_Image.SetActive(true);
        }

        //add
        Review_Collection1_Get_Status();

        //コレクション所持数をUIに反映
        //string text = "みつけたかず　" + complete + "/" + game_manager_script.Collection_Table.Count + "コ";
        get_count_top_text.GetComponent<Text>().text = "みつけたかず\n" + complete + "/" + game_manager_script.Collection_Table.Count + "コ";
        get_count_text.GetComponent<Text>().text = "みつけたかず　" + complete + "/" + game_manager_script.Collection_Table.Count + "コ";

    }

    //そのコレクションを所持しているかの判定とテキスト編集
    private int Review_Collection_Get_Status(string name, bool torf)
    {
        if (!this.transform.Find(name + "_view"))
        {
            Debug.Log("コレクション「" + name + "_view」が見つかりませんでした。");
            return 0;
        }

        //string collection_name = collection.transform.GetChild(0).name;
        Text collection_text = this.transform.Find(name + "_view").transform.Find("Collection_Text").GetComponent<Text>();
        Image collection_image = this.transform.Find(name + "_view").GetComponent<Image>();
        if (torf)
        {
            //Sprite sp = GameObject.Find(name).GetComponent<Itemdata>().itemIcon;
            //collection_image.sprite = sp;
            collection_text.text = name;

            return 1;
        }
        else
        {
            collection_image.sprite = unknown_image;
            collection_text.text = "みつけてみよう！\n(" + name + ")";

            return 0;
        }
    }

    //Image_Target/Collection1 をすべて所持しているかの判定
    private void Review_Collection1_Get_Status()
    {
        if (Review_Specific_Collections_Get_Status(game_manager_script.Collection1_name))
        {
            Complete_Image_Collection1.SetActive(true);
        }
    }

    //ある特定のコレクションをすべて所持しているかの判定
    private bool Review_Specific_Collections_Get_Status(string[] collection_name)
    {
        int i = 0;
        //complete = を追加
        for(i = 0; i < collection_name.Length; i++){
            if (!game_manager_script.Collection_Table[collection_name[i]])
            {
                return false;
            }
        }
        return true;
    }



    //オブジェクトがアクティブの間、ゲーム画面が縦、横に変更された際にコレクション一覧オブジェクトの並びを修正する
    private void Check_Screen_Direction()
    {
        //画面向きによってサイズを変更するUIの設定
        if ((Screen.orientation == ScreenOrientation.LandscapeLeft || Screen.orientation == ScreenOrientation.LandscapeRight) && screen_direction != "landscape")
        {

            Alignment_Display_Collections();
            screen_direction = "landscape";
        }
        else if ((Screen.orientation == ScreenOrientation.Portrait || Screen.orientation == ScreenOrientation.PortraitUpsideDown) && screen_direction != "portrait")
        {
            Alignment_Display_Collections();
            screen_direction = "portrait";
        }
        else
        {
            //Debug.Log("変換済みか端末ではありません。角度="+ Screen.orientation);
        }
    }

    //あるコレクションに画像（スクリーンショット）を差し込む
    public void Input_ScreenShot_Image(string name , Sprite sp)
    {
        Image collection_image = this.transform.Find(name + "_view").GetComponent<Image>();
        collection_image.sprite = sp;
    }

}
