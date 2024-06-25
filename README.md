# TaskBusters! v1.0
## 更新履歴
2024年6月25日　アピールポイントを更新しました

## 概要
タスク管理とRPGゲームを組み合わせたスマートフォン向けアプリケーションです。クライアント機能開発担当2人、サーバーおよびサーバー・クライアント間通信機能担当1人で開発を行いました。

本アプリケーションには以下の機能があります。
- タスクを進めることでレベルを上げることが可能
    - タスクの登録や削除、変更といった管理だけでなく、タスクの進行度が100%になると重要度によって決められた敵(Slime・Big Slime・Mega Slime)を倒し、経験値を獲得できます。 

- レベルを上げることでレイドボスと戦うことが可能
    - それぞれ推奨レベルが定められたレイドボスがおり、タスクを進めてレベルを上げることで倒すことができます。各プレイヤーは「勇者」「戦士」「魔導士」「神官」で構成されたパーティーを持ち、その強さはプレイヤーのレベルによって決定します。このキャラクターはそれぞれ固有の技を持ち、MPを消費することで発動することができます。

- フレンド登録およびフレンドのタスク進捗を確認可能
    - フレンドのIDを知っていれば、設定画面で検索し登録することができます。登録したフレンドのレベル、経験値だけでなくタスクの進捗を「完了済みタスク数/全タスク数」の形で見ることができます。

- プレイヤー名・アイコンをいつでも変更可能
    - 設定画面からプレイヤー名やアイコン(全３種※v1.0時点)をいつでも変更することができます。

## スクリーンショット
- プレイヤー登録画面

![プレイヤー登録](https://github.com/Yolog6101/TaskBusters_client/assets/72485319/31936b39-5e0d-4a8a-803f-991f1cf67eee)

- タスク一覧

![タスク一覧](https://github.com/Yolog6101/TaskBusters_client/assets/72485319/645b3280-2d6c-4a9a-a3bf-d7286e9bf638)

- タスク登録画面

![タスク登録](https://github.com/Yolog6101/TaskBusters_client/assets/72485319/124d5870-4d51-4b2b-86e0-0ffdae5747d4)

- タスク変更・削除画面

![タスク変更・削除](https://github.com/Yolog6101/TaskBusters_client/assets/72485319/74977b1b-a2cc-49e0-915a-e90f13654d3f)

- レイドボス一覧

![レイドボス一覧](https://github.com/Yolog6101/TaskBusters_client/assets/72485319/5fba7ef9-34d3-452f-a53d-37fa912dc2d1)

- レイドボスとの対戦画面

![レイドボスバトル](https://github.com/Yolog6101/TaskBusters_client/assets/72485319/7aa060e7-af15-4622-ade9-06c9c0220930)

- フレンドのタスク進捗一覧

![フレンドタスク一覧](https://github.com/Yolog6101/TaskBusters_client/assets/72485319/662f3d53-638f-4000-a511-cde335ee4580)

- プレイヤー名・アイコン変更画面

![プレイヤー情報変更](https://github.com/Yolog6101/TaskBusters_client/assets/72485319/ee0acf42-18f4-4904-b9c9-ee5adf427a47)


## アピールポイント
　このアプリケーションではタスク管理に、こなせばこなすほどレベルを上げて強いレイドボスに挑戦できるというゲーム性を加えることで、プレイヤーのタスクに対するモチベーションを向上させることを狙いとしています。タスクの重要度や期限までの日数によって経験値を稼ぐことができるので、タスクを登録して期限までに完了できたプレイヤーはそれだけレベルが強くなりますし、逆にプレイヤーがタスクを登録しなかったり登録しても期限までに完了できなかったりした場合はレベルが上がることはありません。また、登録したフレンドのレベルや経験値、タスクの進捗をいつでも確認できるため、これによるモチベーションの維持も狙いとしています。

　実装・技術面としてはまずプレイヤーが見て楽しめるかつ、操作性のあるUIを実現しました。固有の技を持つレイドボスの種類も複数用意し、プレイヤーの持つパーティーの各キャラクターにもレベルに伴って固有の技を持たせることでRPGゲームとしての面白さも実現しています。また、サーバー・クライアント間の通信機能を実装し、タスクやフレンド情報の保管をサーバー側で行うようにしたことでアプリケーションの軽量化だけでなく、フレンド間のタスク進捗の共有を可能にしました。このサーバープログラムはAWSを用いて構築したネットワーク上の仮想サーバー上にデプロイし、いつでもアプリケーションを利用することができるようにしました。ネットワーク構成図は以下のようになっています。【現在サーバープログラムは稼働しておりません】

![network_taskbusters](https://github.com/Yolog6101/TaskBusters_client/assets/72485319/ec5a2dc0-9175-438e-bb9d-cfcb3111391b)

## 開発言語・環境
- クライアントサイド：Unity(C#) 2021.3.25f1
- サーバサイド：Go、AWS SDK for Go、AWS(ネットワーク・DynamoDB)
- その他：Unity Version Control(バージョン管理)

## マニュアル
　マニュアルは「TaskBusters!_マニュアル.pdf」をご覧ください。また、「TaskBusters_server」リポジトリ内のサーバプログラムをネットワーク上の仮想サーバーにデプロイしているため、クライアントサイドのプログラムからいつでもインターネットを通して実行することが出来ます。

## 追加実装予定
　BGM機能の追加および選べるアイコンの種類を増やす予定です。加えて、フレンドの登録解除機能やシェア画面でフレンドを選択するとそのフレンドの達成前・達成済みタスク内容が表示される機能を追加する予定です。
