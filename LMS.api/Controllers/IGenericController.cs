using LMS.api.Model;
using Microsoft.AspNetCore.Mvc;

namespace LMS.api.Controllers
{
    public interface IGenericController<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
        where TKey : notnull
    {
        Task<ActionResult<TEntity>> Create(TEntity entity);
        Task<IActionResult> Delete(TKey id);
        Task<ActionResult<TEntity>> Get(TKey id);
        Task<ActionResult<IEnumerable<TEntity>>> GetAll();
        Task<IActionResult> Update(TKey id, TEntity entity);
    }
}