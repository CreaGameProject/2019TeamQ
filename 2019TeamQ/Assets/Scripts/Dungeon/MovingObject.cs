using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovingObject : MonoBehaviour
{
    public Rigidbody2D rb;
    public CircleCollider2D circleCollider;
    public Animator anm;//アニメーションコンポーネント
    private float MoveTime = 0.1f;
    private float InverseMoveTime;

    //Charactorの向いている方向
    public int direction_x;
    public int direction_y;

    void Start()
    {
        this.rb = GetComponent<Rigidbody2D>();
        this.circleCollider = GetComponent<CircleCollider2D>();
        this.anm = GetComponent<Animator>();
        SetCharactorDirection(0, -1);
    }

    protected void SetCharactorDirection(int x,int y)
    {
        direction_x = x;
        direction_y = y;
        anm.SetFloat("Direction_X", direction_x);
        anm.SetFloat("Direction_Y",direction_y);
    }

    protected Vector2 GetCharactorDirection()
    {
        return new Vector2(direction_x, direction_y);
    }

    protected virtual void AttemptMove(int Xdir,int Ydir)
    {
        Vector2 StartPosition = transform.position;
        Vector2 EndPosition = StartPosition + new Vector2(Xdir, Ydir);
        //移動判定用、衝突するレイヤーはすべて入れる
        int LayerObj = LayerMask.GetMask(new string[] { "Enemy", "FPlayer", "Wall"});

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
    protected IEnumerator Movement(Vector3 EndPosition)
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
