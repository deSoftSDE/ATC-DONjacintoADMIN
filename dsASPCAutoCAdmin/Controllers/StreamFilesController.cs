using dsASPCAutoCAdmin.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace dsASPCAutoCAdmin.Controllers
{
    [Produces("application/json")]
    public class StreamFilesController : Controller
    {
        private IConfiguration _configuration;

        public StreamFilesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpPost]
        public async Task<IActionResult> Post()
        {
            ObjectResult result;
            try
            {
                var res = new List<MensajeRespuesta>();
                var files = Request.Form.Files;
                var strigValue = Request.Form.Keys;
                var uploadpath = _configuration.GetSection("StreamFiles")["uploads"];
                foreach (var formFile in files)
                {
                    var a = Guid.NewGuid();
                    string FilePath = string.Concat(uploadpath, a.ToString());
                    if (formFile.Length > 0)
                    {
                        using (var stream = new FileStream(FilePath, FileMode.Create))
                        {
                            await formFile.CopyToAsync(stream);
                            var msj = new MensajeRespuesta
                            {
                                Archivo = FilePath,
                                Contenido = a.ToString(),
                                Tamano = stream.Length,
                            };
                            res.Add(msj);
                        }
                    }
                }
                result = new ObjectResult(res)
                {
                    StatusCode = (int)HttpStatusCode.OK
                };
                return result;
            }
            catch (Exception ex)
            {
                result = new ObjectResult(ex)
                {
                    StatusCode = (int)HttpStatusCode.Conflict
                };
                Request.HttpContext.Response.Headers.Add("dsError", ex.Message);
                return result;
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetImagen(string filename)
        {
            try
            {
                if (filename == null)
                    return Content("Archivo no encontrado");
                var downloadpath = _configuration.GetSection("StreamFiles")["downloads"];
                var path = Path.Combine(
                               downloadpath, filename);

                var memory = new MemoryStream();
                using (var stream = new FileStream(path, FileMode.Open))
                {
                    await stream.CopyToAsync(memory);
                }
                memory.Position = 0;

                return File(memory, "image/png", Path.GetFileName(path));
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(ex)
                {
                    StatusCode = (int)HttpStatusCode.Conflict
                };
                Request.HttpContext.Response.Headers.Add("dsError", ex.Message);
                return result;
            }
        }
        [HttpGet]
        public async Task<IActionResult> Get(string filename)
        {
            try
            {
                if (filename == null)
                    return Content("Archivo no encontrado");
                var downloadpath = _configuration.GetSection("StreamFiles")["downloads"];
                var path = Path.Combine(
                               downloadpath, filename);

                var memory = new MemoryStream();
                using (var stream = new FileStream(path, FileMode.Open))
                {
                    await stream.CopyToAsync(memory);
                }
                memory.Position = 0;

                return File(memory, Path.GetFileName(path));
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(ex)
                {
                    StatusCode = (int)HttpStatusCode.Conflict
                };
                Request.HttpContext.Response.Headers.Add("dsError", ex.Message);
                return result;
            }
        }



        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }

    }
}
