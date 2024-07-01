namespace MineSweeper.MineSweeper;

/// <summary>
/// IDrawable 인터페이스: UI 출력을 위한 메서드를 정의합니다.
/// </summary>
public interface IDrawable
{
    void Draw(Board board, bool showMines); // 게임 보드를 그리는 메서드
    void DrawResult(Progress progress); // 게임 결과를 그리는 메서드
    void DrawCommandError(); // 잘못된 명령 에러를 그리는 메서드
    void DrawOutOfRangeValue(int min, int max); // 잘못된 범위의 초기값 입력 에러를 그리는 메서드
    void DrawWrongValue(); // 잘못된 초기값 입력 에러를 그리는 메서드
    void DrawNewGame(); // 게임 재시작 여부를 그리는 메서드
}