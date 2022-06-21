using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
[CreateAssetMenu(menuName = "CustomTiles/PikeTile")]
public class PikeTile : AnimatedTile
{
    public bool ActivationStarted = false;
    public bool CouldownStarted = false;
    public bool PikeActivated = false;
}
