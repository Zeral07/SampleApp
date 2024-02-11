using SampleApp.ViewModels;

namespace SampleApp.Interface
{
    public interface IFacadeBase<Model> where Model : class
    {
        public Task<Model?> GetByID(int id);
        public Task<List<Model>> GetAll();
        public Task<List<Model>> GetByParameter(string condition, List<object> parameters);
        public Task<PaginationFilter<Model>> GetByParameterWithPaging(string condition, List<object> parameters, int pageCount, int pageSize);
        public Task<Model> Create(Model obj);
        public Task Update(Model obj);
        public Task<bool> Delete(int id);
    }
}
