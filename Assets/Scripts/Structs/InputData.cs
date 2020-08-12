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

    public struct PlayerSelectCommand
    {
        public bool up;
        public bool down;

        public bool Has => up || down;
    }
}