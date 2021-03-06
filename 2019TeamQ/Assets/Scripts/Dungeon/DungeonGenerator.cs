﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    private Transform boardHolder;
    [Header("床のオブジェクト")]
    [SerializeField]
    private GameObject floor;

    [Header("壁のオブジェクト")]
    [SerializeField]
    private GameObject wall;

    [Header("外壁のオブジェクト")]
    [SerializeField]
    private GameObject iron;

    [Header("プレイヤーのトランスフォーム")]
    [SerializeField]
    private Transform Player;

    [Header("敵のトランスフォーム")]
    [SerializeField]
    private GameObject Enemy;

    [Header("剣のトランスフォーム")]
    [SerializeField]
    private GameObject MysteriousSword;

    [Header("盾のトランスフォーム")]
    [SerializeField]
    private GameObject　MysteriousShield;

    [Header("弓のトランスフォーム")]
    [SerializeField]
    private GameObject Arrow;

    [Header("ヒールポーションのトランスフォーム")]
    [SerializeField]
    private GameObject HeelPotion;

    [Header("エクスヒールポーションのトランスフォーム")]
    [SerializeField]
    private GameObject Ex_HeelPotion;

    [Header("フルヒールポーションのトランスフォーム")]
    [SerializeField]
    private GameObject Full_HeelPotion;

    //敵の数
    [SerializeField]
    [Range(0, 5)]
    int Enemycount;
    //剣の数
    [SerializeField]
    [Range(0, 5)]
    int MysteriousSwordcount;
    //弓の数
    [SerializeField]
    [Range(0, 5)]
    int Arrowcount;

    [SerializeField]
    [Range(0, 5)]
    int MysteriousShieldcount;

    [SerializeField]
    [Range(0, 5)]
    int HeelPotioncount;

    [SerializeField]
    [Range(0, 5)]
    int Ex_HeelPotioncount;

    [SerializeField]
    [Range(0, 5)]
    int Full_HeelPotioncount;

    [Header("マップ全体の大きさ")]
    [SerializeField]
    [Range(20, 100)]
    int MapWidth = 50;
    [SerializeField]
    [Range(20, 100)]
    int MapHeight = 30;

   



    const int wallID = 0;
    const int roomID = 1;
    const int roadID = 2;

    private int[,] Map;
    [Header("部屋の数 Min,Max（最小,最大）")]
    [SerializeField]
    [Range(1, 10)]
    int MinRooms = 1;
    [SerializeField]
    [Range(1, 10)]
    int MaxRooms = 10;

    private int roomNum;


    private List<DviRoomInfomation> RoomDVI = new List<DviRoomInfomation>();


    // Start is called before the first frame update
    void Start()
    {
        MapResetData();
        MapDivisionCreate();
        RoomCreate();
        RoadCreate();
        CreateDangeon();
        InitPlayer();
        InitEnemy();
        InitMysteriousSword();
        InitArrow();
        InitMysteriousShield();
        InitHeelPotion();
        InitEx_HeelPotion();
        InitFull_HeelPotion();
    }

    // 壁しかないMapデータの生成
    private void MapResetData()
    {
        Map = new int[MapWidth, MapHeight]; //Mapデータ[横,縦]

        // 壁しかないMapデータを作る -------------------
        for (int i = 0; i < MapWidth; i++)
        {
            for (int j = 0; j < MapHeight; j++)
            {
                Map[i, j] = wallID;
            }
        }
    }

    // 最初の区画の情報を作る
    private void MapDivisionCreate()
    {
        int dviPos;
        roomNum = Random.Range(MinRooms, MaxRooms);
        RoomDVI.Add(new DviRoomInfomation());
        RoomDVI[0].Top = 0;
        RoomDVI[0].Left = 0;
        RoomDVI[0].Bottom = MapHeight - 1;
        RoomDVI[0].Right = MapWidth - 1;
        RoomDVI[0].areaRank = RoomDVI[0].Bottom + RoomDVI[0].Right;

        for (int i = 1; i < roomNum; i++)
        {
            RoomDVI.Add(new DviRoomInfomation());
            int Target = 0;
            int AreaMax = 0;
            // 最大の面積を持つ区画を指定する
            for (int j = 0; j < i; j++)
            {
                if (RoomDVI[j].areaRank >= AreaMax)
                {
                    AreaMax = RoomDVI[j].areaRank;
                    Target = j;
                }
            }

            // 分割点を求める
            if ((RoomDVI[Target].Bottom - RoomDVI[Target].Top) > 12 && (RoomDVI[Target].Right - RoomDVI[Target].Left) > 12)
            {
                RoomDVI[i].nextRoom = Target;
                dviPos = Random.Range(0, RoomDVI[Target].areaRank);

                if (dviPos > (RoomDVI[Target].Bottom - RoomDVI[Target].Top))
                {
                    RoomDVI[i].Left = RoomDVI[Target].Left + Random.Range(6, RoomDVI[Target].Right - RoomDVI[Target].Left - 6);
                    RoomDVI[i].Right = RoomDVI[Target].Right;
                    RoomDVI[Target].Right = RoomDVI[i].Left - 1;
                    RoomDVI[i].Top = RoomDVI[Target].Top;
                    RoomDVI[i].Bottom = RoomDVI[Target].Bottom;
                    RoomDVI[i].isNextX = true;
                    RoomDVI[i].NextRoomPos = RoomDVI[i].Left;
                }
                else
                {
                    RoomDVI[i].Top = RoomDVI[Target].Top + Random.Range(6, RoomDVI[Target].Bottom - RoomDVI[Target].Top - 6);
                    RoomDVI[i].Bottom = RoomDVI[Target].Bottom;
                    RoomDVI[Target].Bottom = RoomDVI[i].Top - 1;
                    RoomDVI[i].Left = RoomDVI[Target].Left;
                    RoomDVI[i].Right = RoomDVI[Target].Right;
                    RoomDVI[i].isNextX = false;
                    RoomDVI[i].NextRoomPos = RoomDVI[i].Top;

                }

                RoomDVI[i].areaRank = RoomDVI[i].Bottom - RoomDVI[i].Top + RoomDVI[i].Right - RoomDVI[i].Left;
                RoomDVI[Target].areaRank = RoomDVI[Target].Bottom - RoomDVI[Target].Top + RoomDVI[Target].Right - RoomDVI[Target].Left;
            }
            else
            {
                roomNum = i;
                break;
            }
        }
    }

    private void RoomCreate()
    {
        int ratioX;
        int ratioY;
        int moveX;
        int moveY;
        for (int i = 0; i < roomNum; i++)
        {
            if (RoomDVI[i] != null)
            {
                ratioY = RoomDVI[i].Bottom - RoomDVI[i].Top;
                ratioY = Mathf.FloorToInt(Random.Range(0.500f, 0.800f) * ratioY);
                ratioX = RoomDVI[i].Right - RoomDVI[i].Left;
                ratioX = Mathf.FloorToInt(Random.Range(0.500f, 0.800f) * ratioX);
                moveY = Mathf.FloorToInt((RoomDVI[i].Bottom - RoomDVI[i].Top - ratioY) / 2.0f);
                moveX = Mathf.FloorToInt((RoomDVI[i].Right - RoomDVI[i].Left - ratioX) / 2.0f);
                RoomDVI[i].Top = RoomDVI[i].Top + moveY;
                RoomDVI[i].Bottom = RoomDVI[i].Top + ratioY;
                RoomDVI[i].Left = RoomDVI[i].Left + moveX;
                RoomDVI[i].Right = RoomDVI[i].Left + ratioX;

                for (int j = 0; j < ratioY; j++)
                {
                    for (int k = 0; k < ratioX; k++)
                    {
                        Map[RoomDVI[i].Left + k + 1, RoomDVI[i].Top + j + 1] = roomID;
                    }
                }
            }
            else
                break;
        }
    }

    private void RoadCreate()
    {
        int NOWpos = 0;
        int NOWdis = 0;
        int NEXTpos = 0;
        int NEXTdis = 0;
        for (int i = 1; i < roomNum; i++)
        {
            if (RoomDVI[i].isNextX)
            {
                // 通路の開始地点、及び終了地点を求める
                NOWpos = RoomDVI[i].Bottom - RoomDVI[i].Top;
                NOWpos = Random.Range(1, NOWpos) + RoomDVI[i].Top;
                NEXTpos = RoomDVI[RoomDVI[i].nextRoom].Bottom - RoomDVI[RoomDVI[i].nextRoom].Top;
                NEXTpos = Random.Range(1, NEXTpos) + RoomDVI[RoomDVI[i].nextRoom].Top;

                // 通路を引く線の長さを（開始、終了地点共に）求める
                NOWdis = RoomDVI[i].Left - RoomDVI[i].NextRoomPos + 1;
                NEXTdis = RoomDVI[i].NextRoomPos - RoomDVI[RoomDVI[i].nextRoom].Right + 1;


                // ←ライン作成
                for (int j = 0; j < NOWdis; j++)
                {
                    Map[-j + RoomDVI[i].Left, NOWpos] = roadID;
                }

                // →ライン作成
                for (int j = 0; j < NEXTdis; j++)
                {
                    Map[j + RoomDVI[RoomDVI[i].nextRoom].Right, NEXTpos] = roadID;
                }

                // 縦ライン作成
                for (int j = 0; ; j++)
                {
                    // NOWとNEXT、どちらの方が高さが大きいか調べ、縦ラインを作成する
                    if (NOWpos >= NEXTpos)
                    {
                        if ((NEXTpos + j) < NOWpos)
                        {
                            Map[RoomDVI[i].NextRoomPos, NEXTpos + j] = roadID;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        if ((NOWpos + j) < NEXTpos)
                        {
                            Map[RoomDVI[i].NextRoomPos, NOWpos + j] = roadID;
                        }
                        else
                        {
                            break;
                        }
                    }
                }

            }
            else
            {
                // 通路の開始地点、及び終了地点を求める
                NOWpos = RoomDVI[i].Right - RoomDVI[i].Left;
                NOWpos = Random.Range(1, NOWpos) + RoomDVI[i].Left;
                NEXTpos = RoomDVI[RoomDVI[i].nextRoom].Right - RoomDVI[RoomDVI[i].nextRoom].Left;
                NEXTpos = Random.Range(1, NEXTpos) + RoomDVI[RoomDVI[i].nextRoom].Left;

                // 通路を引く線の長さを（開始、終了地点共に）求める
                NOWdis = RoomDVI[i].Top - RoomDVI[i].NextRoomPos + 1;
                NEXTdis = RoomDVI[i].NextRoomPos - RoomDVI[RoomDVI[i].nextRoom].Bottom + 1;


                // ↑ライン作成
                for (int j = 0; j < NOWdis; j++)
                {
                    Map[NOWpos, -j + RoomDVI[i].Top] = roadID;
                }

                // ↓ライン作成
                for (int j = 0; j < NEXTdis; j++)
                {
                    Map[NEXTpos, j + RoomDVI[RoomDVI[i].nextRoom].Bottom] = roadID;
                }

                // 横ライン作成
                for (int j = 0; ; j++)
                {
                    // NOWとNEXT、どちらの方が幅が大きいか調べ、横ラインを作成する
                    if (NOWpos >= NEXTpos)
                    {
                        if ((NEXTpos + j) < NOWpos)
                        {
                            Map[NEXTpos + j, RoomDVI[i].NextRoomPos] = roadID;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        if ((NOWpos + j) < NEXTpos)
                        {
                            Map[NOWpos + j, RoomDVI[i].NextRoomPos] = roadID;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }

    }

    private void CreateDangeon()
    {
        boardHolder = new GameObject("Board").transform;
        for (int i = 0; i < MapWidth; i++)
        {
            for (int j = 0; j < MapHeight; j++)
            {
                if (Map[i, j] == roadID||Map[i,j]==roomID)
                {// 床を敷き詰める
                    GameObject instance = Instantiate(floor, new Vector2(i - MapWidth / 2, j - MapHeight / 2), Quaternion.identity) as GameObject;
                    instance.transform.SetParent(boardHolder);
                }
                
                // 壁だった場合壁にする
                if (Map[i, j] == wallID)
                {

                    GameObject instance = Instantiate(wall, new Vector2(i - MapWidth / 2, j - MapHeight / 2), Quaternion.identity) as GameObject;
                    instance.transform.SetParent(boardHolder);
                    
                }
            }
        }

        // 外壁を作る
        for (int i = -1; i < MapHeight + 1; i++)
        {
            GameObject instance1 = Instantiate(iron, new Vector2(-1 - MapWidth / 2, i - MapHeight / 2), Quaternion.identity);
            GameObject instance2 = Instantiate(iron, new Vector2(MapWidth - MapWidth / 2, i - MapHeight / 2), Quaternion.identity);
            instance1.transform.SetParent(boardHolder);
            instance2.transform.SetParent(boardHolder);
        }
        for (int i = -1; i < MapWidth; i++)
        {
            GameObject instance1 = Instantiate(iron, new Vector2(i - MapWidth / 2, -1 - MapHeight / 2), Quaternion.identity);
            GameObject instance2 = Instantiate(iron, new Vector2(i - MapWidth / 2, MapHeight - MapHeight / 2), Quaternion.identity);
            instance1.transform.SetParent(boardHolder);
            instance2.transform.SetParent(boardHolder);
        }

    }
    private void InitPlayer()
    {
        int InitRoom = Random.Range(0, roomNum);
        int x = Random.Range(0, RoomDVI[InitRoom].Right - RoomDVI[InitRoom].Left) + RoomDVI[InitRoom].Left;
        int y = Random.Range(0, RoomDVI[InitRoom].Bottom - RoomDVI[InitRoom].Top) + RoomDVI[InitRoom].Top;
        Player.transform.position = new Vector2(x - MapWidth / 2 + 1, y - MapHeight / 2 + 1);

        //Instantiate(Player, new Vector2(x - MapWidth / 2 + 1, y - MapHeight / 2 + 1), Quaternion.identity);
    }
    private void InitEnemy()
    {
        for (int e = 0; e < Enemycount; e++)
        {
            int InitRoom = Random.Range(0, roomNum);
            int x = Random.Range(0, RoomDVI[InitRoom].Right - RoomDVI[InitRoom].Left) + RoomDVI[InitRoom].Left;
            int y = Random.Range(0, RoomDVI[InitRoom].Bottom - RoomDVI[InitRoom].Top) + RoomDVI[InitRoom].Top;
            //Enemy.transform.position = new Vector2(x - MapWidth / 2 + 1, y - MapHeight / 2 + 1);
            Instantiate(Enemy, new Vector2(x - MapWidth / 2+1, y - MapHeight / 2+1), Quaternion.identity);
        }
    }
    private void InitMysteriousSword()
    {
        for (int e = 0; e < MysteriousSwordcount; e++)
        {
            int InitRoom = Random.Range(0, roomNum);
            int x = Random.Range(0, RoomDVI[InitRoom].Right - RoomDVI[InitRoom].Left) + RoomDVI[InitRoom].Left;
            int y = Random.Range(0, RoomDVI[InitRoom].Bottom - RoomDVI[InitRoom].Top) + RoomDVI[InitRoom].Top;
            Instantiate(MysteriousSword, new Vector2(x - MapWidth / 2 + 1, y - MapHeight / 2 + 1), Quaternion.identity);
        }
    }
    private void InitArrow()
    {
        for (int e = 0; e < Arrowcount; e++)
        {
            int InitRoom = Random.Range(0, roomNum);
            int x = Random.Range(0, RoomDVI[InitRoom].Right - RoomDVI[InitRoom].Left) + RoomDVI[InitRoom].Left;
            int y = Random.Range(0, RoomDVI[InitRoom].Bottom - RoomDVI[InitRoom].Top) + RoomDVI[InitRoom].Top;
            Instantiate(Arrow, new Vector2(x - MapWidth / 2 + 1, y - MapHeight / 2 + 1), Quaternion.identity);
        }
    }
    private void InitMysteriousShield()
    {
        for (int e = 0; e < MysteriousShieldcount; e++)
        {
            int InitRoom = Random.Range(0, roomNum);
            int x = Random.Range(0, RoomDVI[InitRoom].Right - RoomDVI[InitRoom].Left) + RoomDVI[InitRoom].Left;
            int y = Random.Range(0, RoomDVI[InitRoom].Bottom - RoomDVI[InitRoom].Top) + RoomDVI[InitRoom].Top;
            Instantiate(MysteriousShield, new Vector2(x - MapWidth / 2 + 1, y - MapHeight / 2 + 1), Quaternion.identity);
        }
    }

    private void InitHeelPotion()
    {
        for (int e = 0; e < HeelPotioncount; e++)
        {
            int InitRoom = Random.Range(0, roomNum);
            int x = Random.Range(0, RoomDVI[InitRoom].Right - RoomDVI[InitRoom].Left) + RoomDVI[InitRoom].Left;
            int y = Random.Range(0, RoomDVI[InitRoom].Bottom - RoomDVI[InitRoom].Top) + RoomDVI[InitRoom].Top;
            Instantiate(HeelPotion, new Vector2(x - MapWidth / 2 + 1, y - MapHeight / 2 + 1), Quaternion.identity);
        }
    }

    private void InitEx_HeelPotion()
    {
        for (int e = 0; e < Ex_HeelPotioncount; e++)
        {
            int InitRoom = Random.Range(0, roomNum);
            int x = Random.Range(0, RoomDVI[InitRoom].Right - RoomDVI[InitRoom].Left) + RoomDVI[InitRoom].Left;
            int y = Random.Range(0, RoomDVI[InitRoom].Bottom - RoomDVI[InitRoom].Top) + RoomDVI[InitRoom].Top;
            Instantiate(Ex_HeelPotion, new Vector2(x - MapWidth / 2 + 1, y - MapHeight / 2 + 1), Quaternion.identity);
        }
    }

    private void InitFull_HeelPotion()
    {
        for (int e = 0; e < Full_HeelPotioncount; e++)
        {
            int InitRoom = Random.Range(0, roomNum);
            int x = Random.Range(0, RoomDVI[InitRoom].Right - RoomDVI[InitRoom].Left) + RoomDVI[InitRoom].Left;
            int y = Random.Range(0, RoomDVI[InitRoom].Bottom - RoomDVI[InitRoom].Top) + RoomDVI[InitRoom].Top;
            Instantiate(Full_HeelPotion, new Vector2(x - MapWidth / 2 + 1, y - MapHeight / 2 + 1), Quaternion.identity);
        }
    }
}



public class DviRoomInfomation
{
    public int Top = 0;
    public int Left = 0;
    public int Bottom = 0;
    public int Right = 0;
    public int areaRank = 0;
    public int nextRoom = 0;
    public bool isNextX = false;
    public int NextRoomPos = 0;
}
