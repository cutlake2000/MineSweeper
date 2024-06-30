namespace MineSweeper.MineSweeper;

/// <summary>
/// TileSpawner 클래스 : Tile 타입의 2차원 배열을 생성하고 반환하는 메서드를 제공합니다.
/// </summary>
public static class TileSpawner
{
    /// <summary>
    /// CreateTiles 메서드: 맵의 가로 세로 사이즈를 입력 받아 Tile 타입의 2차원 배열을 생성합니다.
    /// </summary>
    /// <param name="mapX">보드판의 너비</param>
    /// <param name="mapY">보드판의 높이</param>
    public static Tile[,] CreateTiles(int mapX, int mapY)
    {
        var tiles = new Tile[mapX, mapY];
        for (var i = 0; i < mapX; i++)
        {
            for (var j = 0; j < mapY; j++)
            {
                tiles[i, j] = new Tile();
            }
        }
        
        return tiles;
    }
}