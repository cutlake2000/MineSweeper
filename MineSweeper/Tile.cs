namespace MineSweeper.MineSweeper;

/// <summary>
/// State 열거형: 타일의 현재 상태를 나타냅니다.
/// </summary>
public enum TileState
{
    /// <summary>
    /// Close: 닫힌 상태입니다.
    /// </summary>
    Close,

    /// <summary>
    /// Flag: 타일에 깃발에 꽂힌 상태입니다.
    /// </summary>
    Flag,

    /// <summary>
    /// Open: 타일이 뒤집힌 (열린) 상태입니다.
    /// </summary>
    Open
}

/// <summary>
/// Tile 클래스: 타일에 지뢰를 설치하거나, 주변에 위치한 지뢰 수를 증가시키고, 상태를 변경하는 메서드를 제공합니다.
/// </summary>
public class Tile()
{
    public bool IsMine { get; private set; } = false; // 지뢰 보유 여부
    public int NearbyMineCount { get; private set; } = 0; // 주변에 위치한 지뢰 수
    public TileState CurrentTileState { get; private set; } = TileState.Close; // 타일의 현재 상태

    /// <summary>
    /// 현재 타일에 지뢰를 심습니다.
    /// </summary>
    public void PlaceMine()
    {
        IsMine = true;
    }
    
    /// <summary>
    /// 주변에 위치한 지뢰 수를 증가시킵니다.
    /// </summary>
    public void IncreaseNearbyMineCount()
    {
        NearbyMineCount++;
    }

    /// <summary>
    /// 타일의 상태를 변경합니다.
    /// </summary>
    /// <param name="newTileState">변경하고자 하는 타일 상태</param>
    public void ChangeState(TileState newTileState)
    {
        CurrentTileState = newTileState;
    }
}
