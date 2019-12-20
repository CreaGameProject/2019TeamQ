using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPlayerAttack : MonoBehaviour
{
    public GameObject TextPanel;
    public GameObject DungeonManager;
    public DungeonState PlayerState;
    private Animator anm;

    private Rigidbody2D rb;
    private CircleCollider2D circleCollider;
    private PlayerPurameter playerpurameter;

    // Start is called before the first frame update
    void Start()
    {
        DungeonManager = GameObject.Find("DungeonManager");
        this.rb = GetComponent<Rigidbody2D>();
        this.circleCollider = GetComponent<CircleCollider2D>();
        this.playerpurameter = GameObject.Find("GameManager").GetComponent<PlayerPurameter>();
        this.anm = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Attack()
    {
        PlayerState = DungeonManager.GetComponent<DungeonManager>().CurrentDungeonState;
        if (PlayerState == DungeonState.keyInput)
        {
            DungeonManager.GetComponent<DungeonManager>().SetCurrentState(DungeonState.PlayerTurn);
            Vector2 NowPosition = transform.position;
            Vector2 AtkRange = NowPosition + new Vector2(playerpurameter.Pdirection_x, playerpurameter.Pdirection_y);
            //攻撃判定用
            int LayerCha = LayerMask.GetMask(new string[] { "Enemy" });

            //攻撃先に敵がいるかどうか判定する
            RaycastHit2D Hitcha = Physics2D.Linecast(NowPosition, AtkRange, LayerCha);
            anm.SetTrigger("Attack");
            
            //Physics2Dで攻撃先に敵がいればダメージ計算
            if (Hitcha.transform != null)
            {
                //敵の関数を取得し、変数を代入可能にする
                GameObject HitComponent = Hitcha.transform.gameObject;
                Enemy Script = HitComponent.GetComponent<Enemy>();
                //ダメージ計算
                int Damage = playerpurameter.PAtk * playerpurameter.PAtk / (playerpurameter.PAtk + Script.Def);
                //オブジェクトのHp変数にダメージを与える
                Script.Hp -= Damage;
               DungeonManager.GetComponent<DungeonTextController>().ShowMessage(Damage + "のダメージを与えた\n");
            }
            DungeonManager.GetComponent<DungeonManager>().SetCurrentState(DungeonState.PlayerEnd);
        }
    }
}
