using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MovingObject
{
    public SpriteRenderer Spr;//表示スプライト
    private GameObject Player;//プレイヤーの座標取得用
    private Vector2 TargetPos;//プレイヤーの位置情報
    public GameObject HitEffect;
    public GameObject DeathEffect;

    [SerializeField]
    public string Name;
    [System.NonSerialized] public int Hp = 40;
    [System.NonSerialized] public int Atk = 20;
    [System.NonSerialized] public int Def = 20;

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
    protected override void AttemptMove(int Xdir,int Ydir)
    {
        base.AttemptMove(Xdir, Ydir);
    }
   

    // Update is called once per frame
    void Update()
    {
        
    }
   
}
