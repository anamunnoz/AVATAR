using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards_Game.Logic.Inter;

/// <summary>
/// Variable
/// </summary>
class Var:AST
{
    //Atomo
    public TOKENS token;
    //valor de la variable
    public string value;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="token"></param>
    public Var(TOKENS token)
    {
        this.token = token;
        value = token.value;
    }
}

