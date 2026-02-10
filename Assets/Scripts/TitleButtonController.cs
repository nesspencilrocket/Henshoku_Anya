using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks; // UniTask
using DG.Tweening;             // DOTween
using System.Threading;

public class TitleButtonController : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private CanvasGroup uiGroup; // タイトル全体の透明度

    void Start()
    {
        // 最初の演出：ふわっとボタンを浮かび上がらせる
        startButton.transform.localScale = Vector3.zero;
        startButton.transform.DOScale(1f, 0.6f).SetEase(Ease.OutBack);

        // ボタンにクリックイベントを登録
        startButton.onClick.AddListener(() => OnClickStart().Forget());
    }

    private async UniTaskVoid OnClickStart()
    {
        // 二重押しを防ぐ
        startButton.interactable = false;
        var token = this.GetCancellationTokenOnDestroy();

        // --- モダンな演出1: ポヨンと弾む ---
        // 0.2秒で少し縮んで戻る（DOPunchScale）
        await startButton.transform.DOPunchScale(Vector3.one * -0.1f, 0.2f)
            .ToUniTask(cancellationToken: token);

        // --- モダンな演出2: 画面を暗転させてから遷移 ---
        // UI全体を0.5秒で透明にする
        await uiGroup.DOFade(0f, 0.5f).ToUniTask(cancellationToken: token);

        // シーン移動
        SceneManager.LoadScene("GameScene");
    }
}