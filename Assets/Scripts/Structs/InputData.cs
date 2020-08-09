namespace Game.InputManagment
{
    public struct PlayerMoveCommand
    {
        public bool a;
        public bool w;
        public bool s;
        public bool d;

        public bool Has => a || w || s || d;
    }
}