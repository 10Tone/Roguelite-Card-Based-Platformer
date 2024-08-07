using System.Collections.Generic;
using Game;
using Game.WorldBuilding;
using Godot;

namespace AutoLoads;

public partial class GlobalVariables: Node
{
    public BuildItemResource SelectedBuildItem { get; set; }
    public GameStates GameState { get; set; }
    public Vector2I WorldGridSize { get; set; }
    
    private List<BuildItemResource> _buildItemResources;
    
    public List<BuildItemResource> BuildItemResources
    {
        get
        {
            if (_buildItemResources == null)
            {
                _buildItemResources = new List<BuildItemResource>();
                string dirPath = "res://Source/Game/World/WorldBuilding/BuildItemResources/";
                LoadResourcesFromDirectory(dirPath);
            }
            return _buildItemResources;
        }
    }

    private void LoadResourcesFromDirectory(string dirPath)
    {
        var dir = DirAccess.Open(dirPath);

        if(dir == null)
        {GD.PushWarning("BuildItemResources folder is empty or not found");}
        
        else
        {
            dir.ListDirBegin();
            string fileName = dir.GetNext();
            while (fileName != "")
            {
                if (fileName != "." && fileName != "..")
                {
                    string fullPath = dirPath + fileName;
                    if (dir.DirExists(fullPath))
                    {
                        LoadResourcesFromDirectory(fullPath + "/");
                    }
                    else if (fileName.EndsWith(".tres"))
                    {
                        var resource = ResourceLoader.Load<BuildItemResource>(fullPath);
                        if (resource != null)
                        {
                            _buildItemResources.Add(resource);
                            // GD.Print($"Loaded BuildItemResource: {resource.Name}");
                        }
                    }
                }
                fileName = dir.GetNext();
            }
            dir.ListDirEnd();
        }
    }
}