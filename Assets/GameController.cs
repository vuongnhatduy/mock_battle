using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class GameController : Controller
{
    [SerializeField]
    private Tilemap movementTilemap;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private Vector2 destination;
    private Vector2 lastDestination;

    private delegate void OnSelected();
    private event OnSelected onSelected;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            destination = GetTilePosition();
        }
        if(destination != lastDestination)
        {
            player.transform.position = Vector2.MoveTowards(player.transform.position, destination, Time.deltaTime);
        }
    }

    public Vector2 GetTilePosition()
    {
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int posInt = movementTilemap.WorldToCell(pos);
        if(movementTilemap.HasTile(posInt))
        {
            movementTilemap.SetTileFlags(posInt, TileFlags.None);
            movementTilemap.SetColor(posInt, new Color(1, 0, 0));
            Debug.Log("G");
            return movementTilemap.GetCellCenterWorld(posInt);
        }
        return lastDestination;
    }
}
