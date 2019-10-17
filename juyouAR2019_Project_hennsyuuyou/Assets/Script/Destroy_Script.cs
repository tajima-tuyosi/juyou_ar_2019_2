using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//一時的に表示するポップアップ、などにアタッチ

//自身を破壊するメソッドがpublicでないため作成
public class Destroy_Script : MonoBehaviour
{
    public void Destroy_GameObject()
    {
        Destroy(gameObject);
    }

    //非アクティブになった時削除
    private void OnDisable()
    {
        Destroy(gameObject);
    }

}
