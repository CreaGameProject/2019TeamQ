using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPlayerMove : MonoBehaviour
{
    public GameObject TextPanel;
    public GameObject DungeonManager;
    public DungeonState PlayerState;
    public GameObject DirectionPanel;

    private Rigidbody2D rb;
    private CircleCollider2D circleCollider;
    private Animator anm;//アニメーションコンポーネント
    private PlayerPurameter playerpurameter;

    private float MoveTime = 0.1f;
    private float InverseMoveTime;
    private int horizontal;
    private int vertical;

    // Start is called before the first frame update
    void Start()
    {
        DungeonManager = GameObject.FindGameObjectWithTag("GameManager");
        this.rb = GetComponent<Rigidbody2D>();
        this.circleCollider = GetComponent<CircleCollider2D>();
        this.anm = GetComponent<Animator>();
        this.playerpurameter = GetComponent<PlayerPurameter>();
        playerpurameter = new PlayerPurameter();
        playerpurameter.Pdirection_x = 0;
        playerpurameter.Pdirection_y = -1;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = 0; //水平方向
        vertical = 0; //垂直方向
        horizontal = (int)(Input.GetAxisRaw("Horizontal"));
        vertical = (int)(Input.GetAxisRaw("Vertical"));

        if (horizontal != 0 || vertical != 0)
        {
            AttemptMove(horizontal, vertical);

        }
    }

   private void AttemptMove(int Xdir, int Ydir)
    {
        TextPanel.SetActive(false);
        PlayerState = DungeonManager.GetComponent<DungeonManager>().CurrentDungeonState;
        if (PlayerState == DungeonState.keyInput)
        {
            DungeonManager.GetComponent<DungeonManager>().SetCurrentState(DungeonState.PlayerTurn);
            Vector2 StartPosition = transform.position;
            Vector2 EndPosition = StartPosition + new Vector2(Xdir, Ydir);
            //移動判定用、衝突するレイヤーはすべて入れる
            int LayerObj = LayerMask.GetMask(new string[] { "Enemy", "Wall" });

            //移動先に障害物があるか判定する
            RaycastHit2D HitObj = Physics2D.Linecast(StartPosition, EndPosition, LayerObj);

            playerpurameter.Pdirection_x =Xdir;
            playerpurameter.Pdirection_y = Ydir;
            anm.SetFloat("Direction_X", Xdir);
            anm.SetFloat("Direction_Y", Ydir);
            
            //Physics2Dで移動先に障害物がなければMovementを実行
            if (HitObj.transform == null)
            {
                
                StartCoroutine(Movement(EndPosition));
            }
            //physics2Dで移動先に何かあれば何もしない
            else if (HitObj.transform != null)
            {
                DungeonManager.GetComponent<DungeonManager>().SetCurrentState(DungeonState.keyInput);
            }

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
        DungeonManager.GetComponent<DungeonManager>().SetCurrentState(DungeonState.PlayerEnd);

    }

    public void Attack()
    {
        PlayerState = DungeonManager.GetComponent<DungeonManager>().CurrentDungeonState;
        if (PlayerState == DungeonState.keyInput)
        {
            DungeonManager.GetComponent<DungeonManager>().SetCurrentState(DungeonState.PlayerTurn);
            Vector2 NowPosition = transform.position;
            Vector2 AtkRange = NowPosition + new Vector2(playerpurameter.Pdirection_x, playerpurameter.Pdirection_y);
            Debug.Log(AtkRange);
            //攻撃判定用
            int LayerCha = LayerMask.GetMask(new string[] { "Enemy" });

            //攻撃先に敵がいるかどうか判定する
            RaycastHit2D Hitcha = Physics2D.Linecast(NowPosition, AtkRange, LayerCha);

            Debug.Log(Hitcha.transform);
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
                Debug.Log(Damage + "のダメージを与えた");
            }
            DungeonManager.GetComponent<DungeonManager>().SetCurrentState(DungeonState.PlayerEnd);
        }
    }
    public void Button_up()
    {
        AttemptMove(0, 1);
    }
    public void Button_upperright()
    {
        AttemptMove(1, 1);
    }
    public void Button_upperleft()
    {
        AttemptMove(-1, 1);
    }
    public void Button_right()
    {
        AttemptMove(1, 0);
    }
    public void Button_left()
    {
        AttemptMove(-1, 0);
    }
    public void Button_bottomright()
    {
        AttemptMove(1, -1);
    }
    public void Button_buttomleft()
    {
        AttemptMove(-1, -1);
    }
    public void Button_down()
    {
        AttemptMove(0, -1);
    }
}
