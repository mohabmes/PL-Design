
using System;
using System.IO;
using System.Runtime.Serialization;
using com.calitha.goldparser.lalr;
using com.calitha.commons;
using System.Windows.Forms;

namespace com.calitha.goldparser
{

    [Serializable()]
    public class SymbolException : System.Exception
    {
        public SymbolException(string message) : base(message)
        {
        }

        public SymbolException(string message,
            Exception inner) : base(message, inner)
        {
        }

        protected SymbolException(SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }

    }

    [Serializable()]
    public class RuleException : System.Exception
    {

        public RuleException(string message) : base(message)
        {
        }

        public RuleException(string message,
                             Exception inner) : base(message, inner)
        {
        }

        protected RuleException(SerializationInfo info,
                                StreamingContext context) : base(info, context)
        {
        }

    }

    enum SymbolConstants : int
    {
        SYMBOL_EOF                         =  0, // (EOF)
        SYMBOL_ERROR                       =  1, // (Error)
        SYMBOL_WHITESPACE                  =  2, // Whitespace
        SYMBOL_MINUS                       =  3, // '-'
        SYMBOL_MINUSMINUS                  =  4, // '--'
        SYMBOL_EXCLAMEQ                    =  5, // '!='
        SYMBOL_PERCENT                     =  6, // '%'
        SYMBOL_LPAREN                      =  7, // '('
        SYMBOL_RPAREN                      =  8, // ')'
        SYMBOL_TIMES                       =  9, // '*'
        SYMBOL_COMMA                       = 10, // ','
        SYMBOL_DOT                         = 11, // '.'
        SYMBOL_DIV                         = 12, // '/'
        SYMBOL_COLON                       = 13, // ':'
        SYMBOL_COLONT                      = 14, // ':t'
        SYMBOL_SEMI                        = 15, // ';'
        SYMBOL_LBRACE                      = 16, // '{'
        SYMBOL_LBRACERBRACE                = 17, // '{}'
        SYMBOL_RBRACE                      = 18, // '}'
        SYMBOL_PLUS                        = 19, // '+'
        SYMBOL_PLUSPLUS                    = 20, // '++'
        SYMBOL_LT                          = 21, // '<'
        SYMBOL_LTEQ                        = 22, // '<='
        SYMBOL_EQ                          = 23, // '='
        SYMBOL_EQEQ                        = 24, // '=='
        SYMBOL_GT                          = 25, // '>'
        SYMBOL_GTEQ                        = 26, // '>='
        SYMBOL_GTGT                        = 27, // '>>'
        SYMBOL_CHAR                        = 28, // char
        SYMBOL_CLASS                       = 29, // Class
        SYMBOL_DECIMAL                     = 30, // decimal
        SYMBOL_DO                          = 31, // do
        SYMBOL_ELSE                        = 32, // else
        SYMBOL_FOR                         = 33, // for
        SYMBOL_FUNCTION                    = 34, // function
        SYMBOL_ID                          = 35, // id
        SYMBOL_IF                          = 36, // if
        SYMBOL_INTEGER                     = 37, // integer
        SYMBOL_NUMBER                      = 38, // number
        SYMBOL_PRIVATE                     = 39, // Private
        SYMBOL_PROTECTED                   = 40, // Protected
        SYMBOL_PUBLIC                      = 41, // Public
        SYMBOL_REAL                        = 42, // real
        SYMBOL_STATIC                      = 43, // static
        SYMBOL_STRING                      = 44, // String
        SYMBOL_VOID                        = 45, // void
        SYMBOL_WHILE                       = 46, // while
        SYMBOL_ARGUMENTS                   = 47, // <arguments>
        SYMBOL_ASSIGN                      = 48, // <assign>
        SYMBOL_CLASSMINUSMEMBER            = 49, // <class-member>
        SYMBOL_CLASSMINUSMEMBERMINUSMETHOD = 50, // <class-member-method>
        SYMBOL_CLASSMINUSMEMBERMINUSTYPE   = 51, // <class-member-type>
        SYMBOL_CLASSMINUSSTATEMENT         = 52, // <class-statement>
        SYMBOL_CLASSMINUSSTMTMINUSLIST     = 53, // <class-stmt-list>
        SYMBOL_CONDITION                   = 54, // <condition>
        SYMBOL_COUNTER                     = 55, // <counter>
        SYMBOL_EXPRESSION                  = 56, // <expression>
        SYMBOL_FACTOR                      = 57, // <factor>
        SYMBOL_FORMINUSSTATEMENT           = 58, // <for-statement>
        SYMBOL_FUNCTIONMINUSCALL           = 59, // <function-call>
        SYMBOL_FUNCTIONMINUSSTATEMENT      = 60, // <function-statement>
        SYMBOL_FUNCTIONMINUSSTMT           = 61, // <function-stmt>
        SYMBOL_FUNCTIONMINUSSTMTMINUSLIST  = 62, // <function-stmt-list>
        SYMBOL_IFMINUSSTATEMENT            = 63, // <if-statement>
        SYMBOL_INITIALMINUSLIST            = 64, // <initial-list>
        SYMBOL_OPERATION                   = 65, // <operation>
        SYMBOL_PROGRAM                     = 66, // <program>
        SYMBOL_RETURNMINUSVALUE            = 67, // <return-value>
        SYMBOL_STATEMENT                   = 68, // <statement>
        SYMBOL_STATEMENTS                  = 69, // <statements>
        SYMBOL_TERM                        = 70, // <term>
        SYMBOL_TYPES                       = 71, // <types>
        SYMBOL_WHILEMINUSSTATEMENT         = 72  // <While-statement>
    };

    enum RuleConstants : int
    {
        RULE_PROGRAM_LBRACE_RBRACE                               =  0, // <program> ::= '{' <statements> '}'
        RULE_STATEMENTS_DOT                                      =  1, // <statements> ::= <statement> '.'
        RULE_STATEMENTS_DOT2                                     =  2, // <statements> ::= <statement> '.' <statements>
        RULE_STATEMENT_DOT                                       =  3, // <statement> ::= <assign> '.'
        RULE_STATEMENT_LBRACERBRACE                              =  4, // <statement> ::= <if-statement> '{}'
        RULE_STATEMENT_LBRACERBRACE2                             =  5, // <statement> ::= <for-statement> '{}'
        RULE_STATEMENT_LBRACERBRACE3                             =  6, // <statement> ::= <While-statement> '{}'
        RULE_STATEMENT_LBRACERBRACE4                             =  7, // <statement> ::= <function-statement> '{}'
        RULE_STATEMENT_LBRACERBRACE5                             =  8, // <statement> ::= <class-statement> '{}'
        RULE_ASSIGN_ID_EQ                                        =  9, // <assign> ::= id '=' <expression>
        RULE_EXPRESSION_PLUS                                     = 10, // <expression> ::= <term> '+' <expression>
        RULE_EXPRESSION_MINUS                                    = 11, // <expression> ::= <term> '-' <expression>
        RULE_EXPRESSION                                          = 12, // <expression> ::= <term>
        RULE_TERM_TIMES                                          = 13, // <term> ::= <factor> '*' <term>
        RULE_TERM_DIV                                            = 14, // <term> ::= <factor> '/' <term>
        RULE_TERM_PERCENT                                        = 15, // <term> ::= <factor> '%' <term>
        RULE_TERM                                                = 16, // <term> ::= <factor>
        RULE_FACTOR_LPAREN_RPAREN                                = 17, // <factor> ::= '(' <expression> ')'
        RULE_FACTOR_ID                                           = 18, // <factor> ::= id
        RULE_FACTOR_NUMBER                                       = 19, // <factor> ::= number
        RULE_FACTOR_REAL                                         = 20, // <factor> ::= real
        RULE_IFSTATEMENT_IF_GTGT                                 = 21, // <if-statement> ::= if <condition> '>>' <statements>
        RULE_IFSTATEMENT_IF_GTGT_ELSE                            = 22, // <if-statement> ::= if <condition> '>>' <statements> else <statements>
        RULE_CONDITION                                           = 23, // <condition> ::= <expression> <operation> <expression>
        RULE_OPERATION_LT                                        = 24, // <operation> ::= '<'
        RULE_OPERATION_GT                                        = 25, // <operation> ::= '>'
        RULE_OPERATION_EQEQ                                      = 26, // <operation> ::= '=='
        RULE_OPERATION_LTEQ                                      = 27, // <operation> ::= '<='
        RULE_OPERATION_GTEQ                                      = 28, // <operation> ::= '>='
        RULE_OPERATION_EXCLAMEQ                                  = 29, // <operation> ::= '!='
        RULE_FORSTATEMENT_FOR_COMMA_COMMA_LBRACE_RBRACE          = 30, // <for-statement> ::= for <types> <assign> ',' <condition> ',' <counter> '{' <statements> '}'
        RULE_TYPES_INTEGER                                       = 31, // <types> ::= integer
        RULE_TYPES_DECIMAL                                       = 32, // <types> ::= decimal
        RULE_TYPES_STRING                                        = 33, // <types> ::= String
        RULE_TYPES_CHAR                                          = 34, // <types> ::= char
        RULE_COUNTER_ID_PLUSPLUS                                 = 35, // <counter> ::= id '++'
        RULE_COUNTER_ID_MINUSMINUS                               = 36, // <counter> ::= id '--'
        RULE_WHILESTATEMENT_WHILE_LPAREN_RPAREN_DO_LPAREN_RPAREN = 37, // <While-statement> ::= while '(' <expression> ')' do '(' <statements> ')'
        RULE_FUNCTIONSTATEMENT_FUNCTION_ID_LPAREN_RPAREN_DOT     = 38, // <function-statement> ::= function <return-value> id '(' <initial-list> ')' '.' <function-stmt-list>
        RULE_RETURNVALUE                                         = 39, // <return-value> ::= <types>
        RULE_RETURNVALUE_VOID                                    = 40, // <return-value> ::= void
        RULE_INITIALLIST_ID_COMMA                                = 41, // <initial-list> ::= <types> id ',' <initial-list>
        RULE_INITIALLIST_ID                                      = 42, // <initial-list> ::= <types> id
        RULE_FUNCTIONSTMTLIST                                    = 43, // <function-stmt-list> ::= <function-statement>
        RULE_FUNCTIONSTMTLIST2                                   = 44, // <function-stmt-list> ::= <function-statement> <function-stmt-list>
        RULE_FUNCTIONSTMT_DOT                                    = 45, // <function-stmt> ::= <assign> '.'
        RULE_FUNCTIONSTMT_LBRACERBRACE                           = 46, // <function-stmt> ::= <if-statement> '{}'
        RULE_FUNCTIONSTMT_LBRACERBRACE2                          = 47, // <function-stmt> ::= <for-statement> '{}'
        RULE_FUNCTIONSTMT_DOT2                                   = 48, // <function-stmt> ::= <counter> '.'
        RULE_FUNCTIONSTMT_DOT3                                   = 49, // <function-stmt> ::= <types> <assign> '.'
        RULE_FUNCTIONCALL_ID_LPAREN_RPAREN                       = 50, // <function-call> ::= id '(' <arguments> ')'
        RULE_ARGUMENTS_COMMA                                     = 51, // <arguments> ::= <factor> ',' <arguments>
        RULE_ARGUMENTS                                           = 52, // <arguments> ::= <factor>
        RULE_CLASSSTATEMENT_CLASS_ID_COLON                       = 53, // <class-statement> ::= Class id ':' <class-stmt-list>
        RULE_CLASSSTMTLIST                                       = 54, // <class-stmt-list> ::= <class-member> <class-stmt-list>
        RULE_CLASSSTMTLIST2                                      = 55, // <class-stmt-list> ::= <class-member-method> <class-stmt-list>
        RULE_CLASSSTMTLIST3                                      = 56, // <class-stmt-list> ::= <class-member>
        RULE_CLASSSTMTLIST4                                      = 57, // <class-stmt-list> ::= <class-member-method>
        RULE_CLASSMEMBER_SEMI                                    = 58, // <class-member> ::= <class-member-type> <types> <assign> ';'
        RULE_CLASSMEMBERMETHOD_COLONT                            = 59, // <class-member-method> ::= <class-member-type> <function-stmt> ':t'
        RULE_CLASSMEMBERTYPE_PUBLIC                              = 60, // <class-member-type> ::= Public
        RULE_CLASSMEMBERTYPE_PRIVATE                             = 61, // <class-member-type> ::= Private
        RULE_CLASSMEMBERTYPE_PROTECTED                           = 62, // <class-member-type> ::= Protected
        RULE_CLASSMEMBERTYPE_STATIC                              = 63  // <class-member-type> ::= static
    };

    public class MyParser
    {
        private LALRParser parser;
        ListBox lstErrors;

        Label Label1;

        public MyParser(string filename , ListBox lstErrors , Label Label1)
        {
            FileStream stream = new FileStream(filename,
                                               FileMode.Open, 
                                               FileAccess.Read, 
                                               FileShare.Read);
            Init(stream);
            stream.Close();

            this.lstErrors = lstErrors;
            this.Label1 = Label1;
        }

        public MyParser(string baseName, string resourceName)
        {
            byte[] buffer = ResourceUtil.GetByteArrayResource(
                System.Reflection.Assembly.GetExecutingAssembly(),
                baseName,
                resourceName);
            MemoryStream stream = new MemoryStream(buffer);
            Init(stream);
            stream.Close();
        }

        public MyParser(Stream stream)
        {
            Init(stream);
        }

        private void Init(Stream stream)
        {
            CGTReader reader = new CGTReader(stream);
            parser = reader.CreateNewParser();
            parser.TrimReductions = false;
            parser.StoreTokens = LALRParser.StoreTokensMode.NoUserObject;

            parser.OnTokenError += new LALRParser.TokenErrorHandler(TokenErrorEvent);
            parser.OnParseError += new LALRParser.ParseErrorHandler(ParseErrorEvent);

            parser.OnTokenRead += new LALRParser.TokenReadHandler(TokenReadEvent);
        }

        private void TokenReadEvent(LALRParser parser , TokenReadEventArgs Args)
        {
            Args.Token.UserObject = CreateObject(Args.Token);
            Label1.Text += Args.Token.Text + " ==> " + ((SymbolConstants)Args.Token.Symbol.Id) + "\n";

        }

        public void Parse(string source)
        {
            NonterminalToken token = parser.Parse(source);
            if (token != null)
            {
                Object obj = CreateObject(token);
                //todo: Use your object any way you like
            }
        }

        private Object CreateObject(Token token)
        {
            if (token is TerminalToken)
                return CreateObjectFromTerminal((TerminalToken)token);
            else
                return CreateObjectFromNonterminal((NonterminalToken)token);
        }

        private Object CreateObjectFromTerminal(TerminalToken token)
        {
            switch (token.Symbol.Id)
            {
                case (int)SymbolConstants.SYMBOL_EOF :
                //(EOF)
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ERROR :
                //(Error)
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_WHITESPACE :
                //Whitespace
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_MINUS :
                //'-'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_MINUSMINUS :
                //'--'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EXCLAMEQ :
                //'!='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PERCENT :
                //'%'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LPAREN :
                //'('
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_RPAREN :
                //')'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TIMES :
                //'*'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_COMMA :
                //','
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DOT :
                //'.'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DIV :
                //'/'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_COLON :
                //':'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_COLONT :
                //':t'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_SEMI :
                //';'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LBRACE :
                //'{'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LBRACERBRACE :
                //'{}'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_RBRACE :
                //'}'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PLUS :
                //'+'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PLUSPLUS :
                //'++'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LT :
                //'<'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LTEQ :
                //'<='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EQ :
                //'='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EQEQ :
                //'=='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_GT :
                //'>'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_GTEQ :
                //'>='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_GTGT :
                //'>>'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CHAR :
                //char
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CLASS :
                //Class
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DECIMAL :
                //decimal
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DO :
                //do
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ELSE :
                //else
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FOR :
                //for
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FUNCTION :
                //function
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ID :
                //id
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_IF :
                //if
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_INTEGER :
                //integer
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_NUMBER :
                //number
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PRIVATE :
                //Private
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PROTECTED :
                //Protected
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PUBLIC :
                //Public
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_REAL :
                //real
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STATIC :
                //static
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STRING :
                //String
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_VOID :
                //void
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_WHILE :
                //while
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ARGUMENTS :
                //<arguments>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ASSIGN :
                //<assign>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CLASSMINUSMEMBER :
                //<class-member>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CLASSMINUSMEMBERMINUSMETHOD :
                //<class-member-method>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CLASSMINUSMEMBERMINUSTYPE :
                //<class-member-type>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CLASSMINUSSTATEMENT :
                //<class-statement>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CLASSMINUSSTMTMINUSLIST :
                //<class-stmt-list>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CONDITION :
                //<condition>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_COUNTER :
                //<counter>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EXPRESSION :
                //<expression>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FACTOR :
                //<factor>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FORMINUSSTATEMENT :
                //<for-statement>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FUNCTIONMINUSCALL :
                //<function-call>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FUNCTIONMINUSSTATEMENT :
                //<function-statement>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FUNCTIONMINUSSTMT :
                //<function-stmt>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FUNCTIONMINUSSTMTMINUSLIST :
                //<function-stmt-list>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_IFMINUSSTATEMENT :
                //<if-statement>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_INITIALMINUSLIST :
                //<initial-list>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_OPERATION :
                //<operation>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PROGRAM :
                //<program>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_RETURNMINUSVALUE :
                //<return-value>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STATEMENT :
                //<statement>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STATEMENTS :
                //<statements>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TERM :
                //<term>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TYPES :
                //<types>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_WHILEMINUSSTATEMENT :
                //<While-statement>
                //todo: Create a new object that corresponds to the symbol
                return null;

            }
            throw new SymbolException("Unknown symbol");
        }

        public Object CreateObjectFromNonterminal(NonterminalToken token)
        {
            switch (token.Rule.Id)
            {
                case (int)RuleConstants.RULE_PROGRAM_LBRACE_RBRACE :
                //<program> ::= '{' <statements> '}'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENTS_DOT :
                //<statements> ::= <statement> '.'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENTS_DOT2 :
                //<statements> ::= <statement> '.' <statements>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENT_DOT :
                //<statement> ::= <assign> '.'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENT_LBRACERBRACE :
                //<statement> ::= <if-statement> '{}'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENT_LBRACERBRACE2 :
                //<statement> ::= <for-statement> '{}'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENT_LBRACERBRACE3 :
                //<statement> ::= <While-statement> '{}'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENT_LBRACERBRACE4 :
                //<statement> ::= <function-statement> '{}'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENT_LBRACERBRACE5 :
                //<statement> ::= <class-statement> '{}'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ASSIGN_ID_EQ :
                //<assign> ::= id '=' <expression>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPRESSION_PLUS :
                //<expression> ::= <term> '+' <expression>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPRESSION_MINUS :
                //<expression> ::= <term> '-' <expression>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPRESSION :
                //<expression> ::= <term>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TERM_TIMES :
                //<term> ::= <factor> '*' <term>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TERM_DIV :
                //<term> ::= <factor> '/' <term>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TERM_PERCENT :
                //<term> ::= <factor> '%' <term>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TERM :
                //<term> ::= <factor>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FACTOR_LPAREN_RPAREN :
                //<factor> ::= '(' <expression> ')'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FACTOR_ID :
                //<factor> ::= id
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FACTOR_NUMBER :
                //<factor> ::= number
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FACTOR_REAL :
                //<factor> ::= real
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_IFSTATEMENT_IF_GTGT :
                //<if-statement> ::= if <condition> '>>' <statements>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_IFSTATEMENT_IF_GTGT_ELSE :
                //<if-statement> ::= if <condition> '>>' <statements> else <statements>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CONDITION :
                //<condition> ::= <expression> <operation> <expression>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OPERATION_LT :
                //<operation> ::= '<'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OPERATION_GT :
                //<operation> ::= '>'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OPERATION_EQEQ :
                //<operation> ::= '=='
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OPERATION_LTEQ :
                //<operation> ::= '<='
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OPERATION_GTEQ :
                //<operation> ::= '>='
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OPERATION_EXCLAMEQ :
                //<operation> ::= '!='
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FORSTATEMENT_FOR_COMMA_COMMA_LBRACE_RBRACE :
                //<for-statement> ::= for <types> <assign> ',' <condition> ',' <counter> '{' <statements> '}'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TYPES_INTEGER :
                //<types> ::= integer
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TYPES_DECIMAL :
                //<types> ::= decimal
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TYPES_STRING :
                //<types> ::= String
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TYPES_CHAR :
                //<types> ::= char
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_COUNTER_ID_PLUSPLUS :
                //<counter> ::= id '++'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_COUNTER_ID_MINUSMINUS :
                //<counter> ::= id '--'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_WHILESTATEMENT_WHILE_LPAREN_RPAREN_DO_LPAREN_RPAREN :
                //<While-statement> ::= while '(' <expression> ')' do '(' <statements> ')'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FUNCTIONSTATEMENT_FUNCTION_ID_LPAREN_RPAREN_DOT :
                //<function-statement> ::= function <return-value> id '(' <initial-list> ')' '.' <function-stmt-list>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_RETURNVALUE :
                //<return-value> ::= <types>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_RETURNVALUE_VOID :
                //<return-value> ::= void
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_INITIALLIST_ID_COMMA :
                //<initial-list> ::= <types> id ',' <initial-list>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_INITIALLIST_ID :
                //<initial-list> ::= <types> id
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FUNCTIONSTMTLIST :
                //<function-stmt-list> ::= <function-statement>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FUNCTIONSTMTLIST2 :
                //<function-stmt-list> ::= <function-statement> <function-stmt-list>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FUNCTIONSTMT_DOT :
                //<function-stmt> ::= <assign> '.'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FUNCTIONSTMT_LBRACERBRACE :
                //<function-stmt> ::= <if-statement> '{}'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FUNCTIONSTMT_LBRACERBRACE2 :
                //<function-stmt> ::= <for-statement> '{}'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FUNCTIONSTMT_DOT2 :
                //<function-stmt> ::= <counter> '.'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FUNCTIONSTMT_DOT3 :
                //<function-stmt> ::= <types> <assign> '.'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FUNCTIONCALL_ID_LPAREN_RPAREN :
                //<function-call> ::= id '(' <arguments> ')'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ARGUMENTS_COMMA :
                //<arguments> ::= <factor> ',' <arguments>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ARGUMENTS :
                //<arguments> ::= <factor>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CLASSSTATEMENT_CLASS_ID_COLON :
                //<class-statement> ::= Class id ':' <class-stmt-list>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CLASSSTMTLIST :
                //<class-stmt-list> ::= <class-member> <class-stmt-list>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CLASSSTMTLIST2 :
                //<class-stmt-list> ::= <class-member-method> <class-stmt-list>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CLASSSTMTLIST3 :
                //<class-stmt-list> ::= <class-member>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CLASSSTMTLIST4 :
                //<class-stmt-list> ::= <class-member-method>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CLASSMEMBER_SEMI :
                //<class-member> ::= <class-member-type> <types> <assign> ';'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CLASSMEMBERMETHOD_COLONT :
                //<class-member-method> ::= <class-member-type> <function-stmt> ':t'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CLASSMEMBERTYPE_PUBLIC :
                //<class-member-type> ::= Public
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CLASSMEMBERTYPE_PRIVATE :
                //<class-member-type> ::= Private
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CLASSMEMBERTYPE_PROTECTED :
                //<class-member-type> ::= Protected
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CLASSMEMBERTYPE_STATIC :
                //<class-member-type> ::= static
                //todo: Create a new object using the stored tokens.
                return null;

            }
            throw new RuleException("Unknown rule");
        }

        private void TokenErrorEvent(LALRParser parser, TokenErrorEventArgs args)
        {
            string message = "Token error with input: '"+args.Token.ToString()+"'";
            lstErrors.Items.Add(message);
            //todo: Report message to UI?
        }

        private void ParseErrorEvent(LALRParser parser, ParseErrorEventArgs args)
        {
            string message = "Parse error caused by token: '" + args.UnexpectedToken.ToString() + "'" + "In lines : '" + args.UnexpectedToken.Location.LineNr;
            lstErrors.Items.Add(message);
            string message2 = "Expected token : '" + args.ExpectedTokens.ToString() + "'";
            lstErrors.Items.Add(message2);
            //todo: Report message to UI?
        }

    }
}
