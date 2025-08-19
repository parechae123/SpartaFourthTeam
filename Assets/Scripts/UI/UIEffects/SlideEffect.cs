using UnityEngine;
using DG.Tweening;

public class SlideEffect : MonoBehaviour
{
    public enum Dir { Left, Right, Up, Down }
    public void Slide(RectTransform panel, Dir dir, float duration = 0.6f, bool show = true)
    {
        var canvas = GetComponentInParent<Canvas>();
        var rootRT = canvas ? canvas.GetComponent<RectTransform>() : null;
        var size = rootRT ? rootRT.rect.size : new Vector2(Screen.width, Screen.height);

        Vector2 off = dir switch
        {
            Dir.Left  => new(-size.x, 0),
            Dir.Right => new(size.x, 0),
            Dir.Up    => new(0,  size.y),
            Dir.Down  => new(0, -size.y),
            _ => Vector2.zero
        };

        panel.gameObject.SetActive(true);
        panel.DOKill();

        if (show)
        {
            var target = Vector2.zero;
            panel.anchoredPosition = target - off;
            panel.DOAnchorPos(target, duration).SetEase(Ease.OutCubic);
        }
        else
        {
            var start = Vector2.zero;
            panel.DOAnchorPos(start + off, duration).SetEase(Ease.OutCubic)
                .OnComplete(() =>
                {
                    panel.anchoredPosition = start;
                    panel.gameObject.SetActive(false);
                });
        }
    }

}
