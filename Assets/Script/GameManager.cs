using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TileManager))]
public class GameManager : MonoBehaviour
{
    [SerializeField] private TileManager tileManager;
    #region Singleton
    public static GameManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    #endregion
    #region Properties
    public TileManager TileManager { get => tileManager; }
    #endregion
    // Start is called before the first frame update
    private void Start()
    {
        tileManager = GetComponent<TileManager>();
        tileManager.GetTileBlocks();
    }

    // Update is called once per frame
    private void Update()
    {
        tileManager.GetTileOnClick();
    }
}
