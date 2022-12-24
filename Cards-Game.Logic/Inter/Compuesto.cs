using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards_Game.Logic.Inter;
/// <summary>
/// Compuesto
/// </summary>
public class Compuesto:AST
{
    //nodos del compuesto
    public List<AST> children;
    /// <summary>
    /// Constructor
    /// </summary>
    public Compuesto()
    {
        children = new List<AST>();
    }
}

