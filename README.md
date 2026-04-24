# 【開発失敗】偏食ア〇ニャの料理格付け

UnityとLLMを用いた対話型AIアプリケーション・ゲーム

## 開発の目的
ローカルLLMでキャラクターの特徴をSystemPromptの範囲で真似させることで、
他のゲームでの会話や文章生成する際に精度がどうなるかの確認

## 失敗原因

SystemPrompt内でのローカルLLMへの指示が回答に正しく反映されない
(アーニャの指示を出したにもかかわらず、アーニャに似た何かになる)

(検証:Llama3.2-3B,Qwen3-12B)

## 使用ライブラリ (Libraries Used)
- [LLMUnity](https://github.com/undreamai/LLMUnity)
- [UniTask](https://github.com/Cysharp/UniTask)
- [DOTween](https://dotween.demigiant.com/)

## ライセンス (License)

### 本プロジェクトのライセンス
このプロジェクト自体のソースコードは **MIT License** の下で公開されています。詳細は [LICENSE](LICENSE) ファイルをご覧ください。

### サードパーティ・ライセンス
本プロジェクトで使用している外部ライブラリおよびAIモデルの著作権表示とライセンス全文は、[THIRD-PARTY-NOTICES.md](THIRD-PARTY-NOTICES.md) に記載されています。

- **LLMUnity / UniTask / DOTween:** MIT License
- **Gemma-2-2b-it:** Gemma Terms of Use (Google)

---
Copyright (c) 2026 Ness(nesspencilrocket)
