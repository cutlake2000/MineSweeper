namespace MineSweeper.MineSweeper;

/// <summary>
/// UIController 클래스: 사용자 인터페이스를 관리하고 게임 상태에 따라 적절한 출력을 제공합니다.
/// </summary>
public class UIManager : IDrawable
{
    /// <summary>
    /// 보드판과 게임 정보 UI를 콘솔에 출력합니다.
    /// </summary>
    /// <param name="board">보드 객체</param>
    /// <param name="onCheat">치트 활성화 여부</param>
    public void Draw(Board board, bool onCheat)
    {
        Console.Clear();
        DrawGameProgressInfo(board, onCheat);
        DrawBoardTopUi(board);

        for (var i = 0; i < board.MapY; i++)
        {
            DrawBoardSideUi(i + 1);

            for (var j = 0; j < board.MapX; j++)
            {
                var tile = board.GetTile(j, i);
                DrawEachTile(tile, onCheat);
            }

            Console.WriteLine();
        }
        
        Console.WriteLine();

        DrawBottomUi();
    }

    /// <summary>
    /// 보드판 사이즈, 지뢰 수, 치트 활성화 여부를 출력합니다.
    /// </summary>
    /// <param name="board">보드 객체</param>
    /// <param name="onCheat">치트 활성화 여부</param>
    private void DrawGameProgressInfo(Board board, bool onCheat)
    {
        Console.Write($"Size {board.MapX} x {board.MapY}");
        if (onCheat)
        {
            Console.Write(" | 치트 모드 활성화");
        }
        
        Console.WriteLine();
        Console.WriteLine($"지뢰 {board.EntireMineCounts}개");
    }

    /// <summary>
    /// 보드판 상단 열 UI를 출력합니다.
    /// </summary>
    /// <param name="board">보드 객체</param>
    private void DrawBoardTopUi(Board board)
    {
        Console.Write(" Y\\X ");
        for (var j = 1; j <= board.MapX; j++)
        {
            Console.Write(j < 10 ? $"  {j}. " : $" {j}. ");
        }
        Console.WriteLine();
    }

    /// <summary>
    /// 보드판 좌측 행 UI를 출력합니다.
    /// </summary>
    /// <param name="i">행 번호</param>
    private void DrawBoardSideUi(int i)
    {
        Console.Write(i < 10 ? $"  {i}. " : $" {i}. ");
    }

    /// <summary>
    /// 타일의 State에 맞는 UI를 출력합니다.
    /// </summary>
    /// <param name="tile">타겟 타일 객체</param>
    /// <param name="onCheat">치트 활성화 여부</param>
    private void DrawEachTile(Tile tile, bool onCheat)
    {
        if (onCheat)
        {
            Console.Write(tile.IsMine ? "  *  " : tile.NearbyMineCount > 0 ? $"  {tile.NearbyMineCount}  " : "     ");
        }
        else
        {
            switch (tile.CurrentTileState)
            {
                case TileState.Close:
                    Console.Write("[   ]");
                    break;
                case TileState.Flag:
                    Console.Write("[ ? ]");
                    break;
                case TileState.Open:
                    if (tile.IsMine)
                        Console.Write("  @  ");
                    else
                        Console.Write(tile.NearbyMineCount > 0 ? $"  {tile.NearbyMineCount}  " : "     ");
                    break;
            }
        }
    }
    
    /// <summary>
    /// 하단의 가이드 메시지 UI를 출력합니다.
    /// </summary>
    private void DrawBottomUi()
    {
        Console.WriteLine("# 실행할 명령과 좌표를 입력해주세요. (F - 타일 뒤집기 | R - 깃발 세우기)");
        Console.WriteLine("ex) F 3 5 ");
        Console.Write("> ");
    }

    /// <summary>
    /// 게임 결과를 출력합니다.
    /// </summary>
    /// <param name="progress">게임 진행 상태</param>
    public void DrawResult(Progress progress)
    {
        switch (progress)
        {
            case Progress.GameOver:
                Console.WriteLine("지뢰가 터졌습니다. GameOver.");
                break;
            case Progress.Victory:
                Console.WriteLine("모든 지뢰를 찾았습니다. Victory!!");
                break;
        }
    }
}