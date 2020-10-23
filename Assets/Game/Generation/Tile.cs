﻿using System;
using System.IO;
using UnityEngine;

namespace Game.Generation
{
    [Serializable]
    public class Tile
    {
        public int id = -1;
        public string name;
        public Sprite sprite;
    }

    [Serializable]
    public class Tiles
    {
        public Tile[] tiles;
    }
    
    public static class TileLoader
    {
        public static Tile[] LoadTiles()
        {
            try
            {
                // Load all tiles in tiles.json
                var stream = new StreamReader(Application.dataPath + "/tiles.json");
                var contents = stream.ReadToEnd();
                stream.Close();

                // Create tile objects from all the tiles in the file
                var tiles = JsonUtility.FromJson<Tiles>(contents);
                foreach (var tile in tiles.tiles)
                {
                   tile.sprite = Resources.Load<Sprite>("Tiles/" + tile.name);
                   if (tile.sprite == null)
                       Debug.LogError("[ERROR] Unable to load sprite for tile with ID " + tile.id);
                }
                return tiles.tiles;
            }
            catch (FileNotFoundException)
            {
                Debug.LogError("[ERROR LOADING TILES] tiles.json does not exist in the expected location");
                return null;
            }
        }
    }
}