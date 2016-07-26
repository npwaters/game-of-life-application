using CPT373_AS2.Models;

namespace CPT373_AS2
{
    public interface ITemplate
    {
        string Name { get; }
        int Height { get; }
        int Width { get; }
        Cell[][] Cells { get; }
    }
}
