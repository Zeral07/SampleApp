using SampleApp.ViewModels;

namespace SampleApp.Interface
{
    public interface IFacadeBase<ViewModel, Model> where ViewModel : class where Model : class
    {
        public Task<ViewModel> GetByID(int id);
        public Task<List<ViewModel>> GetAll();
        public Task<List<ViewModel>> GetByParameter(string condition, List<object> parameters);
        public Task<ResultResponse<ViewModel>> GetByParameterWithPaging(string condition, List<object> parameters, int pageCount, int pageSize);
        public Task<ViewModel> Create(Model obj);
        public Task<ViewModel> Update(Model obj);
        public Task<ViewModel> Delete(Model obj);
    }
}
