namespace MineSweeper.MineSweeper;

/// <summary>
/// Progress 열거형: 게임의 현재 상태를 나타냅니다.
/// </summary>
public enum Progress
{
    /// <summary>
    /// OnProgress: 진행 중인 상태입니다.
    /// </summary>
    OnProgress,

    /// <summary>
    /// GameOver: 게임이 오버된 상태입니다.
    /// </summary>
    GameOver,

    /// <summary>
    /// Victory: 승리한 상태입니다.
    /// </summary>
    Victory
}

/// <summary>
/// Game 클래스 : 사용자의 입력에 따른 주요 로직을 처리합니다.
/// </summary>
/// <param name="board">Board 객체</param>
/// <param name="uiManager">UIController 객체</param>
public class GameManager(Board board, IDrawable uiManager)
{
    private bool _onCheat;
    private int _score;

    /// <summary>
    /// 게임을 시작하고, 게임 진행 상태를 지속적으로 업데이트합니다.
    /// </summary>
    public void Start()
    {
        _onCheat = false;
        _score = 0;
        
        var progress = Progress.OnProgress;

        while (progress == Progress.OnProgress)
        {
            uiManager.Draw(board, _onCheat);

            InputCommand();

            progress = CheckCurrentProgressResult();
        }

        uiManager.DrawResult(progress);
    }

    /// <summary>
    /// 사용자로부터 입력을 받아 로직을 처리합니다.
    /// </summary>
    private void InputCommand()
    {
        var input = Console.ReadLine();
        var command = input?.Split(" ");

        switch (command?[0])
        {
            case "F":
                if (_onCheat) _onCheat = false;
                if (board.FlipTile(int.Parse(command[1]) - 1, int.Parse(command[2]) - 1)) _score = -1;
                break;
            case "R":
                if (_onCheat) _onCheat = false;
                _score += board.RaiseFlagOnTile(int.Parse(command[1]) - 1, int.Parse(command[2]) - 1);
                break;
            case "ShowMine":
                if (!_onCheat) _onCheat = true;
                break;
            case "HideMine":
                if (_onCheat) _onCheat = false;
                break;
        }
    }

    /// <summary>
    /// 게임의 현재 상태를 업데이트합니다.
    /// </summary>
    /// <returns>업데이트된 게임 상태</returns>
    private Progress CheckCurrentProgressResult()
    {
        if (_score == -1) return Progress.GameOver;
        return _score >= board.EntireMineCounts ? Progress.Victory : Progress.OnProgress;
    }
} 