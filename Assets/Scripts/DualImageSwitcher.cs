using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks; // UniTaskを使うために必須
using System.Threading;

// DualImageSwitcher.cs の修正版（Aだけ管理）
public class DualImageSwitcher : MonoBehaviour
{
    [Header("セットA (6枚用)")]
    public Image displayImageA;
    public Sprite[] spritesA;

    // セットBの変数は削除、または使わないようにする

    private const float IntervalSeconds = 0.5f;

    void Start()
    {
        var token = this.GetCancellationTokenOnDestroy();
        SwitchLoop(token).Forget();
    }

    private async UniTaskVoid SwitchLoop(CancellationToken token)
    {
        int indexA = 0;
        while (!token.IsCancellationRequested)
        {
            if (spritesA.Length > 0)
            {
                displayImageA.sprite = spritesA[indexA];
                indexA = (indexA + 1) % spritesA.Length;
            }
            await UniTask.Delay(System.TimeSpan.FromSeconds(IntervalSeconds), cancellationToken: token);
        }
    }
}

