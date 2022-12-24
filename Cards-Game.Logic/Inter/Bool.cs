using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards_Game.Logic.Inter;

/// <summary>
/// Booleanos
/// </summary>
class Bool: AST
{
    //valor
    public bool value;
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="value"></param>
    public Bool(bool value)
    {
        this.value = value;
    }
}

