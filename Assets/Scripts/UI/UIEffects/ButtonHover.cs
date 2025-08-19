using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Target")]
    [SerializeField] Image target;

    [Header("Colors")]
    [SerializeField] Color normalColor = new(0.00f, 0.90f, 1f, 1f);
    [SerializeField] Color hoverColor  = new(0.12f, 0.55f, 1f, 1f);

    [Header("Motion")]
    [SerializeField] float hoverScale = 1.05f;
    [SerializeField] float duration   = 0.12f;

    Tween loop;

    void Awake()
    {
        if (!target) target = GetComponent<Image>();
        if (target) target.color = normalColor;
        transform.localScale = Vector3.one;
    }

    public void OnPointerEnter(PointerEventData e)
    {
        KillAll();
        if (target) target.DOColor(hoverColor, duration);
        transform.DOScale(hoverScale, duration).SetEase(Ease.OutQuad);

        loop = transform.DOScale(hoverScale + 0.02f, 0.4f)
                        .SetLoops(-1, LoopType.Yoyo)
                        .SetEase(Ease.InOutSine);
    }

    public void OnPointerExit(PointerEventData e)
    {
        KillAll();
        if (target) target.DOColor(normalColor, duration);
        transform.DOScale(1f, duration).SetEase(Ease.OutQuad);
    }

    void KillAll()
    {
        loop?.Kill();
        transform.DOKill();
        target?.DOKill();
    }
}
