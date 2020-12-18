using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibreriaConexion.IRepository;
using LibreriaConexion.Repository;
using Entidades;

namespace TadeoSystems_Examen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AreaController : ControllerBase
    {
        public IRepository<Area> _area = null;
        private TadeoSystemsBDContext _context = null;

        public AreaController(TadeoSystemsBDContext context)
        {
            this._context = context;
            _area = new BaseRepository<Area>(_context);
        }
        [HttpGet]
        public IActionResult Get()
        {
            var result = _area.Get(filter: null, orderBy: null, includeProperties: "Empleado");
            return Ok(result);
        }
        [HttpGet]
        [Route("[action]")]
        ///api/area/getbyid?id=1
        public IActionResult GetById(int id)
        {
            var result = _area.Get(filter: x => x.IdArea == id, orderBy: null, includeProperties: "Empleado");
            return Ok(result);
        }
        [HttpPost]
        public IActionResult Post([FromBody] Area area)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            _area.Insert(area);
            _area.Save();
            var LastInsert = _area.Get(filter: null, orderBy: x=>x.OrderByDescending(x=>x.IdArea), includeProperties: "Empleado").Take(1).First();
            return Ok(LastInsert);
        }
        [HttpPut]
        public IActionResult Put([FromBody] Area area)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            Area oldArea = _area.GetById(area.IdArea);
            oldArea.Nombre = area.Nombre;
            oldArea.Descripcion = area.Descripcion;
            _area.Update(oldArea);
            _area.Save();
            return Ok(area);
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            Area area = _area.GetById(id);
            _area.Delete(area);
            _area.Save();
            return Ok(area);
        }
    }
}
