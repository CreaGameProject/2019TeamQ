using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public GameObject DungeonManager;
    private DungeonState EnemyState;
    private Rigidbody2D rb;
    private CircleCollider2D circleCollider;
    private Animator anm;//アニメーションコンポーネント
    private float MoveTime = 0.1f;
    private float InverseMoveTime;

    private GameObject Player;//プレイヤーの座標取得用
    private Vector2 TargetPos;//プレイヤーの位置情報

    //Charactorの向いている方向
    public int direction_x;
    public int direction_y;

    [SerializeField]
    public string Name;
    [System.NonSerialized] public int Hp = 40;
    [System.NonSerialized] public int Atk = 20;
    [System.NonSerialized] public int Def = 20;

    void Start()
    {
        this.DungeonManager = GameObject.Find("DungeonMnager");
        this.rb = GetComponent<Rigidbody2D>();
        this.circleCollider = GetComponent<CircleCollider2D>();
        this.anm = GetComponent<Animator>();
        SetCharactorDirection(0, -1);
    }

    private void SetCharactorDirection(int x, int y)
    {
        direction_x = x;
        direction_y = y;
        anm.SetFloat("Direction_X", direction_x);
        anm.SetFloat("Direction_Y", direction_y);
    }

    public void MoveEnemy()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        TargetPos = Player.transform.position;
        int Xdir = 0;
        int Ydir = 0;
        Xdir = (int)TargetPos.x - (int)this.transform.position.x;
        Ydir = (int)TargetPos.y - (int)this.transform.position.y;
        int AbsXdir = System.Math.Abs(Xdir);
        int AbsYdir = System.Math.Abs(Ydir);
        if (AbsXdir > 5 || AbsYdir > 5)
        {
            return;
        }
        else if (AbsXdir > AbsYdir)
        {
            Xdir = Xdir / AbsXdir;
            AttemptMove(Xdir,0);
        }
        else if (AbsXdir < AbsYdir)
        {
            Ydir = Ydir / AbsYdir;
            AttemptMove(0, Ydir);
        }
        else if (AbsXdir == AbsYdir)
        {
            Xdir = Xdir / AbsXdir;
            Ydir = Ydir / AbsYdir;
            AttemptMove(Xdir, Ydir);
        }
    }
    private void AttemptMove(int Xdir,int Ydir)
    {
        Vector2 StartPosition = transform.position;
        Vector2 EndPosition = StartPosition + new Vector2(Xdir, Ydir);
        //移動判定用、衝突するレイヤーはすべて入れる
        int LayerObj = LayerMask.GetMask(new string[] { "Enemy", "FPlayer", "Wall" });

        //地震の衝突判定をなくしてPhysics2Dの誤認をなくす
        circleCollider.enabled = false;
        //移動先に障害物があるか判定する
        RaycastHit2D HitObj = Physics2D.Linecast(StartPosition, EndPosition, LayerObj);
        //衝突判定を戻す
        circleCollider.enabled = true;

        //Physics2Dで移動先に障害物がなければMovementを実行
        if (HitObj.transform == null)
        {
            SetCharactorDirection(Xdir, Ydir);
            StartCoroutine(Movement(EndPosition));
        }
        //physics2Dで移動先に何かあれば何もしない
        else if (HitObj.transform != null)
        {
            SetCharactorDirection(Xdir, Ydir);
            return;

        }
        
        
    }

    private IEnumerator Movement(Vector3 EndPosition)
    {
        float sqrRemainingDistance = (transform.position - EndPosition).sqrMagnitude;
        InverseMoveTime = 1f / MoveTime;
        while (sqrRemainingDistance > float.Epsilon)
        {
            Vector3 NewPosition = Vector3.MoveTowards(rb.position, EndPosition, InverseMoveTime * Time.deltaTime);
            rb.MovePosition(NewPosition);
            sqrRemainingDistance = (transform.position - EndPosition).sqrMagnitude;
            yield return null;
        }
    }
   
}
