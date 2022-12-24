using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards_Game.Logic.Inter;
/// <summary>
/// Ciclo While
/// </summary>
public class While: AST
{
    //Condicion del ciclo
    public BinOp condition;
    //Compuesto
    public Compuesto comp;

    /// <summary>
    /// Construcror
    /// </summary>
    /// <param name="condition"></param>
    /// <param name="comp"></param>
    public While(BinOp condition, Compuesto comp)
    {
        this.condition = condition;
        this.comp = comp;
    }
}

