using UnityEngine;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// 清理版本的目标监控器 - 不引用删除的组件
/// </summary>
public class CleanRoguelikeTargetMonitor : MonoBehaviour
{
    [Header("监控设置")]
    public bool showDetailedLogs = false;
    
    private void Start()
    {
        Debug.Log("🎯 清理版目标监控器启动");
    }
    
    /// <summary>
    /// 显示当前系统状态
    /// </summary>
    [ContextMenu("Show System Status")]
    public void ShowSystemStatus()
    {
        Debug.Log("📊 系统状态检查:");
        
        // 检查VirtualTarget
        VirtualTarget[] allTargets = FindObjectsOfType<VirtualTarget>();
        Debug.Log($"🎯 找到 {allTargets.Length} 个VirtualTarget");
        
        // 检查UIFlyingEnemySpawner
        UIFlyingEnemySpawner spawner = FindObjectOfType<UIFlyingEnemySpawner>();
        Debug.Log($"🦅 UIFlyingEnemySpawner: {(spawner != null ? "✅ 运行中" : "❌ 未找到")}");
        
        Debug.Log("✅ 系统状态检查完成");
    }
}