using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards_Game.Logic.Inter;

/// <summary>
/// Clase de Condiciones
/// </summary>
public class Condition: AST
{
    //Compuesto
    public Compuesto compuesto;
    //Operacion Binaria
    public BinOp condition;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="compuesto"></param>
    /// <param name="condition"></param>
    public Condition(Compuesto compuesto, BinOp condition)
    {
        this.compuesto = compuesto;
        this.condition = condition;
    }
}

