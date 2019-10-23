using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//一時的に表示するポップアップオブジェクトなどにアタッチ。
public class Popup : MonoBehaviour
{

    //オブジェクトを破壊する
    public void Destroy_GameObject()
    {
        Destroy(gameObject);
    }

    //アクティブになった時アニメーション再生
    public void Start_Animation()
    {
        this.GetComponent<Animator>().SetTrigger("Popup");
    }
    
    //アニメーション終了時に呼び出す。非アクティブ化する
    public void Inactive()
    {
        this.gameObject.SetActive(false);
    }


}
