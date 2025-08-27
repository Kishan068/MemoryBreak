using UnityEngine;

/// <summary>
/// 快速设置UI飞行敌人系统
/// 一键移除旧系统并设置新的飞恐龙系统
/// </summary>
public class QuickUIEnemySetup : MonoBehaviour
{
    [ContextMenu("Setup UI Flying Enemy System")]
    public void SetupUIFlyingEnemySystem()
    {
        Debug.Log("🚀 开始设置UI飞行敌人系统...");
        
        // 1. 移除所有旧的生成器
        RemoveOldSpawners();
        
        // 2. 创建新的UI飞行敌人生成器
        CreateUIFlyingEnemySpawner();
        
        Debug.Log("✅ UI飞行敌人系统设置完成！");
        Debug.Log("🦅 按 [C] 键 - 在VirtualTarget位置生成飞恐龙（Pterodactyl）");
        Debug.Log("🗑️ 按 [X] 键 - 清除所有飞恐龙");
        Debug.Log("📷 摄像机保持固定 - 2D立面射击游戏风格");
    }
    
    /// <summary>
    /// 移除所有旧的生成器
    /// </summary>
    private void RemoveOldSpawners()
    {
        Debug.Log("🧹 清理旧的生成器...");
        
        // 移除所有旧组件 - 使用字符串名称避免编译错误
        var componentNamesToRemove = new string[]
        {
            "RoguelikeSystemSetup",
            "RoguelikeEnemySpawner", 
            "SimpleEnemySpawner",
            "Arcade2DShooterSpawner",
            "RoguelikeTargetVisualizer",
            "RoguelikeTargetAdapter",
            "RoguelikeTargetMonitor",
            "RoguelikeVirtualTargetSpawner",
            "RoguelikeCameraFixer",
            "RoguelikeGameStarter",
            "RoguelikeGameEventHandlerPatch"
        };
        
        foreach (string componentName in componentNamesToRemove)
        {
            // 查找所有MonoBehaviour并按名称匹配
            MonoBehaviour[] allComponents = FindObjectsOfType<MonoBehaviour>();
            foreach (var component in allComponents)
            {
                if (component != null && component.GetType().Name == componentName)
                {
                    Debug.Log($"🗑️ 移除旧组件: {componentName} 从 {component.gameObject.name}");
                    DestroyImmediate(component);
                }
            }
        }
        
        Debug.Log("✅ 旧组件清理完成");
    }
    
    /// <summary>
    /// 创建新的UI飞行敌人生成器
    /// </summary>
    private void CreateUIFlyingEnemySpawner()
    {
        Debug.Log("🦅 创建UI飞行敌人生成器...");
        
        // 查找或创建生成器对象
        GameObject spawnerObj = GameObject.Find("UIFlyingEnemySpawnerSystem");
        if (spawnerObj == null)
        {
            spawnerObj = new GameObject("UIFlyingEnemySpawnerSystem");
        }
        
        // 移除可能存在的旧组件
        UIFlyingEnemySpawner oldSpawner = spawnerObj.GetComponent<UIFlyingEnemySpawner>();
        if (oldSpawner != null)
        {
            DestroyImmediate(oldSpawner);
        }
        
        // 添加新的UI飞行敌人生成器
        UIFlyingEnemySpawner spawner = spawnerObj.AddComponent<UIFlyingEnemySpawner>();
        
        // 配置参数
        spawner.spawnDistanceZ = 20f;      // 飞恐龙生成距离
        spawner.playerDistanceZ = -10f;    // 玩家位置
        spawner.flyingSpeed = 8f;          // 飞行速度
        spawner.enemyScale = 0.3f;         // 恐龙大小
        
        Debug.Log("✅ UI飞行敌人生成器创建完成");
        Debug.Log($"  生成距离: {spawner.spawnDistanceZ}");
        Debug.Log($"  玩家距离: {spawner.playerDistanceZ}");
        Debug.Log($"  飞行速度: {spawner.flyingSpeed}");
        Debug.Log($"  恐龙缩放: {spawner.enemyScale}");
    }
    
    /// <summary>
    /// 检查系统状态
    /// </summary>
    [ContextMenu("Check System Status")]
    public void CheckSystemStatus()
    {
        Debug.Log("📊 系统状态检查:");
        
        // 检查UI飞行敌人生成器
        UIFlyingEnemySpawner spawner = FindObjectOfType<UIFlyingEnemySpawner>();
        Debug.Log($"UIFlyingEnemySpawner: {(spawner != null ? "✅ 运行中" : "❌ 未找到")}");
        
        // 检查旧组件是否已清理 - 使用字符串名称避免编译错误
        MonoBehaviour[] allComponents = FindObjectsOfType<MonoBehaviour>();
        bool foundOldSetup = false;
        foreach (var component in allComponents)
        {
            if (component != null && component.GetType().Name == "RoguelikeSystemSetup")
            {
                foundOldSetup = true;
                break;
            }
        }
        Debug.Log($"旧RoguelikeSystemSetup: {(!foundOldSetup ? "✅ 已清理" : "❌ 仍存在")}");
        
        // 检查摄像机
        Camera mainCam = Camera.main;
        if (mainCam != null)
        {
            Debug.Log($"摄像机位置: {mainCam.transform.position}");
            Debug.Log($"摄像机旋转: {mainCam.transform.rotation.eulerAngles}");
        }
    }
}