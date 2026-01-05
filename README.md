# C# development environment for learning

- 学習用のC#開発環境のスタートアップキットをまとめたリポジトリです。DevContainerを用いて、環境に依存せず再現できる方式で構成しています。
- この構成方法は実際の業務でも役に立ちます。コピーして使うだけですから、これをベースにプロジェクトに合わせてカスタムしてみてください。
  - 自分のPC内で複数の開発環境を同時並行で使用できます。既存の環境を壊したりすることがありません。
  - コンテナなので、壊してしまっても数分ですぐに再構築できます (注意: コンテナ内だけに保存したファイルは救えないので、保存場所は使い分けるようにしましょう)。

## 📋 概要

このプロジェクトは、C#を用いた学習でビルド、実行が簡単に行えることを目的として作っています。
Win11のWSL2上でLinuxコンテナを動かす構成としていますから、GUIアプリケーション開発には向きませんが、C#の基礎学習やコンソールアプリケーション、ASP.NET Core Web APIの学習には最適です。

- ✅ **DevContainer対応**: Docker + VS Code での統一開発環境
- ✅ **.NET 9.0**: 最新の .NET SDK を使用
- ✅ **エンタープライズグレード**: Roslyn解析器、Code品質ツール標準装備

## 🚀 クイックスタート

### 前提条件

- Win11 Pro (WSL2が使用できること)
- Docker Desktopは使用しません (インストールされている場合は競合などのトラブルが発生する可能性があります)
- [VS Code](https://code.visualstudio.com/) がInstallされていること
- VSCodeのExtensionで [Dev Containers拡張機能](https://marketplace.visualstudio.com/items?itemName=ms-vscode-remote.remote-containers) がInstallされていること (本リポジトリに推奨Extensinosが定義されています `.vscode/extensions.json` )
- Git がInstallされていること (出来るだけ新しいバージョンを使うとトラブルが減ります)

### セットアップ

1. **リポジトリをクローン**

   ```bash
   git clone https://github.com/sun-flat-yamada/learn-dev-env-csharp.git
   cd learn-dev-env-csharp
   ```

2. **VS Code で DevContainer を開く**
   - VS Code を開き、リポジトリフォルダを開く
   - コマンドパレット (`Ctrl+Shift+P`) で `Dev Containers: Reopen in Container` を実行
   - コンテナのビルドと初期化が完了するまで待機（初回は3-5分）

3. **環境確認用サンプルプログラム（HelloWorld）を実行**

   C# 開発環境が正しく構築されたか確認するために、サンプルプログラムを実行できます。

   ```bash
   # プロジェクトのビルド
   dotnet build

   # プロジェクトの実行
   dotnet run
   ```

   **期待される出力:**
   ```
   === Hello World ===
   Hello, World! Welcome to C# development.
   --- Environment Information ---
   .NET Version: .NET 9.0.11
   OS: Debian GNU/Linux 12 (bookworm)
   Processor Count: 4
   ✓ Sample program executed successfully!
   ```

   **別の実行方法（VS Code タスク）:**
   - コマンドパレット (`Ctrl+Shift+P`) で `Tasks: Run Task` を実行
   - 実行したいタスクを選択 (例: `build`, `run`, `test`)

4. **プロジェクトの独立実行**

   `src` ディレクトリ以下に作成した特定のプロジェクト（HelloWorldなど）を個別に実行する場合:

   ```bash
   # 特定のプロジェクトをビルド
   dotnet build src/HelloWorld

   # 特定のプロジェクトを実行
   dotnet run --project src/HelloWorld
   ```
## 📝 新しいプロジェクトを作成する手順

このリポジトリに新しいプロジェクトを追加する場合は、以下の手順に従ってください。

### 1. 新しいプロジェクトディレクトリを作成

```bash
# src ディレクトリ下に新しいプロジェクトフォルダを作成
mkdir -p src/YourProjectName
cd src/YourProjectName
```

### 2. プロジェクトファイルを作成

```bash
# C# コンソールアプリケーションプロジェクトを生成
dotnet new console -n YourProjectName
```

このコマンドで以下のファイルが自動生成されます:
- `YourProjectName.csproj` - プロジェクトファイル
- `Program.cs` - エントリーポイント
- `obj/` - ビルド中間ファイル

### 3. ルートプロジェクトに新規プロジェクトを追加（オプション）

複数のプロジェクトを一括ビルドする場合は、ルートの `lean-dev-env-csharp.csproj` に新しいプロジェクトへの参照を追加します:

```bash
# ルートディレクトリで実行
dotnet sln add src/YourProjectName/YourProjectName.csproj
```

### 4. 必要に応じてNuGetパッケージを追加

```bash
# プロジェクトディレクトリで実行
cd src/YourProjectName
dotnet add package <PackageName>
```

例: `Newtonsoft.Json` パッケージを追加する場合
```bash
dotnet add package Newtonsoft.Json
```

### 5. 新しいプロジェクトをビルド・実行

```bash
# 特定のプロジェクトをビルド
dotnet build src/YourProjectName

# 特定のプロジェクトを実行
dotnet run --project src/YourProjectName

# または、ルートから全プロジェクトをビルド（YourProjectNameも含まれます）
dotnet build
```

### ディレクトリ構造例

```
src/
├── HelloWorld/
│   ├── Program.cs
│   ├── YourProjectName.csproj
│   └── Services/
│       └── GreetingService.cs
└── YourProjectName/          # ← 新しいプロジェクト
    ├── Program.cs
    ├── YourProjectName.csproj
    ├── Models/
    ├── Services/
    └── Utils/
```

### 推奨事項

- **ネームスペース**: `YourProjectName` または `YourProjectName.Services` など、プロジェクト名に基づいた階層構造を使用
- **レイヤー分離**: 大規模なプロジェクトは `Services/`, `Models/`, `Utils/` などのサブディレクトリで機能を分離
- **テストプロジェクト**: テストが必要な場合は `tests/YourProjectName.Tests/` ディレクトリを作成
## � プロジェクト構造

```
lean-dev-env-csharp/
├── lean-dev-env-csharp.csproj       # ルートプロジェクトファイル（リポジトリ名と同じ）
├── README.md                        # このファイル
├── .devcontainer/                   # DevContainer設定
│   ├── devcontainer.json
│   └── Dockerfile
├── .vscode/                         # VS Code設定
│   ├── extensions.json              # 推奨拡張機能
│   ├── settings.json                # ワークスペース設定
│   └── tasks.json                   # ビルド・実行タスク
├── src/                             # ソースコードディレクトリ
│   └── HelloWorld/                  # 環境確認用サンプルプロジェクト
│       ├── Program.cs               # エントリーポイント
│       └── Services/                # ソースコード群
│           └── GreetingService.cs   #   :
└── bin/                             # ビルド出力（自動生成）
```

### ディレクトリ構造について

- **`lean-dev-env-csharp.csproj`**: リポジトリのルートプロジェクトファイル。ベストプラクティスに従いリポジトリ名と同一です。
- **`src/` ディレクトリ**: 複数のサブプロジェクトを追加可能な拡張構造
  - 各サブプロジェクト（例: `HelloWorld`, `AnotherProject`）を独立して実行可能
  - サブプロジェクトごとにネームスペースを分離
  - `Services/`, `Models/`, `Tests/` など、層別構造を採用

## 🛠️ トラブルシューティング

### ビルドやコンパイルエラーが出る場合

```bash
# 1. NuGet パッケージを復元
dotnet restore

# 2. クリーンビルド
dotnet clean
dotnet build

# 3. 詳細なエラー情報を表示
dotnet build --verbosity diagnostic
```

### 推奨拡張機能がインストールされていない場合

VS Code のコマンドパレット (`Ctrl+Shift+P`) で以下を実行:
- `Extensions: Show Recommended Extensions` → 推奨拡張機能を確認・インストール

## 🛠️ WSL2をセットアップしていない場合の環境構築ガイド（Windows 11 Pro + WSL2）

このセクションでは、Windows 11 Pro上にWSL2をセットアップし、DevContainerを使用して開発環境を構築する完全な方法を説明します。

### 📋 前提条件

- Windows 11 Pro （Home エディションでは Hyper-V が利用できないため不可）
- システム管理者権限でのアクセス
- インターネット接続

### 1️⃣ WSL2のセットアップと Ubuntu のインストール

#### ステップ1: Hyper-V の有効化

1. **Windows の機能の有効化** を開く
   - スタートメニューで「機能」と検索
   - 「Windows の機能の有効化または無効化」をクリック

2. 以下の項目をチェック
   - ☑️ **Hyper-V**
   - ☑️ **Virtual Machine Platform**
   - ☑️ **Windows Subsystem for Linux**

3. **OK** をクリック → システムの再起動が必要

```powershell
# または PowerShell（管理者）で一括有効化
Enable-WindowsOptionalFeature -Online -FeatureName Microsoft-Hyper-V -All
Enable-WindowsOptionalFeature -Online -FeatureName VirtualMachinePlatform
Enable-WindowsOptionalFeature -Online -FeatureName Microsoft-Windows-Subsystem-Linux
```

#### ステップ2: WSL バージョンの確認と設定

PowerShell（管理者）で実行:

```powershell
# WSL のバージョン確認
wsl --version

# WSL 2 をデフォルトに設定
wsl --set-default-version 2

# WSLのUpdateを実施して最新にする
wsl --update
```

#### ステップ3: Ubuntu のインストール

##### コマンドライン経由

```powershell
# インストール可能な Linux ディストリビューション一覧
wsl --list --online

# Ubuntu (最新) のインストール
wsl --install -d ubuntu

# WSL 再起動
wsl --shutdown

# その後、PowerShell で再度実行
wsl
```

#### ステップ4: Ubuntu 環境の初期化

- 本リポジトリをVSCodeでcloneした後、コマンドパレット (`Ctrl+Shift+P`) で `Dev Containers: Reopen in Container` を実行してください。
- DockerをInstallするか聞かれますので、Yesを選択して自動セットアップを実施してください。

#### ステップ5: WSL2 からのアクセス確認

```bash
# Windows ファイルシステムへのアクセス確認
ls /mnt/c

# ホームディレクトリへのアクセス
cd /mnt/c/Users/<YourUsername>/

# WSL2 バージョン確認
wsl --version
```

#### ステップ6: Docker ユーザーグループの設定 (エラーが出て必要な場合)

Docker コマンドを `sudo` なしで実行できるようにセットアップします。

```bash
# docker グループの存在確認（通常は自動作成）
grep docker /etc/group

# 現在のユーザーを docker グループに追加
sudo usermod -aG docker $USER

# グループの変更を反映（以下いずれかを実行）
# 方法1: ログアウト・ログイン（推奨）
exit  # WSL2 ターミナルを終了

# 方法2: グループ変更の即時反映
newgrp docker

# インストール確認
docker run --rm hello-world
```

⚠️ **重要**: ユーザーをdockerグループに追加した後は、**WSL2 ターミナルを完全に終了して再度起動** してください。

### 3️⃣ VS Code の DevContainer セットアップ

#### ステップ1: VS Code のインストール

1. [Visual Studio Code](https://code.visualstudio.com/) をダウンロード・インストール
2. 「Add to PATH」オプションをチェック

#### ステップ2: VS Code 拡張機能のインストール

VS Code を開き、以下の拡張機能をインストール:

1. **Dev Containers** (ms-vscode-remote.remote-containers)
   - 拡張機能ウィンドウで検索してインストール
   - または Ctrl+P → `ext install ms-vscode-remote.remote-containers`

2. **WSL** (ms-vscode-remote.remote-wsl)
   - オプション: WSL2 環境との直接統合

#### ステップ3: リポジトリのクローンと VS Code で開く

PowerShell で実行:

```powershell
# WSL2 Ubuntu のホームディレクトリにクローン（推奨）
# WSL2 ターミナルで以下を実行
cd ~
git clone https://github.com/sun-flat-yamada/learn-structured-programming.git
cd learn-structured-programming

# VS Code で開く（WSL2 環境から）
code .
```

#### ステップ4: DevContainer を開く

1. VS Code の左下に **「WSL: Ubuntu」** が表示されていることを確認
2. コマンドパレット（`Ctrl+Shift+P`）を開く
3. **「Dev Containers: Reopen in Container」** を検索して実行
4. コンテナのビルドが開始（初回は3-5分）
5. ビルド完了後、VS Code はコンテナ内で動作

#### ステップ5: DevContainer での確認

```bash
# コンテナ内のターミナルで実行
dotnet --version   # .NET 9.0 が表示される
docker --version   # Docker バージョン確認
```

### 4️⃣ Docker サービスの自動起動設定

WSL2 Ubuntu 起動時に Docker を自動的に開始するように設定します。

#### ステップ1: systemd の有効化（Ubuntu 20.04以降）

WSL2 ターミナルで実行:

```bash
# /etc/wsl.conf を編集
sudo nano /etc/wsl.conf
```

以下の内容を追加 (Ubuntu24.04以降であれば最初から反映されており対応不要なはずです)。

```ini
[boot]
systemd=true

[interop]
appendWindowsPath=true
```

**Ctrl+O** → **Enter** で保存、**Ctrl+X** で終了

#### ステップ2: Docker サービスの自動起動設定

```bash
# Docker サービスを systemd で自動起動
sudo systemctl enable docker

# サービスの確認
sudo systemctl status docker

# 起動状態の確認
docker ps
```

#### ステップ3: WSL2 の再起動と確認

PowerShell（管理者）で実行:

```powershell
# WSL2 を再起動
wsl --shutdown

# Ubuntu を再度起動
wsl
```

WSL2 Ubuntu 再起動後、コンテナ内で確認:

```bash
# Docker が自動起動しているか確認
docker ps

# または
docker run --rm hello-world
```

#### ステップ4: パスワードなしで Docker 操作（オプション）

`sudo systemctl start docker` を実行時にパスワード要求を避けるために:

```bash
# sudoers ファイル を編集（安全に）
sudo visudo

# 以下の行を末尾に追加
username ALL=(ALL) NOPASSWD: /usr/sbin/service docker *
username ALL=(ALL) NOPASSWD: /usr/sbin/systemctl *

# 例: ユーザー名が 'yamada' の場合
yamada ALL=(ALL) NOPASSWD: /usr/sbin/service docker *
yamada ALL=(ALL) NOPASSWD: /usr/sbin/systemctl *
```

Ctrl+O → Enter で保存、Ctrl+X で終了

### 5️⃣ トラブルシューティング

#### Docker デーモンが起動しない場合

```bash
# Docker サービスの状態確認
sudo systemctl status docker

# ログの確認
sudo journalctl -u docker -n 50

# サービスの再起動
sudo systemctl restart docker
```

#### WSL2 と Docker のパフォーマンス最適化

`%USERPROFILE%\.wslconfig` を作成（Windows）:

```ini
[wsl2]
memory=8GB
processors=8
swap=4GB
localhostForwarding=true

[interop]
appendWindowsPath=true
```

#### Docker コマンドが見つからない

```bash
# Docker パスの確認
which docker

# インストール状態の確認
docker --version
```

#### Docker グループのパーミッション確認

```bash
# ユーザーが docker グループに属しているか確認
id $USER
```

### 📝 環境構築完了チェックリスト

DevContainer 環境が完全にセットアップできたか確認:

- [ ] WSL2 が有効化されている
- [ ] Ubuntu 22.04 LTS 以降がインストールされている
- [ ] Docker が WSL2 で実行可能（`docker --version`）
- [ ] ユーザーが docker グループに属している
- [ ] Ubuntu 起動時に Docker が自動起動する
- [ ] VS Code Dev Containers 拡張機能がインストール済み
- [ ] リポジトリを DevContainer で開くことができる

---

## 🏗️ プロジェクト構造

```text
learn-structured-programming/
├── .devcontainer/
│   └── devcontainer.json                    # DevContainer設定
├── README.md                                # このファイル
├── LICENSE                                  # ライセンス
└── src/                                     # 講座用ソースコード
    └── (あなたが自由に構成する場所)
## 🛠️ 開発環境仕様

### イメージ & ランタイム

| 項目                | 仕様                               |
| ------------------- | ---------------------------------- |
| **Base Image**      | `mcr.microsoft.com/dotnet/sdk:9.0` |
| **.NET バージョン** | 9.0                                |
| **OS**              | Linux (Debian 12)                  |
| **C# バージョン**   | 13.0                               |

### リソース割当

| リソース   | 設定値 |
| ---------- | ------ |
| **CPU**    | 4コア  |
| **メモリ** | 4GB    |

### リソース割当の調整方法

DevContainer のリソース（CPU・メモリ）を調整する必要がある場合、以下の方法で変更できます。

#### 🔧 方法1: DevContainer 設定ファイルで調整（推奨）

`.devcontainer/devcontainer.json` の `runArgs` パラメータを編集します:

```jsonc
{
  "runArgs": ["--cpus=4", "--memory=4gb"],
  // 他の設定...
}
```

変更後、DevContainer を再ビルドしてください:

- VS Code コマンドパレット (`Ctrl+Shift+P`) で `Dev Containers: Rebuild Container` を実行

#### 📊 リソース割当のガイドライン

| CPU   | メモリ  | 推奨用途                 |
| ----- | ------- | ------------------------ |
| 1-2   | 1-2GB   | 軽量な学習・開発         |
| **4** | **4GB** | **推奨（デフォルト）**   |
| 6-8   | 6-8GB   | ビルド・テスト頻繁       |
| 8+    | 8GB+    | 複数プロジェクト同時開発 |

#### ⚠️ 注意事項

- **ホストマシンのリソース確認**: DevContainer に割り当てたリソースはホストから確保されます
- **メモリ不足エラーが発生した場合**:

```bash
# コンテナ内から確認
free -h           # メモリ使用状況
docker stats      # リアルタイム監視
```

- **変更の反映**:
  - 設定ファイル編集後は**必ず DevContainer を再ビルド**してください
  - 単なるコンテナ再起動では古い設定で実行されます

#### 🔍 現在のリソース設定確認

コンテナ内で以下のコマンドで確認できます:

```bash
# CPU 情報
nproc                  # コア数表示
cat /proc/cpuinfo      # 詳細情報

# メモリ情報
free -h                # メモリ使用状況
cat /proc/meminfo      # メモリ詳細

# Docker 側から確認
docker stats [container_id]
```

### インストール済み機能

| 機能                 | 説明                     |
| -------------------- | ------------------------ |
| **Docker-in-Docker** | コンテナ内でのDocker操作 |
| **Git**              | バージョン管理統合       |
| **GitHub CLI**       | GH コマンドラインツール  |

## 📦 VS Code 拡張機能

### C# & .NET 開発

| 拡張機能                                                | 機能                         |
| ------------------------------------------------------- | ---------------------------- |
| **C#** (ms-dotnettools.csharp)                          | 言語サポート、インテリセンス |
| **C# Dev Kit** (ms-dotnettools.csdevkit)                | プロジェクト管理、テスト統合 |
| **.NET Runtime** (ms-dotnettools.vscode-dotnet-runtime) | ランタイム管理               |
| **Test Explorer** (ms-dotnettools.test-explorer)        | ユニットテスト実行           |

### コード品質

| 拡張機能                                     | 機能           |
| -------------------------------------------- | -------------- |
| **SonarLint** (sonarsource.sonarlint-vscode) | コード品質分析 |
| **EditorConfig** (EditorConfig.EditorConfig) | スタイル統一   |

### 開発補助

| 拡張機能                                      | 機能                     |
| --------------------------------------------- | ------------------------ |
| **GitLens** (eamodio.gitlens)                 | Git情報表示              |
| **GitHub Copilot** (GitHub.copilot)           | AI コード補完            |
| **GitHub Copilot Chat** (GitHub.copilot-chat) | チャットインターフェース |

### その他

| 拡張機能                                 | 機能             |
| ---------------------------------------- | ---------------- |
| **Docker** (ms-azuretools.vscode-docker) | Docker操作       |
| **PowerShell** (ms-vscode.powershell)    | スクリプティング |
| **C/C++** (ms-vscode.cpptools)           | C/C++ サポート   |

## ⚙️ 開発者設定

### VS Code 設定 (自動適用)

```jsonc
// C# 解析
omnisharp.enableRoslynAnalyzers: true
omnisharp.enableEditorConfigSupport: true

// コード整形
editor.formatOnSave: true
editor.defaultFormatter: ms-dotnettools.csharp

// [csharp] ファイル設定
[csharp]:
  editor.tabSize: 2
  editor.insertSpaces: true
```

### 環境変数 (コンテナ内)

```env
DOTNET_CLI_TELEMETRY_OPTOUT=true       # テレメトリ無効化
DOTNET_SKIP_FIRST_TIME_EXPERIENCE=true # 初回実行スキップ
```

## 🌐 ポート転送

DevContainer 起動時に以下のポートがホストに転送されます:

| ポート   | 用途                         | 状態           |
| -------- | ---------------------------- | -------------- |
| **5000** | HTTP (ASP.NET Core)          | 自動転送・通知 |
| **5001** | HTTPS (ASP.NET Core)         | 自動転送・通知 |
| **8080** | Web アプリケーション         | 自動転送・通知 |
| **8443** | Web アプリケーション (HTTPS) | 自動転送・通知 |

## 📖 使用例

### 新規プロジェクトの作成

```bash
# Console アプリケーション
dotnet new console -n MyApp
cd MyApp
dotnet run

# ASP.NET Core Web API
dotnet new webapi -n MyWebApi
cd MyWebApi
dotnet run
```

### ビルド & テスト

```bash
# ビルド
dotnet build

# テスト実行
dotnet test

# リリースビルド
dotnet publish -c Release
```

### ツール復元

```bash
# ローカルツール復元
dotnet tool restore

# グローバルツール復元
dotnet tool restore --configfile nuget.config
```

## 🔄 DevContainer ライフサイクル

### 初期化フロー

1. **postCreateCommand**: `dotnet restore && dotnet tool restore`
   - 依存パッケージ復元
   - ローカルツール復元

2. **postStartCommand**: `dotnet build --no-restore`
   - コンテナ再開時にビルド実行（失敗時は続行）

## 🤝 貢献ガイドライン

### コミットメッセージ形式

[Gitmoji](https://gitmoji.dev/) を推奨:

```text
📝 READMEを更新
✨ 新機能を追加
🐛 バグを修正
♻️ リファクタリング
🧪 テストを追加
```

### コーディング規約

- **言語**: C# 12.0
- **スタイル**: EditorConfig に準拠
- **分析**: Roslyn Analyzer により自動チェック
- **インデント**: スペース2文字

## 📋 チェックリスト

DevContainer が正常に動作しているか確認:

- [ ] コンテナがビルド・起動できる
- [ ] `dotnet --version` で .NET 9.0 が表示される
- [ ] `dotnet build` が成功する
- [ ] VS Code 拡張機能がすべてインストール済み
- [ ] C# IntelliSense が動作する
- [ ] テスト エクスプローラーが表示される

## 🔗 参考リソース

### 公式ドキュメント

- [.NET 9.0 Documentation](https://learn.microsoft.com/en-us/dotnet/)
- [C# Programming Guide](https://learn.microsoft.com/en-us/dotnet/csharp/)
- [Dev Containers Documentation](https://containers.dev/)

### 学習リソース

- [Microsoft Learn - C#](https://learn.microsoft.com/en-us/dotnet/csharp/)
- [Structured Programming](https://en.wikipedia.org/wiki/Structured_programming)

## 📝 ライセンス

このプロジェクトは [LICENSE](./LICENSE) ファイルに記載されているライセンスの下で公開されています。

## 💡 トラブルシューティング

### コンテナが起動しない場合

```bash
# コンテナを再ビルド
Dev Containers: Rebuild Container
```

### ポート競合が発生した場合

`.devcontainer/devcontainer.json` の `forwardPorts` を編集し、別のポートを指定してください。

### dotnet restore が失敗する場合

```bash
# NuGet キャッシュをクリア
dotnet nuget locals all --clear
dotnet restore
```

## 🙏 サポート

問題や質問がある場合は、GitHub Issues で報告してください。

---

**最終更新**: 2025-12-25 Youhei Yamada
**環境**: .NET 9.0 LTS | VS Code | Docker | DevContainer
