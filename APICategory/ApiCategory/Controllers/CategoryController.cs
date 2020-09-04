using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiCategory.DataContext;
using ApiCategory.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiCategory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private DataBaseConnection _db = new DataBaseConnection();

        /// <summary>
        /// Retorna todas as Categorias do DB
        /// </summary>
        /// <remarks>
        ///     Exemplo de retorno:
        ///     [
        ///         {
        ///             "id": 1,
        ///             "name": "name1",
        ///             "description": "desc1",
        ///             "inUse": true,
        ///             "date": "2020-09-01T17:13:19"
        ///         },
        ///         {
        ///             "id": 2,
        ///             "name": "name2",
        ///             "description": "desc2",
        ///             "inUse": false,
        ///             "date": "2020-09-01T17:13:19"
        ///         }
        ///     ]
        /// </remarks>
        /// <returns>Uma Lista de Categorias</returns>
        /// <param>Sem parâmetro</param>
        [HttpGet]
        public ActionResult<List<Category>> Get()
        {
            return _db.GetAllCategories();
        }

        /// <summary>
        /// Retorna uma Categoria específica por Id
        /// </summary>
        /// <remarks>
        ///     Exemplo de retorno:
        ///     {
        ///         "id": 1,
        ///         "name": "name1",
        ///         "description": "desc1",
        ///         "inUse": true,
        ///         "date": "2020-09-01T17:13:19"
        ///     }
        /// </remarks>
        /// <returns>Um objeto de Categoria</returns>
        /// <param name="id">O Id da Categoria que está buscando</param>
        [HttpGet("{id}")]
        public ActionResult<Category> Get(long id)
        {
            return _db.GetCategoryById(id);
        }

        /// <summary>
        /// Inserir uma nova Categoria
        /// </summary>
        /// <remarks>
        ///     Exemplo de retorno:
        ///         true
        /// </remarks>
        /// <returns>Um booleano</returns>
        /// <param name="newCat">O objeto de Categoria para inserção</param>
        [HttpPost]
        public bool Post([FromBody] Category newCat)
        {
            return _db.InsertCategory(newCat);
        }

        /// <summary>
        /// Atualizar Categoria
        /// </summary>
        /// <remarks>
        ///     Exemplo de retorno:
        ///         true
        /// </remarks>
        /// <returns>Um booleano</returns>
        /// <param name="newCat">O objeto de Categoria para Atualização</param>
        [HttpPut]
        public bool Put([FromBody] Category newCat)
        {
            return _db.UpdateCategory(newCat);
        }

        /// <summary>
        /// Deleta uma Categoria específica por Id
        /// </summary>
        /// <param name="id">O Id da Categoria para exclusão</param>
        [HttpDelete("{id}")]
        public bool Delete(long id)
        {
            return _db.DeleCategoryById(id);
        }
    }
}
