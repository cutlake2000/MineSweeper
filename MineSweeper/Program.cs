﻿using System.Data;

namespace MineSweeper.MineSweeper;

/// <summary>
/// Program 클래스 : 초기 설정값 (보드판 너비, 높이, 지뢰 수)을 입력 받고 게임을 시작합니다.
/// </summary>
public static class Program
{
    private static readonly UIManager UiManager = new();
    
    public static void Main()
    {
        
        do
        {
            // 사용자로부터 초기 설정값을 입력 받습니다.
            InputInitialSetting(out var mapX, out var mapY, out var entireMineCount);

            var mineSpawner = new MineSpawner(mapX, mapY, entireMineCount); // MineSpawner 초기화
            var board = new Board(mapX, mapY, mineSpawner); // Board 초기화
            var gameManager = new GameManager(board, UiManager); // GameManager 초기화

            gameManager.Start(); // 게임 시작

        } while (RestartGame());
    }

    /// <summary>
    /// 사용자로부터 보드판의 크기와 지뢰 수를 입력받습니다.
    /// </summary>
    /// <param name="mapX">보드판의 너비</param>
    /// <param name="mapY">보드판의 높이</param>
    /// <param name="entireMineCount">심어진 지뢰의 개수</param>
    private static void InputInitialSetting(out int mapX, out int mapY, out int entireMineCount)
    {
        Console.Clear();
        Console.WriteLine("지뢰찾기 v1.0.0");
        mapX = ReadIntFromConsole("# 맵의 X축 값을 입력하세요 : ");
        mapY = ReadIntFromConsole("# 맵의 Y축 값을 입력하세요 : ");
        entireMineCount = ReadIntFromConsole("# 전체 지뢰 수를 입력하세요 : ", 1, mapX * mapX);
    }

    /// <summary>
    /// 사용자로부터 정수를 입력 받습니다. 정수로 파싱이 불가능하거나, 0보다 작다면 다시 입력 받습니다.
    /// </summary>
    /// <param name="message">출력 메시지</param>
    /// <returns>입력 받은 정수 값</returns>
    private static int ReadIntFromConsole(string message)
    {
        int output;
        bool isValid;

        do
        {
            Console.Write(message);
            
            var input = Console.ReadLine();
            isValid = int.TryParse(input, out output) && output > 0;

            if (isValid) continue;
            
            UiManager.DrawWrongValue();
        } while (!isValid); // 정수로 파싱이 가능하고, 0보다 큰 경우에만 루프를 종료합니다.

        return output;
    }

    /// <summary>
    /// 사용자로부터 정수를 입력 받습니다. 정수로 파싱이 불가능하거나, 범위 밖의 정수라면 다시 입력 받습니다.
    /// </summary>
    /// <param name="message">출력 메시지</param>
    /// <param name="min">최소</param>
    /// <param name="max">최대</param>
    /// <returns>입력 받은 정수 값</returns>
    private static int ReadIntFromConsole(string message, int min, int max)
    {
        int output;
        bool isValid;

        do
        {
            Console.Write(message);
            var input = Console.ReadLine();
            isValid = int.TryParse(input, out output) && min < output && output < max;
            
            if (isValid) continue;

            UiManager.DrawOutOfRangeValue(min, max);
        } while (!isValid);  // 정수로 파싱이 가능하고, min 이상 max 이하의 경우에만 루프를 종료합니다.

        return output;
    }
    
    /// <summary>
    /// 사용자로부터 새 게임을 진행할지 여부를 입력받습니다.
    /// </summary>
    /// <returns>새 게임 진행 여부</returns>
    private static bool RestartGame()
    {
        UiManager.DrawNewGame();
        
        var command = Console.ReadLine();

        return command switch
        {
            "Yes" => true,
            "No" => false,
            _ => false
        };
    }
}