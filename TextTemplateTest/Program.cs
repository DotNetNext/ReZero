using ReZero.TextTemplate;
using System.Text;

/****************该功能还在开发中*****************/
var x = new TextTemplateManager();
var template = @"
            <div>Visible</div>
           <%  foreach(var item in collection) {  %>
            <div>{{item}}</div>
             <% }  %>
        ";
var data = new { condition = true, collection = new[] { "Item 1", "Item 2", "Item 3" } };
var output = new StringBuilder();
var str=x.RenderTemplate(template, data);
Console.WriteLine(str);
Console.WriteLine("该功能还在开发中!!");
Console.ReadLine();