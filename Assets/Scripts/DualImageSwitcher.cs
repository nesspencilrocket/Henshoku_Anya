using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks; // UniTaskを使うために必須
using System.Threading;

public class DualImageSwitcher : MonoBehaviour
{
    [Header("セットA (6枚用)")]
    public SpriteRenderer displayImageA;
    public Sprite[] spritesA;

    [Header("セットB (5枚用)")]
    public SpriteRenderer displayImageB;
    public Sprite[] spritesB;

    private const float IntervalSeconds = 0.5f;

    void Start()
    {
        // オブジェクトが破棄されたら自動で止まるように設定して実行
        var token = this.GetCancellationTokenOnDestroy();
        SwitchLoop(token).Forget();
    }

    private async UniTaskVoid SwitchLoop(CancellationToken token)
    {
        int indexA = 0;
        int indexB = 0;

        // ループ開始
        while (!token.IsCancellationRequested)
        {
            // セットAの更新
            if (spritesA.Length > 0)
            {
                displayImageA.sprite = spritesA[indexA];
                indexA = (indexA + 1) % spritesA.Length;
            }

            // セットBの更新
            if (spritesB.Length > 0)
            {
                displayImageB.sprite = spritesB[indexB];
                indexB = (indexB + 1) % spritesB.Length;
            }

            // 0.5秒（500ミリ秒）待機
            await UniTask.Delay(System.TimeSpan.FromSeconds(IntervalSeconds), cancellationToken: token);
        }
    }
}
