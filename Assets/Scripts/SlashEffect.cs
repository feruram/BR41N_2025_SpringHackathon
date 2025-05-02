using UnityEngine;

public class SlashEffect : MonoBehaviour
{
    public float speed = 5f; // エフェクトの移動速度

    void Update()
    {
        // プレイヤーの正面方向に移動
        transform.position += transform.forward * speed * Time.deltaTime;
    }
}
