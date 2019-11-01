using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FPlayerMove : MovingObject
{
    public SpriteRenderer Spr;//表示スプライト
    public GameObject DungeonManager;
    public DungeonState PlayerState;
    public GameObject HitEffect;
    public GameObject DeathEffect;

    [System.NonSerialized] public int Hp = 100;
    [System.NonSerialized] public int Atk = 30;
    [System.NonSerialized]public int Def = 25;

    [SerializeField] public GameObject DirectionPanel;
       
    public AudioClip sord;
    public AudioClip damage;


   
    // Update is called once per frame
    void Update()
    {
        int horizontal = 0; //水平方向
        int vertical = 0; //垂直方向
        horizontal = (int)(Input.GetAxisRaw("Horizontal"));
        vertical = (int)(Input.GetAxisRaw("Vertical"));

        if (horizontal != 0 || vertical != 0)
        {
            AttemptMove(horizontal, vertical);
            
        }
    }
   
   
    protected override void AttemptMove(int Xdir, int Ydir)
    {
        DungeonManager = GameObject.FindGameObjectWithTag("GameManager");
        PlayerState = DungeonManager.GetComponent<DungeonManager>().CurrentDungeonState;
        if (PlayerState == DungeonState.keyInput)
        {
            DungeonManager.GetComponent<DungeonManager>().SetCurrentState(DungeonState.PlayerTurn);
            base.AttemptMove(Xdir, Ydir);
        }
    }

    public void Attack()
    {

    }



    //ボタン用のメソッド
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
    public void DirectionMode()
    {
        DirectionPanel.SetActive(true);
    }
    public void Direction_up()
    {
        SetCharactorDirection(0, 1);
        DirectionPanel.SetActive(false);
    }
    public void Direction_upperright()
    {
        SetCharactorDirection(1, 1);
        DirectionPanel.SetActive(false);
    }
    public void Direction_upperleft()
    {
        SetCharactorDirection(-1, 1);
        DirectionPanel.SetActive(false);
    }
    public void Direction_right()
    {
        SetCharactorDirection(1, 0);
        DirectionPanel.SetActive(false);
    }
    public void Direction_left()
    {
        SetCharactorDirection(-1, 0);
        DirectionPanel.SetActive(false);
    }
    public void Direction_bottomright()
    {
        SetCharactorDirection(1, -1);
        DirectionPanel.SetActive(false);
    }
    public void Direction_buttomleft()
    {
        SetCharactorDirection(-1, -1);
        DirectionPanel.SetActive(false);
    }
    public void Direction_down()
    {
        SetCharactorDirection(0, -1);
        DirectionPanel.SetActive(false);
    }
    public void Button_foot()
    {
        PlayerState = DungeonManager.GetComponent<DungeonManager>().CurrentDungeonState;
        if (PlayerState == DungeonState.keyInput)
        {

            DungeonManager.GetComponent<DungeonManager>().SetCurrentState(DungeonState.EnemyBegin);
        }
    }

}
