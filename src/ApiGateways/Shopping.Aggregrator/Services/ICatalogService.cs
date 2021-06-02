using Shopping.Aggregrator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopping.Aggregrator.Services
{
    public interface ICatalogService
    {
        Task<IEnumerable<CatalogModel>> GetCatalog();
        Task<IEnumerable<CatalogModel>> GetCatalogByCategory(string category);
        Task<CatalogModel> GetCatalog(string id);
    }
}
