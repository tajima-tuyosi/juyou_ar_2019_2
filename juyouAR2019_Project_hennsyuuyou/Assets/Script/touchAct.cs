using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

    
//ARCameraにアタッチ
//カメラ内に表示されている3Dオブジェクトへのタッチ関係スクリプト

public class touchAct : MonoBehaviour
{
    private float distance = 300f;
    private  GameObject getmessage;
    private Text text;
    private GameObject gameobject;
    Game_Manager gm;
    public GameObject gamemanager;
    public GameObject getimage;

    //スクリーンショット画像差し込み用
    public GameObject content;
    private RectTransform camerafinder_rect;
    
    // Start is called before the first frame update
    void Start()
    {
        getmessage = GameObject.Find("Canvas").transform.Find("GetImage").gameObject;
        text = getmessage.transform.GetChild(0).GetComponent<Text>();
        gm = gamemanager.GetComponent<Game_Manager>();

        camerafinder_rect = GameObject.Find("CameraFinder").GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            //別のオブジェクトに重なっているときは反応しない
            //if (EventSystem.current.IsPointerOverGameObject())
            //{
            //    return;
            //}

            if (IsUGUIHit(Input.mousePosition))
            {
                return;
            }

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();

            if (Physics.Raycast(ray, out hit, distance)) {
                gameobject = hit.collider.gameObject;
                Debug.Log(gameobject.name + " is click");
                Get_Collection_Process();
                CaptureScreen();
            }
        }

        if(Input.touchCount > 0)
        {
            //UIと重なっていたら表示反応しない
            Touch touch = Input.GetTouch(0);
            if (IsUGUIHit(touch.position))
            {
                return;
            }

            if (touch.phase == TouchPhase.Began){
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit = new RaycastHit();

                if (Physics.Raycast(ray, out hit, distance)) {
                    gameobject = hit.collider.gameObject;
                    Debug.Log(gameobject.name+ "is Touch");
                    Get_Collection_Process();
                    CaptureScreen();
                }
            }
        }
    }


    private void Get_Collection_Process()
    {
        //Itemdata item = gameobject.GetComponent<Itemdata>();
        //item.GetIcon();
        gm.Collection_Table_Set(gameobject.name, true);
        content.GetComponent<Content>().Review_Collections_Get_Status();
        text.text = gameobject.name + "\nをコレクションに追加しました";
        //ポップアップの表示
        GameObject popup_prefab = (GameObject)Instantiate(getimage); //オブジェクトを生成
        popup_prefab.transform.SetParent(GameObject.Find("Canvas").transform, false); // 生成したオブジェクトをcanvasの子クラスに
        popup_prefab.SetActive(true);
    }

    public void CaptureScreen()
    {
        Camera cam = this.GetComponent<Camera>();
        Texture2D screenShot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        RenderTexture rt = new RenderTexture(screenShot.width, screenShot.height, 24);
        RenderTexture prev = cam.targetTexture; //値を戻せるようにprevに保存
        cam.targetTexture = rt; //スクショ用変数を代入
        cam.Render(); // カメラの画像をrtに取得
        cam.targetTexture = prev;　
        RenderTexture.active = rt;//rtのみアクティブ状態にする。
        screenShot.ReadPixels(new Rect(0, 0, screenShot.width, screenShot.height), 0, 0);
        screenShot.Apply(); //ReadPixcelを反映

        //Texture2DをSpriteに変換、代入
        //var rect = new Rect(0, 0, rt.width, rt.height);
        Debug.Log(screenShot.width + "  , "  + screenShot.height);
        //Debug.Log(camerafinder_rect.localPosition.x + " , " + camerafinder_rect.localPosition.y);
        //Debug.Log(camerafinder_rect.anchoredPosition.x  + "  , " + camerafinder_rect.anchoredPosition.y + " ;;; " + camerafinder_rect.sizeDelta.x + " . " + camerafinder_rect.sizeDelta.y);
        //canvasとスクリーンの縮尺比を出す。
        var scale_ratio = Screen.width / GameObject.Find("Canvas").GetComponent<RectTransform>().sizeDelta.x;
        //Debug.Log(Screen.width + " / " + GameObject.Find("Canvas").GetComponent<RectTransform>().sizeDelta.x + " = " + scale_ratio);
        var camerafinder_senter = new Vector2(Screen.width/2 + camerafinder_rect.anchoredPosition.x * scale_ratio, Screen.height / 2 + camerafinder_rect.anchoredPosition.y * scale_ratio);
        //var rect = new Rect(camerafinder_rect.anchoredPosition.x * scale_ratio, camerafinder_rect.anchoredPosition.y * scale_ratio, camerafinder_rect.sizeDelta.x * scale_ratio, camerafinder_rect.sizeDelta.y * scale_ratio);
        //左下を軸に、横幅と縦幅を左上に向けて伸ばす
        var rect = new Rect(camerafinder_senter.x - camerafinder_rect.sizeDelta.x/2* scale_ratio , camerafinder_senter.y - camerafinder_rect.sizeDelta.y/2 *scale_ratio ,  camerafinder_rect.sizeDelta.x * scale_ratio ,  camerafinder_rect.sizeDelta.y * scale_ratio);
        //Debug.Log("切り取る範囲は" + (camerafinder_senter.x - camerafinder_rect.sizeDelta.x / 2* scale_ratio) + " , " + (camerafinder_senter.y - camerafinder_rect.sizeDelta.y / 2 * scale_ratio) + " ::: "  + (camerafinder_senter.x + camerafinder_rect.sizeDelta.x / 2 * scale_ratio) + " , " +  (camerafinder_senter.y + camerafinder_rect.sizeDelta.y / 2 * scale_ratio));
        var pivot = new Vector2(0.5f, 0.5f);
        var sprite = Sprite.Create(screenShot, rect, pivot);

        content.GetComponent<Content>().Input_ScreenShot_Image(gameobject.name , sprite);
    }


    public static bool IsUGUIHit(Vector3 _scrPos)// Input.mousePosition
    { 
        PointerEventData pointer = new PointerEventData(EventSystem.current);//ポインタ（マウス/タッチ）イベントに関連する情報を取得
        pointer.position = _scrPos;
        List<RaycastResult> result = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointer, result);//UIのみポインターに重なっているかを探索
        return (result.Count > 0);
    }

}