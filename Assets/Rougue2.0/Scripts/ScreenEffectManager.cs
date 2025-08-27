using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenEffectManager : MonoBehaviour
{
    public static ScreenEffectManager Instance;

    [Header("Red Flash Settings")]
    public Image redOverlayImage;     // 拖入你的 RedOverlay UI Image
    public bool useSetActive = false; // 是否使用 SetActive
    public float flashAlpha = 0.4f;
    public float flashDuration = 0.2f;

    [Header("Shake Settings")]
    public RectTransform canvasTransform; // 拖入 Canvas
    public float shakeDuration = 0.3f;
    public float shakeStrength = 20f;

    void Awake()
    {
        Instance = this;

        if (redOverlayImage != null)
        {
            // 保证一开始透明
            var c = redOverlayImage.color;
            c.a = 0;
            redOverlayImage.color = c;

            if (useSetActive)
                redOverlayImage.gameObject.SetActive(false);
        }
    }

    public void FlashRed()
    {
        StartCoroutine(RedFlashCoroutine());
    }

    IEnumerator RedFlashCoroutine()
    {
        if (useSetActive)
            redOverlayImage.gameObject.SetActive(true);

        redOverlayImage.color = new Color(1, 0, 0, flashAlpha);
        yield return new WaitForSeconds(flashDuration);
        redOverlayImage.color = new Color(1, 0, 0, 0);

        if (useSetActive)
            redOverlayImage.gameObject.SetActive(false);
    }

    public void ShakeScreen()
    {
        StartCoroutine(ShakeCoroutine());
    }

    IEnumerator ShakeCoroutine()
    {
        Vector3 originalPos = canvasTransform.anchoredPosition;
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeStrength;
            float y = Random.Range(-1f, 1f) * shakeStrength;
            canvasTransform.anchoredPosition = originalPos + new Vector3(x, y, 0);

            elapsed += Time.deltaTime;
            yield return null;
        }

        canvasTransform.anchoredPosition = originalPos;
    }
}
