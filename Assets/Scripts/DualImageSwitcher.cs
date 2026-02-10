using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks; // UniTask
using System.Threading;

public class DualImageSwitcher : MonoBehaviour
{
    [Header("セットA (6枚用)")]
    public Image displayImageA;
    public Sprite[] spritesA;

    [Header("セットB (5枚用)")]
    public Image displayImageB;
    public Sprite[] spritesB;

    // 切り替え間隔（0.5秒）
    private const float IntervalSeconds = 0.5f;

    void Start()
    {
        // 破棄時にタスクが止まるようにCancellationTokenを取得
        CancellationToken token = this.GetCancellationTokenOnDestroy();

        // 非同期ループを開始（.Forget()で投げっぱなしにする）
        SwitchLoopAsync(token).Forget();
    }

    private async UniTaskVoid SwitchLoopAsync(CancellationToken token)
    {
        int indexA = 0;
        int indexB = 0;

        // 無限ループ（オブジェクトが消えるまで継続）
        while (!token.IsCancellationRequested)
        {
            // --- セットAの表示更新 ---
            if (spritesA != null && spritesA.Length > 0)
            {
                displayImageA.sprite = spritesA[indexA];
                // 次の番号へ。枚数(6)に達したら0に戻る
                indexA = (indexA + 1) % spritesA.Length;
            }

            // --- セットBの表示更新 ---
            if (spritesB != null && spritesB.Length > 0)
            {
                displayImageB.sprite = spritesB[indexB];
                // 次の番号へ。枚数(5)に達したら0に戻る
                indexB = (indexB + 1) % spritesB.Length;
            }

            // --- 0.5秒待機 ---
            // ミリ秒指定(500)でもOKですが、TimeSpanの方が直感的です
            await UniTask.Delay(System.TimeSpan.FromSeconds(IntervalSeconds), cancellationToken: token);
        }
    }
}