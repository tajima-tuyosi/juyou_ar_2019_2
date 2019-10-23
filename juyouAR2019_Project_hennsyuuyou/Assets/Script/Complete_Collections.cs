using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//CompleteImage にアタッチしている
public class Complete_Collections : MonoBehaviour
{
    //コンプリート時に表示するオブジェクト
    private GameObject completeimage;

    void Start()
    {
        //実行時にイメージを非アクティブにする
        completeimage = GameObject.Find("CompleteImage");
        completeimage.SetActive(false);


    }


}
