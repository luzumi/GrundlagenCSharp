
namespace TicTacToe
{
    class Button
    {
        private Point Position { get; }

        public FieldState FieldState;

        public Button( Point position )
        {
            this.Position = position;
        }
    }
}
