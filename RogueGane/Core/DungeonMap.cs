using RLNET;
using RogueSharp;
using System;

namespace RogueGame.Core
{
    public class DungeonMap : Map
    {
        // the Draw method will be called each time the map is updated
        // it will render all of the symbols/colors for each cell to the map sub console

        public void Draw(RLConsole mapConsole)
        {
            mapConsole.Clear();
            foreach(Cell cell in GetAllCells() )
            {
                SetConsoleSybmolForCell(mapConsole, cell);
            }
        }

        private void SetConsoleSybmolForCell(RLConsole mapConsole, Cell cell)
        {
            if(!cell.IsExplored)
            {
                return;
            }

            // when a cell is currently in the field-of-view it should be drawn with lighter colors
            if (IsInFov(cell.X, cell.Y))
            {
                if(cell.IsWalkable)
                {
                    mapConsole.Set(cell.X, cell.Y, Colors.FloorFov, Colors.FloorBackgroundFov, '.');
                }
                else
                {
                    mapConsole.Set(cell.X, cell.Y, Colors.WallFov, Colors.WallBackgroundFov, '#');
                }
            }
            // when a cell is outside of the field of view draw it with darker colors
            else
            {
                if(cell.IsWalkable)
                {
                    mapConsole.Set(cell.X, cell.Y, Colors.Floor, Colors.FloorBackground, '.');
                }
                else
                {
                    mapConsole.Set(cell.X, cell.Y, Colors.Wall, Colors.WallBackground, '#');
                }
            }
        }

        public void UpdatePlayerFov()
        {
            Player player = Game.Player;
            // compute fov based on player's location and awareness
            ComputeFov(player.X, player.Y, player.Awareness, true);
            foreach(Cell cell in GetAllCells())
            {
                if(IsInFov(cell.X, cell.Y))
                {
                    SetCellProperties(cell.X, cell.Y, cell.IsTransparent, cell.IsWalkable, true);
                }
            }
        }
    }
}
