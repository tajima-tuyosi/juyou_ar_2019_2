//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using UnityEngine.EventSystems;

//現在はこのスクリプトの代わりに動作が軽いItemTouchスクリプトをを使用

////ARCameraにアタッチ。カメラ内に表示されている3Dオブジェクトへのタッチ関係スクリプト
//public class touchAct : MonoBehaviour
//{
//    private float distance = 300f;
//    private  GameObject getmessage;
//    private Text text;
//    private GameObject gameobject;
//    private Game_Manager gm;
//    public GameObject gamemanager;
//    public GameObject getimage;

//    //スクリーンショット画像差し込み用
//    public GameObject content;
//    private RectTransform camerafinder_rect;

//    private Camera cam;
//    private float scale_ratio;
//    private Vector2 camerafinder_senter;
//    private Rect rect;
//    private Vector2 pivot;

//    void Start()
//    {
//        getmessage = GameObject.Find("Canvas").transform.Find("GetImage").gameObject;
//        text = getmessage.transform.GetChild(0).GetComponent<Text>();
//        gm = gamemanager.GetComponent<Game_Manager>();
//        cam = this.GetComponent<Camera>();
//        camerafinder_rect = GameObject.Find("CameraFinder").GetComponent<RectTransform>();

//        //canvasとスクリーンの縮尺比を出す。
//        scale_ratio = Screen.width / GameObject.Find("Canvas").GetComponent<RectTransform>().sizeDelta.x;
//        camerafinder_senter = new Vector2(Screen.width / 2 + camerafinder_rect.anchoredPosition.x * scale_ratio, Screen.height / 2 + camerafinder_rect.anchoredPosition.y * scale_ratio);
//        //左下を軸に、横幅と縦幅を左上に向けて伸ばす
//        rect = new Rect(camerafinder_senter.x - camerafinder_rect.sizeDelta.x / 2 * scale_ratio, camerafinder_senter.y - camerafinder_rect.sizeDelta.y / 2 * scale_ratio, camerafinder_rect.sizeDelta.x * scale_ratio, camerafinder_rect.sizeDelta.y * scale_ratio);
//        pivot = new Vector2(0.5f, 0.5f); //画像中心にピボットを置く

//    }

//    void Update()
//    {

//        if (Input.GetMouseButtonDown(0))
//        {
//            //GUIにクリックが重なっていたら行動しない
//            if (IsUGUIHit(Input.mousePosition))
//            {
//                return;
//            }

//            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//            RaycastHit hit = new RaycastHit();

//            if (Physics.Raycast(ray, out hit, distance)) {
//                gameobject = hit.collider.gameObject;
//                Debug.Log(gameobject.name + " is click");
//                Get_Collection_Process();
//                CaptureScreen();
//            }
//        }

//        if(Input.touchCount > 0)
//        {
//            //UIにタッチが重なっていたら表示行動しない
//            Touch touch = Input.GetTouch(0);
//            if (IsUGUIHit(touch.position))
//            {
//                return;
//            }

//            if (touch.phase == TouchPhase.Began){
//                Ray ray = Camera.main.ScreenPointToRay(touch.position);
//                RaycastHit hit = new RaycastHit();

//                if (Physics.Raycast(ray, out hit, distance)) {
//                    gameobject = hit.collider.gameObject;
//                    Debug.Log(gameobject.name+ "is Touch");
//                    Get_Collection_Process();
//                    CaptureScreen();
//                }
//            }
//        }
//    }


//    private void Get_Collection_Process()
//    {
//        gm.Collection_Table_Set(gameobject.name, true);
//        content.GetComponent<Content>().Review_Collections_Get_Status();
//        text.text = gameobject.name + "\nをコレクションに追加しました";
//        //ポップアップの表示
//        GameObject popup_prefab = (GameObject)Instantiate(getimage); //オブジェクトを生成
//        popup_prefab.transform.SetParent(GameObject.Find("Canvas").transform, false); // 生成したオブジェクトをcanvasの子クラスに
//        popup_prefab.SetActive(true);
//    }

//    public void CaptureScreen()
//    {
//        Debug.Log("スクリーンショット保存します");
//        Texture2D screenShot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
//        RenderTexture rt = new RenderTexture(screenShot.width, screenShot.height, 24);
//        RenderTexture prev = cam.targetTexture; //値を戻せるようにprevに保存
//        cam.targetTexture = rt; //スクショ用変数を代入
//        cam.Render(); // カメラの画像をrtに取得
//        cam.targetTexture = prev;　
//        RenderTexture.active = rt;//rtのみアクティブ状態にする。
//        screenShot.ReadPixels(new Rect(0, 0, screenShot.width, screenShot.height), 0, 0);
//        screenShot.Apply(); //ReadPixcelを反映


//        Sprite sprite = Sprite.Create(screenShot, rect, pivot);
//        //メモリを圧迫しないようにTexture2Dの破棄
//        Debug.Log(screenShot);
//        Destroy(screenShot);
//        Debug.Log("後: " + screenShot);

//        Debug.Log("スクショ貼り付けます");
//        content.GetComponent<Content>().Input_ScreenShot_Image(gameobject.name , sprite);
//    }


//    //GUIにタッチが重なっているかを判定
//    public static bool IsUGUIHit(Vector3 _scrPos)// Input.mousePosition
//    { 
//        PointerEventData pointer = new PointerEventData(EventSystem.current);//ポインタ（マウス/タッチ）イベントに関連する情報を取得
//        pointer.position = _scrPos;
//        List<RaycastResult> result = new List<RaycastResult>();
//        EventSystem.current.RaycastAll(pointer, result);//UIのみポインターに重なっているかを探索
//        return (result.Count > 0);
//    }

//}