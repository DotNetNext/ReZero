using ReZero.TextTemplate;
using System.Text;
 
Console.WriteLine(Demo1());
Console.WriteLine(Demo2());
Console.WriteLine(Demo3());
Console.ReadLine();

static string Demo1()
{
    Print("demo1");
    var x = new TextTemplateManager();
    var template = @"<div>{ {condition.ToString().ToUpper()}}</div> 
 <% foreach(var item in collection) {  %>
<div>{{item}}</div>
<%  } %>
";
    var data = new Model { condition = true, collection = new[] { "Item 1", "Item 2", "Item 3" } };
    var str = x.RenderTemplate(template, data);
    return str;
}
static string Demo2()
{
    Print("demo2"); 
    var x = new TextTemplateManager();
    var template = @" { { condition } }";
    var data = new Model { condition = true, collection = new[] { "Item 1", "Item 2", "Item 3" } };
    var str = x.RenderTemplate(template, data);
    return str;
}
static string Demo3()
{
    Print("demo3");
    var x = new TextTemplateManager();
    var template = @"< %  var id=1;  % >
{{id}}+{{true}}";
    var data = new Model { condition = true, collection = new[] { "Item 1", "Item 2", "Item 3" } };
    var str = x.RenderTemplate(template, data);
    return str;
}

static void Print(string name)
{
    Console.WriteLine("-----"+name+ "-----");
    Console.WriteLine();
}