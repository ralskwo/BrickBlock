// BlockFallHandler.cs
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlockFallHandler : MonoBehaviour
{
    [SerializeField] private float fallSpeed = 800f; // 인스펙터에서 조정 가능

    public void HandleFall()
    {
        StartCoroutine(HandleFallCoroutine());
    }

    private IEnumerator HandleFallCoroutine()
    {
        var grid = GameManager.Instance.GridManager;
        List<Coroutine> fallCoroutines = new();

        // 1. 기존 블록 낙하
        for (int x = 0; x < grid.Width; x++)
        {
            int emptyCount = 0;

            for (int y = 0; y < grid.Height; y++)
            {
                Vector2Int pos = new(x, y);
                var block = grid.GetBlock(pos);

                if (block == null)
                {
                    emptyCount++;
                }
                else if (emptyCount > 0)
                {
                    Vector2Int newPos = new(x, y - emptyCount);
                    grid.SetBlock(newPos, block);
                    grid.ClearBlock(pos);

                    block.SetGridPosition(newPos);
                    Coroutine anim = StartCoroutine(block.AnimateMoveAtSpeed(grid.GridToUIPosition(newPos), fallSpeed));
                    fallCoroutines.Add(anim);
                }
            }
        }

        foreach (var coroutine in fallCoroutines)
            yield return coroutine;

        // 2. 새로운 블록 낙하 (윗칸일수록 더 아래에서 생성 — 반전된 위치)
        List<Coroutine> newFallCoroutines = new();

        for (int x = 0; x < grid.Width; x++)
        {
            List<Vector2Int> spawnTargets = new();

            for (int y = 0; y < grid.Height; y++)
            {
                Vector2Int pos = new(x, y);
                if (grid.GetBlock(pos) == null)
                {
                    spawnTargets.Add(pos);
                }
            }

            int spawnHeightStart = grid.Height;
            for (int i = 0; i < spawnTargets.Count; i++)
            {
                Vector2Int finalPos = spawnTargets[i];
                int spawnOffset = i;
                Vector2Int spawnPos = new(x, spawnHeightStart + spawnOffset);

                Block newBlock = Instantiate(grid.BlockPrefab, grid.gridParent);
                int randomValue = Random.Range(1, grid.maxBlockValue + 1);
                newBlock.Setup(randomValue, finalPos);

                RectTransform rt = newBlock.GetComponent<RectTransform>();
                rt.anchoredPosition = grid.GridToUIPosition(spawnPos);
                grid.SetBlock(finalPos, newBlock);

                Coroutine anim = StartCoroutine(newBlock.AnimateMoveAtSpeed(grid.GridToUIPosition(finalPos), fallSpeed));
                newFallCoroutines.Add(anim);
            }
        }

        foreach (var coroutine in newFallCoroutines)
            yield return coroutine;
    }
}
