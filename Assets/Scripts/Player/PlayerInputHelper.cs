using System;
using UnityEngine;

namespace Game.InputManagment
{
    public static class PlayerInputHelper
    {
        public static PlayerMoveCommand GetMoveCommand()
        {
            PlayerMoveCommand moveCommand = new PlayerMoveCommand();

            if (Input.GetKey(KeyCode.A)) {
                moveCommand.a = true;
            }

            if (Input.GetKey(KeyCode.W)) {
                moveCommand.w = true;
            }

            if (Input.GetKey(KeyCode.S)) {
                moveCommand.s = true;
            }

            if (Input.GetKey(KeyCode.D)) {
                moveCommand.d = true;
            }

            return moveCommand;
        }

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