namespace MineSweeper.MineSweeper;

/// <summary>
/// Board 클래스: 지뢰찾기 게임 타일들을 관리합니다.
/// </summary>
public class Board
{
    public int MapX { get; }
    public int MapY { get; }
    public int EntireMineCounts => _mineSpawner.EntireMineCount;

    private readonly Tile[,] _tiles;
    private readonly MineSpawner _mineSpawner;
    
    /// <summary>
    /// Board 생성자: 보드판을 초기화하고, mineSpawner을 통해 지뢰를 생성합니다.
    /// </summary>
    /// <param name="mapX">보드판의 너비</param>
    /// <param name="mapY">보드판의 높이</param>
    /// <param name="mineSpawner">MineSpawner 객체</param>
    public Board(int mapX, int mapY, MineSpawner mineSpawner)
    {
        MapX = mapX;
        MapY = mapY;
        _mineSpawner = mineSpawner;
        
        _tiles = TileSpawner.CreateTiles(mapX, mapY);
        _mineSpawner.PlaceMines(_tiles);
    }

    /// <summary>
    /// GetTile 메서드: 해당 좌표의 타일 객체를 반환합니다.
    /// </summary>
    /// <param name="x">타겟 타일의 x 좌표</param>
    /// <param name="y">타겟 타일의 y 좌표</param>
    /// <returns>해당 좌표의 타일 객체</returns>
    public Tile GetTile(int x, int y)
    {
        return _tiles[x, y];
    }

    /// <summary>
    /// 타겟 타일을 뒤집습니다. 타일이 지뢰인 경우 true를 반환합니다.
    /// </summary>
    /// <param name="x">타겟 타일의 x 좌표</param>
    /// <param name="y">타겟 타일의 y 좌표</param>
    /// <returns>타겟 타일의 지뢰 여부를 리턴합니다. true - 지뢰, false - 지뢰 아님</returns>
    public bool FlipTile(int x, int y)
    {
        var tile = GetTile(x, y);
        
        if (tile.CurrentTileState == TileState.Open) return false;
        
        if (tile.IsMine) return true;

        TraverseEmptyTiles(x, y);
        
        return false;
    }
    
    /// <summary>
    /// 타겟 타일에 깃발을 설치하거나 제거합니다.
    /// </summary>
    /// <param name="x">타겟 타일의 x 좌표</param>
    /// <param name="y">타겟 타일의 y 좌표</param>
    /// <returns>최종 점수 업데이트 값</returns>
    public int RaiseFlagOnTile(int x, int y)
    {
        var tile = GetTile(x, y);
        
        if (tile.CurrentTileState == TileState.Flag) return 0;

        // 깃발을 꽂거나 제거할 때 점수 변동 사항을 처리합니다.
        switch (tile.CurrentTileState)
        {
            case TileState.Close:
                tile.ChangeState(TileState.Flag);
                return tile.IsMine ? 1 : 0;
            
            case TileState.Flag:
                tile.ChangeState(TileState.Close);
                return tile.IsMine ? -1 : 0;
            default:
                return 0;
        }
    }
    
    /// <summary>
    /// 타겟 타일에 인접한 모든 타일들 중 비어있는 타일들을 엽니다.
    /// Open 상태가 아니고 주변에 설치된 지뢰가 없는 타일의 경우만 재귀적으로 동작합니다.
    /// </summary>
    /// <param name="x">타겟 타일의 x 좌표</param>
    /// <param name="y">타겟 타일의 y 좌표</param>
    private void TraverseEmptyTiles(int x, int y)
    {
        var queue = new Queue<Tuple<int, int>>();
        queue.Enqueue(new Tuple<int, int>(x, y));

        while (queue.Count > 0)
        {
            var targetTile = queue.Dequeue();
            var posX = targetTile.Item1;
            var posY = targetTile.Item2;

            var tile = GetTile(posX, posY);

            if (tile.CurrentTileState == TileState.Open) continue; // 타일이 이미 열린 상태라면 넘어갑니다.

            tile.ChangeState(TileState.Open);

            if (tile.NearbyMineCount == 0)
            {
                foreach (var nearby in TraverseNearbyTiles(posX, posY))
                {
                    var nearbyTile = GetTile(nearby.Item1, nearby.Item2);

                    if (nearbyTile.CurrentTileState != TileState.Open)
                    {
                        queue.Enqueue(new Tuple<int, int>(nearby.Item1, nearby.Item2));
                    }
                }
            }
        }
    }

    /// <summary>
    /// 타겟 타일에 인접한 모든 타일들을 반환합니다.
    /// </summary>
    /// <param name="x">타겟 타일의 x 좌표</param>
    /// <param name="y">타겟 타일의 y 좌표</param>
    /// <returns>인접한 모든 타일 (보드판 내부에 위치한 타일만 검출)</returns>
    private List<Tuple<int, int>> TraverseNearbyTiles(int x, int y)
    {
        var targetDir = new[] { (-1, -1), (-1, 0), (-1, 1), (0, -1), (0, 1), (1, -1), (1, 0), (1, 1) };
        var targetTiles = new List<Tuple<int, int>>();

        foreach (var dir in targetDir)
        {
            var targetX = x + dir.Item1;
            var targetY = y + dir.Item2;
            
            if (targetX >= 0 && targetX < MapX && targetY >= 0 && targetY < MapY)
            {
                targetTiles.Add(new Tuple<int, int>(targetX, targetY));
            }
        }

        return targetTiles;
    }
}
