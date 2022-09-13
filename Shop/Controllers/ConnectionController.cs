using Microsoft.AspNetCore.Mvc;

using Shop.Services;

namespace Shop.Controllers
{
    public class ConnectionController : Controller
    {
        [HttpGet]
        public async Task ConnectionString()
        {
            string content = @"
        <h1>Подключеие к БД</h1>
        <form method='post'
        <label>Server:</label><br />
        <input name='Server' /><br />
        <label>Database:</label><br />
        <input name='Database' /><br />
        <label>User:</label><br />
        <input name='User' /><br />
        <label>Password:</label><br />
        <input name='Password' /><br />
        <p>
            <button type='submit'>Получить продукты</button> 
        </p> 
        </form>";
            Response.ContentType = "text/html;charset=utf-8";
            await Response.WriteAsync(content);
        }

        [HttpPost]
        public IActionResult ConnectionString(Dictionary<string, string> items)
        {
            StringConnection.Server = items["Server"];
            StringConnection.InitialCatalog = items["Database"];
            StringConnection.UserID = items["User"];
            StringConnection.Password = items["Password"];

            return RedirectToRoute("default", new { controller = "Products", action = "ListProducts" });
        }
    }
}
