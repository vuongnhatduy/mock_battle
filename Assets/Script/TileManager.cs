using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    #region Field
    [SerializeField] private Tilemap map;
    [SerializeField] private Grid grid;
    [SerializeField] private TileBase[,] blocks;
    #endregion
    #region Behavior
    public void GetTileBlocks()
    {
        BoundsInt bounds = map.cellBounds;
        ushort length = (ushort)Mathf.Sqrt(GetLenght(bounds));
        Test(length, "length");
        blocks = new TileBase[length, length];
        ushort index = 0;
        for (sbyte row = 0; row < length; row++)
        {
            for (sbyte column = 0; column < length; column++)
            {
                if(map.GetTilesBlock(bounds)[index] != null)
                {
                    blocks[row, column] = map.GetTilesBlock(bounds)[index];
                    index++;
                }
            }
        }
    }
    private ushort GetLenght(BoundsInt bounds)
    {
        ushort count = 0;
        foreach (TileBase tile in map.GetTilesBlock(bounds))
        {
            if (tile != null)
            {
                count++;
            }
        }
        return count;
    }
    private void Test(ushort paragramValue, string paragramName)
    {
        if(Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            Debug.Log(string.Format("{1}: {0}", paragramValue, paragramName));
        }
    }
    public void GetTileOnClick()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector3Int mousePos = new Vector3Int ((int)Input.mousePosition.x, (int)Input.mousePosition.y, (int)Input.mousePosition.z);
            Debug.Log(string.Format("mouse to tilemap position: {0}",grid.GetCellCenterWorld(mousePos)));
        }
    }
    #endregion
}
