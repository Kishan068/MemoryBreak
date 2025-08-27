using UnityEngine;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// UI位置飞行敌人生成器
/// 在VirtualTarget位置生成飞恐龙，沿Z轴飞向玩家
/// 完全模拟simulator场景的街机射击游戏风格
/// </summary>
public class UIFlyingEnemySpawner : MonoBehaviour
{
    [Header("飞恐龙预制体")]
    public GameObject pterodactylPrefab;
    
    [Header("生成设置")]
    public KeyCode spawnKey = KeyCode.C;
    public KeyCode removeAllKey = KeyCode.X;
    
    [Header("飞行参数")]
    public float spawnDistanceZ = 20f;      // 恐龙生成的Z轴距离
    public float playerDistanceZ = -10f;    // 玩家位置的Z轴距离
    public float flyingSpeed = 8f;          // 飞行速度
    public float enemyScale = 0.3f;         // 恐龙缩放（让它们更小）
    
    [Header("随机生成范围")]
    [Tooltip("是否使用随机偏移（关闭则精确对齐UI位置）")]
    public bool useRandomOffset = false;    // 是否使用随机偏移
    [Tooltip("X轴随机偏移范围")]
    public float randomOffsetX = 10f;       // X轴随机偏移范围
    [Tooltip("Y轴随机偏移范围")]
    public float randomOffsetY = 5f;        // Y轴随机偏移范围
    [Tooltip("是否收集所有Lane的VirtualTarget")]
    public bool useAllLanes = true;         // 使用所有Lane的VirtualTarget
    
    [Header("摄像机设置")]
    public Vector3 cameraPosition = new Vector3(0, 3, -15);
    public Vector3 cameraRotation = new Vector3(15, 0, 0);
    
    private List<VirtualTarget> allTargets = new List<VirtualTarget>();
    private List<GameObject> flyingEnemies = new List<GameObject>();
    private Camera mainCamera;
    private Canvas targetCanvas;
    
    private void Start()
    {
        mainCamera = Camera.main;
        SetupSystem();
    }
    
    private void Update()
    {
        HandleInput();
        CleanupDestroyedEnemies();
    }
    
    /// <summary>
    /// 设置系统
    /// </summary>
    private void SetupSystem()
    {
        Debug.Log("🎮 设置UI飞行敌人系统...");
        
        // 设置摄像机
        SetupCamera();
        
        // 加载飞恐龙预制体
        LoadPterodactylPrefab();
        
        // 收集VirtualTarget
        CollectVirtualTargets();
        
        Debug.Log($"✅ 系统设置完成 - 找到 {allTargets.Count} 个VirtualTarget");
        Debug.Log("🦕 按 [C] 键在VirtualTarget位置生成飞恐龙");
        Debug.Log("🗑️ 按 [X] 键清除所有飞恐龙");
    }
    
    /// <summary>
    /// 摄像机设置 - 保持固定位置（已移除）
    /// </summary>
    private void SetupCamera()
    {
        // 不移动摄像机 - 保持2D游戏风格
        Debug.Log($"📷 摄像机保持当前位置: {mainCamera.transform.position} - 2D立面射击游戏风格");
    }
    
    /// <summary>
    /// 加载飞恐龙预制体
    /// </summary>
    private void LoadPterodactylPrefab()
    {
        if (pterodactylPrefab == null)
        {
            #if UNITY_EDITOR
            pterodactylPrefab = UnityEditor.AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Toon Dinosaurs/Pterodactyl/Prefabs/Pterodactyl.prefab");
            #endif
        }
        
        Debug.Log($"🦅 飞恐龙预制体: {(pterodactylPrefab != null ? "✅ 已加载" : "❌ 未找到")}");
    }
    
    /// <summary>
    /// 收集所有VirtualTarget
    /// </summary>
    private void CollectVirtualTargets()
    {
        allTargets.Clear();
        
        // 查找TargetMarkers/EvikeZone1
        GameObject game = GameObject.Find("Game");
        if (game == null)
        {
            Debug.LogError("❌ 找不到Game对象");
            return;
        }
        
        Transform canvas = game.transform.Find("Canvas");
        if (canvas == null)
        {
            Debug.LogError("❌ 找不到Canvas");
            return;
        }
        
        targetCanvas = canvas.GetComponent<Canvas>();
        
        Transform targetMarkers = canvas.Find("TargetMarkers");
        if (targetMarkers == null)
        {
            Debug.LogError("❌ 找不到TargetMarkers");
            return;
        }
        
        Transform evikeZone1 = targetMarkers.Find("EvikeZone1");
        if (evikeZone1 == null)
        {
            Debug.LogError("❌ 找不到EvikeZone1");
            return;
        }
        
        if (useAllLanes)
        {
            // 收集所有Lane的VirtualTarget
            for (int i = 1; i <= 5; i++) // Lane1 到 Lane5
            {
                Transform lane = evikeZone1.Find($"Lane{i}");
                if (lane != null)
                {
                    VirtualTarget[] targets = lane.GetComponentsInChildren<VirtualTarget>();
                    foreach (VirtualTarget target in targets)
                    {
                        allTargets.Add(target);
                        Debug.Log($"📍 添加VirtualTarget: {target.name} 从 Lane{i} - UI位置: {target.GetComponent<RectTransform>().anchoredPosition}");
                    }
                    Debug.Log($"✅ Lane{i} 收集到 {targets.Length} 个VirtualTarget");
                }
            }
        }
        else
        {
            // 只收集Lane1
            Transform lane1 = evikeZone1.Find("Lane1");
            if (lane1 != null)
            {
                VirtualTarget[] targets = lane1.GetComponentsInChildren<VirtualTarget>();
                foreach (VirtualTarget target in targets)
                {
                    allTargets.Add(target);
                    Debug.Log($"📍 添加VirtualTarget: {target.name} - UI位置: {target.GetComponent<RectTransform>().anchoredPosition}");
                }
            }
        }
        
        Debug.Log($"✅ 总共收集到 {allTargets.Count} 个VirtualTarget");
        
        // 检查数量是否正确
        if (allTargets.Count == 45)
        {
            Debug.Log("✅ VirtualTarget数量正确！(5个Lane × 9个 = 45个)");
        }
        else
        {
            Debug.LogWarning($"⚠️ VirtualTarget数量异常！找到{allTargets.Count}个，预期45个");
        }
        
        // 显示各Lane的VirtualTarget数量
        Debug.Log("📍 各Lane VirtualTarget数量:");
        for (int i = 1; i <= 5; i++)
        {
            int laneCount = allTargets.Count(target => target.transform.parent.name.Contains($"Lane{i}"));
            Debug.Log($"  Lane{i}: {laneCount} 个VirtualTarget {(laneCount == 9 ? "✅" : "❌")}");
        }
    }
    
    /// <summary>
    /// 处理输入
    /// </summary>
    private void HandleInput()
    {
        if (Input.GetKeyDown(spawnKey))
        {
            SpawnFlyingEnemyAtVirtualTarget();
        }
        
        if (Input.GetKeyDown(removeAllKey))
        {
            RemoveAllFlyingEnemies();
        }
    }
    
    /// <summary>
    /// 在随机VirtualTarget位置生成飞恐龙
    /// </summary>
    private void SpawnFlyingEnemyAtVirtualTarget()
    {
        if (allTargets.Count == 0)
        {
            Debug.LogWarning("⚠️ 没有可用的VirtualTarget");
            return;
        }
        
        if (pterodactylPrefab == null)
        {
            Debug.LogWarning("⚠️ 飞恐龙预制体未加载");
            return;
        }
        
        // 选择随机VirtualTarget
        VirtualTarget randomTarget = allTargets[Random.Range(0, allTargets.Count)];
        
        // 将UI坐标转换为世界坐标
        Vector3 uiWorldPosition = ConvertUIToWorldPosition(randomTarget);
        
        // 计算偏移（可选）
        Vector3 finalOffset = Vector3.zero;
        if (useRandomOffset)
        {
            float randomX = Random.Range(-randomOffsetX, randomOffsetX);
            float randomY = Random.Range(-randomOffsetY, randomOffsetY);
            finalOffset = new Vector3(randomX, randomY, 0);
        }
        
        // 设置恐龙生成位置（精确对齐UI位置或加上随机偏移）
        Vector3 spawnPosition = new Vector3(
            uiWorldPosition.x + finalOffset.x, 
            uiWorldPosition.y + finalOffset.y, 
            spawnDistanceZ
        );
        
        // 生成飞恐龙
        GameObject flyingEnemy = Instantiate(pterodactylPrefab, spawnPosition, Quaternion.identity);
        flyingEnemy.name = $"FlyingPterodactyl_{randomTarget.name}";
        
        // 调整恐龙大小
        flyingEnemy.transform.localScale = Vector3.one * enemyScale;
        
        // 计算目标位置（玩家位置）
        Vector3 targetPosition = new Vector3(
            uiWorldPosition.x + finalOffset.x, 
            uiWorldPosition.y + finalOffset.y, 
            playerDistanceZ
        );
        
        // 让恐龙朝向玩家
        Vector3 playerDirection = targetPosition - spawnPosition;
        flyingEnemy.transform.LookAt(flyingEnemy.transform.position + playerDirection);
        
        // 添加飞行控制组件
        UIFlyingEnemyMover mover = flyingEnemy.AddComponent<UIFlyingEnemyMover>();
        mover.targetWorldPosition = targetPosition;
        mover.speed = flyingSpeed;
        
        // 禁用可能干扰的组件
        DisableInterferingComponents(flyingEnemy);
        
        flyingEnemies.Add(flyingEnemy);
        
        Debug.Log($"🦅 生成飞恐龙: {flyingEnemy.name}");
        Debug.Log($"  📍 基础UI目标: {randomTarget.name} - UI坐标: {randomTarget.GetComponent<RectTransform>().anchoredPosition}");
        Debug.Log($"  🌍 世界坐标: {uiWorldPosition}");
        if (useRandomOffset)
        {
            Debug.Log($"  🎲 随机偏移: {finalOffset}");
            Debug.Log($"  🌍 最终世界位置: {uiWorldPosition + finalOffset}");
        }
        else
        {
            Debug.Log($"  🎯 精确对齐UI位置 - 无偏移");
        }
        Debug.Log($"  🚀 生成位置: {spawnPosition}");
        Debug.Log($"  🎯 飞行目标: {targetPosition}");
    }
    
    /// <summary>
    /// 将UI坐标转换为世界坐标
    /// </summary>
    private Vector3 ConvertUIToWorldPosition(VirtualTarget target)
    {
        RectTransform rectTransform = target.GetComponent<RectTransform>();
        Vector2 anchoredPos = rectTransform.anchoredPosition;
        
        // 方法1：使用RectTransformUtility进行正确的UI到世界坐标转换
        Vector3 worldPosition;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(
            rectTransform, anchoredPos, mainCamera, out worldPosition))
        {
            // 转换成功
        }
        else
        {
            // 备用方法：简单的比例转换
            // UI坐标(-1919, -439)映射到世界坐标
            float worldX = anchoredPos.x * 0.01f; // 缩放到合适的世界坐标
            float worldY = anchoredPos.y * 0.01f;
            worldPosition = new Vector3(worldX, worldY, 0);
        }
        
        Debug.Log($"🔄 UI坐标转换: {anchoredPos} → 世界坐标: {worldPosition}");
        return worldPosition;
    }
    
    /// <summary>
    /// 禁用干扰组件
    /// </summary>
    private void DisableInterferingComponents(GameObject enemy)
    {
        // 保持Animator启用，但设置为空Controller以防止干扰
        Animator animator = enemy.GetComponent<Animator>();
        if (animator != null)
        {
            animator.runtimeAnimatorController = null;
        }
        
        // 设置Rigidbody为Kinematic
        Rigidbody rb = enemy.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
        }
    }
    
    /// <summary>
    /// 清理被销毁的敌人
    /// </summary>
    private void CleanupDestroyedEnemies()
    {
        for (int i = flyingEnemies.Count - 1; i >= 0; i--)
        {
            if (flyingEnemies[i] == null)
            {
                flyingEnemies.RemoveAt(i);
            }
        }
    }
    
    /// <summary>
    /// 移除所有飞恐龙
    /// </summary>
    private void RemoveAllFlyingEnemies()
    {
        foreach (GameObject enemy in flyingEnemies)
        {
            if (enemy != null)
            {
                Destroy(enemy);
            }
        }
        flyingEnemies.Clear();
        Debug.Log("🗑️ 清除所有飞恐龙");
    }
    
    /// <summary>
    /// 显示状态信息
    /// </summary>
    [ContextMenu("Show Status")]
    public void ShowStatus()
    {
        Debug.Log($"📊 UI飞行敌人系统状态:");
        Debug.Log($"  VirtualTarget数量: {allTargets.Count}");
        Debug.Log($"  飞行敌人数量: {flyingEnemies.Count}");
        Debug.Log($"  飞恐龙预制体: {(pterodactylPrefab != null ? "✅" : "❌")}");
        Debug.Log($"  目标Canvas: {(targetCanvas != null ? "✅" : "❌")}");
        Debug.Log($"  使用所有Lane: {(useAllLanes ? "✅" : "❌ 仅Lane1")}");
        Debug.Log($"  X轴随机偏移: ±{randomOffsetX}");
        Debug.Log($"  Y轴随机偏移: ±{randomOffsetY}");
    }
    
    /// <summary>
    /// 在Scene视图中绘制调试信息
    /// </summary>
    private void OnDrawGizmos()
    {
        // 绘制生成区域和玩家区域
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(new Vector3(0, 0, spawnDistanceZ), new Vector3(20, 10, 1));
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector3(0, 0, playerDistanceZ), new Vector3(20, 10, 1));
        
        // 绘制所有VirtualTarget的世界位置
        if (allTargets != null && Application.isPlaying)
        {
            Gizmos.color = Color.yellow;
            foreach (VirtualTarget target in allTargets)
            {
                if (target != null)
                {
                    // 简单显示UI位置映射
                    Vector2 anchoredPos = target.GetComponent<RectTransform>().anchoredPosition;
                    Vector3 worldPos = new Vector3(anchoredPos.x * 0.01f, anchoredPos.y * 0.01f, 0);
                    Gizmos.DrawWireSphere(worldPos, 0.5f);
                    
                    // 绘制随机偏移范围
                    Gizmos.color = Color.cyan;
                    Gizmos.DrawWireCube(worldPos, new Vector3(randomOffsetX * 2, randomOffsetY * 2, 0.1f));
                }
            }
        }
    }
}

/// <summary>
/// UI飞行敌人移动组件
/// 让恐龙沿Z轴飞向指定的世界位置
/// </summary>
public class UIFlyingEnemyMover : MonoBehaviour
{
    [Header("飞行参数")]
    public Vector3 targetWorldPosition;
    public float speed = 8f;
    
    private void Update()
    {
        // 沿Z轴飞向目标位置
        transform.position = Vector3.MoveTowards(transform.position, targetWorldPosition, speed * Time.deltaTime);
        
        // 如果到达目标位置，继续飞行
        if (Vector3.Distance(transform.position, targetWorldPosition) < 0.5f)
        {
            // 继续向玩家后方飞行
            targetWorldPosition = targetWorldPosition + Vector3.back * 10f;
        }
        
        // 如果飞得太远，销毁自己
        if (transform.position.z < -20f)
        {
            Destroy(gameObject);
        }
    }
}