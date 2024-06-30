namespace Minesweeper;

/// <summary>
/// Program 클래스 : 초기 설정값 (보드판 너비, 높이, 지뢰 수)을 입력 받고 게임을 시작합니다.
/// </summary>
public static class Program
{
    public static void Main()
    {
        // 사용자로부터 초기 설정값을 입력 받습니다.
        InputInitialSetting(out var mapX, out var mapY, out var entireMineCount);

        var mineSpawner = new MineSpawner(mapX, mapY, entireMineCount); // MineSpawner 초기화
        var board = new Board(mapX, mapY, mineSpawner); // Board 초기화
        var uiManager = new UIManager(); // UIController 초기화
        var gameManager = new GameManager(board, uiManager); // GameManager 초기화

        gameManager.Start(); // 게임 시작
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
        Console.Write("# 맵의 X축 값을 입력하세요 : ");
        mapX = int.Parse(Console.ReadLine() ?? string.Empty);
        Console.Write("# 맵의 Y축 값을 입력하세요 : ");
        mapY = int.Parse(Console.ReadLine() ?? string.Empty);
        Console.Write("# 전체 지뢰 수를 입력하세요 : ");
        entireMineCount = int.Parse(Console.ReadLine() ?? string.Empty);
    }
}