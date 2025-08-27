using UnityEngine;
using System.Collections;

public class TargetSeeker : MonoBehaviour
{
    [Header("目标追踪器设置")]
    public string myTargetId;
    public float explosionDuration = 1f;
    
    [Header("视觉效果")]
    public GameObject explosionEffect;
    
    private Renderer meshRenderer;
    private bool isDestroyed = false;
    
    private void Start()
    {
        meshRenderer = GetComponent<Renderer>();
        if (meshRenderer == null)
        {
            meshRenderer = GetComponentInChildren<Renderer>();
        }
    }
    
    /// <summary>
    /// 击杀无人机（带动画）
    /// </summary>
    public void Kill()
    {
        if (isDestroyed) return;
        
        Debug.Log($"💥 TargetSeeker {myTargetId} 被击杀");
        isDestroyed = true;
        
        // 播放爆炸效果
        StartCoroutine(PlayDeathAnimation());
    }
    
    /// <summary>
    /// 无动画击杀
    /// </summary>
    public void KillWithoutAnimating()
    {
        if (isDestroyed) return;
        
        Debug.Log($"🗑️ TargetSeeker {myTargetId} 被快速销毁");
        isDestroyed = true;
        
        Destroy(gameObject);
    }
    
    /// <summary>
    /// 播放死亡动画
    /// </summary>
    private IEnumerator PlayDeathAnimation()
    {
        // 创建爆炸效果
        if (explosionEffect != null)
        {
            GameObject explosion = Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Destroy(explosion, explosionDuration);
        }
        else
        {
            // 简单的闪烁效果
            yield return StartCoroutine(FlashEffect());
        }
        
        // 销毁对象
        Destroy(gameObject);
    }
    
    /// <summary>
    /// 简单的闪烁效果
    /// </summary>
    private IEnumerator FlashEffect()
    {
        if (meshRenderer == null) yield break;
        
        Color originalColor = meshRenderer.material.color;
        Color flashColor = Color.white;
        
        float flashDuration = 0.1f;
        int flashCount = 3;
        
        for (int i = 0; i < flashCount; i++)
        {
            // 闪白
            meshRenderer.material.color = flashColor;
            yield return new WaitForSeconds(flashDuration);
            
            // 恢复原色
            meshRenderer.material.color = originalColor;
            yield return new WaitForSeconds(flashDuration);
        }
    }
    
    /// <summary>
    /// 设置目标ID
    /// </summary>
    public void SetTargetId(string targetId)
    {
        myTargetId = targetId;
    }
    
    /// <summary>
    /// 获取目标ID
    /// </summary>
    public string GetTargetId()
    {
        return myTargetId;
    }
}