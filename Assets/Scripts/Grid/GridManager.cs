using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int Width = 5;
    public int Height = 5;
    public float cellSize = 100f;
    public float spacing = 5f;
    public int maxBlockValue = 3;

    public RectTransform gridParent;
    public Block BlockPrefab;

    private Block[,] grid;

    void Start()
    {
        grid = new Block[Width, Height];
        InitializeGrid();
    }

    void InitializeGrid()
    {
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                Vector2Int pos = new(x, y);
                Block block = Instantiate(BlockPrefab, gridParent);
                block.Setup(Random.Range(1, maxBlockValue + 1), pos);
                block.GetComponent<RectTransform>().anchoredPosition = GridToUIPosition(pos);
                SetBlock(pos, block);
            }
        }
    }

    public Vector2 GridToUIPosition(Vector2Int pos)
    {
        float totalWidth = Width * cellSize + (Width - 1) * spacing;
        float totalHeight = Height * cellSize + (Height - 1) * spacing;

        float originX = -totalWidth / 2f + cellSize / 2f;
        float originY = -totalHeight / 2f + cellSize / 2f;

        float x = originX + pos.x * (cellSize + spacing);
        float y = originY + pos.y * (cellSize + spacing);

        return new Vector2(x, y);
    }

    public Block GetBlock(Vector2Int pos) => IsInBounds(pos) ? grid[pos.x, pos.y] : null;
    public void SetBlock(Vector2Int pos, Block block) { if (IsInBounds(pos)) grid[pos.x, pos.y] = block; }
    public void ClearBlock(Vector2Int pos) { if (IsInBounds(pos)) grid[pos.x, pos.y] = null; }
    public bool IsInBounds(Vector2Int pos) => pos.x >= 0 && pos.x < Width && pos.y >= 0 && pos.y < Height;

    public void ReplaceBlock(Vector2Int pos)
    {
        var existing = GetBlock(pos);
        if (existing != null)
            Destroy(existing.gameObject);

        Block block = Instantiate(BlockPrefab, gridParent);
        block.Setup(Random.Range(1, maxBlockValue + 1), pos);
        block.GetComponent<RectTransform>().anchoredPosition = GridToUIPosition(pos);
        SetBlock(pos, block);
    }
}