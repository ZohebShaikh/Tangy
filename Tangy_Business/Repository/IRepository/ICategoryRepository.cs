using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tangy_Models;

namespace Tangy_Business.Repository.IRepository
{
  public interface ICategoryRepository
  {
    public Task <CategoryDTO> Create(CategoryDTO objDTO);
    public Task <CategoryDTO> Update(CategoryDTO objDTO);
    public Task<int> Delete(int id); // returns number of items deleted
    public Task<CategoryDTO> Get(int id);
    public Task<IEnumerable<CategoryDTO>> GetAll();
  }
}
