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
    public class EmpladoController : ControllerBase
    {
        public IRepository<Empleado> _empleado = null;
        private TadeoSystemsBDContext _context = null;

        public EmpladoController(TadeoSystemsBDContext context)
        {
            this._context = context;
            _empleado = new BaseRepository<Empleado>(_context);
        }
        [HttpGet]
        public IActionResult Get()
        {
            var result = _empleado.Get(filter: null, orderBy: null,includeProperties: "EmpleadoHabilidad");
            return Ok(result);
        }
        [HttpGet]
        [Route("[action]")]
        ///api/emplado/getbyid?id=1
        public IActionResult GetById(int id)
        {
            var result = _empleado.Get(filter: x => x.IdArea == id, orderBy: null,includeProperties: "EmpleadoHabilidad");
            return Ok(result);
        }
        [HttpPost]
        public IActionResult Post([FromBody] Empleado empleado)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            _empleado.Insert(empleado);
            _empleado.Save();
            var LastInsert = _empleado.Get(filter: null, orderBy: x => x.OrderByDescending(x => x.IdEmpleado), includeProperties: "EmpleadoHabilidad").Take(1).First();
            return Ok(LastInsert);
        }
        [HttpPut]
        public IActionResult Put([FromBody] Empleado empleado)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            _empleado.Update(empleado);
            _empleado.Save();
            return Ok(empleado);
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            Empleado empleado = _empleado.GetById(id);
            _empleado.Delete(empleado);
            _empleado.Save();
            return Ok(empleado);
        }
    }
}
