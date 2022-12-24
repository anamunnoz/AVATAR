using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards_Game.Logic.Inter;

/// <summary>
/// Operaciones Unarias
/// </summary>
class UnaryOp : AST
{ 
    //Atomo de operacion
    public TOKENS op;
    public TOKENS token;
    //nodo de expresion
    public AST expression;

    public UnaryOp(TOKENS op, AST expression)
    {
        this.op = op;
        this.expression = expression;
        token = op;
    }
}

