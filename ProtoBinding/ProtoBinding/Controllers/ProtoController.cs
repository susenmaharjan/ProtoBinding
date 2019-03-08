using Google.Protobuf;
using Microsoft.AspNetCore.Mvc;
using Personpackage;
using ProtoBinding.Models;


namespace ProtoBinding.Controllers
{
  [ApiController]
  [Route("/api/{controller}/{action}")]
  public class ProtoController : Controller
  {

    [HttpPost]
    public IActionResult PostProto([FromBody] PersonPoco person)
    {
      return Ok(person);
    }


    [HttpGet]
    public IActionResult ProtoForm()
    {
      return View("ProtoForm");
    }

    [HttpPost]
    public IActionResult ProtoForm([FromForm]PersonPoco person)
    {
      var test = person;
      return Ok(test);
    }

    [HttpGet]
    public IActionResult GetProto()
    {

      var person = new Person
      {
        Id = 1001,
        Name = "Susen",
        Email = "susen.maharjan@javra.com"
      };
      
      return Ok(person.ToByteArray());
    }

    [HttpGet]
    public IActionResult ProtoDetail(int id)
    {
      var person = new PersonPoco
      {
        Id = id,
        Name = "Susen",
        Email = "susen.maharjan@javra.com"
      };

      return Ok(person);
    }
  }
}
