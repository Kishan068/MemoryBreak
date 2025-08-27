using UnityEngine;
using System.Linq;

/// <summary>
/// 项目清理器 - 移除不必要的旧脚本和组件
/// </summary>
public class ProjectCleaner : MonoBehaviour
{
    [Header("清理选项")]
    [Tooltip("是否显示详细的清理信息")]
    public bool showDetailedLogs = true;
    
    /// <summary>
    /// 清理所有旧的不必要组件
    /// </summary>
    [ContextMenu("Clean Old Components")]
    public void CleanOldComponents()
    {
        Debug.Log("🧹 开始清理旧组件...");
        
        // 要清理的旧组件类型名称（使用字符串避免编译错误）
        var componentNamesToRemove = new string[]
        {
            "RoguelikeSystemSetup",
            "RoguelikeEnemySpawner",
            "SimpleEnemySpawner",
            "Arcade2DShooterSpawner",
            "Arcade2DSetup",
            "RoguelikeTargetVisualizer",
            "RoguelikeTargetAdapter",
            "RoguelikeTargetMonitor",
            "RoguelikeVirtualTargetSpawner",
            "RoguelikeCameraFixer",
            "RoguelikeGameStarter",
            "RoguelikeGameEventHandlerPatch",
            "GameEventHandlerPatch",
            "RoguelikeLaneMode"
        };
        
        int totalRemoved = 0;
        
        foreach (string componentName in componentNamesToRemove)
        {
            // 查找所有MonoBehaviour并按名称匹配
            MonoBehaviour[] allComponents = FindObjectsOfType<MonoBehaviour>();
            var matchingComponents = allComponents.Where(c => c.GetType().Name == componentName).ToArray();
            
            if (matchingComponents.Length > 0)
            {
                foreach (var component in matchingComponents)
                {
                    if (component != null)
                    {
                        if (showDetailedLogs)
                        {
                            Debug.Log($"🗑️ 移除旧组件: {componentName} 从 {component.gameObject.name}");
                        }
                        DestroyImmediate(component);
                        totalRemoved++;
                    }
                }
            }
        }
        
        Debug.Log($"✅ 清理完成！移除了 {totalRemoved} 个旧组件");
    }
    
    /// <summary>
    /// 检查VirtualTarget数量
    /// </summary>
    [ContextMenu("Check VirtualTarget Count")]
    public void CheckVirtualTargetCount()
    {
        Debug.Log("📊 检查VirtualTarget数量...");
        
        // 查找所有VirtualTarget
        VirtualTarget[] allVirtualTargets = FindObjectsOfType<VirtualTarget>();
        Debug.Log($"🎯 总共找到 {allVirtualTargets.Length} 个VirtualTarget");
        
        // 按Lane分组统计
        var laneGroups = allVirtualTargets
            .Where(vt => vt.transform.parent != null)
            .GroupBy(vt => vt.transform.parent.name)
            .OrderBy(group => group.Key);
        
        foreach (var laneGroup in laneGroups)
        {
            Debug.Log($"  📍 {laneGroup.Key}: {laneGroup.Count()} 个VirtualTarget");
            
            if (showDetailedLogs)
            {
                foreach (var target in laneGroup)
                {
                    Vector2 pos = target.GetComponent<RectTransform>().anchoredPosition;
                    Debug.Log($"    - {target.name}: UI坐标({pos.x}, {pos.y})");
                }
            }
        }
        
        // 检查是否符合预期
        if (allVirtualTargets.Length == 45)
        {
            Debug.Log("✅ VirtualTarget数量正确！应该是45个（5个Lane × 9个 = 45个）");
        }
        else
        {
            Debug.LogWarning($"⚠️ VirtualTarget数量异常！找到{allVirtualTargets.Length}个，预期45个");
        }
    }
    
    /// <summary>
    /// 检查当前系统状态
    /// </summary>
    [ContextMenu("Check System Status")]
    public void CheckSystemStatus()
    {
        Debug.Log("📊 当前系统状态:");
        
        // 检查UIFlyingEnemySpawner
        UIFlyingEnemySpawner spawner = FindObjectOfType<UIFlyingEnemySpawner>();
        Debug.Log($"UIFlyingEnemySpawner: {(spawner != null ? "✅ 运行中" : "❌ 未找到")}");
        
        if (spawner != null)
        {
            Debug.Log($"  使用所有Lane: {(spawner.useAllLanes ? "✅" : "❌")}");
            Debug.Log($"  使用随机偏移: {(spawner.useRandomOffset ? "✅" : "❌")}");
        }
        
        // 检查旧组件是否已清理
        var oldComponentNames = new string[]
        {
            "RoguelikeSystemSetup",
            "RoguelikeEnemySpawner",
            "SimpleEnemySpawner"
        };
        
        bool hasOldComponents = false;
        foreach (string componentName in oldComponentNames)
        {
            MonoBehaviour[] allComponents = FindObjectsOfType<MonoBehaviour>();
            var matchingComponents = allComponents.Where(c => c.GetType().Name == componentName).ToArray();
            
            if (matchingComponents.Length > 0)
            {
                Debug.LogWarning($"❌ 发现旧组件: {componentName} ({matchingComponents.Length}个)");
                hasOldComponents = true;
            }
        }
        
        if (!hasOldComponents)
        {
            Debug.Log("✅ 所有旧组件已清理");
        }
        
        // 检查VirtualTarget数量
        CheckVirtualTargetCount();
    }
    
    /// <summary>
    /// 一键完整清理和检查
    /// </summary>
    [ContextMenu("Full Cleanup and Check")]
    public void FullCleanupAndCheck()
    {
        Debug.Log("🚀 执行完整清理和检查...");
        
        CleanOldComponents();
        CheckSystemStatus();
        
        Debug.Log("✅ 完整清理和检查完成！");
    }
}