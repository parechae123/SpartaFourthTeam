using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FadeController : MonoBehaviour
{
    [SerializeField] private Image titleImage;
    private Sequence titleSequence;
    private Tween loopTween;

    void Start()
    {
        if (titleImage == null) titleImage = GetComponent<Image>();

        titleImage.color = new Color(1, 1, 1, 0); 
        transform.localScale = Vector3.one * 0.8f;

        titleSequence = DOTween.Sequence();
        titleSequence.Append(titleImage.DOFade(1f, 4f));
        titleSequence.Join(transform.DOScale(1f, 4f).SetEase(Ease.OutBack));

        titleSequence.AppendCallback(() =>
        {
            loopTween = transform.DOScale(1.02f, 2f)
                                 .SetLoops(-1, LoopType.Yoyo)
                                 .SetEase(Ease.InOutSine);
        });
    }
    //화면전환시 종료하도록 설정
    void OnDestroy()
    {
        if (titleSequence != null && titleSequence.IsActive())
        {
            titleSequence.Kill();
        }
        if (loopTween != null && loopTween.IsActive())
        {
            loopTween.Kill();
        }
    }
}