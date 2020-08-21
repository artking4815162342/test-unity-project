using System;
using UnityEngine;

namespace Game.InputManagment
{
    public static class PlayerInputHelper
    {
        public static PlayerSelectCommand GetSelectCommand()
        {
            PlayerSelectCommand selectCommand = new PlayerSelectCommand();

            if (Input.GetKeyDown(KeyCode.UpArrow)) {
                selectCommand.up = true;
            }

            if (Input.GetKeyDown(KeyCode.DownArrow)) {
                selectCommand.down = true;
            }

            return selectCommand;
        }
    }
}