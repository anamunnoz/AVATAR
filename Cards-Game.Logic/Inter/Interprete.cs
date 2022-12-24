using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards_Game.Logic.Inter;

/// <summary>
/// Interprete
/// </summary>
public class Interprete : NodeVisitor
{
    //analizadpr
    Analizador parser;
    //contexto
    public Context context;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="parser"></param>
    /// <param name="context"></param>
    public Interprete(Analizador parser, Context context)
    {
        this.parser = parser;
        this.context=context;
    }
    /// <summary>
    /// Interpreta un arbol de sintaxis abstracta
    /// </summary>
    public void interpretar()
    {
        AST arbol = parser.Parse();
        Visita(arbol);
    }

    //METODOS DE VISITA DE NODOS

    Num Visit_Num(Num y)
    {
        return y;
    }
    AST Visit_Binop(BinOp y)
    {
        Object Left = Visita(y.Left);
        Object Right = Visita(y.Right);
        string sleft;
        string sright;
        if (Left is Num && Right is Num)
        {
            sleft = (Left as Num).value.ToString();
            sright = (Right as Num).value.ToString();
        }
        else
        {
            sleft = (Left as Bool).value.ToString();
            sright = (Right as Bool).value.ToString();
        }
            
            
        if (y.op.type == "PLUS")
        {
            return new Num(double.Parse(sleft) + double.Parse(sright));
        }
        if (y.op.type == "MINUS")
        {
            return new Num(double.Parse(sleft) - double.Parse(sright));
        }
        if (y.op.type == "MULT")
        {
            return new Num(double.Parse(sleft) * double.Parse(sright));
        }
        if (y.op.type == "DIV")
        {
            return new Num (double.Parse(sleft) / double.Parse(sright));
        }
            
        if (y.op.type == "MENOR")
        {
            return new Bool (double.Parse(sleft) < double.Parse(sright));
        }
        if (y.op.type == "MAYOR")
        {
            return new Bool (double.Parse(sleft) > double.Parse(sright));
        }
        if (y.op.type == "IGUAL")
        {
            return new Bool (double.Parse(sleft) == double.Parse(sright));
        }
        if (y.op.type == "AND")
        {
            return new Bool (sleft=="True" && sright=="True");
        }
        if (y.op.type == "OR")
        {
            return new Bool (sleft == "True" || sright == "True");
        }
        return null;
    }

    Num Visit_UnaryOP(UnaryOp x)
    {           
        if (x.op.type == "PLUS")
        {
            return new Num (double.Parse((Visita(x.expression) as Num).value.ToString()));               
        }
        if (x.op.type == "MINUS")
        {
            return new Num(double.Parse((Visita(x.expression) as Num).value.ToString()) * -1);
        }
        return null;
    }

    void Visit_Compuesto(Compuesto x)
    {
        foreach (AST child in x.children)
        {
            Visita(child);
        }  
    }

    void Visit_Asignar(Asignar x)
    {
        string var_name = x.left.value;

        AST visit = Visita(x.right);
        Num result = visit as Num;

        if (context.ContainsId(var_name))
        {
            context.Update(var_name, result.value);
        }
        else
        {
            context.Add(new TOKENS("ENTERO", var_name), result.value);
        }
    }
        
    Num Visit_Var(Var x)
    {
        string var_name = x.value;
        return new Num(context.GetValue(var_name));
    }

    void Visit_While(While x)
    {
        bool follow = true;
        while (follow)
        {
            AST val = Visit_Binop(x.condition);

            Bool cond = val as Bool;
            if (cond.value == true)
            {
                Visita(x.comp);
            }
            else
            {
                follow = false;
            }
        }
    }
    

    void Visit_Condition(Condition x)
    {
        Bool boool = (Visit_Binop(x.condition)) as Bool;
        if (boool.value.ToString() == "True")
        {
            Visita(x.compuesto);
        }
    }
    Bool Visit_Bool(Bool x)
    {
        return x;
    }

    public AST Visita(object node)
    {
        if (node is Num)
        {
            return Visit_Num(node as Num);
        }
        if (node is BinOp)
        {
            return Visit_Binop(node as BinOp);   
        }
        if (node is UnaryOp)
        {
            return Visit_UnaryOP(node as UnaryOp);

        }
        if (node is Compuesto)
        {
            Visit_Compuesto(node as Compuesto);

        }
        if (node is NoOp)
        {

        }
        if (node is Asignar)
        {
            Visit_Asignar(node as Asignar);

        }
        if (node is Var)
        {
            return Visit_Var(node as Var);  
        }
        if(node is Condition)
        {
            Visit_Condition(node as Condition);
        }
        if (node is While)
        {
            Visit_While(node as While);
        }
        if(node is bool)
        {
            Visit_Bool(node as Bool);
        }
        return null;
    }
}

