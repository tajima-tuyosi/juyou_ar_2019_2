using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//CompleteImage にアタッチしている
public class Complete_Collections : MonoBehaviour
{
    //コンプリート時に表示するイメージ
    private GameObject completeimage;

    // Start is called before the first frame update
    void Start()
    {
        //実行時にイメージを非アクティブにする
        completeimage = GameObject.Find("CompleteImage");
        completeimage.SetActive(false);


    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
