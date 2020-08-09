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
    }
}