/*
 *  キューブ生成のスクリプト
 * 
 *  決め事
 * 
 *  命名規則：   Pascal形式　例) AttackCount; Camel形式　例) attackCount
 *      名前空間 Pascal形式　クラス、構造体　Pascal形式　プロパティ　Pascal形式
 *      メンバ変数(フィールド)　Camel形式　メソッド　Pascal形式　パラメータ　Camel形式
 *      
 *  メソット    1メソッド10行以内　最大2インデント　名前をわかりやすく (https://codic.jp/engine) 
 *  Property    getのみ行う　setは、プライベート
 * 
 * SendMessageを使わない　Editorから読み込むだけなら[Serialize Failed]を使用する
 * 
 * 状態管理をしっかり行う　ジェネリック思考で考える
 * 
 * Code by shinnnosuke hiratsuka
 * 
 */
using UnityEngine;
using System.Collections;

public class CubeGeneration : MonoBehaviour {

    /// <summary>
    /// 生成する奴
    /// </summary>
    [SerializeField]
    private GameObject cubeGeneration = null;
    /// <summary>
    /// 生成するときの親
    /// </summary>
    [SerializeField]
    private GameObject myFather = null;
    /// <summary>
    /// 生成する個数
    /// </summary>
    [SerializeField]
    private Vector3 generatingCount = Vector3.zero;
    /// <summary>
    /// 生成用
    /// </summary>
    private GameObject clone = null;
    /// <summary>
    /// 位置を返す
    /// </summary>
    private Vector3 position = Vector3.zero;
    /// <summary>
    /// 何番目か
    /// </summary>
    private Vector3 index = Vector3.zero;

    /// <summary>
    /// ゲームが始まる前に呼ばれる
    /// </summary>
    private void Awake()
    {
        ZeroCheck();
        Centering();
    }

    // Use this for initialization
    private void Start ()
    {
        Generation();
    }
    
    // Update is called once per frame
    private void Update ()
    {
	
	}
    /// <summary>
    /// 生成する
    /// </summary>
    private void Generation()
    {
        for (int i = 0; i < generatingCount.x * generatingCount.y * generatingCount.z; i++)
        {
            clone = (GameObject)Instantiate(cubeGeneration);    //  生成して
            clone.transform.SetParent(myFather.transform);      //  親決めて
            clone.transform.position = PositionSetting;         //  位置変えて
            clone.name = cubeGeneration.name + (i + 1).ToString();                   //  名前変える
        }
    }

    /// <summary>
    /// 位置の計算
    /// </summary>
    private Vector3 PositionSetting
    {
        get
        {
            /// 計算式
            position.x = cubeGeneration.transform.position.x + cubeGeneration.transform.localScale.x * index.x;
            position.y = cubeGeneration.transform.position.y - cubeGeneration.transform.localScale.x * index.y;
            position.z = cubeGeneration.transform.position.z + cubeGeneration.transform.localScale.x * index.z;

            DriveIndex();

            return position;
        }
    }
    /// <summary>
    /// インデックスを動かす
    /// </summary>
    private void DriveIndex()
    {
        index.x += 1;   

        if (index.x >= generatingCount.x / 2 + cubeGeneration.transform.localScale.x / 2)
        {
            Centering();
            index.y += 1;
        }
        if (index.y >= generatingCount.y)
        {
            Centering();
            index.y = 0;
            index.z += 1;
        }

        Debug.Log(index);

    }

    /// <summary>
    /// Cubeを中央揃えに
    /// </summary>
    private void Centering()
    {
        index.x = -generatingCount.x / 2 + cubeGeneration.transform.localScale.x / 2;
    }


    /// <summary>
    /// generatingCountの値に0が入っていないか
    /// </summary>
    private void ZeroCheck()
    {
        if(generatingCount.x == 0 || generatingCount.y == 0|| generatingCount.z == 0)
        {
            Debug.LogError("generatingCountの初期値に0いててんじゃねーよ(# ﾟДﾟ)");
        }
        if(generatingCount.x == 0)
        {
            Debug.LogError("xの初期値が0だわボケヽ(`Д´)ﾉﾌﾟﾝﾌﾟﾝ");
        }
        if (generatingCount.y == 0)
        {
            Debug.LogError("yの初期値が0だわボケヽ(`Д´)ﾉﾌﾟﾝﾌﾟﾝ");
        }
        if (generatingCount.z == 0)
        {
            Debug.LogError("zの初期値が0だわボケヽ(`Д´)ﾉﾌﾟﾝﾌﾟﾝ");
        }

    }
}
