namespace MineSweeper.MineSweeper;

/// <summary>
/// MineSpawner 클래스 : 지뢰를 설치하고, 지뢰 타일 주변의 타일들의 카운트를 증가시키는 메서드를 제공합니다.
/// </summary>
public class MineSpawner
{
    public int EntireMineCount { get; }
    
    private readonly int _mapX;
    private readonly int _mapY;
    
    private readonly Random _random;
    private readonly HashSet<Tuple<int, int>> _mineLocations;
    
    /// <summary>
    /// MineSpawner 생성자 : 지뢰찾기 보드판의 가로 세로 길이에 맞춰 지뢰를 랜덤한 위치에 설치합니다.
    /// </summary>
    /// <param name="mapX">보드판의 너비</param>
    /// <param name="mapY">보드판의 높이</param>
    /// <param name="entireMineCount">총 지뢰 수</param>
    public MineSpawner(int mapX, int mapY, int entireMineCount)
    {
        _mapX = mapX;
        _mapY = mapY;
        EntireMineCount = entireMineCount;
        
        _random = new Random();
        _mineLocations = new HashSet<Tuple<int, int>>();
        
        InitializeMines(EntireMineCount);
    }

    /// <summary>
    /// InitializeMines 메서드: 지뢰를 설치할 랜덤 좌표를 생성하고 이를 저장합니다.
    /// </summary>
    /// <param name="entireMineCount">총 지뢰 수</param>
    private void InitializeMines(int entireMineCount)
    {
        var minesPlaced = 0;
        
        while (minesPlaced < entireMineCount)
        {
            var posX = _random.Next(_mapX);
            var posY = _random.Next(_mapY);

            var targetPos = new Tuple<int, int>(posX, posY);

            if (_mineLocations.Add(targetPos))
            {
                minesPlaced++;
            }
        }
    }

    /// <summary>
    /// PlaceMines 메서드: 타일에 지뢰를 심고, 해당 타일에 인접하고 있는 타일들의 '근처 지뢰 수'를 증가시킵니다.
    /// </summary>
    /// <param name="tile">타일 배열</param>
    public void PlaceMines(Tile[,] tile)
    {
        foreach (var targetMine in _mineLocations)
        {
            var targetPosX = targetMine.Item1;
            var targetPosY = targetMine.Item2;

            tile[targetPosX, targetPosY].PlaceMine();
            
            // 그리드를 벗어나지 않는 선에서 인접하고 있는 타일들의 '근처 지뢰 수'를 증가시킵니다.
            for (var dx = -1; dx <= 1; dx++)
            {
                for (var dy = -1; dy <= 1; dy++)
                {
                    var nx = targetPosX + dx;
                    var ny = targetPosY + dy;
                
                    if (nx >= 0 && nx < _mapX && ny >= 0 && ny < _mapY)
                    {
                        tile[nx, ny].IncreaseNearbyMineCount();
                    }
                }
            }
        }
    }
}