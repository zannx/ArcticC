[![Generic badge](https://img.shields.io/badge/build-passing-green.svg)](https://shields.io/)
# ArcticC
A simple programming language. <br>
It is using source-to-source compiler, translating source code from ArcticC (.arc filetype) to C#. <br>
(Keep in mind that this programming language is still in development and it's not even close yet to be finished). <br>

# Building

To build ArcticC, simple clone it and build it:
```bash
$ git clone https://github.com/zannx/ArcticC.git
$ cd ArcticC
$ dotnet restore
$ dotnet build
```
Make sure that you are inside ArcticC folder in which is located the .sln file. <br>
Then run the following dotnet commands written above. <br>
Once you build it, inside console its going to be written the location of the folder which contains .exe file. <br>


# Functionality 

+ Declaring variables (syntax: {variablename} + '=' + <integer,decimal,boolean,string> + ';').
+ Declaring functions.
+ Declaring 'if' and 'else' statements. (only '==' as a condition for now!)
+ "run" function which will take the source code through lexer, parser and evaluator.
<hr>

To see some examples find 'Examples' folder inside the project and paste them inside of compiler, then type 'run' to start the whole process. <br>

<h3>Declaration of Variables</h3>

```bash
Variable = 1.2;
VariableTwo = false;
VariableThree = "ArcticC";
```

<h3>Declaration of functions</h3>

```bash
func Hello() {
  izpisi("Hello, world!\n");
}

Hello();
```

<h3>Declaration of 'if' and 'else' statements</h3>
Keep in mind that only '==' (equalsequals) is supported in if statements for now! <br>

```bash
Variable=1;
VariableTwo=2;

if (Variable==VariableTwo) {
  izpisi("It is equals.");
} else {
  izpisi("It is not equals.");
}
```

![declaring variables](https://i.gyazo.com/7b3e22e456130548fa4b5396e20cfadf.gif)<br>

<h1>Lexer</h1>
Lexer in ArcticC creates a tokens out of a source code.
<hr>
+ Working for variables <br>
+ Working for separators <br>
+ Working for operators <br>
+ Working for keywords (if, else, break, continue, case, switch, for, default and while) <br>


![lexer variables](imgs/LexerVariables.PNG)<br>

![lexer simple algo](imgs/LexerNew.PNG)<br>

<h1>Parser</h1>
Parser in ArcticC creates a string of tasks which only the evaluator understands.<br>
<hr>
+ Working for declaration of variables. <br>
+ Working for declaration of functions. <br>
+ Working for declaration of 'if' and 'else' statements. <br>

<h1>Evaluator</h1>
Evaluator in ArcticC processes a string of the parser and creates a behaviour out of the tasks.<br>
<hr>
+ Working for declaration of variables. <br>
+ Working for declaration of functions. <br>
+ Working for declaration of 'if' and 'else' statements. <br>
