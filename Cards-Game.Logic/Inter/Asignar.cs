using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards_Game.Logic.Inter;

/// <summary>
/// Nodo de asignacion
/// </summary>
class Asignar:AST
{
    //variable
    public Var left;
    //nodo asignado
    public AST right;
    //operacion
    public TOKENS op;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <param name="op"></param>
    public Asignar(Var left, AST right, TOKENS op)
    {
        this.left = left;
        this.right = right;
        this.op = op;  
    }
}

