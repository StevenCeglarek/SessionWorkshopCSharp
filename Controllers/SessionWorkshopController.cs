using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SessionWorkshop.Models;

namespace SessionWorkshop.Controllers
{
	public class SessionWorkshopController : Controller
	{
		[HttpGet("")]
		public IActionResult HomePage()
		{
			return View("Index");
		}

		[HttpPost("process")]
		public IActionResult Process(string InputtedName)
		{
			if(InputtedName == null)
			{
				InputtedName = "";
			}
			HttpContext.Session.SetString("Name", InputtedName);
			HttpContext.Session.SetInt32("Number", 22);

			return RedirectToAction("GameToPlay");
		}

		[HttpGet("SessionGame")]
		public IActionResult GameToPlay()
		{
			string? name = HttpContext.Session.GetString("Name");

            if (name.Equals(""))
			{
				return RedirectToAction("HomePage");
			}
			return View("SessionGame");
		}

		[HttpPost("processGame")]
		public IActionResult ProcessGame(string Selection)
		{
			int? num = HttpContext.Session.GetInt32("Number");
			if(Selection.Equals("addOne"))
			{
				num += 1;
			}
            else if (Selection.Equals("subtractOne"))
            {
                num -= 1;
            }
            else if (Selection.Equals("multiplyTwo"))
            {
                num *= 2;
            }
            else if (Selection.Equals("random"))
            {
				Random rand = new Random();
				var randNum = rand.NextInt64(1, 10);
				int convert = (int)randNum;
                num += convert;
            }

			HttpContext.Session.SetInt32("Number", (int)num);

			return RedirectToAction("GameToPlay");
        }

		[HttpGet("logout")]
		public IActionResult Logout()
		{
            HttpContext.Session.Clear();
            return RedirectToAction("HomePage");
		}
	}
}

