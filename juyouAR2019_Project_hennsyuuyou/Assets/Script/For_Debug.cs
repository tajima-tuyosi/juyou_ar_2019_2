using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//デバッグ用。デバッグしたい時に適当なTextUIに付けて使用
public class For_Debug : MonoBehaviour
{
    public GameObject obj_ui;
    public bool only_width_and_height_change = false;

    Text text;
    void Start()
    {
        text = gameObject.GetComponent<Text>();
    }

    void Update()
    {
        //画面の向きを表示
        if (Screen.orientation == ScreenOrientation.LandscapeLeft || Screen.orientation == ScreenOrientation.LandscapeRight)
        {
            text.text = "横画面。角度=" + Screen.orientation + "\n座標＝" + obj_ui.GetComponent<RectTransform>().anchoredPosition + "\n大きさ＝" + obj_ui.GetComponent<RectTransform>().sizeDelta;
        }
        else if ((Screen.orientation == ScreenOrientation.Portrait || Screen.orientation == ScreenOrientation.PortraitUpsideDown))
        {
            text.text = "縦画面。角度=" + Screen.orientation + "\n座標＝" + obj_ui.GetComponent<RectTransform>().anchoredPosition + "\n大きさ＝" + obj_ui.GetComponent<RectTransform>().sizeDelta;
        }
        else
        {
            text.text = "変換済みか端末ではありません。角度=" + Screen.orientation;
        }
    }
}
