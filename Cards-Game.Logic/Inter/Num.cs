using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards_Game.Logic.Inter;

/// <summary>
/// Numeros
/// </summary>
class Num:AST
{
    //valor
    public double value;
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="value"></param>
    public Num(double value)
    {
        this.value = value;
    }
}

