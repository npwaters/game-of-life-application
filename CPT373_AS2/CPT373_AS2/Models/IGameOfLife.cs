using CPT373_AS2.Models;

namespace CPT373_AS2
{
    public interface IGameOfLife
    {
        int Height { get; }
        int Width { get; }
        Cell[][] Cells { get; }
        void InsertTemplate(ITemplate template, int templateX, int templateY);
        void TakeTurn();
        void PlayGame();
    }
}
