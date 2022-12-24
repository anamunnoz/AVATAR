using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards_Game.Logic.Inter;
/// <summary>
/// Parser
/// </summary>
public class Analizador
{
    //Lexer
    Lexer lexer;
    //token actual
    TOKENS current_token;
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="lexer"></param>
    public Analizador(Lexer lexer)
    {
        this.lexer = lexer;
        current_token = lexer.gnt();
    }
    /// <summary>
    /// Analiza que la sintaxis sea correcta y busca el siguiente token
    /// </summary>
    /// <param name="token_type"></param>
    public void eat(string token_type)
    {
        if (current_token.type == token_type) current_token = lexer.gnt();
    }

    /// <summary>
    /// Devuelve el factor que analizamos
    /// </summary>
    /// <returns></returns>
    AST factor() 
    {
        
        TOKENS token = current_token;
        if (token.type == "ENTERO")
        {
            eat("ENTERO");
            

            return new Num(double.Parse(token.value));
        }
           
        if (token.type == "PLUS")
        {
            eat("PLUS");
            UnaryOp node = new UnaryOp(token, factor());
            return node;
        }

        if (token.type == "MINUS")
        {
            eat("MINUS");
            UnaryOp node = new UnaryOp(token, factor());
            return node;
        }
        if (token.type == "LPAREN")
        {
            eat("LPAREN");
            AST node = expression();
            
            eat("RPAREN");
            return node;
        }
        else
        {
            Var node = variable();
            return node;
        }
    }

    /// <summary>
    /// Maneja la multiplicacion y la division en los nodos
    /// </summary>
    /// <returns></returns>
    AST term() 
    {
        AST node = factor();
        while (current_token.type == "MULT" || current_token.type == "DIV")
        {
            TOKENS token = current_token;
            if (token.type == "MULT")
            {
                eat("MULT");
               
            }
            if (token.type == "DIV")
            {
                eat("DIV");
                
            }
            node = new BinOp(node, factor(), token);
        }
        return node;
    }

    /// <summary>
    /// Compuesto de Nodos
    /// </summary>
    /// <returns></returns>
    Compuesto program()
    {
        Compuesto Node = compoud_statement();
        eat("DOT");
        return Node;
    }

    /// <summary>
    /// Compuesto de declaraciones
    /// </summary>
    /// <returns></returns>
    Compuesto compoud_statement()
    {
        eat("BEGIN");
        List<AST> nodes = statement_list();
        eat("END");

        Compuesto root = new Compuesto();

        foreach (AST nodo in nodes)
        {
            root.children.Add(nodo);
        }
        return root;
    }

    /// <summary>
    /// Lista de declaraciones
    /// </summary>
    /// <returns></returns>
    List<AST> statement_list()
    {
        AST node = declaracion();
        List<AST> resultados = new List<AST>();
        resultados.Add(node);

        while (current_token.type == "SEMI")
        {
            eat("SEMI");
            resultados.Add(declaracion());
        }

        return resultados;
    }

    /// <summary>
    /// Declaraciones
    /// </summary>
    /// <returns></returns>
    AST declaracion()
    {
        AST node;
        if (current_token.type == "BEGIN")
        {
            node = compoud_statement();
        }
        else if (current_token.type == "ID")
        {
            node = assignment_statement();
        }
        else if (current_token.type == "IF")
        {
            return Conditional();

        }
        else if (current_token.type == "WHILE")
        {
            return Ciclo();

        }
       
        else
        {
            node = vacio();
        }
        return node;

    }
    /// <summary>
    /// Asignaciones
    /// </summary>
    /// <returns></returns>
    AST assignment_statement()
    {
        Var left = variable();
        TOKENS token = current_token;
        eat("ASSIGN");
        AST right = expression();
        AST node = new Asignar(left, right, token);
        return node;
    }
    /// <summary>
    /// Variable
    /// </summary>
    /// <returns></returns>
    Var variable()
    {
        Var node = new Var(current_token);
        eat("ID");
        return node;
    }

    /// <summary>
    /// Nodo vacio
    /// </summary>
    /// <returns></returns>
    AST vacio()
    {
        return new NoOp();
    }

    /// <summary>
    /// Maneja la suma y resta en los nodos
    /// </summary>
    /// <returns></returns>
    public AST expression() 
    {
        AST node = term();

        while (current_token.type == "PLUS" || current_token.type == "MINUS")
        {
            TOKENS token = current_token;
            if (token.type == "PLUS")
            {
                eat("PLUS");
               
            }
            if (token.type == "MINUS")
            {
                eat("MINUS");
                
            }
            node = new BinOp(node, term(), token);
        }
        return node;
    }
    /// <summary>
    /// Analiza el AST
    /// </summary>
    /// <returns></returns>
    public AST Parse()
    {
        AST node = program();
        return node;
    }

    /// <summary>
    /// Evalua las condiciones
    /// </summary>
    /// <returns></returns>
    public Condition Conditional()
    {
        eat("IF");
        eat("LPAREN");
        BinOp comparer = Logic();
        eat("RPAREN");
        Compuesto componentStatement = compoud_statement();
        return new Condition(componentStatement, comparer);
    }

    /// <summary>
    /// Maneja los & y || de las expresiones boleanas
    /// </summary>
    /// <returns></returns>
    public BinOp Logic()
    {
        BinOp node = Comparer();
        while (current_token.type == "AND" || current_token.type == "OR")
        {
            TOKENS token = new TOKENS(current_token.type,current_token.value);
            eat(token.type);
            node = new BinOp(node, Comparer(), token);
        }
        return node;
    }

    /// <summary>
    /// Maneja los terminos de '<', '>' o '==" de las expresiones boleanas 
    /// </summary>
    /// <returns></returns>
    public BinOp Comparer()
    {
        AST node = expression();
        AST expr2 = null;
        TOKENS token = new TOKENS(current_token.type,current_token.value);

        if (current_token.type == "MENOR" || current_token.type == "MAYOR" || current_token.type == "IGUAL")
        {
            eat(current_token.type);
            expr2 = expression();
        }
        return new BinOp(node, expr2,token);
    }

    /// <summary>
    /// Analiza los ciclos
    /// </summary>
    /// <returns></returns>
    public While Ciclo()
    {
        eat("WHILE");
        eat("LPAREN");
        BinOp comparer = Logic();
        eat("RPAREN");
        Compuesto componentStatement = compoud_statement();
        return new While(comparer, componentStatement);
    }
    
}

