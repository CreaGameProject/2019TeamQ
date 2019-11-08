using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public enum DungeonState
{
    keyInput,//キー入力待ち＝プレイヤーターン開始
    PlayerTurn,//プレイヤーの行動中
    PlayerEnd,//プレイヤーのターン終了
    EnemyBegin,//エネミーターン開始
    EnemyTurn,//エネミーの行動中
    TurnEnd,//ターン終了→KeyInputへ
    LoadDungeon//ダンジョンロード中
};

public class DungeonManager : MonoBehaviour
{
    public static DungeonManager instance;
    public GameObject[] EnemyObj;//エネミーにアタッチしているコンポーネントを使うための箱
    public DungeonState CurrentDungeonState;//現在のダンジョン内の状態
    float TurnDelay = 0.20f;//移動ごとの感覚
    private float levelStartFloor = 2f; //レベル表示画面で2秒待つ
    public Text floorText; //kaisouテキスト
    public GameObject kaisouImage; //レベルイメージ
    private int kaisou = 0; //レベルは1にしておく
    private bool doingSetup; //kaisouImageの表示等で活用
    private void Awake()
    {
        SetCurrentState(DungeonState.LoadDungeon);//初期状態は何もできない

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        kaisou++; //レベルを1プラスする
        InitGame();
    }


    void InitGame()
    {
        //kaisouImageオブジェクト・LevelTextオブジェクトの取得
        kaisouImage = GameObject.Find("FloorLevel");
        floorText = GameObject.Find("kaisouText").GetComponent<Text>();
        floorText.text = kaisou+"F/10F"; //最新のレベルに更新
        kaisouImage.SetActive(true); //LebelImageをアクティブにし表示
        StartCoroutine("StartFloor");
       // enemies.Clear();
        //boardScript.SetupScene(level);
    }
    IEnumerator StartFloor()
    {
        yield return new WaitForSeconds(levelStartFloor);
        HideLevelImage();
    }
    private void HideLevelImage()
    {
        kaisouImage.SetActive(false); //LevelImage非アクティブ化
        SetCurrentState(DungeonState.keyInput); ; //プレイヤーが動けるようになる
    }

    public void GameOver()
    {

        //ゲームオーバーメッセージを表示
        floorText.text = " Game Over";
        kaisouImage.SetActive(true);
        SetCurrentState(DungeonState.LoadDungeon);

    }

    //現在のゲームステータスを変更する関数　外部及び内部から
    public void SetCurrentState(DungeonState state)
    {
        CurrentDungeonState = state;
        OnGameStateChanged(CurrentDungeonState);
    }
   
    void OnGameStateChanged(DungeonState state)
    {
        switch (state)
        {
            case DungeonState.LoadDungeon:
                break;
            case DungeonState.keyInput:
                break;
            case DungeonState.PlayerEnd:
                StartCoroutine("PlayerEnd");
                break;
            case DungeonState.EnemyBegin:
                SetCurrentState(DungeonState.EnemyTurn);
                break;
            case DungeonState.EnemyTurn:
                StartCoroutine("EnemyTurn");
                break;
            case DungeonState.TurnEnd:
                SetCurrentState(DungeonState.keyInput);
                break;
        }
    }

    //キー入力後のプレイヤーの移動の処理
    IEnumerator PlayerEnd()
    {
        yield return new WaitForSeconds(TurnDelay);
        SetCurrentState(DungeonState.EnemyBegin);
    }

    //エネミーターンの処理
    IEnumerator EnemyTurn()
    {
        //yield return new WaitForSeconds(TurnDelay);
        GameObject[] EnemyObj = GameObject.FindGameObjectsWithTag("Enemy");
        //EnemyObjの数だけEnemyにアタッチしている移動処理を実行
        for(int x = 0; x < EnemyObj.Length; x++)
        {
            EnemyObj[x].GetComponent<Enemy>().MoveEnemy();
            yield return new WaitForSeconds(TurnDelay);
        }
        SetCurrentState(DungeonState.TurnEnd);
    }

}



