using UnityEngine;
using UnityEngine.UI;

public class HoleCell : MonoBehaviour
{
    public int index;
    public GameObject meteorGO;
    public GameObject explosionEffectPrefab;
    public Transform explosionSpawnPoint;

    private Spawner spawner;

    private bool isFake = false;
    private bool isTarget = false;

    void Start()
    {
        spawner = FindObjectOfType<Spawner>();
        
        // Connect button click to our OnClick method
        var button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnClick);
        }
        
        ResetCell();
    }

    public void SetAsTarget()
    {
        isFake = false;
        isTarget = true;
        meteorGO.SetActive(true);
        meteorGO.GetComponent<Image>().color = Color.white;
    }

    public void SetAsFake()
    {
        isFake = true;
        isTarget = false;
        meteorGO.SetActive(true);
        meteorGO.GetComponent<Image>().color = Color.gray; // 灰色表示假目标
    }

    public void ResetCell()
    {
        meteorGO.SetActive(false);
        isFake = false;
        isTarget = false;
        meteorGO.GetComponent<Image>().color = Color.white;
    }

    public void ShowFlash(int order)
    {
        meteorGO.SetActive(true);
    }

    public void HideFlash()
    {
        meteorGO.SetActive(false);
    }

    public void ShowSuccess()
    {
        meteorGO.SetActive(false);
        if (explosionEffectPrefab && explosionSpawnPoint)
        {
            Instantiate(explosionEffectPrefab, explosionSpawnPoint.position, Quaternion.identity);
        }
    }

    public void ShowErrorFeedback()
    {
        ScreenEffectManager.Instance.FlashRed();
        ScreenEffectManager.Instance.ShakeScreen();
    }

    public void OnClick()
    {
        if (!meteorGO.activeSelf) 
        {
            Debug.Log($"[HoleCell] Click ignored on cell {index} - meteor not active");
            return;
        }
        
        Debug.Log($"[HoleCell] Processing click on cell {index}, isFake: {isFake}, isTarget: {isTarget}");
        spawner.HandleClick(this, isFake);
    }

    public void ResetColor()
{
    Image img = meteorGO.GetComponent<Image>();
    if (img != null)
    {
        img.color = Color.white;
        Debug.Log($"[Reset Color] cell #{index} set to white.");
    }
}

}
