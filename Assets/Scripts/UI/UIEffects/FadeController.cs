using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TitleAnimator : MonoBehaviour
{
    [SerializeField] private Image titleImage;

    void Start()
    {
        if (titleImage == null) titleImage = GetComponent<Image>();

        titleImage.color = new Color(1, 1, 1, 0); 
        transform.localScale = Vector3.one * 0.8f;

        Sequence seq = DOTween.Sequence();
        seq.Append(titleImage.DOFade(1f, 4f));
        seq.Join(transform.DOScale(1f, 4f).SetEase(Ease.OutBack));

        seq.AppendCallback(() =>
        {
            transform.DOScale(1.02f, 2f)
                     .SetLoops(-1, LoopType.Yoyo)
                     .SetEase(Ease.InOutSine);
        });
    }
}
