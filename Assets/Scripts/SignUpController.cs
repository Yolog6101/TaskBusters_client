using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignUpController : MonoBehaviour
{
    private bool is_Login; //プレイヤー登録まで完了したかどうか
    public Button sign_up_button;
    public Button player_info_button;
    public Button add_friends_button;

    // Start is called before the first frame update
    void Start()
    {
        //テスト用
        if(PlayerBasedata.GetUserid()!=-1){
            isLoginProperty=true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(is_Login == false){
            sign_up_button.interactable = true;
            player_info_button.interactable = false;
            add_friends_button.interactable = false;
        }else{
            sign_up_button.interactable = false;
            player_info_button.interactable = true;
            add_friends_button.interactable = true;
        }
    }

    public bool isLoginProperty // ここでプロパティを使う。publicをつけます。
    {
     get { return is_Login; }  // これがgettr。他のスクリプトから呼び出した時、returnのあとに書いた変数を返す。
     set { is_Login = value; } // これがsettr。valueには他のスクリプトで代入された値が格納されます。（そこまで気にしなくて大丈夫。）
    }

}
