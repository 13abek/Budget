using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Budget.Filters;
using Budget.Libs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Budget.Controllers.V1
{
    [Route("api/v1/files")]
    [ApiController]
    [TypeFilter(typeof(UserAuthFilter))]
    public class FileManagerController : ControllerBase
    {
        private readonly IFileManager _fileManager;
        public FileManagerController(IFileManager fileManager)
        {
            _fileManager = fileManager;
        }

        [HttpPost]
        [Route("upload")]
        public IActionResult Upload([FromForm]IFormFile file)
        {
            if (file == null) return BadRequest();

            var filename = _fileManager.Upload(file);

            return Ok(new { 
            filename
            });
        }
        [HttpDelete]
        [Route("delete")]
        public IActionResult Delete([FromQuery] string filename)
        {
            if (string.IsNullOrEmpty(filename)) return BadRequest(); // filename -yeni file ici boshdursa "badRequest" qaytaririq

            if (!_fileManager.FileExists(filename)) return NotFound(); // bool cinsinden yazdigimiz method ile burda yoxlayiriq ki file movcutdurmu

            _fileManager.Delete(filename);

            return Ok();
        }
    }
}