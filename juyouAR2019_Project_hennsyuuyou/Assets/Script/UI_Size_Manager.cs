using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

//縦、横画面で表示サイズを変更したいUIすべてにアタッチ
//端末の縦横画面に合わせてUIの大きさ、ポジションを変更する
public class UI_Size_Manager : MonoBehaviour
{
    //[Header("Width　、Heigthのみ反映")]
    [SerializeField]　bool only_width_height_move = false;

    //[Header("横画面のとき")]
    [SerializeField] float pos_x_landscape = 0;
    [SerializeField] float pos_y_landscape = 0;
    [SerializeField] float width_landscape = 100;
    [SerializeField] float height_landscape = 100;
    //[Space(10)]
    //[Header("縦画面のとき")]
    [SerializeField] float pos_x_portrait = 0;
    [SerializeField] float pos_y_portrait = 0;
    [SerializeField] float width_portrait = 100;
    [SerializeField] float height_portrait = 100;

    string screen_direction = "";

    //画面の向きを固定するかどうか
#if UNITY_EDITOR //Unityエディタでテスト時
    //エディタ実行の際の画面の向きはPortrait扱い。
    bool not_change_screen = true;
#else
        bool not_change_screen = false;
#endif


    private void Start()
    {
        //画面回転無しの場合、横画面を標準にする
        if (!not_change_screen)
        {
            Change_UI_Size(pos_x_landscape, pos_y_landscape, width_landscape, height_landscape);
            screen_direction = "landscape";
        }
    }


    void Update()
    {
        Check_Screen_Direction();
        //#if UNITY_EDITOR //Unityエディタでテスト時
        //    //エディタ実行の際の画面の向きはPortrait扱い。
        //    not_change_editor_screen = true;
        //    Check_Screen_Direction();
        //#else
        //    Check_Screen_Direction();
        //#endif

    }


    public void Check_Screen_Direction()
    {
        if (not_change_screen)
        {
            return;
        }

        //画面向きによってサイズを変更するUIの設定
        if ((Screen.orientation == ScreenOrientation.LandscapeLeft || Screen.orientation == ScreenOrientation.LandscapeRight) && screen_direction != "landscape")
        {
            Change_UI_Size(pos_x_landscape, pos_y_landscape, width_landscape, height_landscape);
            screen_direction = "landscape";
        }
        else if ((Screen.orientation == ScreenOrientation.Portrait || Screen.orientation == ScreenOrientation.PortraitUpsideDown) && screen_direction != "portrait")
        {
            Change_UI_Size(pos_x_portrait, pos_y_portrait, width_portrait, height_portrait);
            screen_direction = "portrait";
        }
        else
        {
            //Debug.Log("変換済みか端末ではありません。角度="+ Screen.orientation);
        }
    }



    private void Change_UI_Size(float pos_x , float pos_y , float width , float height)
    {
        //Debug.Log(pos_x + " , " + pos_y + " , " + width + " , " + height);
        //座標は変更しない場合はそのまま
        if (!only_width_height_move)
        {
            this.GetComponent<RectTransform>().anchoredPosition = new Vector3(pos_x, pos_y, 0);
        }
        this.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
    }


    //-------------------
    // ----拡張コード----
    //-------------------

#if UNITY_EDITOR
    //インスペクターを拡張するクラス
    [CustomEditor(typeof(UI_Size_Manager))]
    public class UI_Size_Manager_Editor : Editor //Editorクラスを継承
    {

        SerializedProperty[] landscapeProperty = new SerializedProperty[4];
        SerializedProperty[] portraitProperty = new SerializedProperty[4];
        SerializedProperty[] otherProperty = new SerializedProperty[1];

        void OnEnable()
        {
            // 基礎クラスから変数をSerializedPropertyで受け取る
            landscapeProperty[0] = serializedObject.FindProperty("pos_x_landscape");
            landscapeProperty[1] = serializedObject.FindProperty("pos_y_landscape");
            landscapeProperty[2] = serializedObject.FindProperty("width_landscape");
            landscapeProperty[3] = serializedObject.FindProperty("height_landscape");

            portraitProperty[0] = serializedObject.FindProperty("pos_x_portrait");
            portraitProperty[1] = serializedObject.FindProperty("pos_y_portrait");
            portraitProperty[2] = serializedObject.FindProperty("width_portrait");
            portraitProperty[3] = serializedObject.FindProperty("height_portrait");

            otherProperty[0] = serializedObject.FindProperty("only_width_height_move");
        }

        //GUIのオーバーライド
        //一定周期で呼び出される
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            DrawProperties();
            serializedObject.ApplyModifiedProperties();
        }

        void DrawProperties()
        {

            EditorGUIUtility.labelWidth = 80;//ラベルの大きさ設定
            UI_Size_Manager　 ui_status = target as UI_Size_Manager;
            RectTransform ui_rect = ui_status.GetComponent<RectTransform>();

            //アプリ実行中width,heightのみ動かすか
            using (new EditorGUILayout.HorizontalScope())
            {
                EditorGUILayout.LabelField("アプリ実行時はWidth、Heigthのみ反映");
                                EditorGUILayout.PropertyField(otherProperty[0] , new GUIContent());
            }


            //---横画面のインスペクター---
            EditorGUILayout.LabelField("横画面");
            using (new EditorGUILayout.HorizontalScope())
            {
                EditorGUILayout.PropertyField(landscapeProperty[0] , new GUIContent("Pos_X") , GUILayout.MinWidth(100));
                EditorGUILayout.PropertyField(landscapeProperty[1] , new GUIContent("Pos_Y") , GUILayout.MinWidth(100));
            }
            using (new EditorGUILayout.HorizontalScope())
            {
                EditorGUILayout.PropertyField(landscapeProperty[2], new GUIContent("Width"), GUILayout.MinWidth(100));
                EditorGUILayout.PropertyField(landscapeProperty[3], new GUIContent("Height"), GUILayout.MinWidth(100));
            }
            using (new EditorGUILayout.HorizontalScope())
            {
                if (GUILayout.Button("Rect Transformに代入"))
                {
                    //GameObjectやTargetじゃなくRectTransformの入った変数をセット
                    Undo.RecordObject(ui_rect, "Push to  RectTransform");
                    ui_rect.anchoredPosition = new Vector2(ui_status.pos_x_landscape, ui_status.pos_y_landscape);
                    ui_rect.sizeDelta = new Vector2(ui_status.width_landscape, ui_status.height_landscape);
                    EditorUtility.SetDirty(ui_rect);
                }
                if (GUILayout.Button("Rect Transformnを取得"))
                { 
                    Undo.RecordObject(ui_status, "Get RectTransform");
                    ui_status.pos_x_landscape = ui_rect.anchoredPosition.x;
                    ui_status.pos_y_landscape = ui_rect.anchoredPosition.y;
                    ui_status.width_landscape = ui_rect.sizeDelta.x;
                    ui_status.height_landscape = ui_rect.sizeDelta.y;
                    EditorUtility.SetDirty(ui_status);
                }
            }

            ////---縦画面のインスペクター---
            EditorGUILayout.LabelField("縦画面");
            using (new EditorGUILayout.HorizontalScope())
            {
                EditorGUILayout.PropertyField(portraitProperty[0] , new GUIContent("Pos_X"), GUILayout.MinWidth(100));
                EditorGUILayout.PropertyField(portraitProperty[1] , new GUIContent("Pos_Y"), GUILayout.MinWidth(100));
            }
            using (new EditorGUILayout.HorizontalScope())
            {
                EditorGUILayout.PropertyField(portraitProperty[2] , new GUIContent("Width"), GUILayout.MinWidth(100));
                EditorGUILayout.PropertyField(portraitProperty[3] , new GUIContent("Height"), GUILayout.MinWidth(100));
            }

            using (new EditorGUILayout.HorizontalScope())
            {
                if (GUILayout.Button("Rect Transformに代入"))
                {
                    //GameObjectやTargetじゃなくRectTransformの入った変数をセット
                    Undo.RecordObject(ui_rect, "Push to  RectTransform");
                    ui_rect.anchoredPosition = new Vector2(ui_status.pos_x_portrait , ui_status.pos_y_portrait);
                    ui_rect.sizeDelta = new Vector2(ui_status.width_portrait , ui_status.height_portrait);
                    EditorUtility.SetDirty(ui_rect);
                }
                if (GUILayout.Button("Rect Transformnを取得"))
                {
                    Undo.RecordObject(ui_status, "Get RectTransform");
                    ui_status.pos_x_portrait = ui_rect.anchoredPosition.x;
                    ui_status.pos_y_portrait = ui_rect.anchoredPosition.y;
                    ui_status.width_portrait = ui_rect.sizeDelta.x;
                    ui_status.height_portrait = ui_rect.sizeDelta.y;
                    EditorUtility.SetDirty(ui_status);
                }
            }

        }
    }
#endif






}
