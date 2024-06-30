namespace Minesweeper;

/// <summary>
/// IDrawable 인터페이스: UI 출력을 위한 메서드를 정의합니다.
/// </summary>
public interface IDrawable
{
    void Draw(Board board, bool showMines);  // 게임 보드를 그리는 메서드
    void DrawResult(Progress progress);     // 게임 결과를 그리는 메서드
}