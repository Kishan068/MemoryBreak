using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 2f); // 粒子存在2秒后销毁
    }
}
