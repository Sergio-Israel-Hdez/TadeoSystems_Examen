using Entidades;
using LibreriaConexion.IRepository;
using LibreriaConexion.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TadeoSystems_Examen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpladoHabilidadController : ControllerBase
    {
        public IRepository<EmpleadoHabilidad> _habilidad = null;
        private TadeoSystemsBDContext _context = null;

        public EmpladoHabilidadController(TadeoSystemsBDContext context)
        {
            this._context = context;
            _habilidad = new BaseRepository<EmpleadoHabilidad>(_context);
        }
        [HttpGet]
        public IActionResult Get()
        {
            var result = _habilidad.Get(filter: null, orderBy: null);
            return Ok(result);
        }
        [HttpGet]
        [Route("[action]")]
        ///api/empladohabilidad/getbyid?id=1
        public IActionResult GetById(int id)
        {
            var result = _habilidad.Get(filter: x => x.IdHabilidad == id, orderBy: null);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult Post([FromBody] EmpleadoHabilidad habilidad)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            _habilidad.Insert(habilidad);
            _habilidad.Save();
            var LastInsert = _habilidad.Get(filter: null, orderBy: x => x.OrderByDescending(x => x.IdHabilidad)).Take(1).First();
            return Ok(LastInsert);
        }
        [HttpPut]
        public IActionResult Put([FromBody] EmpleadoHabilidad habilidad)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            EmpleadoHabilidad oldHabilidad = _habilidad.GetById(habilidad.IdHabilidad);
            oldHabilidad.NombreHabilidad = habilidad.NombreHabilidad;
            oldHabilidad.IdEmpleado = habilidad.IdEmpleado;
            _habilidad.Update(oldHabilidad);
            _habilidad.Save();
            return Ok(habilidad);
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            EmpleadoHabilidad habilidad = _habilidad.GetById(id);
            _habilidad.Delete(habilidad);
            _habilidad.Save();
            return Ok(habilidad);
        }
    }
}
