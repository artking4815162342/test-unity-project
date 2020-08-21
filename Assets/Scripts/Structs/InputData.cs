namespace Game.InputManagment
{
    public struct PlayerSelectCommand
    {
        public bool up;
        public bool down;

        public bool Has => up || down;
    }
}