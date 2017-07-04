using Hamurabi.Core;

namespace Hamurabi.WebUI.Services
{
    public class HamurabiGameService
    {
        private readonly Game _game;

        public HamurabiGameService()
        {
            _game = new Game();
        }


        public void Start()
        {
            _game.Start();
        }
    }
}