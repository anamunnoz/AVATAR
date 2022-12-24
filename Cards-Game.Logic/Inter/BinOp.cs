using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards_Game.Logic.Inter;

/// <summary>
/// Operaciones Binarias
/// </summary>
public class BinOp: AST
{
    //Nodo izquierdo
    public AST Left;
    //Nodo derecho
    public AST Right;
    //atomo
    public TOKENS op;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="Left"></param>
    /// <param name="Right"></param>
    /// <param name="op"></param>
    public BinOp(AST Left, AST Right, TOKENS op)
    {
        this.Left = Left;
        this.Right = Right;
        this.op = op;
    }
}

