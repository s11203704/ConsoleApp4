namespace ConsoleApp4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            OXGameUI gameUI = new OXGameUI();
            gameUI.StartGame();
        }
    }
    // 定義遊戲引擎類別
    public class OXGameEngine
    {
        private char[,] gameMarkers;// 遊戲棋盤
        //初始化遊戲棋盤並重置遊戲
        public OXGameEngine()
        {
            gameMarkers = new char[3, 3];
            ResetGame();
        }
        // 設置玩家標記到指定位置
        public void SetMarker(int x, int y, char player)
        {
            // 驗證移動是否合法
            if (IsValidMove(x, y))
            {
                gameMarkers[x, y] = player;
            }
            else
            {
                throw new ArgumentException("Invalid move!");
            }
        }
        // 重置遊戲棋盤
        public void ResetGame()
        {
            // 將遊戲棋盤中所有格子重置為空格
            gameMarkers = new char[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    gameMarkers[i, j] = ' ';
                }
            }
        }
        // 檢查是否有玩家獲勝
        public char IsWinner()
        {
            // 檢查橫向
            for (int i = 0; i < 3; i++)
            {
                if (gameMarkers[i, 0] != ' ' && gameMarkers[i, 0] == gameMarkers[i, 1] && gameMarkers[i, 1] == gameMarkers[i, 2])
                {
                    return gameMarkers[i, 0];
                }
            }
            // 檢查縱向
            for (int j = 0; j < 3; j++)
            {
                if (gameMarkers[0, j] != ' ' && gameMarkers[0, j] == gameMarkers[1, j] && gameMarkers[1, j] == gameMarkers[2, j])
                {
                    return gameMarkers[0, j];
                }
            }
            // 檢查對角線
            if (gameMarkers[0, 0] != ' ' && gameMarkers[0, 0] == gameMarkers[1, 1] && gameMarkers[1, 1] == gameMarkers[2, 2])
            {
                return gameMarkers[0, 0];
            }

            if (gameMarkers[0, 2] != ' ' && gameMarkers[0, 2] == gameMarkers[1, 1] && gameMarkers[1, 1] == gameMarkers[2, 0])
            {
                return gameMarkers[0, 2];
            }
            return ' '; // 沒有贏家出現
        }
        // 驗證化的OX是否合法
        private bool IsValidMove(int x, int y)
        {
            if (x < 0 || x >= 3 || y < 0 || y >= 3)
            {
                return false;
            }

            if (gameMarkers[x, y] != ' ')
            {
                return false;
            }
            return true;
        }
        // 獲取指定位置的標記
        public char GetMarker(int x, int y)
        {
            return gameMarkers[x, y];
        }

    }
    // 定義遊戲使用者介面類別
    public class OXGameUI
    {
        private OXGameEngine gameEngine;

        public OXGameUI()
        {
            gameEngine = new OXGameEngine();
        }
        // 開始遊戲
        public void StartGame()
        {
            char currentPlayer = 'O'; // 遊戲開始時，第一個玩家為 'O'
            while (true)
            {
                DisplayGameBoard();// 顯示遊戲棋盤
                // 請求玩家輸入位置
                Console.Write($"輸入玩家位置 (輸入0-2，以空格分隔，玩家 {currentPlayer}): ");
                string input = Console.ReadLine();
                string[] coordinates = input.Split(' ');
                // 解析輸入的座標
                if (coordinates.Length != 2 || !int.TryParse(coordinates[0], out int x) || !int.TryParse(coordinates[1], out int y))
                {
                    Console.WriteLine("輸入格式錯誤，請重新輸入。");
                    continue;
                }

                try
                {
                    // 設置玩家標記到指定位置
                    gameEngine.SetMarker(x, y, currentPlayer);
                    char winner = gameEngine.IsWinner();// 檢查是否有玩家獲勝
                    if (winner != ' ')
                    {
                        DisplayGameBoard();// 顯示最終遊戲棋盤
                        Console.WriteLine($"贏家為: {winner}");// 顯示贏家
                        break;
                    }
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message); // 顯示錯誤訊息
                    continue;// 繼續迴圈，等待有效輸入
                }

                // 切換到下一個玩家
                currentPlayer = (currentPlayer == 'X') ? 'O' : 'X';
            }
        }
        // 顯示遊戲棋盤
        private void DisplayGameBoard()
        {
            for (int i = 0; i < 5; i++)
            {
                if (i % 2 == 0)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (j % 2 == 0)
                        {
                            Console.Write($"{gameEngine.GetMarker(i / 2, j / 2)}");
                        }
                        else
                        {
                            Console.Write("|");// 列分隔線
                        }
                    }
                }
                else
                {
                    Console.Write("-----");// 行分隔線
                }
                Console.WriteLine();
            }
        }
    }
}