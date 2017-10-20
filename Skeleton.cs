using System;
using System.IO;
using System.Runtime.Serialization;
using com.calitha.goldparser.lalr;
using com.calitha.commons;
using System.Windows.Forms;
using System.Collections;
using System.Data;

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
        SYMBOL_EOF            =  0, // (EOF)
        SYMBOL_ERROR          =  1, // (Error)
        SYMBOL_WHITESPACE     =  2, // Whitespace
        SYMBOL_MINUS          =  3, // '-'
        SYMBOL_MINUSMINUS     =  4, // '--'
        SYMBOL_EXCLAMEQ       =  5, // '!='
        SYMBOL_PERCENT        =  6, // '%'
        SYMBOL_LPAREN         =  7, // '('
        SYMBOL_RPAREN         =  8, // ')'
        SYMBOL_TIMES          =  9, // '*'
        SYMBOL_COMMA          = 10, // ','
        SYMBOL_DIV            = 11, // '/'
        SYMBOL_COLON          = 12, // ':'
        SYMBOL_COLONCOLON     = 13, // '::'
        SYMBOL_SEMI           = 14, // ';'
        SYMBOL_PLUS           = 15, // '+'
        SYMBOL_PLUSPLUS       = 16, // '++'
        SYMBOL_LT             = 17, // '<'
        SYMBOL_LTEQ           = 18, // '<='
        SYMBOL_EQ             = 19, // '='
        SYMBOL_EQEQ           = 20, // '=='
        SYMBOL_GT             = 21, // '>'
        SYMBOL_GTEQ           = 22, // '>='
        SYMBOL_CHAR           = 23, // char
        SYMBOL_CLASS          = 24, // Class
        SYMBOL_ELSE           = 25, // else
        SYMBOL_END            = 26, // End
        SYMBOL_FLOAT          = 27, // Float
        SYMBOL_FROM           = 28, // from
        SYMBOL_FUNCTION       = 29, // function
        SYMBOL_IDENTIFIER     = 30, // Identifier
        SYMBOL_IF             = 31, // if
        SYMBOL_INT            = 32, // int
        SYMBOL_NUM            = 33, // Num
        SYMBOL_PRIVATE        = 34, // Private
        SYMBOL_PROTECTED      = 35, // Protected
        SYMBOL_PUBLIC         = 36, // Public
        SYMBOL_REAL           = 37, // real
        SYMBOL_START          = 38, // Start
        SYMBOL_STATIC         = 39, // static
        SYMBOL_STRING         = 40, // string
        SYMBOL_TO             = 41, // to
        SYMBOL_VOID           = 42, // void
        SYMBOL_ARGS           = 43, // <args>
        SYMBOL_ASSIGN         = 44, // <assign>
        SYMBOL_CMINUSELEMET   = 45, // <c-elemet>
        SYMBOL_CLASS2         = 46, // <class>
        SYMBOL_CMINUSLIST     = 47, // <c-list>
        SYMBOL_CMINUSMETHOD   = 48, // <c-method>
        SYMBOL_CMINUSTYPE     = 49, // <c-type>
        SYMBOL_EXPR           = 50, // <expr>
        SYMBOL_FACTOR         = 51, // <factor>
        SYMBOL_FCALL          = 52, // <fcall>
        SYMBOL_FORMINUSSTMT   = 53, // <for-stmt>
        SYMBOL_FSTMT          = 54, // <fstmt>
        SYMBOL_FSTMTMINUSLIST = 55, // <fstmt-list>
        SYMBOL_FUNCMINUSSTMT  = 56, // <func-stmt>
        SYMBOL_IFMINUSSTMT    = 57, // <if-stmt>
        SYMBOL_INC            = 58, // <inc>
        SYMBOL_INITMINUSLIST  = 59, // <init-list>
        SYMBOL_LOGICMINUSEXPR = 60, // <logic-expr>
        SYMBOL_OP             = 61, // <op>
        SYMBOL_PROGRAM        = 62, // <program>
        SYMBOL_RETURN         = 63, // <return>
        SYMBOL_STMT           = 64, // <stmt>
        SYMBOL_STMTMINUSLIST  = 65, // <stmt-list>
        SYMBOL_TERM           = 66, // <term>
        SYMBOL_TYPES          = 67  // <types>
    };

    enum RuleConstants : int
    {
        RULE_PROGRAM_START_END                             =  0, // <program> ::= Start <stmt-list> End
        RULE_STMTLIST                                      =  1, // <stmt-list> ::= <stmt>
        RULE_STMTLIST2                                     =  2, // <stmt-list> ::= <stmt> <stmt-list>
        RULE_STMT_SEMI                                     =  3, // <stmt> ::= <assign> ';'
        RULE_STMT_COLONCOLON                               =  4, // <stmt> ::= <if-stmt> '::'
        RULE_STMT_COLONCOLON2                              =  5, // <stmt> ::= <for-stmt> '::'
        RULE_STMT_SEMI2                                    =  6, // <stmt> ::= <inc> ';'
        RULE_STMT_SEMI3                                    =  7, // <stmt> ::= <types> <assign> ';'
        RULE_STMT_COLONCOLON3                              =  8, // <stmt> ::= <fstmt> '::'
        RULE_STMT_SEMI4                                    =  9, // <stmt> ::= <fcall> ';'
        RULE_STMT_COLONCOLON4                              = 10, // <stmt> ::= <class> '::'
        RULE_ASSIGN_IDENTIFIER_EQ                          = 11, // <assign> ::= Identifier '=' <expr>
        RULE_EXPR_PLUS                                     = 12, // <expr> ::= <term> '+' <expr>
        RULE_EXPR_MINUS                                    = 13, // <expr> ::= <term> '-' <expr>
        RULE_EXPR                                          = 14, // <expr> ::= <term>
        RULE_TERM_TIMES                                    = 15, // <term> ::= <factor> '*' <term>
        RULE_TERM_DIV                                      = 16, // <term> ::= <factor> '/' <term>
        RULE_TERM_PERCENT                                  = 17, // <term> ::= <factor> '%' <term>
        RULE_TERM                                          = 18, // <term> ::= <factor>
        RULE_FACTOR_LPAREN_RPAREN                          = 19, // <factor> ::= '(' <expr> ')'
        RULE_FACTOR_IDENTIFIER                             = 20, // <factor> ::= Identifier
        RULE_FACTOR_NUM                                    = 21, // <factor> ::= Num
        RULE_FACTOR_FLOAT                                  = 22, // <factor> ::= Float
        RULE_IFSTMT_IF_COLON                               = 23, // <if-stmt> ::= if <logic-expr> ':' <stmt-list>
        RULE_IFSTMT_IF_COLON_ELSE                          = 24, // <if-stmt> ::= if <logic-expr> ':' <stmt-list> else <stmt-list>
        RULE_LOGICEXPR                                     = 25, // <logic-expr> ::= <expr> <op> <expr>
        RULE_OP_LT                                         = 26, // <op> ::= '<'
        RULE_OP_GT                                         = 27, // <op> ::= '>'
        RULE_OP_LTEQ                                       = 28, // <op> ::= '<='
        RULE_OP_GTEQ                                       = 29, // <op> ::= '>='
        RULE_OP_EQEQ                                       = 30, // <op> ::= '=='
        RULE_OP_EXCLAMEQ                                   = 31, // <op> ::= '!='
        RULE_FORSTMT_FROM_TO_COLON                         = 32, // <for-stmt> ::= from <types> <assign> to <logic-expr> ':' <stmt-list>
        RULE_FSTMT_FUNCTION_IDENTIFIER_LPAREN_RPAREN_COLON = 33, // <fstmt> ::= function <return> Identifier '(' <init-list> ')' ':' <fstmt-list>
        RULE_RETURN                                        = 34, // <return> ::= <types>
        RULE_RETURN_VOID                                   = 35, // <return> ::= void
        RULE_INITLIST_IDENTIFIER_COMMA                     = 36, // <init-list> ::= <types> Identifier ',' <init-list>
        RULE_INITLIST_IDENTIFIER                           = 37, // <init-list> ::= <types> Identifier
        RULE_FSTMTLIST                                     = 38, // <fstmt-list> ::= <func-stmt>
        RULE_FSTMTLIST2                                    = 39, // <fstmt-list> ::= <func-stmt> <fstmt-list>
        RULE_FUNCSTMT_SEMI                                 = 40, // <func-stmt> ::= <assign> ';'
        RULE_FUNCSTMT_COLONCOLON                           = 41, // <func-stmt> ::= <if-stmt> '::'
        RULE_FUNCSTMT_COLONCOLON2                          = 42, // <func-stmt> ::= <for-stmt> '::'
        RULE_FUNCSTMT_SEMI2                                = 43, // <func-stmt> ::= <inc> ';'
        RULE_FUNCSTMT_SEMI3                                = 44, // <func-stmt> ::= <types> <assign> ';'
        RULE_FCALL_IDENTIFIER_LPAREN_RPAREN                = 45, // <fcall> ::= Identifier '(' <args> ')'
        RULE_ARGS_COMMA                                    = 46, // <args> ::= <factor> ',' <args>
        RULE_ARGS                                          = 47, // <args> ::= <factor>
        RULE_CLASS_CLASS_IDENTIFIER_COLON                  = 48, // <class> ::= Class Identifier ':' <c-list>
        RULE_CLIST                                         = 49, // <c-list> ::= <c-elemet> <c-list>
        RULE_CLIST2                                        = 50, // <c-list> ::= <c-method> <c-list>
        RULE_CLIST3                                        = 51, // <c-list> ::= <c-elemet>
        RULE_CLIST4                                        = 52, // <c-list> ::= <c-method>
        RULE_CELEMET_SEMI                                  = 53, // <c-elemet> ::= <c-type> <types> <assign> ';'
        RULE_CMETHOD_COLONCOLON                            = 54, // <c-method> ::= <c-type> <fstmt> '::'
        RULE_CTYPE_PUBLIC                                  = 55, // <c-type> ::= Public
        RULE_CTYPE_PRIVATE                                 = 56, // <c-type> ::= Private
        RULE_CTYPE_PROTECTED                               = 57, // <c-type> ::= Protected
        RULE_CTYPE_STATIC                                  = 58, // <c-type> ::= static
        RULE_TYPES_INT                                     = 59, // <types> ::= int
        RULE_TYPES_REAL                                    = 60, // <types> ::= real
        RULE_TYPES_STRING                                  = 61, // <types> ::= string
        RULE_TYPES_CHAR                                    = 62, // <types> ::= char
        RULE_INC_IDENTIFIER_PLUSPLUS                       = 63, // <inc> ::= Identifier '++'
        RULE_INC_IDENTIFIER_MINUSMINUS                     = 64  // <inc> ::= Identifier '--'
    };

    public class MyParser
    {
        private LALRParser parser;
        ListBox errors;
        DataTable dt = new DataTable();
        Label Label1;
        public MyParser(string filename, ListBox errors,  DataTable dt)
        {
            FileStream stream = new FileStream(filename,
                                               FileMode.Open, 
                                               FileAccess.Read, 
                                               FileShare.Read);
            Init(stream);
            stream.Close();
            this.errors = errors;
            this.dt = dt;
            init_dt();
        }
        
        public void init_dt()
        {
            dt.Columns.Add("Lexems", typeof(string));
            dt.Columns.Add("Token",  typeof(string));
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

        private void TokenReadEvent(LALRParser parser, TokenReadEventArgs args)
        {
            args.Token.UserObject = CreateObject(args.Token);

            int tmp = args.Token.Symbol.Id;

            string lexem = args.Token.ToString();
            string token = ((SymbolConstants)args.Token.Symbol.Id).ToString().Substring(7);
            //string token = args.Token.Symbol.Name.ToString();
            dt.Rows.Add(lexem, token);
        }


        public void Parse(string source)
        {
            dt.Rows.Clear();
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

                case (int)SymbolConstants.SYMBOL_DIV :
                //'/'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_COLON :
                //':'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_COLONCOLON :
                //'::'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_SEMI :
                //';'
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

                case (int)SymbolConstants.SYMBOL_CHAR :
                //char
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CLASS :
                //Class
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ELSE :
                //else
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_END :
                //End
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FLOAT :
                //Float
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FROM :
                //from
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FUNCTION :
                //function
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_IDENTIFIER :
                //Identifier
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_IF :
                //if
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_INT :
                //int
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_NUM :
                //Num
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

                case (int)SymbolConstants.SYMBOL_START :
                //Start
                //todo: Create a new object that corresponds to the symbol
                return null;
                
                case (int)SymbolConstants.SYMBOL_STATIC :
                //static
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STRING :
                //string
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TO :
                //to
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_VOID :
                //void
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ARGS :
                //<args>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ASSIGN :
                //<assign>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CMINUSELEMET :
                //<c-elemet>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CLASS2 :
                //<class>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CMINUSLIST :
                //<c-list>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CMINUSMETHOD :
                //<c-method>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CMINUSTYPE :
                //<c-type>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EXPR :
                //<expr>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FACTOR :
                //<factor>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FCALL :
                //<fcall>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FORMINUSSTMT :
                //<for-stmt>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FSTMT :
                //<fstmt>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FSTMTMINUSLIST :
                //<fstmt-list>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FUNCMINUSSTMT :
                //<func-stmt>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_IFMINUSSTMT :
                //<if-stmt>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_INC :
                //<inc>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_INITMINUSLIST :
                //<init-list>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LOGICMINUSEXPR :
                //<logic-expr>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_OP :
                //<op>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PROGRAM :
                //<program>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_RETURN :
                //<return>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STMT :
                //<stmt>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STMTMINUSLIST :
                //<stmt-list>
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

            }
            throw new SymbolException("Unknown symbol");
        }

        public Object CreateObjectFromNonterminal(NonterminalToken token)
        {
            switch (token.Rule.Id)
            {
                case (int)RuleConstants.RULE_PROGRAM_START_END :
                //<program> ::= Start <stmt-list> End
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STMTLIST :
                //<stmt-list> ::= <stmt>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STMTLIST2 :
                //<stmt-list> ::= <stmt> <stmt-list>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STMT_SEMI :
                //<stmt> ::= <assign> ';'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STMT_COLONCOLON :
                //<stmt> ::= <if-stmt> '::'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STMT_COLONCOLON2 :
                //<stmt> ::= <for-stmt> '::'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STMT_SEMI2 :
                //<stmt> ::= <inc> ';'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STMT_SEMI3 :
                //<stmt> ::= <types> <assign> ';'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STMT_COLONCOLON3 :
                //<stmt> ::= <fstmt> '::'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STMT_SEMI4 :
                //<stmt> ::= <fcall> ';'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STMT_COLONCOLON4 :
                //<stmt> ::= <class> '::'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ASSIGN_IDENTIFIER_EQ :
                //<assign> ::= Identifier '=' <expr>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPR_PLUS :
                //<expr> ::= <term> '+' <expr>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPR_MINUS :
                //<expr> ::= <term> '-' <expr>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPR :
                //<expr> ::= <term>
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
                //<factor> ::= '(' <expr> ')'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FACTOR_IDENTIFIER :
                //<factor> ::= Identifier
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FACTOR_NUM :
                //<factor> ::= Num
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FACTOR_FLOAT :
                //<factor> ::= Float
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_IFSTMT_IF_COLON :
                //<if-stmt> ::= if <logic-expr> ':' <stmt-list>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_IFSTMT_IF_COLON_ELSE :
                //<if-stmt> ::= if <logic-expr> ':' <stmt-list> else <stmt-list>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_LOGICEXPR :
                //<logic-expr> ::= <expr> <op> <expr>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OP_LT :
                //<op> ::= '<'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OP_GT :
                //<op> ::= '>'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OP_LTEQ :
                //<op> ::= '<='
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OP_GTEQ :
                //<op> ::= '>='
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OP_EQEQ :
                //<op> ::= '=='
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OP_EXCLAMEQ :
                //<op> ::= '!='
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FORSTMT_FROM_TO_COLON :
                //<for-stmt> ::= from <types> <assign> to <logic-expr> ':' <stmt-list>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FSTMT_FUNCTION_IDENTIFIER_LPAREN_RPAREN_COLON :
                //<fstmt> ::= function <return> Identifier '(' <init-list> ')' ':' <fstmt-list>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_RETURN :
                //<return> ::= <types>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_RETURN_VOID :
                //<return> ::= void
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_INITLIST_IDENTIFIER_COMMA :
                //<init-list> ::= <types> Identifier ',' <init-list>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_INITLIST_IDENTIFIER :
                //<init-list> ::= <types> Identifier
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FSTMTLIST :
                //<fstmt-list> ::= <func-stmt>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FSTMTLIST2 :
                //<fstmt-list> ::= <func-stmt> <fstmt-list>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FUNCSTMT_SEMI :
                //<func-stmt> ::= <assign> ';'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FUNCSTMT_COLONCOLON :
                //<func-stmt> ::= <if-stmt> '::'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FUNCSTMT_COLONCOLON2 :
                //<func-stmt> ::= <for-stmt> '::'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FUNCSTMT_SEMI2 :
                //<func-stmt> ::= <inc> ';'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FUNCSTMT_SEMI3 :
                //<func-stmt> ::= <types> <assign> ';'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FCALL_IDENTIFIER_LPAREN_RPAREN :
                //<fcall> ::= Identifier '(' <args> ')'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ARGS_COMMA :
                //<args> ::= <factor> ',' <args>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ARGS :
                //<args> ::= <factor>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CLASS_CLASS_IDENTIFIER_COLON :
                //<class> ::= Class Identifier ':' <c-list>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CLIST :
                //<c-list> ::= <c-elemet> <c-list>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CLIST2 :
                //<c-list> ::= <c-method> <c-list>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CLIST3 :
                //<c-list> ::= <c-elemet>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CLIST4 :
                //<c-list> ::= <c-method>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CELEMET_SEMI :
                //<c-elemet> ::= <c-type> <types> <assign> ';'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CMETHOD_COLONCOLON :
                //<c-method> ::= <c-type> <fstmt> '::'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CTYPE_PUBLIC :
                //<c-type> ::= Public
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CTYPE_PRIVATE :
                //<c-type> ::= Private
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CTYPE_PROTECTED :
                //<c-type> ::= Protected
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CTYPE_STATIC :
                //<c-type> ::= static
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TYPES_INT :
                //<types> ::= int
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TYPES_REAL :
                //<types> ::= real
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TYPES_STRING :
                //<types> ::= string
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TYPES_CHAR :
                //<types> ::= char
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_INC_IDENTIFIER_PLUSPLUS :
                //<inc> ::= Identifier '++'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_INC_IDENTIFIER_MINUSMINUS :
                //<inc> ::= Identifier '--'
                //todo: Create a new object using the stored tokens.
                return null;

            }
            throw new RuleException("Unknown rule");
        }

        private void TokenErrorEvent(LALRParser parser, TokenErrorEventArgs args)
        {
            string message = "Token error with input: '" + args.Token.ToString() + "'";
            errors.Items.Add(message);
            //todo: Report message to UI?
        }

        private void ParseErrorEvent(LALRParser parser, ParseErrorEventArgs args)
        {
            string ExpectedTokens = "Expected Token : '" + args.ExpectedTokens.ToString() + "'";
            errors.Items.Add(ExpectedTokens);

            string UnexpectedToken = "Wrong Token : '" + args.UnexpectedToken.ToString() + "'";
            errors.Items.Add(UnexpectedToken);

            string errorLine = "Error Near By Line : " + args.UnexpectedToken.Location.LineNr;
            errors.Items.Add(errorLine);
            //todo: Report message to UI?
        }

    }
}
