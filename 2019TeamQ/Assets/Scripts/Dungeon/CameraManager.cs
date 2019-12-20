using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] GameObject player;//playerの格納用変数

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = player.transform.position;//playerのtransform.positionを取得
        this.transform.position = new Vector3(pos.x, pos.y, -1);
    }
}
